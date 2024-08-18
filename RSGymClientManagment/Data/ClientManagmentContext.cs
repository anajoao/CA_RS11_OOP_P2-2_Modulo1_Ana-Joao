using Microsoft.EntityFrameworkCore;
using RSGymClientManagment.Models;
using RSGymClientManagment.Models.Interfaces;
using static RSGymClientManagment.Enums.Enums;

namespace RSGymClientManagment.Data
{
    public partial class ClientManagmentContext : DbContext
    {
        public ClientManagmentContext()
        {
        }

        public ClientManagmentContext(DbContextOptions<ClientManagmentContext> options)
        : base(options)
        {
        }

        public DbSet<Clients> Clients { get; set; } = null!;
        public DbSet<Contracts> Contracts { get; set; } = null!;
        public DbSet<Loyalties> Loyalties { get; set; } = null!;
        public DbSet<Payments> Payments { get; set; } = null!;
        public DbSet<GymClasses> GymClasses { get; set; } = null!;
        public DbSet<ContractsGymClasses> ContractsGymClasses { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Database=ContactDB_EFCore_DatabaseFirst;Trusted_Connection=True;");
            }
            */
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ContractsGymClasses>()
                .HasKey(cgc => cgc.Id);

            modelBuilder.Entity<ContractsGymClasses>()
                .HasOne(cgc => cgc.Contract)
                .WithMany(c => c.ContractsGymClasses)
                .HasForeignKey(cgc => cgc.ContractId);

            modelBuilder.Entity<ContractsGymClasses>()
                .HasOne(cgc => cgc.GymClass)
                .WithMany(gc => gc.ContractsGymClasses)
                .HasForeignKey(cgc => cgc.GymClassId);

            // Adicionar dados de seed para Clients
            modelBuilder.Entity<Clients>().HasData(
                new Clients
                {
                    ClientId = 1,
                    ClientName = "Rui Miguel Azevedo",
                    Username = "rma1",
                    Phone = "913456789",
                    Email = "rma@mail.com",
                    NIF = "111111111",
                    IBAN = "PT50345678902345678901234"
                },
                new Clients
                {
                    ClientId = 2,
                    ClientName = "Ana Dinis Silva",
                    Username = "ads2",
                    Phone = "917654321",
                    Email = "ads@mail.com",
                    NIF = "222222222"
                }
            );

            // Adicionar dados de seed para Contracts
            modelBuilder.Entity<Contracts>().HasData(
                new Contracts
                {
                    ContractId = 1,
                    ClientId = 1,
                    LoyaltyId = 1,
                    Contract = ContractType.Monthly,
                    StartDate = new DateTime(2024, 01, 10),
                    EndDate = null,
                    MonthlyFee = 50
                },
                new Contracts
                {
                    ContractId = 2,
                    ClientId = 2,
                    LoyaltyId = 2,
                    Contract = ContractType.PerSession,
                    StartDate = new DateTime(2024, 07, 10),
                    EndDate = new DateTime(2024, 09, 10),
                    MonthlyFee = 0
                }
            );

            // Adicionar dados de seed para Loyalties
            modelBuilder.Entity<Loyalties>().HasData(
                new Loyalties
                {
                    LoyaltyId = 1,
                    LoyaltyProgram = true,
                    Discount = 10
                },
                new Loyalties
                {
                    LoyaltyId = 2,
                    LoyaltyProgram = false,
                    Discount = 0
                }
            );

            // Adicionar dados de seed para GymClasses
            modelBuilder.Entity<GymClasses>().HasData(
                new GymClasses
                {
                    GymClassId = 1,
                    ClassName = "Yoga",
                    ClassPrice = 10
                },
                new GymClasses
                {
                    GymClassId = 2,
                    ClassName = "Pilates",
                    ClassPrice = 15
                },
                new GymClasses
                {
                    GymClassId = 3,
                    ClassName = "Body Strength",
                    ClassPrice = 20
                }
            );

            // Adicionar dados de seed para ContractsGymClasses
            modelBuilder.Entity<ContractsGymClasses>().HasData(
                new ContractsGymClasses
                {
                    Id = 1,
                    ContractId = 1,
                    GymClassId = 1
                },
                new ContractsGymClasses
                {
                    Id = 2,
                    ContractId = 1,
                    GymClassId = 2
                },
                new ContractsGymClasses
                {
                    Id = 3,
                    ContractId = 1,
                    GymClassId = 3
                },
                new ContractsGymClasses
                {
                    Id = 4,
                    ContractId = 2,
                    GymClassId = 2
                }
            );

            // Adicionar dados de seed para Payments
            modelBuilder.Entity<Payments>().HasData(
                new Payments
                {
                    PaymentId = 1,
                    ContractId = 1,
                    PaymentDate = new DateTime(2024, 07, 01),
                    PaymentType = PaymentType.Transfer,
                    PaymentValue = 45
                },
                new Payments
                {
                    PaymentId = 2,
                    ContractId = 2,
                    PaymentDate = new DateTime(2024, 07, 17),
                    PaymentType = PaymentType.Card,
                    PaymentValue = 15
                }
            );

            OnModelCreatingPartial(modelBuilder);
        }



        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
