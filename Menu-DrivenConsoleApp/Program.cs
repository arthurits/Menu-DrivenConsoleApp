// See https://aka.ms/new-console-template for more information

using ChoicesModel;
using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace MenuApp;

public static partial class Program
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <remarks>
    /// This method serves as the starting point of the console application.
    /// <para />
    /// It continuously displays a menu of choices to the user and executes the
    /// corresponding actions based on their selection.
    /// <para />
    /// The menu is displayed until the user decides to exit the application.
    /// </remarks>
    /// <seealso href="https://www.codeproject.com/Articles/5382189/Building-a-Menu-Driven-Console-Application-in-Csha"/>
    public static void Main()
    {
        while (true)
        {
            DisplayMenu();
            MenuChoices choice = GetUserChoice();

            // Check if choice is within the valid range, since Enum.TryParse on GetUserChoice does return integers outside the enum's range values
            if (Enum.IsDefined(choice))
            {
                // Perform action based on user choice index
                switch (choice)
                {
                    case MenuChoices.EatCandy:
                        Choice_EatCandy();
                        break;

                    case MenuChoices.GoFishing:
                        Choice_GoFishing();
                        break;

                    case MenuChoices.PlayBasketball:
                        Choice_PlayBasketball();
                        break;

                    case MenuChoices.Exit:
                        bool loop = true;
                        while (loop == true)
                        {
                            Console.Write("Are you sure you want to exit the application? (Y/N): ");
                            var userInput = Console.ReadKey().Key;  //Alternate: var userInput = Char.ToUpper(Console.ReadKey().KeyChar);
                            if (userInput == ConsoleKey.Y)
                            {
                                Console.WriteLine();
                                Console.WriteLine();
                                Console.WriteLine("Exiting the application...");
                                return; // Exit the Main method
                            }
                            else if (userInput == ConsoleKey.N)
                            {
                                Console.Clear();
                                loop = false;
                            }
                            else
                            {
                                Console.WriteLine();
                            }
                        }
                        continue;
                    
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            { 
                Console.WriteLine("Invalid choice. Please try again.");
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear(); // Clear the console for the next iteration
        }
    }

    /// <summary>
    /// Displays the menu of choices on the console.
    /// </summary>
    /// <remarks>
    /// This method iterates through all the available menu choices and displays them along with their corresponding numbers.
    /// <para />
    /// The numbering of the choices starts from <c>1</c>.
    /// </remarks>
    private static void DisplayMenu()
    {
        Console.WriteLine("Please choose an action:");
        Console.WriteLine();
        
        // This is one (compact, less code) way to show the menu on screen
        // The loop starts at 1 since value 0 is the NoSelection/Undefined default enum option
        for (int i = 1;  i < Enum.GetValues<MenuChoices>().Length; i++)
            Console.WriteLine($"[{i}]:{(char)ConsoleKey.Tab}{GetEnumDescription(i)}");

        // This is a more verbose (more code) way to show the menu on screen
        //var menuItemNumber = 1;
        //foreach (MenuChoices choice in Enum.GetValues<MenuChoices>())
        //    if (choice is not MenuChoices.NoSelection)
        //    {
        //        var description = GetEnumDescription(choice);
        //        Console.WriteLine($"[{menuItemNumber}]:{(char)ConsoleKey.Tab}{description}");
        //        menuItemNumber++;
        //    }

        Console.WriteLine();
        Console.Write("Enter your selection: ");
    }

    /// <summary>
    /// Retrieves the description attribute value associated with the specified enum value.
    /// </summary>
    /// <param name="value">The <see langword="enum" /> string value for which to retrieve the description.</param>
    /// <returns>
    /// The description associated with the <see langword="enum" /> value, if available; otherwise, the
    /// string representation of the <see langword="enum" /> value.
    /// </returns>
    /// <remarks>
    /// This method retrieves the description attribute value, if present, associated
    /// with the specified <see langword="enum" /> value.
    /// <para />
    /// If no description attribute is found, it returns the string representation of the <see langword="enum" /> value.
    /// </remarks>
    private static string GetEnumDescription(string value)
    {
        var field = value.GetType().GetField(value);
        var attribute = field is null ? null : (DescriptionAttribute?)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));

        return attribute is null ? value.ToString() : attribute.Description;
    }

    /// <summary>
    /// Retrieves the description attribute value associated with the specified enum value.
    /// </summary>
    /// <param name="value">The <see langword="enum" /> value for which to retrieve the description.</param>
    /// <returns>
    /// The description associated with the <see langword="enum" /> value, if available; otherwise, the
    /// string representation of the <see langword="enum" /> value.
    /// </returns>
    /// <remarks>
    /// This method retrieves the description attribute value, if present, associated
    /// with the specified <see langword="enum" /> value.
    /// <para />
    /// If no description attribute is found, it returns the string representation of the <see langword="enum" /> value.
    /// </remarks>
    private static string GetEnumDescription(Enum? value)
    {
        return value is null ? string.Empty : GetEnumDescription(value.ToString());
    }

    /// <summary>
    /// Retrieves the description attribute value associated with the specified enum value.
    /// </summary>
    /// <param name="value">The <see langword="enum" /> numerical value for which to retrieve the description.</param>
    /// <returns>
    /// The description associated with the <see langword="enum" /> value, if available; otherwise, the
    /// string representation of the <see langword="enum" /> value.
    /// </returns>
    /// <remarks>
    /// This method retrieves the description attribute value, if present, associated
    /// with the specified <see langword="enum" /> value.
    /// <para />
    /// If no description attribute is found, it returns the string representation of the <see langword="enum" /> value.
    /// </remarks>
    private static string GetEnumDescription(int value)
    {
        return GetEnumDescription((Enum?)Enum.GetValues<MenuChoices>().GetValue(value));
    }

    /// <summary>
    /// Reads user input from the console and parses it into a <see cref="T:MenuDemo.MenuChoices" /> enumeration value.
    /// </summary>
    /// <returns>
    /// The <see cref="T:MenuDemo.MenuChoices" /> enumeration value corresponding to the user input.
    /// If the input cannot be parsed into a valid enumeration value, returns <see cref="F:MenuDemo.MenuChoices" /> zero value.
    /// </returns>
    /// <remarks>
    /// This method reads a line of text from the console input and attempts to parse
    /// it into a <see cref="T:MenuDemo.MenuChoices" /> enumeration value.
    /// <para />
    /// If the input matches any of the enumeration values, the corresponding enumeration value is returned.
    /// <para />
    /// If the input cannot be parsed into a valid enumeration value, the method returns <see cref="F:MenuDemo.MenuChoices" /> zero value.
    /// </remarks>
    private static MenuChoices GetUserChoice()
    {
        var input = Console.ReadLine();
        return Enum.TryParse(input, out MenuChoices choice) ? choice : default;
    }
}
