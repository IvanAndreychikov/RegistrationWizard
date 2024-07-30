using RegistrationWizard.DAL.Repository;
using RegistrationWizard.DTO;
using RegistrationWizard.Models;

namespace RegistrationWizard.Services.Impl
{
    public class ProvinceService : IProvinceService
    {
        private readonly IRepository<Province> _provinceRepository;

        public ProvinceService(IRepository<Province> provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }
        public async Task<IEnumerable<ProvinceDTO>> GetByCountryId(int countryId, CancellationToken cancellationToken)
        {
            var result = await _provinceRepository.GetByPredicateAsync(p => p.CountryId == countryId, cancellationToken);
            return result.Select(p => new ProvinceDTO { CountryId = p.CountryId, Id = p.Id, Name = p.Name });
        }
    }
}
