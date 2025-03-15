using System.Reflection;
using AppointmentManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Models
{
    public class AppointmentManagementDbContext : DbContext
    {
        public AppointmentManagementDbContext(DbContextOptions<AppointmentManagementDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<UserCredential> userCredentials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Declaring Primary Key for all Models
            modelBuilder.Entity<Appointment>()
                .HasKey(e => e.AppointmentId);
            modelBuilder.Entity<Availability>()
                .HasKey(e => e.DoctorId);
            modelBuilder.Entity<Consultation>()
                .HasKey(e => e.ConsultationId);
            modelBuilder.Entity<Patient>()
                .HasKey(e => e.PatientId);
            modelBuilder.Entity<Doctor>()
                .HasKey(e => e.DoctorId);
            modelBuilder.Entity<TimeSlot>()
                .HasKey(e => e.TimeSlotId);
            modelBuilder.Entity<UserCredential>()
                .HasKey(e => e.CredID);

            //Specify Relations And Foreign Key
            modelBuilder.Entity<Appointment>()
                .HasOne(q => q.Availability)
                .WithMany(q => q.DoctorAppointment)
                .HasForeignKey(q => q.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(q => q.Patient)
                .WithMany(q => q.Appointments)
                .HasForeignKey(q => q.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(q => q.Doctor)
                .WithMany(q => q.Appointments)
                .HasForeignKey(q => q.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Consultation>()
                .HasOne(q => q.Appointment)
                .WithOne(q => q.Consultation)
                .HasForeignKey<Consultation>(q => q.AppointmentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<TimeSlot>()
                .HasOne(q => q.Availability)
                .WithMany(q => q.TimeSlots)
                .HasForeignKey(q => q.DoctorId);

            modelBuilder.Entity<UserCredential>()
                .HasOne(q => q.Patient)
                .WithOne(q => q.UserCredential)
                .HasForeignKey<UserCredential>(q => q.PatientId);

            modelBuilder.Entity<UserCredential>()
                .HasOne(q => q.Doctor)
                .WithOne(q => q.UserCredential)
                .HasForeignKey<UserCredential>(q => q.DoctorId);

            //Getting all Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
