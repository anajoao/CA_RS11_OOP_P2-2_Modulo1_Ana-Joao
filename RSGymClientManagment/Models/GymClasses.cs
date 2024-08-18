using Microsoft.EntityFrameworkCore;
using RSGymClientManagment.Models.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSGymClientManagment.Models
{
    public class GymClasses : IGymClasses
    {
        #region Properties
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int GymClassId { get; set; }

        [Required(ErrorMessage = "Class name is required.")]
        [StringLength(50, ErrorMessage = "Class name cannot exceed 50 characters.")]
        [Column(TypeName = "nvarchar(50)")]
        [Display(Name = "Class Name")]
        public string ClassName { get; set; }

        [Required(ErrorMessage = "Class price is required.")]
        [Range(0, 299.99, ErrorMessage = "Class price must be a positive value.")]
        [Column(TypeName = "decimal(5,2)")]
        [DataType(DataType.Currency)]
        [Display(Name = "Class Price")]
        public decimal ClassPrice { get; set; }
        #endregion

        #region Constructors
        public GymClasses()
        {
            ClassName = string.Empty;
            ClassPrice = 0;
        }
        #endregion

        #region Navegation
        public virtual ICollection<ContractsGymClasses>? ContractsGymClasses { get; set; }
        #endregion
    }
}
