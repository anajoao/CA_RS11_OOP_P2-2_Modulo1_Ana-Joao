using RSGymClientManagment.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static RSGymClientManagment.Enums.Enums;

namespace RSGymClientManagment.Models
{
    public class Payments : IPayments
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Payment id")]
        public int PaymentId { get; set; }

        [ForeignKey("Contracts")]
        [Display(Name = "Contract id")]
        public int ContractId { get; set; }

        [Required(ErrorMessage = "Payment date is required.")]
        [DataType(DataType.DateTime)]
        [Display(Name = "Payment Date")]
        public DateTime PaymentDate { get; set; }

        [Required(ErrorMessage = "Payment type is required.")]
        [Column(TypeName = "int")]
        [Display(Name = "Payment Type")]
        [EnumDataType(typeof(PaymentType))]
        public PaymentType PaymentType { get; set; }

        [Required(ErrorMessage = "Payment value is required.")]
        [Column(TypeName = "decimal(5,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Payment Value")]
        public decimal PaymentValue { get; set; }
        #endregion

        #region Constructors
        public Payments()
        {
            PaymentDate = DateTime.MinValue;
            PaymentValue = 0;
        }

        public Payments(DateTime paymentDate, decimal paymentValue)
        {
            PaymentDate = paymentDate;
            PaymentValue = paymentValue;
        }
        #endregion

        #region Navegation
        public virtual Contracts? Contract { get; set; } 
        #endregion
    }
}
