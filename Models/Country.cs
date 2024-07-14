namespace RegistrationWizard.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Province> Provinces { get; set; }
    }
}
