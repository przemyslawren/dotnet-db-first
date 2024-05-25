namespace PJATK_APBD_E5.models;

public partial class Country
{
    public int IdCountry { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Country_Trip> Country_Trips { get; set; }
}