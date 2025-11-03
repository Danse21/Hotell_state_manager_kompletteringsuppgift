
# Hotel state manager system
### Introduction
This console application is developed to help hotel receptionist to keep track of all the guest rooms in a hotel. The hotel structure is comprised of 3 floors each with 8 rooms, making it a total of 24 rooms in the hotel. The program has only one user, the receptionist.

### Features
 - User can login with already existing credentials saved in a file,
 - User can view all the guest rooms in the hotel,
 - User manage the guest rooms (book, checkout, and mark temporarily unavailable),
 - User can view event records or log,
 - Persistent data storage using ```.txt``` and ```.save``` files.

### How to Use
1. Open your Terminal and run:
   ```bash
   dotnet run
   ```
2. To login, use the already existing user login credentials (see Program.cs line 9):
   - Enter username
   - Enter password
   - NB: All case-sensitive

3. After successful login, you will be presented with main menu options:
   ![alt text](<Screenshot 2025-11-03 at 04.27.55.png>)
    - Enter an alphabet that corresponds to what you want to do (case-sensitive)
    - Press ENTER key to continue.
    - For example; Enter ``` V ``` to view all the guest rooms in the hotel.
   ![alt text](<Screenshot 2025-11-03 at 04.39.53.png>)

4. To book a room for a guest
   - Enter ```B``` to go to booking function,
   - Enter floor number (1 - 3) where the room is located,
   - Enter the room number (1 - 8),
   - Enter the guest's name (either first or last name, not both),
   - A booking confirmation message shows that everything went successfully.

5. To checkout or mark temporarily unavailable a room
   - Enter ```C``` or ```M``` to go to checkout or under maintenance function, respectively,
   - Enter floor number where the room is located,
   - Enter the room number,
   - You will get a confirmation message that indicates task completion.

   ```
   Note that you can also view only the booked (occupied) or checked (available) rooms by entering 'O' or 'A', respectively.
   ```

6. To see when any action or activity (like booking, checkout or mark) happened
   - Enter ```E``` to view Event records,

7. Finally, to exit
   - Enter ```Q``` to close the program

### Implementation Choice
#### Data persistence
The application uses 3 files for data persistence:
- ```loginInfo.save``` - stores user login credentials
- ```roomsInfo.save``` - stores hotel room information
- ```EventLog.txt``` - stores event records

Reason: To have the data readily available whenever the program is restarted. Also, to stay within the file types we used during the course.

#### Object-oriented design
 - The system divided into different entities (receptionist, rooms, etc) which can be treated as a separate object.
 - OOP approach allows to organize each entity (```User```, ```Room```, ```EventLog```) as a separate class with its own elements and properties.
 - ```Enum``` gives safe transition to different parts of the program (```Menu```) and to keep track of room current status (```RoomStatus```).

 Reason: Because it make code well-structured, readable and easy to expand (ex., adding new menu) as well as maintain (Debug/ fix only a specific section without going through the entire code). OOP also allows for code or method reusability, a good example is: ```static void ViewRoomsStatus(Room[,] hotelRooms, RoomStatus status)```, that was reused many times in the program.

 #### Data Structure Choice
 Hotel structure (floors and rooms) was represented using a 2-D array ie., [floor, room];
 ```csharp
 Room[,] hotelRooms = new Room[3, 8];
 ```
The rooms data and user login credentials were stored in a text-based format using list of strings;
```csharp
List<string> saved_rooms = new(); 
```
```csharp
List<User> users = new();
```

Reason:
 - 2-D array: Because it makes it easy to access specific floor and room by indexing, which could be impossible with List<T> or Dictionary.
 - Room data and user credentials were stored as strings in a ```.save``` file  to make data persistence, simple and follow examples used in the course.


 #### File Structure

 - ```loginInfo.save``` - username, password
- ```roomsInfo.save``` - floor number, room number, status, guest name (occupied room only)
- ```EventLog.txt``` - Activity: Floor (number) Room (number): Guest:(name): Date and Time 

#### Future Improvement
- User password protection - currently stored in a plain text.
- Add a unique event ID.
- Add a comment about why a room is marked temporarily unavailable.