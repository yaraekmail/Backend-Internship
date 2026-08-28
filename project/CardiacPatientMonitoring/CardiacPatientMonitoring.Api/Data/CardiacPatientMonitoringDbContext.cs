using CardiacPatientMonitoring.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CardiacPatientMonitoring.Api.Data;

// Represents the EF Core database context for the cardiac patient monitoring system.
public class CardiacPatientMonitoringDbContext : IdentityDbContext<IdentityUser>{
    // Initializes the database context using the configured options.
    public CardiacPatientMonitoringDbContext(
        DbContextOptions<CardiacPatientMonitoringDbContext> options)
        : base(options)
    {
    }

    // Represents the Patients table in the database.
    public DbSet<Patient> Patients { get; set; }

    // Represents the VitalSigns table in the database.
    public DbSet<VitalSign> VitalSigns { get; set; }

    // Represents the Medications table in the database.
    public DbSet<Medication> Medications { get; set; }

    // Represents the Appointments table in the database.
    public DbSet<Appointment> Appointments { get; set; }

    // Represents the MedicalConditions table in the database.
    public DbSet<MedicalCondition> MedicalConditions { get; set; }

    // Represents the Allergies table in the database.
    public DbSet<Allergy> Allergies { get; set; }

    // Configures entity relationships and database behavior.
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    // Configures the precision for oxygen saturation values.
    modelBuilder.Entity<VitalSign>()
        .Property(v => v.OxygenSaturation)
        .HasPrecision(5, 2);

    // Configures the precision for temperature values.
    modelBuilder.Entity<VitalSign>()
        .Property(v => v.Temperature)
        .HasPrecision(5, 2);
        // Configures the one-to-many relationship between Patient and VitalSign.
        modelBuilder.Entity<VitalSign>()
            .HasOne(v => v.Patient)
            .WithMany(p => p.VitalSigns)
            .HasForeignKey(v => v.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configures the one-to-many relationship between Patient and Medication.
        modelBuilder.Entity<Medication>()
            .HasOne(m => m.Patient)
            .WithMany(p => p.Medications)
            .HasForeignKey(m => m.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configures the one-to-many relationship between Patient and Appointment.
        modelBuilder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configures the one-to-many relationship between Patient and MedicalCondition.
        modelBuilder.Entity<MedicalCondition>()
            .HasOne(c => c.Patient)
            .WithMany(p => p.MedicalConditions)
            .HasForeignKey(c => c.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configures the one-to-many relationship between Patient and Allergy.
        modelBuilder.Entity<Allergy>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Allergies)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}