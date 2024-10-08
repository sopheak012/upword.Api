using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using upword.Api.Data;
using upword.Api.Dtos;
using upword.Api.Entities;

namespace upword.Api.Endpoints;

public static class AuthEndpoints
{
    const string LoginEndpointName = "Login";
    const string RegisterEndpointName = "Register";

    public static RouteGroupBuilder MapAuthEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("auth").WithParameterValidation();

        // Endpoint to register a new user
        group
            .MapPost(
                "/register",
                async (RegisterDto registerDto, UserManager<ApplicationUser> userManager) =>
                {
                    var user = new ApplicationUser
                    {
                        UserName = registerDto.Email, // Set UserName to be the same as Email
                        Email = registerDto.Email,
                        DateOfBirth = registerDto.DateOfBirth
                    };

                    var result = await userManager.CreateAsync(user, registerDto.Password);

                    if (result.Succeeded)
                    {
                        // Return the user ID and a success message
                        return Results.CreatedAtRoute(
                            RegisterEndpointName,
                            new { email = user.Email },
                            new
                            {
                                Email = user.Email,
                                Id = user.Id, // Include the user ID
                                Message = "User registered successfully"
                            }
                        );
                    }

                    return Results.BadRequest(result.Errors);
                }
            )
            .WithName(RegisterEndpointName);

        // Endpoint to login
        group
            .MapPost(
                "/login",
                async (
                    LoginDto loginDto,
                    SignInManager<ApplicationUser> signInManager,
                    UserManager<ApplicationUser> userManager
                ) =>
                {
                    var result = await signInManager.PasswordSignInAsync(
                        loginDto.Email, // Use Email for sign-in
                        loginDto.Password,
                        isPersistent: false,
                        lockoutOnFailure: false
                    );

                    if (result.Succeeded)
                    {
                        var user = await userManager.FindByEmailAsync(loginDto.Email);

                        return Results.Ok(
                            new
                            {
                                Message = "User logged in successfully",
                                Id = user?.Id // Include the user Id in the response
                            }
                        );
                    }

                    return Results.BadRequest("Invalid login attempt");
                }
            )
            .WithName(LoginEndpointName);

        return group;
    }
}
