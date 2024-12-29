using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Clinic_Management_System.Models;
using Clinic_Management_System.ViewModels;

namespace Clinic_Management_System.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly AppDbContext _context;

        public AppointmentsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Appointments
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Appointments.Include(a => a.doctor).Include(a => a.patient);
            return View(await appDbContext.ToListAsync());
        }

        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.doctor)
                .Include(a => a.patient)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public async Task<IActionResult> Create()
        {
            var dl =await _context.Doctors.ToListAsync();
            var pl =await _context.Patients.ToListAsync();
            var vd = new AppointmentsViewModel
            {
                doctors = dl,
                patients = pl,
            };
            return View(vd);
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
           
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            
            
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _context.Appointments.Include(a => a.doctor)
                .Include(p => p.patient).FirstOrDefaultAsync(m => m.AppointmentID == id);

            if(appointment == null) { return NotFound(); }
           
            var dl = await _context.Doctors.ToListAsync();
            var pl = await _context.Patients.ToListAsync();

            var viewModel = new AppointmentsViewModel
            {
                AppointmentID = appointment.AppointmentID,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentNotes = appointment.AppointmentNotes,
                DoctorID = appointment.DoctorID,
                PatientID = appointment.PatientID,
                doctors = dl,
                patients = pl,
            };

            return View(viewModel);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(int id,AppointmentsViewModel viewModel)
        {
            if (id != viewModel.AppointmentID)
            {
                return NotFound();
            }

            
                var appointmetn = await _context.Appointments.FindAsync(id);
                if (appointmetn == null) { return NotFound(); }

                appointmetn.AppointmentID = viewModel.AppointmentID;
                appointmetn.AppointmentNotes = viewModel.AppointmentNotes;
                appointmetn.AppointmentDate = viewModel.AppointmentDate;
                appointmetn.PatientID = viewModel.PatientID;
                appointmetn.DoctorID = viewModel.DoctorID;

                _context.Update(appointmetn);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            

                
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointments
                .Include(a => a.doctor)
                .Include(a => a.patient)
                .FirstOrDefaultAsync(m => m.AppointmentID == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Appointment appointment)
        {
            var appointmentt = await _context.Appointments.FindAsync(appointment.AppointmentID);
            if (appointmentt != null)
            {
                _context.Appointments.Remove(appointmentt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointments.Any(e => e.AppointmentID == id);
        }
    }
}
