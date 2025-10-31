namespace App;              // Organize related classes in the same namespace.

enum RoomStatus             // Represents possible room conditions.
{
    Available,              // Room is free booking.
    Occupied,               // Room currently occupied by a guest.
    UnderMaintenance,       // Room temporarily unavailable.
}

class Room      // Represents a hotel room and current condition.
{
    public string GuestName;        // Stores the name of a guest.
    public int FloorNumber;     // Stores information about which floor the room is located.
    public int RoomNumber;      // Store information about room number
    public RoomStatus Status;       // Tracks current room condition. 

    public Room(int floorNum, int roomNum)      // Constructor initialize new room details.
    {
        FloorNumber = floorNum;     // Assign provided floor number information to the Room class field.
        RoomNumber = roomNum;       // Assign provided room number information to the Room class field.
        Status = RoomStatus.Available;      // Set "Available" to a default room condition - all rooms starts as available.
    }

    public void Available()     // Sets room to available.
    {
        Status = RoomStatus.Available;
    }
    public void Occupy(string guestName)        // Books a room for a guest.
    {
        GuestName = guestName;                  // Assign provided guest name.
        Status = RoomStatus.Occupied;           // Updates room condition to occupied.
    }

    public void RoomUnderMaintenance()          // Mark room a temporarily unavailable (under maintenance).
    {
        Status = RoomStatus.UnderMaintenance;
    }
}