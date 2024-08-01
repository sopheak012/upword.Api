using System;
using Microsoft.AspNetCore.Identity;

namespace upword.Api.Entities;

public class ApplicationUser : IdentityUser
{
    public DateOnly? DateOfBirth { get; set; }
}
