namespace App;                  // Organize related classes in the same namespace.

public class User               // Represents a user who can login to the system    
{
    public string Username;     // stores user's username
    public string _password;    // stores user's password

    public User(string username, string password)   // Constructor initializes a new User object 
    {
        Username = username;                        // Assign provided username to the class field
        _password = password;                       // Assign provided password to the class field
    }
   
    public string ToSaveString()                                // Converts user information into a string for a saving in a file.
    {
        return $"{Username},{_password}";                       // Save in a comma-separated format.
    }
}

