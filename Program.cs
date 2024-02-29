using System;

namespace ParkSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            ParkingSystem();
        }

        public static void ParkingSystem()
        {
            Console.WriteLine("===========================================");
            Console.WriteLine("       WELCOME TO MY PARKING SYSTEM        ");
            Console.WriteLine("===========================================");
            Console.WriteLine("\nEnter Plate no. \n(4-wheel vehicle :ABC-1234) ");
            Console.WriteLine("(Motorbike: 123-ABC/A-123-BC)");
            string plateNo = Console.ReadLine();
            while (!ValidatePlateNumber(plateNo))
            {
                Console.WriteLine("Invalid Plate Number format. Please enter a valid format.");
                Console.WriteLine("\nEnter Plate no. \n(4-wheel vehicle: ABC-1234) ");
                Console.WriteLine("(Motorbike: 123-ABC/A-123-BC)");
                plateNo = Console.ReadLine();
            }
            bool input1 = false;
            char vehicleType = '\0';  

            do
            {
                Console.WriteLine("Enter Vehicle Type (A. MotorBike, B. SUV, C. Van, D. Sedan): ");
                string input = Console.ReadLine().ToUpper();

                if (input.Length == 1) 
                {
                    vehicleType = input[0];

                    switch (vehicleType)
                    {
                        case 'A':
                        case 'B':
                        case 'C':
                        case 'D':
                            input1 = true;
                            break;
                        default:
                            Console.WriteLine("\nInvalid Vehicle Type. Please enter A, B, C, or D.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nInvalid input. Please enter a single character (A, B, C, or D).");
                }
            } while (!input1);

            Console.WriteLine("Enter Vehicle Brand: ");
            string vehicleBrand = Console.ReadLine();

            DateTime timeIn = DateTime.Now;

            DateTime timeOut = GetTimeOut(timeIn);

            TimeSpan timeDifference = RoundUpToHour(timeOut - timeIn);
            Console.WriteLine("Extended hours: " + timeDifference.TotalHours);

            double hourlyRate = HourlyRate(vehicleType);
            double baseFee = BaseFee(vehicleType);
            double parkingFee = CalculateParkingFee(timeDifference.TotalHours, hourlyRate, baseFee);
            int roundedParkingFee = (int)Math.Floor(parkingFee);

            Console.WriteLine("Parking Fee for " + vehicleBrand + " (" + plateNo + "): " + roundedParkingFee);


            DisplayInformation(timeIn, timeOut, parkingFee, vehicleBrand, plateNo);

            Console.ReadKey();
        }
        public static bool ValidatePlateNumber(string plateNo)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(plateNo, @"^[A-Z]{3}-\d{4}$|^\d{3}-[A-Z]{3}$|^\d{3}-[A-Z]{2}-\d{2}$");
        }


        public static DateTime GetTimeOut(DateTime timeIn) 
        {
            DateTime timeOut;
            bool input = false;

            do
            {
                Console.WriteLine("\n===========================================");
                Console.WriteLine("===========================================");
                Console.WriteLine("\nPark Time in: \n" + timeIn.ToString("F"));
                Console.WriteLine("Park Time out (dd/MM/yyyy hh:mm tt): "); 
                string timeOutInput = Console.ReadLine();

                if (DateTime.TryParseExact(timeOutInput, "dd/MM/yyyy hh:mm tt", null, System.Globalization.DateTimeStyles.None, out timeOut))
                {
                    if (timeOut > timeIn)
                        input = true;
                    else
                        Console.WriteLine("\nTime out must be after time in! Please enter valid input.\n");
                }
                else
                {
                    Console.WriteLine("\nPlease enter valid input!\n");
                }
            } while (!input);

            return timeOut;
        }


        public static double HourlyRate(char type)
        {
            switch (type)
            {
                case 'A':
                    return 5;
                case 'B':
                    return 20;
                case 'C':
                    return 20;
                case 'D':
                    return 15;
                default:
                    return 0;
            }
        }

        public static double BaseFee(char fee) 
        {
             switch (fee)
            {
                case 'A':
                    return 20;
                case 'B':
                    return 40;
                case 'C':
                    return 40;
                case 'D':
                    return 30;
                default:
                    return 0;
            }
        }
        

        public static double CalculateParkingFee(double hoursSpent, double hourlyRate, double baseFee)
        {
          
                return (hoursSpent * hourlyRate) + baseFee;

        }

        public static TimeSpan RoundUpToHour(TimeSpan timeSpan)
        {
            int roundedHours = (int)Math.Ceiling(timeSpan.TotalHours);
            return TimeSpan.FromHours(roundedHours);
        }

        public static void DisplayInformation(DateTime timeIn, DateTime timeOut, double parkingFee, string vehicleBrand, string plateNo)
        {
            Console.WriteLine("\n===========================================");
            Console.WriteLine("===========================================");
            Console.WriteLine("\nPark in time: " + timeIn.ToString("F"));
            Console.WriteLine("Park out time: " + timeOut.ToString("F"));
            Console.WriteLine("Amount: Php" + parkingFee);
            Console.WriteLine("Vehicle Brand: " + vehicleBrand);
            Console.WriteLine("Plate No.: " + plateNo);
            Console.WriteLine("\n===========================================");
            Console.WriteLine("       THANK YOU AND HAVE A SAFE TRIP!     ");
            Console.WriteLine("===========================================");
        }

    }
}
