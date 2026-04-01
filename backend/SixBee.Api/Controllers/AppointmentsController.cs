using Microsoft.AspNetCore.Mvc;
using SixBee.Api.Data;
using SixBee.Api.DTOs;
using SixBee.Api.Middleware;
using SixBee.Api.Models;

namespace SixBee.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController : ControllerBase
{
    private readonly AppDbContext _db;

    public AppointmentsController(AppDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var appointments = _db.Appointments
            .OrderBy(a => a.AppointmentDateTime)
            .ToList();

        return Ok(appointments);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var appointment = _db.Appointments.Find(id);
        if (appointment == null)
            return NotFound();

        return Ok(appointment);
    }

    [HttpPost]
    [AllowAnonymous]
    public IActionResult Create([FromBody] CreateAppointmentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var appointment = new Appointment
        {
            Name = dto.Name,
            AppointmentDateTime = dto.AppointmentDateTime,
            Description = dto.Description,
            ContactNumber = dto.ContactNumber,
            EmailAddress = dto.EmailAddress
        };

        _db.Appointments.Add(appointment);
        _db.SaveChanges();

        return Created($"/api/appointments/{appointment.Id}", appointment);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateAppointmentDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var appointment = _db.Appointments.Find(id);
        if (appointment == null)
            return NotFound();

        appointment.Name = dto.Name;
        appointment.AppointmentDateTime = dto.AppointmentDateTime;
        appointment.Description = dto.Description;
        appointment.ContactNumber = dto.ContactNumber;
        appointment.EmailAddress = dto.EmailAddress;

        _db.SaveChanges();

        return Ok(appointment);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var appointment = _db.Appointments.Find(id);
        if (appointment == null)
            return NotFound();

        _db.Appointments.Remove(appointment);
        _db.SaveChanges();

        return NoContent();
    }

    [HttpPatch("{id}/approve")]
    public IActionResult Approve(int id)
    {
        var appointment = _db.Appointments.Find(id);
        if (appointment == null)
            return NotFound();

        appointment.Status = AppointmentStatus.Approved;
        _db.SaveChanges();

        return Ok(appointment);
    }
}
