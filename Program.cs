using System;
using System.Collections.Generic;
using System.Media;
using System.Threading;
using System.IO;

class Program
{
    static Dictionary<string, string> responses = new Dictionary<string, string>
    {
        { "how are you", "I'm doing great, thanks for asking! How about you?" },
        { "what's your purpose", "I'm here to teach you about staying safe online, covering topics like phishing, passwords, and safe browsing!" },
        { "what can i ask you about", "You can ask about password safety, phishing scams, safe browsing, or even how I'm doing!" },
        { "password safety", "Use strong passwords with at least 12 characters, including letters, numbers, and symbols. Never reuse passwords across sites!" },
        { "phishing", "Phishing emails trick you into sharing personal info. Always check the sender’s email address and avoid clicking suspicious links." },
        { "safe browsing", "Stick to secure websites (HTTPS), avoid public Wi-Fi for sensitive tasks, and keep your browser updated." }
    };

    static void Main()
    {
        // Display ASCII logo
        DisplayAsciiLogo();

        // Play voice greeting
        PlayVoiceGreeting();

        // Get user's name
        string userName = GetUserName();

        // Main interaction loop
        while (true)
        {
            string input = GetUserInput();
            
            // Exit condition
            if (input.ToLower() == "exit")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"\nGoodbye, {userName}! Stay safe online!");
                Console.ResetColor();
                break;
            }

            // Get and display response
            string response = GetResponse(input, userName);
            DisplayResponseWithTypingEffect(response);
        }
    }

    static void PlayVoiceGreeting()
    {
        if (!OperatingSystem.IsWindows())
        {
            Console.WriteLine("Audio playback is only supported on Windows.");
            return;
        }

        string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
        try
        {
            SoundPlayer player = new SoundPlayer(audioPath);
            player.PlaySync();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine($"Error: 'greeting.wav' not found in {AppDomain.CurrentDomain.BaseDirectory}.");
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Error: Invalid WAV file format. Ensure 'greeting.wav' is a PCM-encoded WAV file.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error playing audio: {ex.Message}");
        }
    }

    static void DisplayAsciiLogo()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        string logo = @"
   =============================
          Cybersecurity Bot
   =============================
         _____
        /     \
       /_______\
       |  ***  | 
       |  ***  | 
       |_______|
   =============================
   Protecting you online!
   =============================
        ";
        Console.WriteLine(logo);
        Console.ResetColor();
    }

    static string GetUserName()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("=====================================");
        Console.WriteLine("  Welcome to the Cybersecurity Bot!  ");
        Console.WriteLine("=====================================");
        Console.ResetColor();

        Console.Write("Please enter your name: ");
        string? name = Console.ReadLine()?.Trim();

        while (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Name cannot be empty. Please enter your name: ");
            name = Console.ReadLine()?.Trim();
        }

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine($"\nNice to meet you, {name}! I'm here to help you stay safe online.");
        Console.ResetColor();
        return name;
    }

    static string GetUserInput()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.Write("\nYour question or command: ");
        Console.ResetColor();

        string? input = Console.ReadLine()?.Trim();
        while (string.IsNullOrWhiteSpace(input))
        {
            Console.WriteLine("Please enter a valid question or command.");
            Console.Write("Your question or command: ");
            input = Console.ReadLine()?.Trim();
        }

        return input;
    }

    static string GetResponse(string input, string userName)
    {
        input = input.ToLower().Trim();

        foreach (var key in responses.Keys)
        {
            if (input.Contains(key))
            {
                return $"{userName}, {responses[key]}";
            }
        }

        return $"{userName}, I didn’t quite understand that. Could you rephrase? Try asking about password safety, phishing, or safe browsing.";
    }

    static void DisplayResponseWithTypingEffect(string response)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n=====================================");
        Console.Write("Cybersecurity Bot: ");

        foreach (char c in response)
        {
            Console.Write(c);
            Thread.Sleep(30);
        }

        Console.WriteLine("\n=====================================");
        Console.ResetColor();
    }
}