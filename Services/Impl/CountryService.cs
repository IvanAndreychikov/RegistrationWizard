using RegistrationWizard.DAL.Repository;
using RegistrationWizard.DTO;
using RegistrationWizard.Models;

namespace RegistrationWizard.Services.Impl
{
    public class CountryService : ICountryService
    {
        private readonly IRepository<Country> _countryRepository;

        public CountryService(IRepository<Country> countryRepository)
        {
            _countryRepository = countryRepository;
        }
        public async Task<IEnumerable<CountryDTO>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _countryRepository.GetAllAsync(cancellationToken);
            return result.Select(c => new CountryDTO { Id = c.Id, Name = c.Name });
        }
    }
}
