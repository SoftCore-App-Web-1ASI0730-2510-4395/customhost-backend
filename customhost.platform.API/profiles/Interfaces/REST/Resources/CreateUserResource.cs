using System.ComponentModel.DataAnnotations;

namespace customhost_backend.profiles.Interfaces.REST.Resources;

public record CreateProfileResource(
    [param: Range(1, int.MaxValue, ErrorMessage = "HotelId debe ser un entero positivo.")]
    int? HotelId,


    [param: Required(ErrorMessage = "FirstName es obligatorio.")]
    [param: StringLength(100, MinimumLength = 1, ErrorMessage = "FirstName debe tener entre 1 y 100 caracteres.")]
    string? FirstName,

    [param: Required(ErrorMessage = "LastName es obligatorio.")]
    [param: StringLength(100, MinimumLength = 1, ErrorMessage = "LastName debe tener entre 1 y 100 caracteres.")]
    string? LastName,

    [param: Required(ErrorMessage = "Email es obligatorio.")]
    [param: EmailAddress(ErrorMessage = "Formato de email inválido.")]
    [param: StringLength(255, ErrorMessage = "Email no puede exceder 255 caracteres.")]
    string? Email,

    [param: Required(ErrorMessage = "Password es obligatorio.")]
    [param: StringLength(100, MinimumLength = 6, ErrorMessage = "Password debe tener entre 6 y 100 caracteres.")]
    string? Password,

    [param: Required(ErrorMessage = "Phone es obligatorio.")]
    [param: StringLength(20, MinimumLength = 1, ErrorMessage = "Phone debe tener entre 1 y 20 caracteres.")]
    string? Phone,

    [param: Required(ErrorMessage = "Role es obligatorio.")]
    string? Role,
    
    [param: Range(1, int.MaxValue, ErrorMessage = "UserId debe ser un entero positivo.")]
    int? UserId
    
    
);