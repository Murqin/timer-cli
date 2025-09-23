namespace timer;
class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("1- Timer Mode 2- Stopwatch Mode");
            int selection = Convert.ToInt16(Console.ReadLine());
            
            if (selection == 1)
            {
                Console.WriteLine("Welcome to Coundown Mode");
                
                Console.WriteLine("Please enter the hour 1+");
                int hour = Convert.ToInt16(Console.ReadLine());
                if (hour < 0)
                {
                    do
                    {
                        Console.WriteLine("Please enter the minute 1-60");
                        hour = Convert.ToInt16(Console.ReadLine());
                    } while (hour <= 0);
                }                
                
                Console.WriteLine("Please enter the minute 1-60");
                int minute = Convert.ToInt16(Console.ReadLine());

                if (minute > 60 | minute <= 0)
                {
                    do
                    {
                        Console.WriteLine("Please enter the minute 1-60");
                        minute = Convert.ToInt16(Console.ReadLine());
                    } while (minute > 60 |  minute <= 0);
                }
                
                Console.WriteLine("Please enter the second 1-60");
                int second = Convert.ToInt16(Console.ReadLine());
                
                if (second > 60 | second <=  0)
                {
                    do
                    {
                        Console.WriteLine("Please enter the minute 1-60");
                        second = Convert.ToInt16(Console.ReadLine());
                    } while (second > 60 | second <=  0);
                }



                TimeSpan totalCountdown = new TimeSpan(hour, minute, second);
                
                DateTime finishTime = DateTime.Now + totalCountdown;
                
                
                Console.WriteLine(totalCountdown);
                
                while (totalCountdown.TotalSeconds > 0)
                {
                    totalCountdown = finishTime - DateTime.Now;
                    Console.Write($"\r{totalCountdown:hh\\:mm\\:ss}");
                }
            }

            else if (selection == 2)
            {
                DateTime startTime;
                
                TimeSpan totalChrono = TimeSpan.Zero;
                
                startTime = DateTime.Now;

                while (true)
                {
                    totalChrono = DateTime.Now - startTime;
                    Console.Write($"\r{totalChrono:hh\\:mm\\:ss\\.fff} ");
                }   
            }

            else
            {
                Console.WriteLine("Please select an option");
            }
        }
        catch (Exception)
        {
            Console.WriteLine("Please select an option");
            throw;
        }
        
    }



}










