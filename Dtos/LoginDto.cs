using System.ComponentModel.DataAnnotations;

namespace upword.Api.Dtos;

public record class LoginDto([Required, EmailAddress] string Email, [Required] string Password);
