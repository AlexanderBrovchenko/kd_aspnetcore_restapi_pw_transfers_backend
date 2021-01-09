using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace kd_pw_transfers_backend.Models
{
    public partial class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Transfer> Transfers { get; set; }
        public DbSet<User> Users { get; set; }

        public int UserBalance (User user)
        {
            if (user == null)
                throw new Exception("No user is set");
            int balanceCredit = Transfers.Where(x => x.PayeeId == user.Id).Sum(x => x.Amount);
            int balanceDebit = Transfers.Where(x => x.PayerId == user.Id).Sum(x => x.Amount);
            return balanceCredit - balanceDebit;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transfer>(entity =>
            {
                entity.ToTable("transfers");

                entity.HasIndex(e => e.PayeeId)
                    .HasName("FK_Transfers_users_2");

                entity.HasIndex(e => e.PayerId)
                    .HasName("FK_Transfers_users");

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Amount).HasColumnType("int(11) unsigned");

                entity.Property(e => e.OperatedAt).HasColumnType("datetime");

                entity.Property(e => e.PayeeId).HasColumnType("int(11)");

                entity.Property(e => e.PayerId).HasColumnType("int(11)");

                entity.HasOne(d => d.Payee)
                    .WithMany(p => p.TransfersPayee)
                    .HasForeignKey(d => d.PayeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Transfers_users_2");

                entity.HasOne(d => d.Payer)
                    .WithMany(p => p.TransfersPayer)
                    .HasForeignKey(d => d.PayerId)
                    .HasConstraintName("FK_Transfers_users");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");

                entity.HasIndex(e => e.Email)
                    .HasName("Email")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("int(11)")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_general_ci");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_general_ci");

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasColumnType("varchar(50)")
                    .HasCharSet("latin1")
                    .HasCollation("latin1_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
