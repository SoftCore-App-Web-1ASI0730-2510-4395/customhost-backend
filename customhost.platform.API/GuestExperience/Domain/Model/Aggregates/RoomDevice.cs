using customhost_backend.GuestExperience.Domain.Model.Commands;

namespace customhost_backend.GuestExperience.Domain.Model.Aggregates;

/// <summary>
/// Room Device aggregate root that represents an IoT device assigned to a specific room
/// </summary>
public class Device
{
    public int Id { get; private set; }
    public int RoomId { get; private set; }
    public int DeviceModelId { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }

    // Navigation properties
    public virtual DeviceModel DeviceModel { get; private set; }

    // For EF Core
    protected Device() { }

    public Device(int roomId, int DeviceModelId, string status = "working")
    {
        if (roomId <= 0)
            throw new ArgumentException("Room ID must be greater than zero", nameof(roomId));
        
        if (DeviceModelId <= 0)
            throw new ArgumentException("IoT Device ID must be greater than zero", nameof(DeviceModelId));

        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be null or empty", nameof(status));

        RoomId = roomId;
        DeviceModelId = DeviceModelId;
        Status = status;
        CreatedAt = DateTime.UtcNow;
    }

    public Device(CreateDeviceCommand command) : this(command.RoomId, command.DeviceModelId, command.Status)
    {
    }
    
    public void ChangeStatus(string newStatus)
    {
        if (string.IsNullOrWhiteSpace(newStatus))
            throw new ArgumentException("New status cannot be null or empty", nameof(newStatus));

        Status = newStatus;
    }
    

    public void UpdateStatus(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status cannot be null or empty", nameof(status));

        Status = status;
    }
}
