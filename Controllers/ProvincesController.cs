using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.DAL.Repository;
using RegistrationWizard.DTO;
using RegistrationWizard.Models;
using RegistrationWizard.Services;

namespace RegistrationWizard.Controllers
{
    [ApiController]
    [Route("provinces")]
    public class ProvincesController : ControllerBase
    {
        private readonly IProvinceService _provinceService;

        public ProvincesController(IProvinceService provinceService)
        {
            _provinceService = provinceService;
        }

        [HttpGet("{countryId:int}")]
        public async Task<IEnumerable<ProvinceDTO>> GetByCountryId(int countryId)
        {
            return await _provinceService.GetByCountryId(countryId);
        }
    }
}