using RegistrationWizard.DTO;

namespace RegistrationWizard.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Create new user. If it fails, returns error message
        /// </summary>
        /// <param name="request"></param>
        /// <returns>Error message if exist. Otherwise empty string</returns>
        public Task<string> CreateNewUser(RegistrationRequestDTO request, CancellationToken cancellationToken);
    }
}
