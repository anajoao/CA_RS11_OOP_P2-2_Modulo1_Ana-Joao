using Microsoft.EntityFrameworkCore;
using RSGymClientManagment.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSGymClientManagment.Models
{
    public class Loyalties : ILoyalties
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Loyalty id")]
        public int LoyaltyId { get; set; }

        [Required(ErrorMessage = "Loyalty program is required.")]
        [Display(Name = "Loyalty Program")]
        public bool LoyaltyProgram { get; set; }

        [Required(ErrorMessage = "Discount is required.")]
        [Range(0, 100, ErrorMessage = "Discount must be between 0 and 100")]
        [Precision(5, 2)] 
        [Display(Name = "Discount %")]
        public decimal Discount { get; set; }
        #endregion

        #region Constructors
        public Loyalties()
        {
            LoyaltyProgram = false;
            Discount = 0;
        }

        public Loyalties(bool loyaltyProgram, decimal discount)
        {
            LoyaltyProgram = loyaltyProgram;
            Discount = discount;
        }
        #endregion

        #region Navegation
        public virtual ICollection<Contracts>? Contracts { get; set; }
        #endregion
    }
}
