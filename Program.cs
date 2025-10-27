// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
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

bool running = true;

while (running)
{
    Console.Clear();
    Console.WriteLine("Welcome! please login to continue....");
    Console.Write("Enter username: ");
    string? username = Console.ReadLine();
    Console.Write("Enter password: ");
    string? password = Console.ReadLine();

    Console.Clear();
    Debug.Assert(username != null);
    Debug.Assert(password != null);

    foreach (User user in users)
    {
        if (user.TryLogin(username, password))
        {
            active_user = user;
            break;
        }
    }
    Console.WriteLine("=====Welcome to the Rooms Tracking System=====");
    Console.ReadLine();


}