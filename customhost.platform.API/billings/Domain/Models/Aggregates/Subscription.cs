using customhost_backend.billings.Domain.Models.Commands;

namespace customhost_backend.billings.Domain.Models.Aggregates;

public class Subscription
{
    
    
    public int Id { get; }
    public int HotelId { get; set; }
    public int SubscriptionPlanId { get; set; }
    public string Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    
    public Subscription() {}

    public Subscription(CreateSubscriptionCommand command)
    {
        
        HotelId = command.HotelId;
        SubscriptionPlanId = command.SubscriptionPlanId;
        Status = command.Status;
        StartDate = command.StartDate;
        EndDate = command.EndDate;
        
    }
    
    
    
    
    
    
}