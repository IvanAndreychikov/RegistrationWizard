using RegistrationWizard.DTO;

namespace RegistrationWizard.Services
{
    public interface ICountryService
    {
        public Task<IEnumerable<CountryDTO>> GetAll();
    }
}
