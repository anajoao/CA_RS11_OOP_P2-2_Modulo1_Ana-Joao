using RSGymClientManagment.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RSGymClientManagment.Models
{
    public class ContractsGymClasses : IContractsGymClasses
    {
        #region Properties
        [Key]
        public int Id { get; set; }

        [Key]
        public int ContractId { get; set; }

        [Key]
        public int GymClassId { get; set; }
        #endregion

        #region Navegation
        public virtual Contracts? Contract { get; set; } 
        public virtual GymClasses? GymClass { get; set; }
        #endregion
    }
}
