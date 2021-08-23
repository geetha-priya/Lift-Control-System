public class Passenger
{
    public Passenger(int originatingFloor, int destinationFloor, int passengernumber)
    {
        OriginatingFloor = originatingFloor;
        DestinationFloor = destinationFloor;
        Id = passengernumber;
    }

    public int Id { get; private set; }
    public int OriginatingFloor { get; private set; }
    public int DestinationFloor { get; private set; }

    public Direction Direction
    {
        get { return OriginatingFloor < DestinationFloor ? Direction.Up : Direction.Down; }
    }
}
