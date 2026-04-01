using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using SixBee.Api.Controllers;
using SixBee.Api.Data;
using SixBee.Api.DTOs;
using SixBee.Api.Models;

namespace SixBee.Api.Tests;

[TestFixture]
public class AppointmentsControllerTests
{
    private AppDbContext _context = null!;
    private AppointmentsController _controller = null!;

    [SetUp]
    public void SetUp()
    {
        _context = TestHelper.CreateInMemoryContext();
        _controller = new AppointmentsController(_context);
    }

    [TearDown]
    public void TearDown()
    {
        _context.Dispose();
    }

    [Test]
    public void GetAll_ReturnsAppointmentsOrderedByDate()
    {
        _context.Appointments.AddRange(
            new Appointment { Name = "Later", AppointmentDateTime = new DateTime(2026, 6, 1), Description = "d", ContactNumber = "1", EmailAddress = "a@a.com" },
            new Appointment { Name = "Earlier", AppointmentDateTime = new DateTime(2026, 4, 1), Description = "d", ContactNumber = "1", EmailAddress = "a@a.com" }
        );
        _context.SaveChanges();

        var result = _controller.GetAll() as OkObjectResult;

        result.Should().NotBeNull();
        var appointments = result!.Value as List<Appointment>;
        appointments.Should().HaveCount(2);
        appointments![0].Name.Should().Be("Earlier");
        appointments[1].Name.Should().Be("Later");
    }

    [Test]
    public void Get_WithValidId_ReturnsAppointment()
    {
        var appointment = new Appointment
        {
            Name = "Test",
            AppointmentDateTime = DateTime.UtcNow,
            Description = "desc",
            ContactNumber = "123",
            EmailAddress = "test@test.com"
        };
        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        var result = _controller.Get(appointment.Id) as OkObjectResult;

        result.Should().NotBeNull();
        var returned = result!.Value as Appointment;
        returned!.Name.Should().Be("Test");
    }

    [Test]
    public void Get_WithInvalidId_ReturnsNotFound()
    {
        var result = _controller.Get(999);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public void Create_WithValidData_ReturnsCreated()
    {
        var dto = new CreateAppointmentDto
        {
            Name = "New Patient",
            AppointmentDateTime = new DateTime(2026, 5, 1, 10, 0, 0),
            Description = "Checkup",
            ContactNumber = "07700900000",
            EmailAddress = "patient@test.com"
        };

        var result = _controller.Create(dto) as CreatedResult;

        result.Should().NotBeNull();
        var appointment = result!.Value as Appointment;
        appointment!.Name.Should().Be("New Patient");
        appointment.Status.Should().Be(AppointmentStatus.Pending);
        _context.Appointments.Should().HaveCount(1);
    }

    [Test]
    public void Update_WithValidData_ReturnsUpdatedAppointment()
    {
        var appointment = new Appointment
        {
            Name = "Old Name",
            AppointmentDateTime = DateTime.UtcNow,
            Description = "desc",
            ContactNumber = "123",
            EmailAddress = "old@test.com"
        };
        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        var dto = new UpdateAppointmentDto
        {
            Name = "Updated Name",
            AppointmentDateTime = new DateTime(2026, 7, 1),
            Description = "Updated desc",
            ContactNumber = "456",
            EmailAddress = "updated@test.com"
        };

        var result = _controller.Update(appointment.Id, dto) as OkObjectResult;

        result.Should().NotBeNull();
        var updated = result!.Value as Appointment;
        updated!.Name.Should().Be("Updated Name");
        updated.EmailAddress.Should().Be("updated@test.com");
    }

    [Test]
    public void Update_WithInvalidId_ReturnsNotFound()
    {
        var dto = new UpdateAppointmentDto
        {
            Name = "Name",
            AppointmentDateTime = DateTime.UtcNow,
            Description = "desc",
            ContactNumber = "123",
            EmailAddress = "test@test.com"
        };

        var result = _controller.Update(999, dto);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public void Delete_WithValidId_RemovesAppointment()
    {
        var appointment = new Appointment
        {
            Name = "To Delete",
            AppointmentDateTime = DateTime.UtcNow,
            Description = "desc",
            ContactNumber = "123",
            EmailAddress = "del@test.com"
        };
        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        var result = _controller.Delete(appointment.Id);

        result.Should().BeOfType<NoContentResult>();
        _context.Appointments.Should().BeEmpty();
    }

    [Test]
    public void Delete_WithInvalidId_ReturnsNotFound()
    {
        var result = _controller.Delete(999);

        result.Should().BeOfType<NotFoundResult>();
    }

    [Test]
    public void Approve_SetsStatusToApproved()
    {
        var appointment = new Appointment
        {
            Name = "To Approve",
            AppointmentDateTime = DateTime.UtcNow,
            Description = "desc",
            ContactNumber = "123",
            EmailAddress = "ap@test.com",
            Status = AppointmentStatus.Pending
        };
        _context.Appointments.Add(appointment);
        _context.SaveChanges();

        var result = _controller.Approve(appointment.Id) as OkObjectResult;

        result.Should().NotBeNull();
        var approved = result!.Value as Appointment;
        approved!.Status.Should().Be(AppointmentStatus.Approved);
    }

    [Test]
    public void Approve_WithInvalidId_ReturnsNotFound()
    {
        var result = _controller.Approve(999);

        result.Should().BeOfType<NotFoundResult>();
    }
}
