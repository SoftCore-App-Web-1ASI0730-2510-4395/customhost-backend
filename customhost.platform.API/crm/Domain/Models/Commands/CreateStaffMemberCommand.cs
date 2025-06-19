using customhost_backend.crm.Domain.Models.ValueObjects;

namespace customhost_backend.crm.Domain.Models.Commands;

public record CreateStaffMemberCommand
{
    public int HotelId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Department Department { get; set; }

    public CreateStaffMemberCommand(int hotelId, string firstName, string lastName, string email, string phone, Department department)
    {
        if (hotelId < 1)
        {
            throw new ArgumentException("Hotel ID must be a positive integer.", nameof(hotelId));
        }
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name cannot be empty.", nameof(firstName));
        }
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name cannot be empty.", nameof(lastName));
        }
        if (string.IsNullOrWhiteSpace(email) || !email.Contains("@"))
        {
            throw new ArgumentException("Email must be a valid email address.", nameof(email));
        }
        if (string.IsNullOrWhiteSpace(phone) || phone.Length < 9)
        {
            throw new ArgumentException("Phone number must be at least 9 characters long.", nameof(phone));
        }
        
        HotelId = hotelId;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Phone = phone;
        Department = department;
    }
};