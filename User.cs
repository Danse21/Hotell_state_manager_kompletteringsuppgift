namespace App;

public class User
{
    public string Username;
    public string _password;

    public User(string username, string password)
    {
        Username = username;
        _password = password;
    }
    public bool TryLogin(string username, string password)
    {
        return username == Username && password == _password;
    }
    public string ToSaveString()
    {
        return $"{Username},{_password}";
    }
}

