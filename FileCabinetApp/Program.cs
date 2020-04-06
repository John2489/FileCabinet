using System;
using System.Globalization;

namespace FileCabinetApp
{
    public static class Program
    {
        private const string DeveloperName = "Ivan Babitski";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static bool isRunning = true;
        private static FileCabinetService fileCabinetService = new FileCabinetService();
        private static CultureInfo regionalSetting = CultureInfo.CreateSpecificCulture("en-US");

        private static Tuple<string, Action<string>>[] commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("exit", Exit),
            new Tuple<string, Action<string>>("help", PrintHelp),
            new Tuple<string, Action<string>>("list", List),
            new Tuple<string, Action<string>>("stat", Stat),
            new Tuple<string, Action<string>>($"edit", Edit),
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "create", "create new record", "The 'create' command create new record." },
            new string[] { "edit", "edit record by id", "The 'edit' command create new record. Parametr is id of edit records." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "list", "prints the all records", "The 'list' command prints all records." },
            new string[] { "stat", "prints statistic about records", "The 'stat' command prints quantity of records." },
        };

        public static void Main(string[] args)
        {
            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine(Program.HintMessage);
            Console.WriteLine();

            do
            {
                Console.Write("> ");
                var inputs = Console.ReadLine().Split(' ', 2);
                const int commandIndex = 0;
                var command = inputs[commandIndex];

                if (string.IsNullOrEmpty(command))
                {
                    Console.WriteLine(Program.HintMessage);
                    continue;
                }

                var index = Array.FindIndex(commands, 0, commands.Length, i => i.Item1.Equals(command, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    const int parametersIndex = 1;
                    var parameters = inputs.Length > 1 ? inputs[parametersIndex] : string.Empty;
                    commands[index].Item2(parameters);
                }
                else
                {
                    PrintMissedCommandInfo(command);
                }
            }
            while (isRunning);
        }

        private static void PrintMissedCommandInfo(string command)
        {
            Console.WriteLine($"There is no '{command}' command.");
            Console.WriteLine();
        }

        private static void PrintHelp(string parameters)
        {
            if (!string.IsNullOrEmpty(parameters))
            {
                var index = Array.FindIndex(helpMessages, 0, helpMessages.Length, i => string.Equals(i[Program.CommandHelpIndex], parameters, StringComparison.InvariantCultureIgnoreCase));
                if (index >= 0)
                {
                    Console.WriteLine(helpMessages[index][Program.ExplanationHelpIndex]);
                }
                else
                {
                    Console.WriteLine($"There is no explanation for '{parameters}' command.");
                }
            }
            else
            {
                Console.WriteLine("Available commands:");

                foreach (var helpMessage in helpMessages)
                {
                    Console.WriteLine("\t{0}\t- {1}", helpMessage[Program.CommandHelpIndex], helpMessage[Program.DescriptionHelpIndex]);
                }
            }

            Console.WriteLine();
        }

        private static void List(string parameters)
        {
            FileCabinetRecord[] files = fileCabinetService.GetRecords();
            for (int i = 0; i < files.Length; i++)
            {
                Console.WriteLine($"#{files[i].Id}, {files[i].FirstName}, " +
                                  $"{files[i].LastName}," +
                                  $" {files[i].DateOfBirth.ToString("yyyy-MMM-dd", regionalSetting)}," +
                                  $" {files[i].SuccsesfullDeals}," +
                                  $" {files[i].AdditionCoefficient}," +
                                  $" {files[i].ManegerClass.ToString(regionalSetting).ToUpper(regionalSetting)}");
            }
        }

        private static void Stat(string parameters)
        {
            var recordsCount = Program.fileCabinetService.GetStat();
            Console.WriteLine($"{recordsCount} record(s).");
        }

        private static object[] UserQuestioning()
        {
            string firstName;
            string lastName;
            DateTime dateTime;
            short succsesfullDeals;
            decimal additionCoefficient;
            char manegerClass;

            DateTime minDate = new DateTime(1950, 1, 1);

            while (true)
            {
                Console.Write("First name: ");
                firstName = Console.ReadLine();
                if (firstName.Length < 2 || firstName.Length > 60 || firstName == null || firstName.Contains(' ', StringComparison.CurrentCulture))
                {
                    Console.WriteLine("Invalid First name");
                    continue;
                }
                else
                {
                    break;
                }
            }

            while (true)
            {
                Console.Write("Last name: ");
                lastName = Console.ReadLine();
                if (lastName.Length < 2 || lastName.Length > 60 || lastName == null || lastName.Contains(' ', StringComparison.CurrentCulture))
                {
                    Console.WriteLine("Invalid Last name");
                    continue;
                }
                else
                {
                    break;
                }
            }

            string date;
            string[] dayMonthHear;
            int[] dateSepareted = new int[3];
            while (true)
            {
                Console.Write("Date of birth(mm/dd/hhhh): ");
                date = Console.ReadLine();
                dayMonthHear = date.Split('/');
                if (dayMonthHear.Length != 3)
                {
                    Console.WriteLine("Invalid Date");
                    continue;
                }

                if (int.TryParse(dayMonthHear[0], out dateSepareted[0]) &&
                    int.TryParse(dayMonthHear[1], out dateSepareted[1]) &&
                    int.TryParse(dayMonthHear[2], out dateSepareted[2]))
                {
                    try
                    {
                        dateTime = new DateTime(dateSepareted[2], dateSepareted[0], dateSepareted[1]);
                        if (dateTime < minDate || dateTime >= DateTime.Now)
                        {
                            Console.WriteLine("Invalid Date");
                            continue;
                        }

                        break;
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        Console.WriteLine("Invalid Date");
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Date");
                    continue;
                }
            }

            string succsesfullDealsString;
            while (true)
            {
                Console.Write("Quantity of succsesfull deals: ");
                succsesfullDealsString = Console.ReadLine();
                if (short.TryParse(succsesfullDealsString, out succsesfullDeals) && succsesfullDeals >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Quantity of succsesfull deals");
                    continue;
                }
            }

            string additionCoefficientString;
            while (true)
            {
                Console.Write("Addition сoefficient to salary: ");
                additionCoefficientString = Console.ReadLine();
                if (decimal.TryParse(additionCoefficientString, out additionCoefficient) && additionCoefficient >= 0)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Addition сoefficient");
                    continue;
                }
            }

            string manegerClassString;
            while (true)
            {
                Console.Write("Maneger Class: ");
                manegerClassString = Console.ReadLine();
                if (manegerClassString.Length == 1 && char.IsLetter(manegerClassString[0]))
                {
                    manegerClass = manegerClassString[0];
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Maneger Class");
                    continue;
                }
            }

            return new object[6] { firstName, lastName, dateTime, succsesfullDeals, additionCoefficient, manegerClass };
        }

        private static void Create(string parameters)
        {
            object[] newRecordData = UserQuestioning();
            fileCabinetService.CreateRecord(newRecordData[0].ToString(), newRecordData[1].ToString(), (DateTime)newRecordData[2], (short)newRecordData[3], (decimal)newRecordData[4], (char)newRecordData[5]);
            Console.WriteLine($"Record #{fileCabinetService.GetStat()} is created.");
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }

        private static void Edit(string parameters)
        {
            int indexOfRecord;
            if (string.IsNullOrWhiteSpace(parameters))
            {
                Console.WriteLine("You didn't write which record you want to edit.");
                return;
            }

            if (!int.TryParse(parameters, out indexOfRecord) || indexOfRecord <= 0)
            {
                Console.WriteLine("Number of record is not correct.");
                return;
            }

            if (indexOfRecord > fileCabinetService.GetStat())
            {
                Console.WriteLine($"#{indexOfRecord} record is not found.");
                return;
            }

            object[] newRecordData = UserQuestioning();
            fileCabinetService.EditRecord(indexOfRecord, newRecordData[0].ToString(), newRecordData[1].ToString(), (DateTime)newRecordData[2], (short)newRecordData[3], (decimal)newRecordData[4], (char)newRecordData[5]);
            Console.WriteLine($"Record #{indexOfRecord} is updated.");
        }
    }
}