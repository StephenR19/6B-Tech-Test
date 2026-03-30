using System.ComponentModel.DataAnnotations;

namespace SixBee.Api.DTOs;

public class CreateAppointmentDto
{
    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateTime AppointmentDateTime { get; set; }

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public string ContactNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string EmailAddress { get; set; } = string.Empty;
}
