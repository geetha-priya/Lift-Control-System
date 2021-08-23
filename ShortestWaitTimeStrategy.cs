
using System;
using System.Collections.Generic;
using System.Linq;

class ShortestWaitTimeStrategy : IDispatchingStrategy
{
    public void DispatchPickups(ILiftControls system)
    {
        /* 
         * Commmon dispatch algorithms 
         *  1. First Come First Serve
         *  2. Shortest Seek Time First
         *  3. SCAN
         *  4. LOOK
        */

        // This dispatch algorithm implements Shortest Seek Time First dispatch algorithm.
        var loadUnloadLiftIds = new List<int>();

        /*
         * Handle loading of passengers. Group passengers waiting to be picked up on a level and are going in the
         * direction of a lift avaiable on that level. Make these passengers(embarkingPassengers) part of this lift.
         * Also remove such passengers from the WaitingPassengers list.
        */
        system.WaitingPassengers.GroupBy(r => new { r.OriginatingFloor, r.Direction }).ToList().ForEach(waitingFloor =>
        {
            var availableLift =
                system.Lifts.FirstOrDefault(
                    e =>
                        e.CurrentFloor == waitingFloor.Key.OriginatingFloor &&
                        (e.Direction == waitingFloor.Key.Direction || !e.Passengers.Any()));
            if (availableLift != null)
            {
                loadUnloadLiftIds.Add(availableLift.Id);
                var embarkingPassengers = waitingFloor.ToList();

                // Add passengers to the lift
                Lift.UpdateLift(system.Lifts, availableLift.Id, e => e.Passengers.AddRange(embarkingPassengers));

                if (availableLift.Passengers.Any())
                { 
                    Console.Write($"Lift {availableLift.Id + 1} is in L{availableLift.CurrentFloor} and is loading Passenger(s): ");
                    for (var i = 0; i < availableLift.Passengers.Count(); i++)
                    {
                        Console.Write($"P{availableLift.Passengers[i].Id} ");
                    }
                    Console.WriteLine("");
                }

                // Remove loaded passengers from waiting list
                system.WaitingPassengers = system.WaitingPassengers.Where(r => embarkingPassengers.All(er => er.Id != r.Id)).ToList();
            }
        });

        /*
         * Handle unloading of passengers. Check any passengers in the lift whose destination level is 
         * the same as current level of the lift and remove them from the lift (disembarkingPassengers)
        */
        system.Lifts = system.Lifts.Select(e =>
        {
            var disembarkingPassengers = e.Passengers.Where(r => r.DestinationFloor == e.CurrentFloor).ToList();
            if (disembarkingPassengers.Any())
            {
                loadUnloadLiftIds.Add(e.Id);

                if (e.Passengers.Any())
                {
                    Console.Write($"Lift {e.Id + 1} is in L{e.CurrentFloor} and is unloading Passenger(s): ");
                    for (var i = 0; i < e.Passengers.Count(); i++)
                    {
                        if (e.Passengers[i].DestinationFloor == e.CurrentFloor)
                            Console.Write($"P{e.Passengers[i].Id} ");
                    }
                    Console.WriteLine("");
                }

                // Unload the passengers at their destination floor
                e.Passengers = e.Passengers.Where(r => r.DestinationFloor != e.CurrentFloor).ToList();
            }

            return e;
        }).ToList();

        // Allocate next destination floor to Lifts
        system.Lifts.ForEach(e =>
        {
            var isBusy = loadUnloadLiftIds.Contains(e.Id);

            int destinationFloor;

            if (e.Passengers.Any())
            {
                // Next destination for this lift is the closest drop off floor of its passengers.
                var closestDestinationFloor =
                    e.Passengers.OrderBy(r => Math.Abs(r.DestinationFloor - e.CurrentFloor))
                        .First()
                        .DestinationFloor;
                destinationFloor = closestDestinationFloor;
            }
            else if (e.DestinationFloor == e.CurrentFloor && system.WaitingPassengers.Any())
            {
                // Since there are no passengers in the lift, next destination is originating floor of one/more waiting passengers.
                destinationFloor = system.WaitingPassengers.GroupBy(r => new { r.OriginatingFloor }).OrderBy(g => g.Count()).First().Key.OriginatingFloor;
            }
            else
            {
                // No one to be picked up or dropped off so leave the destination as is.
                destinationFloor = e.DestinationFloor;
            }

            // Move the lift up or down based on the above calculated destination.
            var floorNumber = isBusy
                ? e.CurrentFloor
                : e.CurrentFloor + (destinationFloor > e.CurrentFloor ? 1 : -1);
            Lift.Update(system.Lifts, e.Id, floorNumber, destinationFloor);

            //Console.WriteLine($"Lift {e.Id + 1} is in L{e.CurrentFloor}, Destination Floor Number is L{e.DestinationFloor}");
        });
    }
}

