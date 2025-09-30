using System.Diagnostics;
using System.Runtime.InteropServices;
namespace timer;
class Program
{


    public static class NotificationSystem
    {
        public static Task Notifier(string title, string message) 
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                PushWindows(message, title);
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                PushLinux(title, message);
            }

            return Task.CompletedTask;

            /*
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                PushOsx(title, message);
            }
            */
        }
        
    }
    
    private static void PushWindows(string message, string title)
    {
        string psCommand = $"""
        [Windows.UI.Notifications.ToastNotificationManager, Windows.UI.Notifications, ContentType = WindowsRuntime] | Out-Null;
        $template = [Windows.UI.Notifications.ToastNotificationManager]::GetTemplateContent([Windows.UI.Notifications.ToastTemplateType]::ToastText02);
        $xml = $template.GetXml();
        $xml.GetElementsByTagName('text')[0].AppendChild($xml.CreateTextNode('{message}')) | Out-Null;
        $xml.GetElementsByTagName('text')[1].AppendChild($xml.CreateTextNode('{title}')) | Out-Null;
        $toast = [Windows.UI.Notifications.ToastNotification]::new($xml);
        [Windows.UI.Notifications.ToastNotificationManager]::CreateToastNotifier('Default').Show($toast);
        """;

        var powerShell = new ProcessStartInfo
        {
            FileName = "powershell.exe",
            Arguments = $"-ExecutionPolicy Bypass -Command \"{psCommand.Replace("\"", "'")}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true,
    
        };

        try
        {
            Process.Start(powerShell);
        }
        catch (Exception ex)
        {
            Console.WriteLine("I cant send notification with powershell: " + ex.Message);
        }
    }

    private static void PushLinux(string title, string message)
    {
        var notifySend = new ProcessStartInfo { FileName = "notify-send", Arguments = $"\"{title}\" \"{message}\"", UseShellExecute = false };
        try
        {
            Process.Start(notifySend); 
        }catch(Exception ex)
        {
            Console.WriteLine("I cant send notification with notify-send: " + ex.Message);
        }
        
    }

    //I can add macOS support if I get a chance to test the code on a Mac. I wanted to have the method prepared in advance.
/*
    private static void PushOsx(string title, string message)
    {
    
    }
*/
    static void Main()
    {
        try
        {
            Console.WriteLine("1- Timer Mode 2- Stopwatch Mode");
            int selection = Convert.ToInt16(Console.ReadLine());
            
            if (selection == 1)
            {
                Console.WriteLine("Welcome to Coundown Mode");
                
                Console.WriteLine("Please enter the hour 0 or 1+");
                int hour = Convert.ToInt16(Console.ReadLine());
                if (hour < 0)
                {
                    do
                    {
                        Console.WriteLine("Please enter the hour 0 or 1+");
                        hour = Convert.ToInt16(Console.ReadLine());
                    } while (hour < 0);
                }                
                
                Console.WriteLine("Please enter the minute 0-60");
                int minute = Convert.ToInt16(Console.ReadLine());

                if (minute > 60 | minute < 0)
                {
                    do
                    {
                        Console.WriteLine("Please enter the minute 0-60");
                        minute = Convert.ToInt16(Console.ReadLine());
                    } while (minute > 60 |  minute < 0);
                }
                
                Console.WriteLine("Please enter the second 1-60");
                int second = Convert.ToInt16(Console.ReadLine());
                
                if (second > 60 | second <  0)
                {
                    do
                    {
                        Console.WriteLine("Please enter the second 1-60");
                        second = Convert.ToInt16(Console.ReadLine());
                    } while (second > 60 | second <  0);
                }


                TimeSpan countdown = new TimeSpan(hour, minute, second);
                TimeSpan totalCountdown = new TimeSpan(hour, minute, second);
                DateTime finishTime = DateTime.Now + totalCountdown;
                
                
                
                while (totalCountdown.TotalSeconds > 0)
                {
                    totalCountdown = finishTime - DateTime.Now;
                    Console.Write($"\r{totalCountdown:hh\\:mm\\:ss}");
                    Thread.Sleep(1000);
                }
                
                NotificationSystem.Notifier("timer-cli", $"Timer completed {countdown:hh\\:mm\\:ss}");
                Console.ReadKey();
            }

            else if (selection == 2)
            {
                DateTime startTime = DateTime.Now;
                
                TimeSpan totalChrono;
                

                while (true)
                {
                    totalChrono = DateTime.Now - startTime;
                    Console.Write($"\r{totalChrono:hh\\:mm\\:ss\\.fff}");
                    Thread.Sleep(16);
                }
            } 

            Console.ReadKey();
        }
        catch (Exception)
        {
            Console.WriteLine("Please select an option");
            throw;
        }
        
    }
}










