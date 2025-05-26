using System;
using System.Collections.Generic;
using NAudio.Wave; // Using NAudio as per .csproj
using System.Linq;
using System.Threading;

class CybersecurityChatbot
{
    private static Dictionary<string, string?> userMemory = new Dictionary<string, string?>(); // Nullable values
    private static string? currentTopic = null; // Nullable
    private static Random random = new Random();

    static void Main(string[] args)
    {
        // Display ASCII art logo
        DisplayAsciiArt();

        // Display welcome message with decorative border
        TypeText("=========================================", 20);
        TypeText(" Welcome to the Cybersecurity Awareness Bot ", 20);
        TypeText("=========================================", 20);

        // Play greeting.wav using NAudio
        try
        {
            using (var audioFile = new AudioFileReader("greeting.wav"))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }
        }
        catch (Exception)
        {
            Console.WriteLine("(Voice greeting unavailable - ensure greeting.wav is in the output directory)");
        }

        // Ask for the user's name and personalize responses
        TypeText("\nWhat’s your name?", 20);
        string? userName = Console.ReadLine(); // Nullable
        while (string.IsNullOrWhiteSpace(userName))
        {
            TypeText("I didn’t catch that. Could you please tell me your name?", 20);
            userName = Console.ReadLine();
        }
        userMemory["name"] = userName!; // Null forgiveness after validation

        Console.ForegroundColor = ConsoleColor.Green;
        TypeText($"\nNice to meet you, {userName}! I’m here to help you learn about staying safe online.", 20);
        Console.ResetColor();

        // Ask for favorite cyber topic
        TypeText("\nWhat’s your favorite cybersecurity topic? (e.g., password, phishing, privacy)", 20);
        string? favoriteTopic = Console.ReadLine()?.ToLower().Trim(); // Nullable
        if (!string.IsNullOrWhiteSpace(favoriteTopic))
        {
            userMemory["favoriteTopic"] = favoriteTopic;
            TypeText($"Great choice, {userName}! I’ll keep {favoriteTopic} in mind.", 20);
        }

        // Main chatbot loop
        bool running = true;
        while (running)
        {
            TypeText("\nWhat would you like to know about? (e.g., password, phishing, privacy, or type 'exit' to quit)", 20);
            string? userInput = Console.ReadLine()?.ToLower().Trim(); // Nullable

            // Input validation and error handling
            if (string.IsNullOrWhiteSpace(userInput))
            {
                TypeText("I didn’t quite understand that. Could you rephrase?", 20);
                continue;
            }

            if (userInput == "exit")
            {
                TypeText($"Goodbye, {userMemory["name"]}! Stay safe online!", 20);
                running = false;
                continue;
            }

            // Sentiment detection
            string? sentiment = DetectSentiment(userInput);
            if (!string.IsNullOrEmpty(sentiment))
            {
                TypeText($"I sense you might be feeling {sentiment}. Let me help you with that!", 20);
            }

            // Keyword recognition and conversation flow
            HandleConversation(userInput!); // Null forgiveness after validation
        }
    }

    static void DisplayAsciiArt()
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(@"   _____ _   _ ______  _____ ");
        Console.WriteLine(@"  / ____| \ | |  ____|/ ____|");
        Console.WriteLine(@" | |    |  \| | |__  | (___  ");
        Console.WriteLine(@" | |    | . ` |  __|  \___ \ ");
        Console.WriteLine(@" | |____| |\  | |____ ____) |");
        Console.WriteLine(@"  \_____|_| \_|______|_____/ ");
        Console.WriteLine("   Cybersecurity Awareness Bot   ");
        Console.ResetColor();
    }

    static void TypeText(string text, int delayMs)
    {
        foreach (char c in text)
        {
            Console.Write(c);
            Thread.Sleep(delayMs);
        }
        Console.WriteLine();
    }

    static string? DetectSentiment(string? input) // Nullable return type
    {
        if (input != null && (input.Contains("worried") || input.Contains("scared"))) return "worried";
        if (input != null && input.Contains("curious")) return "curious";
        if (input != null && input.Contains("frustrated")) return "frustrated";
        return null;
    }

    static void HandleConversation(string input)
    {
        // Basic responses
        if (input.Contains("how are you"))
        {
            TypeText("I’m doing great, thanks for asking! How can I assist you today?", 20);
        }
        else if (input.Contains("what’s your purpose") || input.Contains("what is your purpose"))
        {
            TypeText("I’m the Cybersecurity Awareness Bot, here to help you stay safe online!", 20);
        }
        else if (input.Contains("what can i ask you about"))
        {
            TypeText("You can ask about password safety, phishing, or privacy. What interests you?", 20);
        }
        // Keyword recognition
        else if (input.Contains("password"))
        {
            currentTopic = "password";
            string[] passwordTips = {
                "Use strong, unique passwords with a mix of characters.",
                "Avoid personal details like your name or birthday.",
                "Consider a password manager for secure storage."
            };
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeText($"\nPassword Safety Tip: {passwordTips[random.Next(passwordTips.Length)]}", 20);
            Console.ResetColor();
        }
        else if (input.Contains("phishing"))
        {
            currentTopic = "phishing";
            string[] phishingTips = {
                "Be cautious of emails asking for personal info.",
                "Check the sender’s email for suspicious domains.",
                "Avoid clicking links in unsolicited messages."
            };
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeText($"\nPhishing Tip: {phishingTips[random.Next(phishingTips.Length)]}", 20);
            Console.ResetColor();
        }
        else if (input.Contains("privacy"))
        {
            currentTopic = "privacy";
            string[] privacyTips = {
                "Adjust your social media privacy settings regularly.",
                "Be wary of sharing personal info online.",
                "Use two-factor authentication for added security."
            };
            Console.ForegroundColor = ConsoleColor.Cyan;
            TypeText($"\nPrivacy Tip: {privacyTips[random.Next(privacyTips.Length)]}", 20);
            Console.ResetColor();
        }
        // Follow-up or confusion handling
        else if (input.Contains("tell me more") && currentTopic != null)
        {
            if (currentTopic == "password")
            {
                TypeText("Additional Tip: Make passwords at least 12 characters long!", 20);
            }
            else if (currentTopic == "phishing")
            {
                TypeText("Additional Tip: Look for poor grammar as a phishing sign!", 20);
            }
            else if (currentTopic == "privacy")
            {
                TypeText("Additional Tip: Use a VPN on public Wi-Fi!", 20);
            }
        }
        else if (input.Contains("confused") || input.Contains("more details"))
        {
            if (currentTopic != null)
            {
                TypeText($"Let’s dive deeper into {currentTopic}. What specifically would you like to know?", 20);
            }
            else
            {
                TypeText("I’m not sure what topic we’re on. Please pick one like password, phishing, or privacy!", 20);
            }
        }
        // Memory and personalization
        else if (userMemory.ContainsKey("favoriteTopic") && input.Contains(userMemory["favoriteTopic"]!)) // Null forgiveness
        {
            TypeText($"Great choice, {userMemory["name"]}! Since you like {userMemory["favoriteTopic"]}, here’s a tip: Check the earlier advice or ask for more!", 20);
        }
        // Default response for unrecognized input
        else
        {
            TypeText("I’m not sure I understand. Can you rephrase or try a topic like password, phishing, or privacy?", 20);
        }
    }
}
