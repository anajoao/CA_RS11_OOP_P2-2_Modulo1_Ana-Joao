namespace RSGymClientManagment.Models.Interfaces
{
    public interface ILoyalties
    {
        int LoyaltyId { get; }
        bool LoyaltyProgram { get; }
        decimal Discount { get; }

    }
}
