// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using App;



List<User> users = new();  // List of users
users.Add(new User("hotell", "pass"));

// save the user to a file (loginInfo.save)
string[] save_users_to_file = new string[users.Count];
for (int i = 0; i < users.Count; ++i)
{
    save_users_to_file[i] = users[i].ToSaveString();
}
File.WriteAllLines("loginInfo.save", save_users_to_file);

string filePath = "roomsInfo.save";
Room[,] hotelRooms = new Room[3, 6]; // 2D array for rooms
                                     // Initialize rooms
for (int floorNum = 0; floorNum < 3; floorNum++)
{
    for (int roomNum = 0; roomNum < 6; roomNum++)
    {
        hotelRooms[floorNum, roomNum] = new Room(floorNum + 1, roomNum + 1);
    }
}
// Load previously saved data
LoadSavedRooms(hotelRooms, filePath);


User? active_user = null;
Menu menu = Menu.None;

bool running = true;

while (running)
{
    Console.Clear();

    switch (menu)
    {
        case Menu.None:
            {
                Console.WriteLine("Welcome! please login to continue....");
                Console.Write("Enter username: ");
                string? username = Console.ReadLine();
                Console.Write("Enter password: ");
                string? password = Console.ReadLine();

                Console.Clear();
                Debug.Assert(username != null);
                Debug.Assert(password != null);


                bool saved_file_found = File.Exists("loginInfo.save");
                if (!saved_file_found)
                {
                    Console.WriteLine("Saved file does not exist.");
                }
                else
                {
                    string[] saved_userInfos = File.ReadAllLines("loginInfo.save");
                    bool saved_userInfoMatch = false;
                    string[] userInfo;
                    foreach (string saved_userInfo in saved_userInfos)
                    {
                        userInfo = saved_userInfo.Split(",");
                        if (userInfo[0] == username && userInfo[1] == password)
                        {
                            saved_userInfoMatch = true;
                            //Console.WriteLine($"Welcome! {userInfo[0]}, {userInfo[1]}");
                            active_user = new User(userInfo[0], userInfo[1]);
                            menu = Menu.Main;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("User does not exist or Invalid username or password");
                            menu = Menu.None;
                        }
                    }
                }
            }
            break;

        case Menu.Main:
            {
                Console.Clear();
                Console.WriteLine("=====Welcome to the Rooms Tracking System=====");
                Console.WriteLine("\nChoose what you want to do: ");
                Console.WriteLine("[V]. View All Rooms Outlook");
                Console.WriteLine("[O]. View List of Occcupied Rooms");
                Console.WriteLine("[A]. View List of Availble Rooms");
                Console.WriteLine("[B]. Book an Available Room");
                Console.WriteLine("[C]. Checkout an Occupied Room");
                Console.WriteLine("[M]. Mark Room as Under Maintenance");
                Console.WriteLine("[E]. View Event Log");
                Console.WriteLine("[Q] - Quit");

                switch (Console.ReadLine())
                {
                    case "V":
                        {
                            ViewAllRoomsOutlook(hotelRooms);
                            Console.WriteLine("\nPress ENETER to continue...");
                            Console.ReadLine();
                            break;
                        }
                    case "O":
                        {
                            ViewRoomsStatus(hotelRooms, RoomStatus.Occupied);
                            break;
                        }
                    case "A":
                        {
                            ViewRoomsStatus(hotelRooms, RoomStatus.Available);
                            break;
                        }
                    case "B":
                        {
                            BookARoom(hotelRooms);
                            SaveRoomsToFile(hotelRooms, filePath);
                            break;
                        }
                    case "C":
                        {
                            CheckoutARoom(hotelRooms);
                            SaveRoomsToFile(hotelRooms, filePath);
                            break;
                        }
                    case "M":
                        {
                            MarkRoomUnderMaintenance(hotelRooms);
                            SaveRoomsToFile(hotelRooms, filePath);
                            break;
                        }
                    case "E":
                        {
                            EventLog.ShowEventLog();
                            break;
                        }
                    case "Q":
                        {
                            running = false;
                            Console.WriteLine("You have quitted successfully!");
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Invalid option!");
                            menu = Menu.Main;
                        }
                        break;
                }
            }
            break;

        default:
            {
                Console.WriteLine("You are currently logged in!");
                menu = Menu.Main;
            }
            break;
    }
}
static void ViewAllRoomsOutlook(Room[,] hotelRooms)
{
    Console.Clear();
    Console.WriteLine("===== Current Hotel Rooms Outlook =====");
    for (int floorNum = 0; floorNum < hotelRooms.GetLength(0); floorNum++)
    {
        Console.Write($"Floor {floorNum + 1}: ");
        for (int roomNum = 0; roomNum < hotelRooms.GetLength(1); roomNum++)
        {
            char mark = hotelRooms[floorNum, roomNum].Status switch
            {
                RoomStatus.Available => 'A',
                RoomStatus.Occupied => 'O',
                RoomStatus.UnderMaintenance => 'M',
            };
            Console.Write($"[{mark}] ");
        }
        Console.WriteLine();
    }
    Console.WriteLine("\nA = Available, O = Occupied, M = UnderMaintenance");
    Console.WriteLine("___________________________________________________");
}

static void ViewRoomsStatus(Room[,] hotelRooms, RoomStatus status)
{
    Console.Clear();
    Console.WriteLine($"===== List of {status} Rooms====");
    bool found = false;
    for (int floorNum = 0; floorNum < hotelRooms.GetLength(0); floorNum++)
    {
        for (int roomNum = 0; roomNum < hotelRooms.GetLength(1); roomNum++)
        {
            if (hotelRooms[floorNum, roomNum].Status == status)
            {
                Console.WriteLine($"Floor {hotelRooms[floorNum, roomNum].FloorNumber}, Room {hotelRooms[floorNum, roomNum].RoomNumber}");
                found = true;
            }
        }
    }
    if (!found)
    {
        Console.WriteLine($"No {status.ToString().ToLower()} rooms found.");
    }
    Console.WriteLine("\nPress ENETER to continue...");
    Console.ReadLine();
}

static void BookARoom(Room[,] hotelRooms)
{
    Console.WriteLine("===== Book a Room =====");
    ViewRoomsStatus(hotelRooms, RoomStatus.Available);

    Console.Write("\nEnter floor number (1 - 3): ");
    int.TryParse(Console.ReadLine(), out int floor);
    Console.Write("\nEnter room number (1 - 6): ");
    int.TryParse(Console.ReadLine(), out int room);

    Room selected_room = hotelRooms[floor - 1, room - 1];
    if (selected_room.Status == RoomStatus.Available)
    {
         Console.Write("\nEnter your first name: ");
        string? guest_name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(guest_name))
        {
            Console.WriteLine("Guest name needed. Booking denied.");
            return;
        }
        selected_room.Occupy(guest_name);
        EventLog.AddEvent(EventType.BookRoom, floor, room, guest_name);
        Console.WriteLine($"{guest_name}, Room {room} on floor {floor} booking is confirmed!");
    }
    else
    {
        Console.WriteLine("Selected room not available.");
    }
    Console.WriteLine("\nPress ENETER to continue...");
    Console.ReadLine();
}

static void CheckoutARoom(Room[,] hotelRooms)
{
    Console.Clear();
    Console.WriteLine("===== Checkout an Occupied Room =====");

    ViewRoomsStatus(hotelRooms, RoomStatus.Occupied);

    Console.Write("\nEnter floor number (1 - 3): ");
    int.TryParse(Console.ReadLine(), out int floor);
    Console.Write("\nEnter room number (1 - 6): ");
    int.TryParse(Console.ReadLine(), out int room);

    Room selected_room = hotelRooms[floor - 1, room - 1];
    if (selected_room.Status == RoomStatus.Occupied)
    {
        selected_room.Available();
        EventLog.AddEvent(EventType.CheckoutRoom, floor, room);
        Console.WriteLine($"Room {room} on floor {floor} checkout confirmed!");
    }
    else
    {
        Console.WriteLine("Selected room not occupied.");
    }
    Console.WriteLine("\nPress ENETER to continue...");
    Console.ReadLine();
}

static void MarkRoomUnderMaintenance(Room[,] hotelRooms)
{
    Console.Clear();
    Console.WriteLine("===== Mark Room as Under Maintenance =====");

    ViewAllRoomsOutlook(hotelRooms);

    Console.Write("\nEnter floor number (1 - 3): ");
    int.TryParse(Console.ReadLine(), out int floor);
    Console.Write("\nEnter room number (1 - 6): ");
    int.TryParse(Console.ReadLine(), out int room);

    Room userSelected = hotelRooms[floor - 1, room - 1];
    userSelected.RoomUnderMaintenance();
    EventLog.AddEvent(EventType.MarkRoomUnderMaintenance, floor, room);
    Console.WriteLine($"Room {room} on floor {floor} marked as under maintenance!");

    Console.WriteLine("\nPress ENETER to continue...");
    Console.ReadLine();
}

static void SaveRoomsToFile(Room[,] hotelRooms, string path)
{
    List<string> saved_rooms = new();
    for (int floorNum = 0; floorNum < hotelRooms.GetLength(0); floorNum++)
    {
        for (int roomNum = 0; roomNum < hotelRooms.GetLength(1); roomNum++)
        {
            saved_rooms.Add($"{hotelRooms[floorNum, roomNum].FloorNumber}, {hotelRooms[floorNum, roomNum].RoomNumber}, {hotelRooms[floorNum, roomNum].Status}, {hotelRooms[floorNum, roomNum].GuestName}");
        }
    }
    File.WriteAllLines("roomsInfo.save", saved_rooms);
}

static void LoadSavedRooms(Room[,] hotelRooms, string path)
{
    bool saved_rooms_found = File.Exists("roomsInfo.save");
    if (!saved_rooms_found)
    {
        return;
    }
    else
    {
        string[] saved_rooms = File.ReadAllLines("roomsInfo.save");
        foreach (string saved_room in saved_rooms)
        {
            string[] parts = saved_room.Split(',');
            int.TryParse(parts[0], out int floor);
            int.TryParse(parts[1], out int room);
            Enum.TryParse<RoomStatus>(parts[2], out RoomStatus status);
            hotelRooms[floor - 1, room - 1].Status = status;
            string guestName = parts[3];
            hotelRooms[floor - 1, room - 1].GuestName = guestName;
        }
    }
}
