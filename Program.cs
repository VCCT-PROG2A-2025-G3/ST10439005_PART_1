using System;
using System.Collections.Generic;
using System.Media;
using System.Linq;

class CybersecurityChatbot
{
    private static Dictionary<string, string> userMemory = new Dictionary<string, string>();
    private static string currentTopic = null;
    private static Random random = new Random();

    static void Main(string[] args)
    {
        // Display ASCII art logo
        DisplayAsciiArt();

        // Display welcome message with decorative border
        Console.WriteLine("=========================================");
        Console.WriteLine(" Welcome to the Cybersecurity Awareness Bot ");
        Console.WriteLine("=========================================");

        // Placeholder for playing the voice greeting (WAV file)
        try
        {
            SoundPlayer player = new SoundPlayer("welcome.wav");
            player.PlaySync(); // Plays the WAV file (placeholder)
        }
        catch (Exception)
        {
            Console.WriteLine("(Voice greeting unavailable in this environment)");
        }

        // Ask for the user's name and personalize responses
        Console.WriteLine("\nWhat’s your name?");
        string userName = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(userName))
        {
            Console.WriteLine("I didn’t catch that. Could you please tell me your name?");
            userName = Console.ReadLine();
        }
        userMemory["name"] = userName;

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nNice to meet you, {userName}! I’m here to help you learn about staying safe online.");
        Console.ResetColor();

        // Ask for favorite cyber topic
        Console.WriteLine("\nWhat’s your favorite cybersecurity topic? (e.g., password, phishing, privacy)");
        string favoriteTopic = Console.ReadLine()?.ToLower().Trim();
        if (!string.IsNullOrWhiteSpace(favoriteTopic))
        {
            userMemory["favoriteTopic"] = favoriteTopic;
            Console.WriteLine($"Great choice, {userName}! I’ll keep {favoriteTopic} in mind.");
        }

        // Main chatbot loop
        bool running = true;
        while (running)
        {
            Console.WriteLine("\nWhat would you like to know about? (e.g., password, phishing, privacy, or type 'exit' to quit)");
            string userInput = Console.ReadLine()?.ToLower().Trim();

            // Input validation and error handling
            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("I didn’t quite understand that. Could you rephrase?");
                continue;
            }

            if (userInput == "exit")
            {
                Console.WriteLine($"Goodbye, {userMemory["name"]}! Stay safe online!");
                running = false;
                continue;
            }

            // Sentiment detection
            string sentiment = DetectSentiment(userInput);
            if (!string.IsNullOrEmpty(sentiment))
            {
                Console.WriteLine($"I sense you might be feeling {sentiment}. Let me help you with that!");
            }

            // Keyword recognition and conversation flow
            HandleConversation(userInput);
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

    static string DetectSentiment(string input)
    {
        if (input.Contains("worried") || input.Contains("scared")) return "worried";
        if (input.Contains("curious")) return "curious";
        if (input.Contains("frustrated")) return "frustrated";
        return null;
    }

    static void HandleConversation(string input)
    {
        // Basic responses
        if (input.Contains("how are you"))
        {
            Console.WriteLine("I’m doing great, thanks for asking! How can I assist you today?");
        }
        else if (input.Contains("what’s your purpose") || input.Contains("what is your purpose"))
        {
            Console.WriteLine("I’m the Cybersecurity Awareness Bot, here to help you stay safe online!");
        }
        else if (input.Contains("what can i ask you about"))
        {
            Console.WriteLine("You can ask about password safety, phishing, or privacy. What interests you?");
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
            Console.WriteLine($"\nPassword Safety Tip: {passwordTips[random.Next(passwordTips.Length)]}");
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
            Console.WriteLine($"\nPhishing Tip: {phishingTips[random.Next(phishingTips.Length)]}");
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
            Console.WriteLine($"\nPrivacy Tip: {privacyTips[random.Next(privacyTips.Length)]}");
            Console.ResetColor();
        }
        // Follow-up or confusion handling
        else if (input.Contains("tell me more") && currentTopic != null)
        {
            if (currentTopic == "password")
            {
                Console.WriteLine("Additional Tip: Make passwords at least 12 characters long!");
            }
            else if (currentTopic == "phishing")
            {
                Console.WriteLine("Additional Tip: Look for poor grammar as a phishing sign!");
            }
            else if (currentTopic == "privacy")
            {
                Console.WriteLine("Additional Tip: Use a VPN on public Wi-Fi!");
            }
        }
        else if (input.Contains("confused") || input.Contains("more details"))
        {
            if (currentTopic != null)
            {
                Console.WriteLine($"Let’s dive deeper into {currentTopic}. What specifically would you like to know?");
            }
            else
            {
                Console.WriteLine("I’m not sure what topic we’re on. Please pick one like password, phishing, or privacy!");
            }
        }
        // Memory and personalization
        else if (userMemory.ContainsKey("favoriteTopic") && input.Contains(userMemory["favoriteTopic"]))
        {
            Console.WriteLine($"Great choice, {userMemory["name"]}! Since you like {userMemory["favoriteTopic"]}, here’s a tip: Check the earlier advice or ask for more!");
        }
        // Default response for unrecognized input
        else
        {
            Console.WriteLine("I’m not sure I understand. Can you rephrase or try a topic like password, phishing, or privacy?");
        }
    }
}
