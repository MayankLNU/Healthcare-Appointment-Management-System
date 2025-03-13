using System.Reflection;
using AppointmentManagement.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement
{
    public class AppointmentManagementDbContext : DbContext
    {
        public AppointmentManagementDbContext(DbContextOptions<AppointmentManagementDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Availability> Availabilities { get; set; }
        public DbSet<Consultation> Consultations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Declaring Primary Key for all Models
            modelBuilder.Entity<Appointment>()
                .HasKey(e => e.AppointmentId);
            modelBuilder.Entity<Availability>()
                .HasKey(e => e.DoctorId);
            modelBuilder.Entity<Consultation>()
                .HasKey(e => e.ConsultationId); 
            modelBuilder.Entity<User>()
                .HasKey(e => e.UserId);
            modelBuilder.Entity<TimeSlot>()
                .HasKey(e => e.TimeSlotId);

            //Specify Relations And Foreign Key
            modelBuilder.Entity<Appointment>()
                .HasOne(q => q.Availability)
                .WithMany(q => q.DoctorAppointment)
                .HasForeignKey(q => q.DoctorId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(q => q.Patient)
                .WithMany(q => q.PatientAppointments)
                .HasForeignKey(q => q.PatientId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Appointment>()
                .HasOne(q => q.Doctor)
                .WithMany(q => q.DoctorAppointments)
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

            //Getting all Configurations
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
