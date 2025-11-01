// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using App;



List<User> users = new();                                   // Create new list of users
users.Add(new User("hotell", "pass"));                      // Add pre-made user login information to the user list.

string[] save_users_to_file = new string[users.Count];      // Initialize an array for user's login info saving.
for (int i = 0; i < users.Count; ++i)
{
    save_users_to_file[i] = users[i].ToSaveString();        // Converts each user to file format.
}
File.WriteAllLines("loginInfo.save", save_users_to_file);   // save the user login info. to a file (loginInfo.save)

// Hotel room initialization
string filePath = "roomsInfo.save";                         // File where hotel rooms identification info will be saved.
Room[,] hotelRooms = new Room[3, 8];                        // Initialize 2-D array for hotel arrangement.
                                     
for (int floorNum = 0; floorNum < 3; floorNum++)            // Use a for-loop to go through every floor.
{
    for (int roomNum = 0; roomNum < 8; roomNum++)           // same as above for room.
    {
        hotelRooms[floorNum, roomNum] = new Room(floorNum + 1, roomNum + 1);    // Creates a new room object in 2-D array. Correct that array values start at 0.  
    }
}

LoadSavedRooms(hotelRooms, filePath);       // Load previously saved hotel rooms info data.


User? active_user = null;                   // Check if user is currently not logged in.
Menu menu = Menu.None;                      // Create a variable menu and initially assigned it to an empty Enum Menu option.
bool running = true;                        // Set condition to manage main loop (while-loop) of the program.

while (running)                             // A while-loop that keeps running as long as the previous condition is met. 
{
    Console.Clear();                        // Clear all previous display on console.

    switch (menu)                           // use switch-statement to select different Enum Menu options.
    {
        case Menu.None:                     // executes login function.
            {
                Console.WriteLine("Welcome! please login to continue....");     // Display message within the quotes.
                Console.Write("Enter username: ");                              // Display input request from user.
                string? username = Console.ReadLine();                          // Take in user input and assign it to a variable.
                Console.Write("Enter password: ");
                string? password = Console.ReadLine();

                Console.Clear();
                Debug.Assert(username != null);                                 // A debugging protector that ensures that variable is not null.
                Debug.Assert(password != null);


                bool saved_file_found = File.Exists("loginInfo.save");          // Check if saved login info file exist in the directory.
                if (!saved_file_found)                                          // Program does not find the file.
                {
                    Console.WriteLine("Saved file does not exist.");            // Error message for file not found.
                }
                else
                {
                    string[] saved_userInfos = File.ReadAllLines("loginInfo.save");             // The file exist. Read each line of the saved file and store as value of a variable, saved_userInfos.                                         
                    string[] userInfos;                                                         // Declare an array of string.
                    foreach (string saved_userInfo in saved_userInfos)                          // Extract every line in "saved_userInfos" and store as "saved_userInfo".
                    {
                        userInfos = saved_userInfo.Split(",");                                  // Assign the extracted lines to "userInfos" and separate words with a comma.
                        if (userInfos[0] == username && userInfos[1] == password)               // Authenticate user input login info with saved info.
                        {
                            //Console.WriteLine($"Welcome! {userInfo[0]}, {userInfo[1]}");
                            active_user = new User(userInfos[0], userInfos[1]);                 // Create new User object with the login info.
                            menu = Menu.Main;                                                   // Continue with the main menu option.
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nUser does not exist or Invalid username or password");       // Error message for failed user login attempt.
                            Console.WriteLine("\nPress ENTER to continue.....");
                            Console.ReadLine();
                            menu = Menu.None;                                                               // Return back to execute login function again. 
                        }
                    }
                }
            }
            break;              // End of the case.

        case Menu.Main:        // Displays main menu containing all room actions a logged-in user can perform.
            {
                Console.Clear();
                Console.WriteLine("\n=====Welcome to the Rooms Tracking System=====");
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
                            // Lists all the floors and rooms in the hotel.
                            ViewAllRoomsOutlook(hotelRooms);
                            Console.WriteLine("\nPress ENTER to continue...");
                            Console.ReadLine();
                            break;
                        }
                    case "O":
                        {
                            // Lists all the rooms currently occupied by guests.
                            ViewRoomsStatus(hotelRooms, RoomStatus.Occupied);
                            break;
                        }
                    case "A":
                        {
                            // Lists all the rooms that are free.
                            ViewRoomsStatus(hotelRooms, RoomStatus.Available);
                            break;
                        }
                    case "B":
                        {
                            // Book a free room to a guest and save the activity information to a file.
                            BookARoom(hotelRooms);
                            SaveRoomsToFile(hotelRooms, filePath);
                            break;
                        }
                    case "C":
                        {
                            // Checkout a guest out of a room and save the activity info to a file.
                            CheckoutARoom(hotelRooms);
                            SaveRoomsToFile(hotelRooms, filePath);
                            break;
                        }
                    case "M":
                        {
                            // Indicates that a room is temporarily not available and save the activity info to a file.
                            MarkRoomUnderMaintenance(hotelRooms);
                            SaveRoomsToFile(hotelRooms, filePath);
                            break;
                        }
                    case "E":
                        {
                            // Displays event log of all the room actions. 
                            EventLog.ShowEventLog();
                            break;
                        }
                    case "Q":
                        {
                            // Exit the program when the while-loop condition is no longer met.
                            running = false;
                            Console.WriteLine("\nYou have quitted successfully!");
                        }
                        break;
                    default:
                        {
                            Console.WriteLine("Invalid option!");           // Error message displayed when user make wrong choice.
                            Console.WriteLine("\nPress ENTER to continue.....");
                            Console.ReadLine();
                            menu = Menu.Main;                               // Return back to execute main menu option again.
                        }
                        break;
                }
            }
            break;
    }
}
static void ViewAllRoomsOutlook(Room[,] hotelRooms)                             // Method that shows all floors and rooms in the hotel
{
    Console.Clear();
    Console.WriteLine("\n===== Current Hotel Rooms Outlook =====");
    for (int floorNum = 0; floorNum < hotelRooms.GetLength(0); floorNum++)      // loop through each floor and give number of rows (floors).
    {
        Console.Write($"\nFloor {floorNum + 1}: ");
        for (int roomNum = 0; roomNum < hotelRooms.GetLength(1); roomNum++)     // loops through each room on the current floor and gives number of columns (rooms)
        {
            char mark = hotelRooms[floorNum, roomNum].Status switch             // access a specific room object in the 2-D array, checks(switch) room current status, and gets its current status. 
            {
                // Defines character symbol for each room status.
                RoomStatus.Available => 'A',
                RoomStatus.Occupied => 'O',
                RoomStatus.UnderMaintenance => 'M',
            };
            Console.Write($"[{mark}]  ");                                        // Display each room status.
        }
        Console.WriteLine();
    }
    Console.WriteLine("\nA = Available, O = Occupied, M = UnderMaintenance");
    Console.WriteLine("___________________________________________________");
}

static void ViewRoomsStatus(Room[,] hotelRooms, RoomStatus status)              // Method that shows floors and rooms based on current status (available or occupied)
{
    Console.Clear();
    Console.WriteLine($"\n===== List of {status} Rooms====");                   // Display a header for current list status.
    bool found = false;
    for (int floorNum = 0; floorNum < hotelRooms.GetLength(0); floorNum++)
    {
        for (int roomNum = 0; roomNum < hotelRooms.GetLength(1); roomNum++)
        {
            if (hotelRooms[floorNum, roomNum].Status == status)                 // check if a room current status corresponds with list status. 
            {
                Console.WriteLine($"Floor {hotelRooms[floorNum, roomNum].FloorNumber}, Room {hotelRooms[floorNum, roomNum].RoomNumber}");       // Display all the rooms with the status.
                found = true; 
            }
        }
    }
    if (!found)                                                         // No room currently with status corresponding with list status found. 
    {
        Console.WriteLine($"No {status} rooms found.");                 // Display error message.
    }
    Console.WriteLine("\nPress ENTER to continue...");
    Console.ReadLine();
}

static void BookARoom(Room[,] hotelRooms)                               // Method for booking an available room.
{
    ViewRoomsStatus(hotelRooms, RoomStatus.Available);                  // Calls the method that shows available rooms.

    Console.WriteLine("\n===== Book a Room =====");
    Console.Write("\nEnter floor number (1 - 3): ");                    // Ask user to select a floor of their choice.
    int.TryParse(Console.ReadLine(), out int floor);                    // Converts string data type to integer.
    Console.Write("\nEnter room number (1 - 8): ");                     // Ask user to select a room of their choice.
    int.TryParse(Console.ReadLine(), out int room);

    Room selected_room = hotelRooms[floor - 1, room - 1];               // Creates a variable with appropriate floor and room indices in 2-D array.
    if (selected_room.Status == RoomStatus.Available)                   // Check if the selected room is free for booking.
    {
         Console.Write("\nEnter your first name: ");                    // Ask for name of the guest.
        string? guest_name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(guest_name))                      // Check if guest name is missing.
        {
            // Guest name is required to complete booking. 
            Console.WriteLine("Guest name needed. Booking denied.");
            return;
        }
        selected_room.Occupy(guest_name);                               // Call method to book selected room and update status to occupied
        EventLog.AddEvent(EventType.BookRoom, floor, room, guest_name); // Add room booking event to the event log.
        Console.WriteLine($"{guest_name}, Room {room} on floor {floor} booking is confirmed!"); // Display booking verification message.
    }
    else
    {
        Console.WriteLine("Selected room not available.");              // Error message if the selected room is NOT free for booking.
    }
    Console.WriteLine("\nPress ENETER to continue...");
    Console.ReadLine();
}

static void CheckoutARoom(Room[,] hotelRooms)                           // Method to checkout a room.
{
    // Console.Clear();
    ViewRoomsStatus(hotelRooms, RoomStatus.Occupied);                   // Calls the method that shows occupied rooms.

    Console.WriteLine("\n===== Checkout an Occupied Room =====");
    Console.Write("\nEnter floor number (1 - 3): ");                    // Ask user to select a floor of the room to checkout.
    int.TryParse(Console.ReadLine(), out int floor);
    Console.Write("\nEnter room number (1 - 8): ");
    int.TryParse(Console.ReadLine(), out int room);                     // Ask user to select a room to checkout.

    Room selected_room = hotelRooms[floor - 1, room - 1];
    if (selected_room.Status == RoomStatus.Occupied)                    // Check if the selected room is currently occupied.
    {
        selected_room.Available();                                      // Call method to update the room status to available
        EventLog.AddEvent(EventType.CheckoutRoom, floor, room);         // Add checkout event to the event log.
        Console.WriteLine($"Room {room} on floor {floor} checkout confirmed!");     // Display checkout verification message.
    }
    else
    {
        Console.WriteLine("Selected room not occupied.");               // Error message if the selected room is NOT occupied.
    }
    Console.WriteLine("\nPress ENETER to continue...");
    Console.ReadLine();
}

static void MarkRoomUnderMaintenance(Room[,] hotelRooms)                // Method to room as temporarily unavailable.
{
    //Console.Clear();
    ViewAllRoomsOutlook(hotelRooms);

    Console.WriteLine("\n===== Mark Room as Under Maintenance =====");
    Console.Write("\nEnter floor number (1 - 3): ");                    // Ask user select the floor the room is located.
    int.TryParse(Console.ReadLine(), out int floor);
    Console.Write("\nEnter room number (1 - 8): ");                     // Ask user to select the room to mark as under maintenance.
    int.TryParse(Console.ReadLine(), out int room);

    Room userSelected = hotelRooms[floor - 1, room - 1];
    userSelected.RoomUnderMaintenance();                                // Call method to update the room status to under maintenance
    EventLog.AddEvent(EventType.MarkRoomUnderMaintenance, floor, room);
    Console.WriteLine($"Room {room} on floor {floor} marked as under maintenance!");    // Displays message that room is currently marked.

    Console.WriteLine("\nPress ENETER to continue...");
    Console.ReadLine();
}

static void SaveRoomsToFile(Room[,] hotelRooms, string path)            // Method to save all room information into a file.
{
    List<string> saved_rooms = new();                                   // Instantiate an empty list that will store info to be saved.
    for (int floorNum = 0; floorNum < hotelRooms.GetLength(0); floorNum++)
    {
        for (int roomNum = 0; roomNum < hotelRooms.GetLength(1); roomNum++)
        {
            // Add selected floor and room to the list.  
            saved_rooms.Add($"{hotelRooms[floorNum, roomNum].FloorNumber}, {hotelRooms[floorNum, roomNum].RoomNumber}, {hotelRooms[floorNum, roomNum].Status}, {hotelRooms[floorNum, roomNum].GuestName}");
        }
    }
    File.WriteAllLines("roomsInfo.save", saved_rooms);                  // Write/save selected room in the file.
}

static void LoadSavedRooms(Room[,] hotelRooms, string path)             // Method to load previously saved file to the program.
{
    bool saved_rooms_found = File.Exists("roomsInfo.save");             // Check if the saved file exist in directory.
    if (!saved_rooms_found)                                             // saved file does not exist
    {
        return;                                                         // return true (saved file not found).
    }
    else
    {
        string[] saved_rooms = File.ReadAllLines("roomsInfo.save");     // Read each line of the saved file and store as value of a variable, saved_rooms.
        foreach (string saved_room in saved_rooms)                      // loop through or extract every line and save in a variable, saved_room.
        {
            string[] parts = saved_room.Split(',');                     // Separate extracted words with a comma.
            int.TryParse(parts[0], out int floor);                      // Convert from string to integer data type.
            int.TryParse(parts[1], out int room);                       
            Enum.TryParse<RoomStatus>(parts[2], out RoomStatus status); // Convert to Enum data type, place in index 2.
            hotelRooms[floor - 1, room - 1].Status = status;            // Gets current status of specific room object.
            string guestName = parts[3];                                // place guest name in index 3 position of the array.
            hotelRooms[floor - 1, room - 1].GuestName = guestName;      // Get guest name of the specific room object.
        }
    }
}
