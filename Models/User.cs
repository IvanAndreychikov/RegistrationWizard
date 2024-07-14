namespace RegistrationWizard.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public Country Country { get; set; }
        public int CountryId { get; set; }
        public Province Province { get; set; }
        public int ProvinceId { get; set; }
    }
}
