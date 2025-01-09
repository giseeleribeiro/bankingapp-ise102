using System;


public class Bank
{
    private Dictionary < string, User > users; // store user credential
    private double totalBalance = 0; // initialise total balance for banking operation

    public Bank()
    {
        // test user data for loginn
        users = new Dictionary < string, User >
        {
            { "Joe.Doe", new User("joe.doe@ex ample.com", 20, "123456789", "Password123") }
        };
    }

    // method for user signup
    public void Signup()
    {
        Console.WriteLine("Welcome! Let's create your account.");

        // Get user input for signup
        Console.Write("Please choose a username: ");
        string username = Console.ReadLine().Trim(); //.trim to clean up input from a user before processing it

        Console.Write("Please enter your email: ");
        string email = Console.ReadLine().Trim();

        Console.Write("Please enter your age: ");
        string ageInput = Console.ReadLine().Trim();
        if (!int.TryParse(ageInput, out int age))
        {
            Console.WriteLine("Invalid age. Please enter a valid number.");
            return;
        }

        Console.Write("Enter your phone number: ");
        string phone = Console.ReadLine().Trim();

        Console.Write("Create a password: ");
        string password = Console.ReadLine().Trim();

        // check if any fields are empty
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("All fields must be filled out. Please try again.");
            return;
        }

        // add user to dictionary
        users[username] = new User(email, age, phone, password);
        Console.WriteLine("Account successfully created! You can now log in.");
    }

    // method for user login
    public void Login()
    {
        Console.WriteLine("Please log in.");

        // allow 3 attempts for login
        for (int attempts = 1; attempts <= 3; attempts++)
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine().Trim();

            Console.Write("Enter your password: ");
            string password = Console.ReadLine().Trim();

            // check if credentials match
            if (users.ContainsKey(username) && users[username].Password == password)
            {
                Console.WriteLine($"Welcome back, {username}!");
                MainMenu(); // show main menu on successful login
                return;
            }
            else
            {
                Console.WriteLine($"Incorrect username or password. Attempt {attempts}/3.");
            }
        }

        Console.WriteLine("Login failed after 3 attempts.");
    }

    // main menu after login
    private void MainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("1: View Balance");
            Console.WriteLine("2: Deposit");
            Console.WriteLine("3: Withdraw");
            Console.WriteLine("4: Logout");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine().Trim();
            switch (choice)
            {
                case "1":
                    ViewBalance();
                    break;
                case "2":
                    Deposit();
                    break;
                case "3":
                    Withdraw();
                    break;
                case "4":
                    Console.WriteLine("Logged out successfully.");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }

    // view balance method
    private void ViewBalance()
    {
        Console.WriteLine($"Your current balance is: ${totalBalance:F2}");
    }

    // deposit method
    private void Deposit()
    {
        Console.Write("Enter the amount to deposit: ");
        if (double.TryParse(Console.ReadLine().Trim(), out double depositAmount) && depositAmount > 0)
        {
            totalBalance += depositAmount;
            Console.WriteLine($"Successfully deposited ${depositAmount:F2}. Updated balance: ${totalBalance:F2}");
        }
        else
        {
            Console.WriteLine("Invalid deposit amount. Please enter a positive number.");
        }
    }

    // withdraw method
    private void Withdraw()
    {
        Console.Write("Enter the amount to withdraw: ");
        if (double.TryParse(Console.ReadLine().Trim(), out double withdrawAmount) && withdrawAmount > 0)
        {
            if (withdrawAmount <= totalBalance)
            {
                totalBalance -= withdrawAmount;
                Console.WriteLine($"Successfully withdrew ${withdrawAmount:F2}. Updated balance: ${totalBalance:F2}");
            }
            else
            {
                Console.WriteLine("Not sufficient funds available.");
            }
        }
        else
        {
            Console.WriteLine("Invalid withdrawal amount. Please enter a positive number.");
        }
    }
}

// user class for storing user informations
public class User
{
    public string Email { get; }
    public int Age { get; }
    public string Phone { get; }
    public string Password { get; }

    public User(string email, int age, string phone, string password)
    {
        Email = email;
        Age = age;
        Phone = phone;
        Password = password;
    }
}

// main program
public class Program
{
    public static void Main()
    {
        Bank bank = new Bank(); // create a Bank instance

        while (true)
        {
            Console.WriteLine("\n--- Welcome to the Bank ---");
            Console.WriteLine("1: Sign up");
            Console.WriteLine("2: Log in");
            Console.WriteLine("3: Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine().Trim();
            switch (choice)
            {
                case "1":
                    bank.Signup(); // call signup method
                    break;
                case "2":
                    bank.Login(); // call login method
                    break;
                case "3":
                    Console.WriteLine("Thank you for using the bank application. Goodbye!");
                    return;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
