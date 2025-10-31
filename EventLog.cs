namespace App;

enum EventType          // Defines all possible events that can take place in the program.
{
    BookRoom,           // A room booking event.
    CheckoutRoom,       // A room checkout event.
    MarkRoomUnderMaintenance        // A maintenance mark event.
}
class EventLog      // Represents an EventLog class that handles creation and storage of all event logs.
{
    public EventType Action;        // Keeps track of what kind of event that takes place.

    public EventLog(EventType action)       // Constructor for initializing a new event log.
    {
        Action = action;        // Assign provided event to the EventLog class field.
    }

    public static string eventLogFilePath = "EventLog.txt";     // File where logs are stored. Save in eventLogFilePath variable.

    public static void AddEvent(EventType action, int floorNum, int roomNum, string guestName)
    {
        // Builds a readable text entry for each event with a timestamp with guest name included
        string logEvent = $"{action}: Floor {floorNum} Room {roomNum}: Guest: {guestName}: {DateTime.Now}";
        File.AppendAllText(eventLogFilePath, logEvent + "\n"); // Add all event details to the log file. 
    }
    
    public static void AddEvent(EventType action, int floorNum, int roomNum)
    {
        // Builds a readable text entry for each event with a timestamp without guest name included.
        string logEvent = $"{action}: Floor {floorNum} Room {roomNum}: {DateTime.Now}";
        File.AppendAllText(eventLogFilePath, logEvent + "\n");
    }
    public static void ShowEventLog()       // Method that reads and shows event log file content.
    {
        Console.Clear();       // Clear previously displayed information.
        Console.WriteLine("\n===== ALL REGISTERED EVENTS =====");       // Display the message inside quotation mark.

        if (File.Exists(eventLogFilePath))      // Check/ ensure that the event log file exist before reading it.
        {
            string[] lines = File.ReadAllLines(eventLogFilePath);       // Raed/ load all event log lines
            foreach (string line in lines)
            {
                Console.WriteLine(line);        // Display each event in the log.
            }
        }
        else
        {
            Console.WriteLine("Event log file not found.");     // Error message to be displayed if the event log file does not exist. 
        }
        Console.WriteLine("\nPress ENTER to continue....");     // Pause before continuing.
        Console.ReadLine();
    }
}