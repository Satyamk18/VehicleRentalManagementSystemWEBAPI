using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace firstwebapi.Models
{
    public partial class ACE42023Context : DbContext
    {
        public ACE42023Context()
        {
        }

        public ACE42023Context(DbContextOptions<ACE42023Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<EmpsRahul> EmpsRahuls { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<TransactionVrm> TransactionVrms { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DEVSQL.corp.local;Database=ACE 4- 2023;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Userid)
                    .HasName("PK__Customer__CBA1B257F2C4ED15");

                entity.ToTable("Customer");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<EmpsRahul>(entity =>
            {
                entity.HasKey(e => e.Eid)
                    .HasName("PK__empsRahu__D9509F6DEC4CB700");

                entity.ToTable("empsRahul");

                entity.Property(e => e.Eid)
                    .ValueGeneratedNever()
                    .HasColumnName("eid");

                entity.Property(e => e.Ecity)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("ecity");

                entity.Property(e => e.Edoj)
                    .HasColumnType("date")
                    .HasColumnName("edoj")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Ename)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ename");

                entity.Property(e => e.Esal).HasColumnName("esal");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.Property(e => e.ReviewId).HasColumnName("reviewId");

                entity.Property(e => e.Review1)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("review");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.VehicleId).HasColumnName("vehicleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__userid__640DD89F");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Reviews__vehicle__6501FCD8");
            });

            modelBuilder.Entity<TransactionVrm>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Transact__9B57CF722FBCDF36");

                entity.ToTable("Transaction_vrm");

                entity.Property(e => e.TransactionId).HasColumnName("transactionId");

                entity.Property(e => e.RentalEndDate)
                    .HasColumnType("date")
                    .HasColumnName("rental_end_date");

                entity.Property(e => e.RentalRate)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("rental_rate");

                entity.Property(e => e.RentalStartDate)
                    .HasColumnType("date")
                    .HasColumnName("rental_start_date");

                entity.Property(e => e.Userid).HasColumnName("userid");

                entity.Property(e => e.VehicleId).HasColumnName("vehicleId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.TransactionVrms)
                    .HasForeignKey(d => d.Userid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__useri__04AFB25B");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.TransactionVrms)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transacti__vehic__03BB8E22");
            });

            modelBuilder.Entity<Vehicle>(entity =>
            {
                entity.ToTable("Vehicle");

                entity.Property(e => e.VehicleId).HasColumnName("vehicleId");

                entity.Property(e => e.DailyRent).HasColumnName("dailyRent");

                entity.Property(e => e.IsAvailable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ModelName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("modelName");

                entity.Property(e => e.RegistrationNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("registrationNumber");

                entity.Property(e => e.VehicleType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("vehicleType");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
