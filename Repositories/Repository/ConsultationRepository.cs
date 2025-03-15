using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Repositories.Repository
{
    public class ConsultationRepository : IConsultationRepository
    {
        private readonly AppointmentManagementDbContext _context;
        public ConsultationRepository(AppointmentManagementDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task<bool> AddConsultationAsync(Consultation consultation)
        {
            await _context.Consultations.AddAsync(consultation);
            await _context.SaveChangesAsync();
            return true;
        }

        //Read
        public async Task<Consultation> GetConsultationByAppointmentIdAsync(int appointmentId)
        {
            return await _context.Consultations.FirstOrDefaultAsync(q => q.AppointmentId == appointmentId);
        }
    }
}
