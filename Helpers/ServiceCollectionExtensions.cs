using RegistrationWizard.DAL.Repository;
using RegistrationWizard.DAL.Repository.Impl;
using RegistrationWizard.Models;
using RegistrationWizard.Services;
using RegistrationWizard.Services.Impl;

namespace RegistrationWizard.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IRepository<Province>, Repository<Province>>();
            services.AddScoped<IRepository<Country>, Repository<Country>>();
        }
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IProvinceService, ProvinceService>();
        }
    }
}
