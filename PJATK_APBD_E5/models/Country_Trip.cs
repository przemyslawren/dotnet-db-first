namespace PJATK_APBD_E5.models;

public partial class Country_Trip
{
    public int IdCountry { get; set; }
    public virtual Country Country { get; set; }
    public int IdTrip { get; set; }
    public virtual Trip Trip { get; set; }
}