namespace customhost_backend.crm.Domain.Models.ValueObjects;

public enum BookingStatus
{
    Pending,
    Confirmed,
    CheckedIn,
    CheckedOut,
    Cancelled,
    NoShow,
    Completed // Agregado para reflejar el estado presente en la base de datos
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Partial,
    Failed,
    Refunded
}
