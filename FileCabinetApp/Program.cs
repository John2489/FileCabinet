using System;
using System.Collections.ObjectModel;
using System.Globalization;

namespace FileCabinetApp
{
    /// <summary>
    /// This class is contains the Main method and runs application.
    /// </summary>
    public static class Program
    {
        private const string DeveloperName = "Ivan Babitski";
        private const string HintMessage = "Enter your command, or enter 'help' to get help.";
        private const int CommandHelpIndex = 0;
        private const int DescriptionHelpIndex = 1;
        private const int ExplanationHelpIndex = 2;

        private static string validationRule = "Using default validation rules.";

        private static bool isRunning = true;
        private static FileCabinetService fileCabinetService = new FileCabinetService(new DefaultValidator());
        private static CultureInfo regionalSetting = CultureInfo.CreateSpecificCulture("en-US");

        private static Tuple<string, Action<string>>[] commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("edit", Edit),
            new Tuple<string, Action<string>>("exit", Exit),
            new Tuple<string, Action<string>>("find", Find),
            new Tuple<string, Action<string>>("list", List),
            new Tuple<string, Action<string>>("help", PrintHelp),
            new Tuple<string, Action<string>>("stat", Stat),
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "create", "create new record", "The 'create' command create new record." },
            new string[] { "edit", "edit record by id", "The 'edit' command create new record. Parametr is id of edit records." },
            new string[] { "exit", "exits the application", "The 'exit' command exits the application." },
            new string[] { "find firstname", "finds specific record record by FirstName", "The 'find' command find specific record by FirstName." },
            new string[] { "find lastname", "finds specific record record by LastName", "The 'find' command find specific record by LastName." },
            new string[] { "find dateofbirth", "finds specific record record by DateOfBirth", "The 'find' command find specific record by DateOfBirth." },
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "list", "prints the all records", "The 'list' command prints all records." },
            new string[] { "stat", "prints statistic about records", "The 'stat' command prints quantity of records." },
        };

        /// <summary>
        /// Main method from which starts application.
        /// </summary>
        /// <param name="args">Parameters for run application.</param>
        public static void Main(string[] args)
        {
            ParametersApplication(args);

            Console.WriteLine($"File Cabinet Application, developed by {Program.DeveloperName}");
            Console.WriteLine(Program.validationRule);
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

        /// <summary>
        /// Method serves to processing parameters application.
        /// </summary>
        /// <param name="args">Parameters for run application.</param>
        public static void ParametersApplication(string[] args)
        {
            foreach (var item in args)
            {
                switch (item.ToUpper(regionalSetting))
                {
                    case "--VALIDATION-RULES=CUSTOM":
                        fileCabinetService = new FileCabinetService(new CustomValidator());
                        validationRule = "Using custom validation rules.";
                        break;
                    case "-V=CUSTOM":
                        fileCabinetService = new FileCabinetService(new CustomValidator());
                        validationRule = "Using custom validation rules.";
                        break;
                    case "--VALIDATION-RULES=DEFAULT":
                        fileCabinetService = new FileCabinetService(new DefaultValidator());
                        validationRule = "Using default validation rules.";
                        break;
                    case "-V=DEFAULT":
                        fileCabinetService = new FileCabinetService(new DefaultValidator());
                        validationRule = "Using default validation rules.";
                        break;
                    default:
                        break;
                }
            }
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
            PrintRecords(fileCabinetService.GetRecords());
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
            fileCabinetService.CreateRecord(new ObjectParametrsForCreateAndEditRecord(
                                                            newRecordData[0].ToString(),
                                                            newRecordData[1].ToString(),
                                                            (DateTime)newRecordData[2],
                                                            (short)newRecordData[3],
                                                            (decimal)newRecordData[4],
                                                            (char)newRecordData[5]));
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
            fileCabinetService.EditRecord(indexOfRecord, new ObjectParametrsForCreateAndEditRecord(
                                                            newRecordData[0].ToString(),
                                                            newRecordData[1].ToString(),
                                                            (DateTime)newRecordData[2],
                                                            (short)newRecordData[3],
                                                            (decimal)newRecordData[4],
                                                            (char)newRecordData[5]));
            Console.WriteLine($"Record #{indexOfRecord} is updated.");
        }

        private static void Find(string parameters)
        {
            string[] findParametrs = parameters.Split(' ', 2);
            string temp;
            DateTime dateTime;
            if (findParametrs.Length != 2)
            {
                Console.WriteLine("Write command with Find, try again.");
                return;
            }

            string paramOfFind = findParametrs[0];
            if (paramOfFind.ToUpper(regionalSetting) == "FIRSTNAME")
            {
                temp = findParametrs[1].Trim('"').ToUpper(regionalSetting);
                PrintRecords(fileCabinetService.FindByFirstName(temp));
            }
            else if (paramOfFind.ToUpper(regionalSetting) == "LASTNAME")
            {
                temp = findParametrs[1].Trim('"').ToUpper(regionalSetting);
                PrintRecords(fileCabinetService.FindByLastName(temp));
            }
            else if (paramOfFind.ToUpper(regionalSetting) == "DATEOFBIRTH")
            {
                temp = findParametrs[1].Trim('"');
                if (DateTime.TryParse(temp, out dateTime))
                {
                    PrintRecords(fileCabinetService.FindByDateOfBirthName(dateTime));
                }
                else
                {
                    Console.WriteLine("Command is not correct, try again.");
                    return;
                }
            }
            else
            {
                Console.WriteLine("Command is not correct, try again.");
                return;
            }
        }

        private static void PrintRecords(ReadOnlyCollection<FileCabinetRecord> records)
        {
            foreach (var record in records)
            {
                Console.WriteLine($"#{record.Id}, {record.FirstName}," +
                  $" {record.LastName}," +
                  $" {record.DateOfBirth.ToString("yyyy-MMM-dd", regionalSetting)}," +
                  $" {record.SuccsesfullDeals}," +
                  $" {record.AdditionCoefficient}," +
                  $" {record.ManagerClass.ToString(regionalSetting).ToUpper(regionalSetting)}");
            }
        }
    }
}