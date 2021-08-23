using System;
using System.Collections.Generic;
using System.Linq;

public class Lift
{
    public Lift(int id)
    {
        Id = id;
        Passengers = new List<Passenger>();
    }

    public int Id { get; private set; }
    public int CurrentFloor { get; set; }
    public int DestinationFloor { get; set; }

    public Direction Direction
    {
        get
        {
            return CurrentFloor == 1
                ? Direction.Up
                : DestinationFloor > CurrentFloor ? Direction.Up : Direction.Down;
        }
    }

    public List<Passenger> Passengers { get; set; }

    public static void Update(List<Lift> lifts, int liftId, int currentFloorNumber, int destFloorNumber)
    {
        UpdateLift(lifts,liftId, e =>
        {
            e.CurrentFloor = (currentFloorNumber < 0) ? 0 : currentFloorNumber;
            e.DestinationFloor = (destFloorNumber < 0) ? 0 : destFloorNumber;
        });
    }

   public static void UpdateLift(List<Lift> lifts, int liftId, Action<Lift> update)
    {
        lifts = lifts.Select(e =>
        {
            if (e.Id == liftId) update(e);
            return e;
        }).ToList();
    }
}
