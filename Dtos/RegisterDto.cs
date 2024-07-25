using System.ComponentModel.DataAnnotations;

namespace upword.Api.Dtos;

public record RegisterDto(
    [Required, EmailAddress] string Email,
    [Required, StringLength(100, MinimumLength = 6)] string Password,
    [Required] string ConfirmPassword,
    [StringLength(50)] string? FirstName,
    [StringLength(50)] string? LastName
);
