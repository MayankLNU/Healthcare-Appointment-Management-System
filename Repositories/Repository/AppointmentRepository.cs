﻿using AppointmentManagement.Models;
using AppointmentManagement.Models.Domain;
using AppointmentManagement.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Repositories.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppointmentManagementDbContext _context;
        public AppointmentRepository(AppointmentManagementDbContext context)
        {
            _context = context;
        }

        //Create
        public async Task<bool> AddAppointmentAsync(Appointment appointment)
        {
            if (appointment != null)
            {
                await _context.Appointments.AddAsync(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //Read
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment> GetAppointmentByIdAsync(int appointmentId)
        {
            return await _context.Appointments.FindAsync(appointmentId);
        }

        //Update
        public async Task<bool> UpdateAppointmentAsync(Appointment appointment)
        {
            if (appointment != null)
            {
                _context.Appointments.Update(appointment);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //Delete
        public async Task DeleteAppointmentAsync(int appointmentId)
        {
            var appointment = await _context.Appointments.FindAsync(appointmentId);
            if (appointment != null)
            {
                _context.Appointments.Remove(appointment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
