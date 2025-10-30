namespace App;

enum EventType
{
    BookRoom,
    CheckoutRoom,
    MarkRoomUnderMaintenance
}
class EventLog
{
    public EventType Action;

    public EventLog(EventType action)
    {
        Action = action;
    }

    public static string eventLogFilePath = "EventLog.txt";
    
    public static void AddEvent(EventType action, int floorNum, int roomNum)
    {
        string logEvent = $"{action}: Floor {floorNum} Room {roomNum}: {DateTime.Now}";
        File.AppendAllText(eventLogFilePath, logEvent + "\n");
    }
    public static void ShowEventLog()
    {
        Console.Clear();
        Console.WriteLine("\n===== ALL REGISTERED EVENTS =====");

        if (File.Exists(eventLogFilePath))
        {
            string[] lines = File.ReadAllLines(eventLogFilePath);
            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }
        else
        {
            Console.WriteLine("Event log file not found.");
        }
        Console.WriteLine("\nPress ENTER to continue....");
        Console.ReadLine();
    }
}