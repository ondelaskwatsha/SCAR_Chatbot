// This imports the System namespace, which contains fundamental classes and base classes that define commonly-used types, events, and event handlers.
using System; 
// This imports the Speech Synthesis namespace, which provides classes for converting text to speech.
// It allows the program to use voice capabilities.
using System.Speech.Synthesis;
// This imports the Regular Expressions namespace, which provides classes for working with regular expressions.
// Regular expressions are used for pattern matching in strings, such as validating user input.
using System.Text.RegularExpressions;
// This imports the Threading namespace, which provides classes and methods for creating and managing threads.
// Threads allow the program to perform multiple tasks at the same time, such as running a voice greeting while displaying ASCII art.
using System.Threading; 

namespace SCAR_Chatbot // This defines a namespace called SCAR_Chatbot, which helps organize code and prevents naming conflicts.
// a name space is a way to organize code in a logical manner. It is used to group classes that are similar in nature.
{
    class Program // This defines a class named Program. A class is like a blueprint for creating objects.
    {
        static void Main(string[] args) // This is the main method where the program starts running.
        {
            // Create a new thread for playing a voice greeting.
            Thread voiceThread = new Thread(PlayVoiceGreeting); // This creates a thread that will run the PlayVoiceGreeting method.
            // Create a new thread for displaying ASCII art.
            Thread asciiThread = new Thread(DisplayAsciiArt); // This creates a thread that will run the DisplayAsciiArt method.

            // Start both threads to run at the same time.
            voiceThread.Start(); // This starts the voice greeting thread.
            asciiThread.Start(); // This starts the ASCII art display thread.

            // Wait for both threads to finish before moving on.
            voiceThread.Join(); // This makes the program wait until the voice greeting is done.
            asciiThread.Join(); // This makes the program wait until the ASCII art is displayed.

            // Ask the user for their name.
            string userName = Console.ReadLine(); // This reads the user's input from the console.

            // This variable will help us check if the name is valid.
            bool isValidName = false; // Initially, we assume the name is not valid.

            // Start an infinite loop to keep asking for a valid name.
            while (true) // This loop will run until we break out of it.
            {
                Console.ForegroundColor = ConsoleColor.Yellow; // Change the text color to yellow for the prompt.
                Console.Write("May I know your name? "); // Ask the user for their name.
                userName = Console.ReadLine(); // Read the user's input for their name.
                Console.ResetColor(); // Reset the text color back to default.

                // Check if the input is empty or just whitespace.
                if (string.IsNullOrWhiteSpace(userName)) // This checks if the name is empty.
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Change the text color to red for error messages.
                    Console.WriteLine("Oops! It looks like you didn't enter your name. Please try again."); // Show an error message.
                    Console.ResetColor(); // Reset the text color back to default.
                }
                // Check if the name contains only letters and spaces.
                else if (!Regex.IsMatch(userName, @"^[a-zA-Z\s]+$")) // This checks for valid characters in the name.
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Change the text color to red for error messages.
                    Console.WriteLine("Invalid input. Please enter a name without numbers or special symbols."); // Show an error message.
                    Console.ResetColor(); // Reset the text color back to default.
                }
                else
                {
                    // If the name is valid, exit the loop.
                    break; // This breaks out of the loop since we have a valid name.
                }
            }

            // Welcome the user with their name.
            Console.ForegroundColor = ConsoleColor.Green; // Change the text color to green for the welcome message.
            Console.WriteLine($"\nWelcome, {userName}! Type 'exit' to end the conversation at any time.\n"); // Display a welcome message.
            Console.ResetColor(); // Reset the text color back to default.

            // Start an infinite loop for user interaction.
            while (true) // This loop will keep running until the user decides to exit.
            {
                // Ask the user to ask a question.
                Console.ForegroundColor = ConsoleColor.Blue; // Change the text color to blue for the question prompt.
                Console.Write($"Ask SCAR a question about cybersecurity, {userName}: "); // Prompt the user for a question.
                string userInput = Console.ReadLine(); // Read the user's input for their question.
                Console.ResetColor(); // Reset the text color back to default.

                // Validate the user's input.
                if (string.IsNullOrWhiteSpace(userInput)) // Check if the input is empty.
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Change the text color to red for error messages.
                    Console.WriteLine("\nI didn't quite understand that. Could you please type something?"); // Show an error message.
                    Console.ResetColor(); // Reset the text color back to default.
                    continue; // Go back to the start of the loop to ask for input again.
                }

                // Check if the user wants to exit the conversation.
                if (userInput.Equals("exit", StringComparison.OrdinalIgnoreCase)) // Check if the input is "exit".
                {
                    Console.ForegroundColor = ConsoleColor.Red; // Change the text color to red for the exit message.
                    Console.WriteLine($"\nThank you for chatting with SCAR, {userName}. Stay vigilant and safe online!"); // Display a goodbye message.
                    Console.ResetColor(); // Reset the text color back to default.
                    break; // Exit the loop and end the conversation.
                }

                // Generate and display a response to the user's question.
                Console.ForegroundColor = ConsoleColor.White; // Change the text color to white for the response.
                Console.Write("\nSCAR is thinking"); // Indicate that the bot is processing the input.
                for (int i = 0; i < 3; i++) // Create a loading effect with dots.
                {
                    Console.Write("."); // Print a dot to show that SCAR is thinking.
                    Thread.Sleep(500); // Wait for half a second before printing the next dot.
                }
                Console.WriteLine(); // Move to the next line after the loading effect.
                string response = GenerateResponse(userInput); // Call the method to generate a response based on user input.
                Console.WriteLine($"\n{response}"); // Display the generated response to the user.
                Console.ResetColor(); // Reset the text color back to default.
            }
        }

        // Method to play a voice greeting.
        static void PlayVoiceGreeting() // This method handles the voice greeting.
        {
            try // Start a try block to catch any errors that might occur.
            {
                using (SpeechSynthesizer synth = new SpeechSynthesizer()) // Create a new SpeechSynthesizer instance to convert text to speech.
                {
                    synth.SetOutputToDefaultAudioDevice(); // Set the output to the default audio device (like speakers).
                    // Speak the greeting message.
                    synth.Speak("Hello! I am SCAR, your Cybersecurity Awareness Assistant. Let's make the internet a safer place together!");
                } // The using statement automatically disposes of the synthesizer when done.
            }
            catch (Exception e) // Catch any exceptions (errors) that occur during speech synthesis.
            {
                Console.ForegroundColor = ConsoleColor.DarkRed; // Change the text color to dark red for error messages.
                Console.WriteLine("Error with speech synthesis: " + e.Message); // Display the error message.
                Console.ResetColor(); // Reset the text color back to default.
            }
        }

        // Method to display ASCII art logo.
        static void DisplayAsciiArt() // This method displays ASCII art and a welcome message.
        {
            Thread.Sleep(300); // Wait for a short time to synchronize with the voice greeting.
            Console.ForegroundColor = ConsoleColor.Magenta; // Change the text color to magenta for the ASCII art.
            // Display the ASCII art.
            Console.WriteLine(@"
      _                  _
    | '-.            .-' |
    | -. '..\\,.//,.' .- |
    |   \  \\\||///  /   |
   /|    )M\/%%%%/\/(  . |\
  (/\  MM\/%/\||/%\\/MM  /\)
  (//M   \%\\\%%//%//   M\\)
(// M________ /\ ________M \\)
 (// M\ \(',)|  |(',)/ /M \\) \\\\  
  (\\ M\.  /,\\//,\  ./M //)
    / MMmm( \\||// )mmMM \  \\
     // MMM\\\||///MMM \\ \\
      \//''\)/||\(/''\\/ \\
      mrf\\( \oo/ )\\\/\
           \'-..-'\/\\
              \\/ \\ ;");
            Console.ResetColor(); // Reset the text color back to default after displaying ASCII art.

            // Personalized welcome message.
            Console.ForegroundColor = ConsoleColor.Cyan; // Change the text color to cyan for the welcome message.
            Console.WriteLine("Hello! I am SCAR, your Cybersecurity Awareness Assistant. Let's make the internet a safer place together!"); // Display the welcome message.
            Console.ResetColor(); // Reset the text color back to default.
        }

        // Method to generate a response based on user input.
        static string GenerateResponse(string input) // This method generates a response based on what the user asks.
        {
            // Check if the input contains specific keywords and respond accordingly.

            // Check if the user mentioned "phishing".
            if (input.Contains("phishing", StringComparison.OrdinalIgnoreCase))
            {
                return "Phishing is a type of cyber attack where attackers try to trick you into giving them sensitive information, like passwords or credit card numbers. They often do this by sending fake emails or creating fake websites that look real. Always be cautious and verify the source before clicking on links or providing personal information."; // Response for phishing.
            }
            // Check if the user mentioned "password".
            else if (input.Contains("password", StringComparison.OrdinalIgnoreCase))
            {
                return "It's important to use strong passwords to protect your accounts. A strong password should be at least 12 characters long and include a mix of uppercase letters, lowercase letters, numbers, and special symbols. Avoid using easily guessable information like your name or birthdate. Consider using a password manager to help you keep track of your passwords securely."; // Response for password safety.
            }
            // Check if the user mentioned "suspicious links".
            else if (input.Contains("suspicious links", StringComparison.OrdinalIgnoreCase))
            {
                return "If you come across a link that looks suspicious, it's best to be cautious. Before clicking, hover your mouse over the link to see the actual URL. If it looks strange or doesn't match the website you expect, don't click it. It's better to type the website address directly into your browser instead."; // Response for suspicious links.
            }
            // Check if the user asked "How".
            else if (input.Contains("How", StringComparison.OrdinalIgnoreCase))
            {
                return "It seems you're asking a question. Could you please provide more details about what you're curious about? This will help me give you a better answer."; // General response for questions.
            }
            // Check if the user asked "how are you".
            else if (input.Contains("how are you", StringComparison.OrdinalIgnoreCase))
            {
                return "I'm just a program, but I'm here to help you! Let's focus on keeping you safe online. What would you like to know about cybersecurity?"; // Response for checking on the bot's status.
            }
            // Check if the user asked about the bot's purpose.
            else if (input.Contains("purpose", StringComparison.OrdinalIgnoreCase))
            {
                return "My purpose is to help you understand cybersecurity better. I provide information and tips to help you stay safe online and protect your personal information from cyber threats."; // Response for purpose.
            }
            // Check if the user asked what they can ask.
            else if (input.Contains("what can I ask", StringComparison.OrdinalIgnoreCase))
            {
                return "You can ask me about various topics related to cybersecurity, such as phishing, password safety, suspicious links, malware, data privacy, and more. I'm here to help you with any questions you have!"; // Response for guiding user questions.
            }
            // Check if the user mentioned "malware".
            else if (input.Contains("malware", StringComparison.OrdinalIgnoreCase))
            {
                return "Malware is short for malicious software. It's designed to harm your computer or steal your information. Common types of malware include viruses, worms, and ransomware. To protect yourself, always keep your antivirus software updated and avoid downloading files from untrusted sources."; // Response for malware.
            }
            // Check if the user asked about "data privacy".
            else if (input.Contains("data privacy", StringComparison.OrdinalIgnoreCase))
            {
                return "Data privacy is about how your personal information is collected, used, and shared. It's important to understand what data you are sharing and with whom. Always read privacy policies before providing your information to websites or apps, and be cautious about what you share online."; // Response for data privacy.
            }
            // Check if the user asked about "two-factor authentication".
            else if (input.Contains("two-factor authentication", StringComparison.OrdinalIgnoreCase))
            {
                return "Two-factor authentication (2FA) is an extra layer of security for your accounts. It requires not only your password but also a second piece of information, like a code sent to your phone. This makes it much harder for someone to access your account, even if they have your password."; // Response for two-factor authentication.
            }
            // Check if the user asked about "cybersecurity tips".
            else if (input.Contains("cybersecurity tips", StringComparison.OrdinalIgnoreCase))
            {
                return "Here are some important cybersecurity tips: 1) Use strong, unique passwords for each of your accounts. 2) Be cautious with emails and messages from unknown senders. 3) Keep your software and devices updated to protect against vulnerabilities. 4) Use two-factor authentication whenever possible. 5) Regularly back up your important data."; // Response for cybersecurity tips.
            }
            // Default response for unrecognized input.
            else
            {
                return "SCAR is still learning! If you could rephrase your question or ask about specific topics like phishing, passwords, or suspicious links, I would be happy to help."; // Default response for unrecognized input.
            }
        }
    }
}
