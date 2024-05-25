namespace PJATK_APBD_E5.models;

public partial class Client_Trip
{
    public int IdClient { get; set; }
    public virtual Client Client { get; set; }
    public int IdTrip { get; set; }
    public virtual Trip Trip { get; set; }
    public DateTime RegisteredAt { get; set; }
    public DateTime? PaymentDate { get; set; }
}