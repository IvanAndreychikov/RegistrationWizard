namespace RegistrationWizard.DTO
{
    public class RegistrationRequestDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int CountryId { get; set; }
        public int ProvinceId { get; set; }
    }
}
