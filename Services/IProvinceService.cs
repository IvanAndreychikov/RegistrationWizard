using RegistrationWizard.DTO;

namespace RegistrationWizard.Services
{
    public interface IProvinceService
    {
        public Task<IEnumerable<ProvinceDTO>> GetByCountryId(int countryId);
    }
}
