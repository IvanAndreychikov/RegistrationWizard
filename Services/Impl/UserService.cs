using Microsoft.EntityFrameworkCore;
using RegistrationWizard.DAL.Repository;
using RegistrationWizard.DTO;
using RegistrationWizard.Models;
using System.Security.Cryptography;
using System.Text;

namespace RegistrationWizard.Services.Impl
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<string> CreateNewUser(RegistrationRequestDTO request, CancellationToken cancellationToken)
        {
            byte[] saltBytes = GenerateSalt();
            string hashedPassword = HashPassword(request.Password, saltBytes);

            var user = new User
            {
                Login = request.Login,
                Password = hashedPassword,
                CountryId = request.CountryId,
                ProvinceId = request.ProvinceId,
                //TODO: Current implementation with salt is useful protection against rainbow tables usage.
                //However, we could improve it by second security factor.
                //Needs to be discussed.
                Salt = saltBytes
            };
            try
            {
                await _userRepository.AddAsync(user, cancellationToken);
                return "";
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("duplicate key value violates unique constraint"))
                {
                    return "User with this login already exists.";
                }
                throw;
            }
        }

        private static string HashPassword(string password, byte[] salt)
        {
            using var sha512 = SHA512.Create();
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[passwordBytes.Length + salt.Length];

            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
            Buffer.BlockCopy(salt, 0, saltedPassword, passwordBytes.Length, salt.Length);

            byte[] hashedBytes = sha512.ComputeHash(saltedPassword);

            byte[] hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
            Buffer.BlockCopy(salt, 0, hashedPasswordWithSalt, 0, salt.Length);
            Buffer.BlockCopy(hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length);

            return Convert.ToBase64String(hashedPasswordWithSalt);
        }

        private static byte[] GenerateSalt()
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[16];
            rng.GetBytes(salt);
            return salt;
        }
    }
}
