using SteamCleaner;
using System;
using System.Threading;

public class Program
{
    static void Main()
    {
        
        ResizeConsoleWindow();
        RandomizeConsoleTitle();

        string text = "Steam Cleaner";
        PrintStyledText(text);
        PrintMenuOptions();

        ConsoleKeyInfo selectedOption = Console.ReadKey(); // Read user input

        Console.WriteLine();

        switch (selectedOption.KeyChar)
        {
            case '1':
                Console.Clear();
               new Options().Option1();
                break;
            case '2':
                Console.Clear();
                new Options().Option2();
                break;
            case '3':
                Console.Clear();
                new Options().Option3();
                break;
            default:
                Console.WriteLine("Wrong Number, Idiot.");
                return;
        }

        Console.ReadKey(); // Pause at the end of the program
    }

    public void Returning()
    {
        string text = "Steam Cleaner";
        PrintStyledText(text);
        PrintMenuOptions();

        ConsoleKeyInfo selectedOption = Console.ReadKey(); // Read user input

        Console.WriteLine();

        switch (selectedOption.KeyChar)
        {
            case '1':
                Console.Clear();
                new Options().Option1();
                break;
            case '2':
                // Add your reference for option 2 here
                break;
            case '3':
                // Add your reference for option 3 here
                break;
            default:
                Console.WriteLine("Invalid selection. Please choose a valid option (1, 2, or 3).");
                break;
        }

        Console.ReadKey(); // Pause at the end of the program
    }

    static void PrintStyledText(string text)
    {
        Console.ForegroundColor = ConsoleColor.Red;

        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(50); // Delay between each character
        }

        Console.ResetColor();
        Console.WriteLine();
        Console.WriteLine(); // Two spaces below the styled text
    }

    static void PrintMenuOptions()
    {
        string[] menuOptions = { "[1] Steam Cache Removal", "[2] Steam Basic Clean", "[3] Steam Deep Clean (CS:GO Cheat)" };

        Console.ForegroundColor = ConsoleColor.DarkYellow;

        foreach (string option in menuOptions)
        {
            foreach (char c in option)
            {
                Console.Write(c);
                Thread.Sleep(16); // Delay between each character
            }

            Console.WriteLine();
        }

        Console.ResetColor();
        Console.WriteLine(); // Add a space after the menu options
        Console.Write(" > "); // Use Console.Write to keep the selection beside ">"
    }

    static void RandomizeConsoleTitle()
    {
        char[] titleLetters = "abcdefghjklmnopqrstuvwxy1234567890+!@#$%^&*()?~".ToCharArray();
        Random random = new Random();

        Timer timer = new Timer(UpdateConsoleTitle, titleLetters, TimeSpan.Zero, TimeSpan.FromSeconds(1));
    }

    static void UpdateConsoleTitle(object state)
    {
        char[] titleLetters = (char[])state;
        Random random = new Random();

        int titleLength = random.Next(25, 30);
        string randomizedTitle = "";

        for (int i = 0; i < titleLength; i++)
        {
            char randomChar = titleLetters[random.Next(titleLetters.Length)];
            randomizedTitle += randomChar;
        }

        Console.Title = randomizedTitle;
    }

    static void ResizeConsoleWindow()
    {
        int originalWidth = Console.WindowWidth;
        int originalHeight = Console.WindowHeight;

        int newWidth = originalWidth / 2; // 50% of the original width
        int newHeight = originalHeight / 2; // 50% of the original height

        Console.SetWindowSize(newWidth, newHeight);
    }
}
