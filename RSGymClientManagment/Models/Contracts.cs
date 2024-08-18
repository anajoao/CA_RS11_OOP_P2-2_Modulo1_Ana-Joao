using Humanizer;
using Microsoft.EntityFrameworkCore;
using RSGymClientManagment.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static RSGymClientManagment.Enums.Enums;

namespace RSGymClientManagment.Models
{
    public class Contracts : IContracts
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ContractId { get; set; }

        [ForeignKey("Clients")]
        [Display(Name = "Client id")]
        public int ClientId { get; set; }

        [ForeignKey("Loyalties")]
        [Display(Name = "Loyalty id")]
        public int LoyaltyId { get; set; }

        [Required(ErrorMessage = "Contract type is required.")]
        [Column(TypeName = "int")]
        [Display(Name = "Contract type")]
        [EnumDataType(typeof(ContractType))]
        public ContractType Contract { get; set; }

        [Required(ErrorMessage = "Start date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
      
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Required(ErrorMessage = "Monthly fee is required.")]
        [Column(TypeName = "decimal(5,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Monthly Fee")]
        public decimal MonthlyFee { get; set; }
        #endregion

        #region Constructors
        public Contracts()
        {
            MonthlyFee = 0;
        }

        public Contracts(ContractType contractType, DateTime startDate, DateTime endDate, decimal monthlyFee)
        {
            Contract = contractType;
            StartDate = startDate;
            EndDate = endDate;
            MonthlyFee = monthlyFee;
        }
        #endregion

        #region Navegation
        public virtual Clients? Client { get; set; } 
        public virtual Loyalties? Loyalty { get; set; } 
        public virtual IEnumerable<ContractsGymClasses>? ContractsGymClasses { get; set; } 

        #endregion

    }
}
