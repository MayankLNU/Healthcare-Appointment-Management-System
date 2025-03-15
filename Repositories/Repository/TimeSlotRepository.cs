using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Repositories.Repository
{
    public class TimeSlotRepository : ITimeSlotRepository
    {
        public readonly AppointmentManagementDbContext _context;
        public TimeSlotRepository(AppointmentManagementDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByDate (DateOnly date)
        {
            return await _context.TimeSlots.Where(x => x.Date == date).Where(x=>x.IsAvailable == true).ToListAsync();
        }

        public async Task<TimeSlot> GetAvailableTimeSlotsByDateTimeAndDrId(DateOnly date, TimeOnly startTime, int doctorId)
        {
            return await _context.TimeSlots.FirstOrDefaultAsync(x => x.Date == date && x.StartTime == startTime && x.DoctorId == doctorId);
        }

        public async Task UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            _context.TimeSlots.Update(timeSlot);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTimeSlotAvailabilityAsync(DateOnly date, TimeOnly startTime, int doctorId, bool isAvailable)
        {
            var timeSlot = await _context.TimeSlots.FirstOrDefaultAsync(x => x.Date == date && x.StartTime == startTime && x.DoctorId == doctorId);
            if (timeSlot != null)
            {
                timeSlot.IsAvailable = isAvailable;
                await _context.SaveChangesAsync();
            }
        }
    }
}
