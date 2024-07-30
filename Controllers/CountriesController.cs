using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.DAL.Repository;
using RegistrationWizard.DTO;
using RegistrationWizard.Models;
using RegistrationWizard.Services;

namespace RegistrationWizard.Controllers
{
    [ApiController]
    [Route("countries")]
    public class CountriesController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public CountriesController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IEnumerable<CountryDTO>> Get(CancellationToken cancellationToken)
        {
            return await _countryService.GetAll(cancellationToken);
        }
    }
}