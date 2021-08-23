using System;

public class Program
{
    public static void Main(string[] args)
    {
        const int numberOfLevels = 10;
        const int numberOfLifts = 1;
        var inputKey = "a";

        do
        {
            var pickupCount = 0;
            var stepCount = 0;
            ILiftControls system = new LiftController(numberOfLifts, new ShortestWaitTimeStrategy());

            // Get pickup requests for the system
            Console.WriteLine($"Floor levels start from 0 (Ground level) and the top level is {numberOfLevels}");
            Console.Write("Enter number of Passengers: ");

            var NumberOfPassengers = Int32.Parse(Console.ReadLine());
            for (var i = 0; i < NumberOfPassengers; i++)
            {
                Console.Write($"Enter Originating floor for Passenger # {i + 1}: ");
                var originatingFloor = Int32.Parse(Console.ReadLine());
                Console.Write($"Enter Destination floor for Passenger # {i + 1}: ");
                var destinationFloor = Int32.Parse(Console.ReadLine());

                if (originatingFloor != destinationFloor)
                {
                    system.PickupRequest(originatingFloor, destinationFloor, i + 1);
                    pickupCount++;
                }
                else
                {
                    Console.WriteLine($"Originating floor cannot be same as Destination floor");
                }
            }

            stepCount = system.ProcessPickups();

            Console.WriteLine($"Dropped {pickupCount} Passenger(s) to their requested destination(s) in {stepCount} steps.");

            Console.WriteLine($"Enter any key to continue or press 'q' to quit");
            inputKey = Console.ReadLine();
        } while (!inputKey.Equals("q"));
    }
}
   

