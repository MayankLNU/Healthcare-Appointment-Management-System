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

        // Read
        public async Task<IEnumerable<TimeSlot>> GetAvailableTimeSlotsByDate (DateOnly date)
        {
            return await _context.TimeSlots.Where(x => x.Date == date && x.IsAvailable == true).ToListAsync();
        }

        public async Task<TimeSlot> GetAvailableTimeSlotsByDateTimeAndDrId(DateOnly date, TimeOnly startTime, int doctorId)
        {
            return await _context.TimeSlots.FirstOrDefaultAsync(x => x.Date == date && x.StartTime == startTime && x.DoctorId == doctorId);
        }

        public async Task<IEnumerable<TimeSlot>> GetBookedTimeSlotsByDateAndDrId(DateOnly date, int doctorId)
        {
            return await _context.TimeSlots.Where(x => x.Date == date && x.DoctorId == doctorId && x.IsAvailable == false).ToListAsync();
        }

        // Update
        public async Task<bool> UpdateTimeSlotAsync(TimeSlot timeSlot)
        {
            if (timeSlot != null)
            {
                _context.TimeSlots.Update(timeSlot);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateTimeSlotAvailabilityAsync(DateOnly date, TimeOnly startTime, int doctorId, bool isAvailable)
        {
            var timeSlot = await _context.TimeSlots.FirstOrDefaultAsync(x => x.Date == date && x.StartTime == startTime && x.DoctorId == doctorId);
            if (timeSlot != null)
            {
                timeSlot.IsAvailable = isAvailable;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Delete
        public async Task DeleteTimeSlotAsync(int timeSlotId)
        {
            var timeSlot = await _context.TimeSlots.FindAsync(timeSlotId);
            if (timeSlot != null)
            {
                _context.TimeSlots.Remove(timeSlot);
                await _context.SaveChangesAsync();
            }
        }
    }
}
