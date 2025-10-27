// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using App;



List<User> users = new();  // List of users
users.Add(new User("receptionist355", "pass"));

// save the user to a file (loginInfo.save)
string[] save_users_to_file = new string[users.Count];
for (int i = 0; i < users.Count; ++i)
{
    save_users_to_file[i] = users[i].ToSaveString();
}
File.WriteAllLines("loginInfo.save", save_users_to_file);

User? active_user = null;
Menu menu = Menu.None;

bool running = true;

while (running)
{
    //Console.Clear();

    switch (menu)
    {
        case Menu.None:
            {
                Console.WriteLine("Welcome! please login to continue....");
                Console.Write("Enter username: ");
                string? username = Console.ReadLine();
                Console.Write("Enter password: ");
                string? password = Console.ReadLine();

                //Console.Clear();
                Debug.Assert(username != null);
                Debug.Assert(password != null);


                bool saved_file_found = File.Exists("loginInfo.save");
                if (!saved_file_found)
                {
                    Console.WriteLine("Saved file does not exist");
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
                            Console.WriteLine($"Welcome! {userInfo[0]}, {userInfo[1]}");
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
            }break;

        case Menu.Main:
            {
                //Console.Clear();
                Console.WriteLine("=====Welcome to the Rooms Tracking System=====");
                Console.WriteLine("\nChoose what you want to do: ");
                Console.WriteLine("1. View List of Occcupied Rooms");
                Console.WriteLine("2. View List of Availble Rooms");
                Console.WriteLine("3. Book an Available Room");
                Console.WriteLine("4. Checkout an Occupied Room");
                Console.WriteLine("5. Mark a Room as In-Service");
                Console.WriteLine("[Q] - Quit");

                switch (Console.ReadLine())
                {
                    case "1":
                        {

                        }
                        break;
                }
            }break;
    }        

}