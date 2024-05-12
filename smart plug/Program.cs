using System;
using System.Threading;

namespace SmartPlugSystem
{
    class SmartPlug
    {
        public bool IsOn { get; private set; }
        public bool IsScheduled { get; private set; }
        public DateTime ScheduledTime { get; private set; }

        private double powerConsumption;

        public SmartPlug()
        {
            IsOn = false;
            IsScheduled = false;
            powerConsumption = 0.0;
        }

        public void TurnOn()
        {
            IsOn = true;
            Console.WriteLine("Smart plug turned on.");
        }

        public void TurnOff()
        {
            IsOn = false;
            Console.WriteLine("Smart plug turned off.");
        }

        public void Schedule(DateTime time)
        {
            ScheduledTime = time;
            IsScheduled = true;
            Console.WriteLine($"Smart plug scheduled to turn {(IsOn ? "off" : "on")} at {ScheduledTime}");
        }

        public void MonitorPowerConsumption()
        {
            while (true)
            {
                if (IsOn)
                {
                    
                    double currentPower = new Random().NextDouble() * 10; 
                    powerConsumption += currentPower;
                    Console.WriteLine($"Current power consumption: {currentPower} watts");
                }
                Thread.Sleep(1000); 
            }
        }

        public double GetTotalPowerConsumption()
        {
            return powerConsumption;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            SmartPlug smartPlug = new SmartPlug();

            
            Thread monitorThread = new Thread(smartPlug.MonitorPowerConsumption);
            monitorThread.Start();

            Console.WriteLine("Welcome to Smart Plug System!");

            while (true)
            {
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Turn On Smart Plug");
                Console.WriteLine("2. Turn Off Smart Plug");
                Console.WriteLine("3. Schedule Smart Plug");
                Console.WriteLine("4. View Total Power Consumption");
                Console.WriteLine("5. Exit");

                Console.Write("Enter your choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        smartPlug.TurnOn();
                        break;
                    case 2:
                        smartPlug.TurnOff();
                        break;
                    case 3:
                        Console.Write("Enter scheduled time (HH:mm:ss): ");
                        DateTime scheduledTime = DateTime.Parse(Console.ReadLine());
                        smartPlug.Schedule(scheduledTime);
                        break;
                    case 4:
                        Console.WriteLine($"Total power consumption: {smartPlug.GetTotalPowerConsumption()} watts");
                        break;
                    case 5:
                        Console.WriteLine("Exiting...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
    }
}
