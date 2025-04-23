# CybersecurityBot
Cybersecurity Awareness Bot
A simple C# console application to educate users about cybersecurity topics like phishing, password safety, and safe browsing. Built for Windows using Visual Studio Code, it features a voice greeting, ASCII art, and interactive responses to promote cybersecurity awareness in South Africa.
Features

Plays a voice greeting (greeting.wav) on startup.
Shows a cybersecurity-themed ASCII logo.
Asks for the user’s name and gives personalized responses.
Answers questions about password safety, phishing, and safe browsing.
Handles invalid inputs with clear messages.
Uses colored text and a typing effect for a better console experience.
Managed with GitHub and a CI workflow.

Requirements

Windows (for audio playback).
.NET 8.0 SDK: dotnet.microsoft.com.
Visual Studio Code with the C# extension.
Audacity (or similar) to record greeting.wav.
Git for version control.

Setup

Clone the Repository:
git clone https://github.com/<your-username>/CyberSecurityAwarenessbot.git
cd CyberSecurityAwarenessbot


Install Dependencies:
dotnet restore


Add Audio File:

Record “Hello! Welcome to the Cybersecurity Awareness Bot...” using Audacity.
Export as WAV (16-bit PCM) and save as greeting.wav in the project root.


Build and Run:
dotnet build
dotnet run



Usage

Run dotnet run to start the chatbot.

Enter your name when prompted.

Ask about “password safety,” “phishing,” “safe browsing,” or type “exit” to quit.


Notes

Audio playback uses System.Media.SoundPlayer, which works only on Windows.
The System.Windows.Extensions package fixes the CS1069 error for SoundPlayer.
CI runs on windows-latest in GitHub Actions.
At least three commits are included in the repository.

Submission

greeting.wav is included or can be created with Audacity.
CI workflow passes (check GitHub Actions for green checkmarks).
Tested on Windows with .NET 8.0 in Visual Studio Code.


