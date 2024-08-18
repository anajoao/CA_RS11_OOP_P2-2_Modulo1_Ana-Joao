namespace RSGymClientManagment.Models.Interfaces
{
    public interface IClients
    {
        int ClientId { get; }
        string ClientName { get; }
        string Username { get; }
        string Phone { get; }
        string Email { get; }
        string NIF { get; }
        string? IBAN { get; }

    }
}
