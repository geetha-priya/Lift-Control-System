using System;
using System.Collections.Generic;
using System.Linq;

public class LiftController : ILiftControls
{
    public List<Lift> Lifts { get; set; }
    public List<Passenger> WaitingPassengers { get; set; }
    public IDispatchingStrategy Dispatcher { get; set; }

    public LiftController(int numberOfLifts, IDispatchingStrategy dispatcher)
    {
        Lifts = Enumerable.Range(0, numberOfLifts).Select(eid => new Lift(eid)).ToList();
        WaitingPassengers = new List<Passenger>();
        Dispatcher = dispatcher;
    }

    public void PickupRequest(int pickupFloor, int destinationFloor, int passengernumber)
    {
        var Passenger = new Passenger(pickupFloor, destinationFloor, passengernumber);
        Console.WriteLine($"Passenger {Passenger.Id} going from L{pickupFloor} to L{destinationFloor}");
        WaitingPassengers.Add(Passenger);
    }

    public bool AnyPendingPickups()
    {
        return WaitingPassengers.Any();
    }
    public int ProcessPickups()
    {
        var stepCount = 0;
        while (AnyPendingPickups())
        {
            Dispatcher.DispatchPickups(this);
            stepCount++;
            Console.WriteLine($"Step {stepCount} completed");
        }
        return stepCount;
    }
}
