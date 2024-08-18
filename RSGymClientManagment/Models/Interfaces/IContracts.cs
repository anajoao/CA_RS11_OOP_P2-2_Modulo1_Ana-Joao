using static RSGymClientManagment.Enums.Enums;

namespace RSGymClientManagment.Models.Interfaces
{
    public interface IContracts
    {
        int ContractId { get; }
        public int ClientId { get; }
        public int LoyaltyId { get; }
        ContractType Contract { get; }
        DateTime StartDate { get; }
        DateTime? EndDate { get; }
        decimal MonthlyFee { get; }
    }
}
