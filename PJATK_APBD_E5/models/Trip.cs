namespace PJATK_APBD_E5.models;

public partial class Trip
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }

    public virtual ICollection<Client_Trip> Client_Trips { get; set; }
    public virtual ICollection<Country_Trip> Country_Trips { get; set; }
}