namespace IBL.BO
{
    public enum Weight 
    {
        Light = 1,
        Medium,
        Heavy
    }
    public enum Priority
    {
        Standart = 1,
        Fast,
        Emergency
    }
    public enum ParcelStatus
    {
        Created = 1,
        Assigned,
        Picked,
        Delivered
    }
    public enum ParcelTransferStatus
    {
        WaitingToBePickedUp,
        OnTheWayToDestination
    }
    public enum DroneStatuses
    {
        Available=1 ,
        Maintenance,
        Shipping
    }

}