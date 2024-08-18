using RSGymClientManagment.Models.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RSGymClientManagment.Models
{
    public class Clients : IClients
    {
        #region Properties

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Client id")]
        public int ClientId { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name max 100 characters.")]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "Client name")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(5, ErrorMessage = "Username max 5 characters.")]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [StringLength(9, ErrorMessage = "Phone max 9 characters.")]
        [Column(TypeName = "nvarchar")]
        [Display(Name = "Phone number")]
        public string Phone { get; set; }


        [StringLength(30, ErrorMessage = "Email max 30 characters.")]
        [Column(TypeName = "nvarchar")]
        public string Email { get; set; }

        [Required(ErrorMessage = "NIF is required.")]
        [StringLength(9, ErrorMessage = "Name max 9 characters.")]
        [Column(TypeName = "nvarchar")]
        public string NIF { get; set; }

        [StringLength(25, ErrorMessage = "Name max 25 characters.")]
        [Column(TypeName = "nvarchar")]
        public string? IBAN { get; set; }

        #endregion

        #region Constructor
        public Clients()
        {
            ClientName = string.Empty;
            Username = string.Empty;
            Phone = string.Empty;
            Email = string.Empty;
            NIF = string.Empty;
            IBAN = string.Empty;

        }

        public Clients(string clientName, string username, string phone, string email, string nif, string iban)
        {
            ClientName = clientName;
            Username = username;
            Phone = phone;
            Email = email;
            NIF = nif;
            IBAN = iban;
        }

        #endregion

        #region Navegation
        public virtual ICollection<Contracts>? Contracts { get; set; }
        

        #endregion
    }
}
