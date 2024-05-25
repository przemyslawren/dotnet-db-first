namespace PJATK_APBD_E5.models;

public partial class Client
{
    public int IdClient { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Pesel { get; set; }

    public virtual ICollection<Client_Trip> Client_Trips { get; set; }
}