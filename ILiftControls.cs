using System.Collections.Generic;

public interface ILiftControls
{
    public List<Lift> Lifts { get; set; }
    public List<Passenger> WaitingPassengers { get; set; }
    void PickupRequest(int pickupFloor, int destinationFloor, int passengerNumber);
    int ProcessPickups();
    bool AnyPendingPickups();
}
