namespace App;      // Organize Menu options in the same namespace.

enum Menu           // Defines states for program navigation. Used Enum because it improves code readability, and can easily be expanded incase more menus is needed.  
{
    None,           // No menu selected. Executed before login block of codes.
    Main,           // Main menu selected. Executed after login and contains central functions of the program.
}