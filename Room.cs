namespace App;

enum RoomStatus
{
    Available,
    Occupied,
    UnderMaintenance,
}

class Room
{
    public string GuestName;
    public int FloorNumber;
    public int RoomNumber;
    public RoomStatus Status;

    public Room(int floorNum, int roomNum)
    {
        FloorNumber = floorNum;
        RoomNumber = roomNum;
        Status = RoomStatus.Available;
    }

    public void Available()
    {
        Status = RoomStatus.Available;
    }
    public void Occupy(string guestName)
    {
        GuestName = guestName;
        Status = RoomStatus.Occupied;
    }

    public void RoomUnderMaintenance()
    {
        Status = RoomStatus.UnderMaintenance;
    }
}