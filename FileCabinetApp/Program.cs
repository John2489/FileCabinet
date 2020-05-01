using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using FileCabinetApp.ConvertersAndVilidators;

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
        private static IFileCabinetService fileCabinetService = new FileCabinetService(new DefaultValidator());
        private static CultureInfo regionalSetting = CultureInfo.CreateSpecificCulture("en-US");

        private static Tuple<string, Action<string>>[] commands = new Tuple<string, Action<string>>[]
        {
            new Tuple<string, Action<string>>("create", Create),
            new Tuple<string, Action<string>>("edit", Edit),
            new Tuple<string, Action<string>>("exit", Exit),
            new Tuple<string, Action<string>>("export", Export),
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
            new string[] { "export csv", "export all records to *.csv file", "Export all records to *.csv file." },
            new string[] { "find firstname", "finds specific record record by FirstName", "The 'find' command find specific record by FirstName." },
            new string[] { "find lastname", "finds specific record record by LastName", "The 'find' command find specific record by LastName." },
            new string[] { "find dateofbirth", "finds specific record record by DateOfBirth", "The 'find' command find specific record by DateOfBirth." },
            new string[] { "help", "prints the help screen", "The 'help' command prints the help screen." },
            new string[] { "list", "prints the all records", "The 'list' command prints all records." },
            new string[] { "stat", "prints statistic about records", "The 'stat' command prints quantity of records." },
        };

        private static Func<string, Tuple<bool, string, string>> stringConverter = Converters.StringConverter;
        private static Func<string, Tuple<bool, string, DateTime>> dateOfBirthConverter = Converters.DateOfBirthConverter;
        private static Func<string, Tuple<bool, string, short>> succsesfullDealsConverter = Converters.SuccsesfullDealsConverter;
        private static Func<string, Tuple<bool, string, decimal>> additionCoefficientConverter = Converters.AdditionCoefficientConverter;
        private static Func<string, Tuple<bool, string, char>> manegerClassConverter = Converters.ManegerClassConverter;

        private static Func<string, Tuple<bool, string>> firstNameValidator = ValidatorsDefault.FirstNameValidator;
        private static Func<string, Tuple<bool, string>> lastNameValidator = ValidatorsDefault.LastNameValidator;
        private static Func<DateTime, Tuple<bool, string>> dateOfBirthValidator = ValidatorsDefault.DateOfBirthValidator;
        private static Func<short, Tuple<bool, string>> succsesfullDealsValidator = ValidatorsDefault.SuccsesfullDealsValidator;
        private static Func<decimal, Tuple<bool, string>> additionCoefficientValidator = ValidatorsDefault.AdditionCoefficientValidator;
        private static Func<char, Tuple<bool, string>> manegerClassValidator = ValidatorsDefault.ManegerClassValidator;

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
                        firstNameValidator = ValidatorsCustom.FirstNameValidator;
                        dateOfBirthValidator = ValidatorsCustom.DateOfBirthValidator;
                        succsesfullDealsValidator = ValidatorsCustom.SuccsesfullDealsValidator;
                        additionCoefficientValidator = ValidatorsCustom.AdditionCoefficientValidator;
                        manegerClassValidator = ValidatorsCustom.ManegerClassValidator;
                        break;
                    case "-V=CUSTOM":
                        fileCabinetService = new FileCabinetService(new CustomValidator());
                        validationRule = "Using custom validation rules.";
                        firstNameValidator = ValidatorsCustom.FirstNameValidator;
                        dateOfBirthValidator = ValidatorsCustom.DateOfBirthValidator;
                        succsesfullDealsValidator = ValidatorsCustom.SuccsesfullDealsValidator;
                        additionCoefficientValidator = ValidatorsCustom.AdditionCoefficientValidator;
                        manegerClassValidator = ValidatorsCustom.ManegerClassValidator;
                        break;
                    case "--VALIDATION-RULES=DEFAULT":
                        fileCabinetService = new FileCabinetService(new DefaultValidator());
                        validationRule = "Using default validation rules.";
                        firstNameValidator = ValidatorsDefault.FirstNameValidator;
                        dateOfBirthValidator = ValidatorsDefault.DateOfBirthValidator;
                        succsesfullDealsValidator = ValidatorsDefault.SuccsesfullDealsValidator;
                        additionCoefficientValidator = ValidatorsDefault.AdditionCoefficientValidator;
                        manegerClassValidator = ValidatorsDefault.ManegerClassValidator;
                        break;
                    case "-V=DEFAULT":
                        fileCabinetService = new FileCabinetService(new DefaultValidator());
                        validationRule = "Using default validation rules.";
                        firstNameValidator = ValidatorsDefault.FirstNameValidator;
                        dateOfBirthValidator = ValidatorsDefault.DateOfBirthValidator;
                        succsesfullDealsValidator = ValidatorsDefault.SuccsesfullDealsValidator;
                        additionCoefficientValidator = ValidatorsDefault.AdditionCoefficientValidator;
                        manegerClassValidator = ValidatorsDefault.ManegerClassValidator;
                        break;
                    default:
                        break;
                }
            }
        }

        private static T ReadInput<T>(Func<string, Tuple<bool, string, T>> converter, Func<T, Tuple<bool, string>> validator)
        {
            do
            {
                T value;

                var input = Console.ReadLine();
                var conversionResult = converter(input);

                if (!conversionResult.Item1)
                {
                    Console.WriteLine($"Conversion failed: {conversionResult.Item2}. Please, correct your input.");
                    continue;
                }

                value = conversionResult.Item3;

                var validationResult = validator(value);
                if (!validationResult.Item1)
                {
                    Console.WriteLine($"Validation failed: {validationResult.Item2}. Please, correct your input.");
                    continue;
                }

                return value;
            }
            while (true);
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
            char managerClass;

            DateTime minDate = new DateTime(1950, 1, 1);

            Console.Write("First name: ");
            firstName = ReadInput<string>(stringConverter, firstNameValidator);

            Console.Write("Last name: ");
            lastName = ReadInput<string>(stringConverter, lastNameValidator);

            Console.Write("Date of birth(mm/dd/hhhh): ");
            dateTime = ReadInput<DateTime>(dateOfBirthConverter, dateOfBirthValidator);

            Console.Write("Quantity of succsesfull deals: ");
            succsesfullDeals = ReadInput<short>(succsesfullDealsConverter, succsesfullDealsValidator);

            Console.Write("Addition сoefficient to salary: ");
            additionCoefficient = ReadInput<decimal>(additionCoefficientConverter, additionCoefficientValidator);

            Console.Write("Maneger Class: ");
            managerClass = ReadInput<char>(manegerClassConverter, manegerClassValidator);

            return new object[6] { firstName, lastName, dateTime, succsesfullDeals, additionCoefficient, managerClass };
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

        private static void Export(string parameters)
        {
            string[] exportParametrs = parameters.Split(' ', 2);
            string modeExport = exportParametrs[0].ToUpper(regionalSetting);

            if (exportParametrs.Length != 2)
            {
                Console.WriteLine("Command is not correct, try again.");
                return;
            }

            if (modeExport == "CSV")
            {
                string[] tempMode = exportParametrs[1].Split('.');
                string fealureIfoLine = @$"Export failed: can't open file {exportParametrs[1]}.";
                string succesExportCSV = $"All records are exported to file {exportParametrs[1]}.";
                int positionOfLastIndex = exportParametrs[1].LastIndexOf('\\');

                if (exportParametrs[1][exportParametrs[1].Length - 1] == '\\')
                {
                    positionOfLastIndex--;
                }

                if (positionOfLastIndex < 0)
                {
                    positionOfLastIndex = 0;
                }

                string directory = exportParametrs[1].Substring(0, positionOfLastIndex);

                if (directory.Contains(':'))
                {
                    if (!Directory.Exists(directory))
                    {
                        Console.WriteLine(fealureIfoLine);
                        return;
                    }
                }

                if (tempMode.Length >= 1 && tempMode[tempMode.Length - 1].ToUpper(regionalSetting) != "CSV")
                {
                    exportParametrs[1] += ".csv";
                }

                StreamWriter streamWriter = new StreamWriter(exportParametrs[1], File.Exists(parameters));

                FileCabinetService castedFileCabinetService = (FileCabinetService)fileCabinetService;
                FileCabinetServiceSnapshot snapshot = castedFileCabinetService.MakeSnapshot();
                snapshot.SaveToCsv(streamWriter);

                streamWriter.Close();
                Console.WriteLine(succesExportCSV);
            }
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