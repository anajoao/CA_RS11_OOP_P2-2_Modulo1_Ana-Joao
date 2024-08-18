using static RSGymClientManagment.Enums.Enums;

namespace RSGymClientManagment.Models.Interfaces
{
    public interface IPayments
    {
        int PaymentId { get; }
        int ContractId { get; }
        DateTime PaymentDate { get; }
        public PaymentType PaymentType { get; }
        decimal PaymentValue { get; }
    }
}
