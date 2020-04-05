﻿using System;
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
        };

        private static string[][] helpMessages = new string[][]
        {
            new string[] { "create", "create new record", "The 'create' command create new record." },
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
                Console.WriteLine($"#{i + 1}, {files[i].FirstName}, " +
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

        private static void Create(string parameters)
        {
            Console.Write("First name: ");
            string firstName = Console.ReadLine();
            if (firstName.Length < 2 || firstName.Length > 60 || firstName == null || firstName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException("User's enter is not correct in \"First name: \" line.");
            }

            Console.Write("Last name: ");
            string lastName = Console.ReadLine();
            if (lastName.Length < 2 || lastName.Length > 60 || lastName == null || lastName.Contains(' ', StringComparison.CurrentCulture))
            {
                throw new ArgumentException("User's enter is not correct in \"First name: \" line.");
            }

            string date;
            string[] dayMonthHear;
            int[] dateSepareted = new int[3];
            DateTime dateTime;
            DateTime minDate = new DateTime(1950, 1, 1);
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
                        if(dateTime < minDate || dateTime >= DateTime.Now)
                        {
                            throw new ArgumentException("User's enter is not correct in \"Date of birth(mm/dd/hhhh): \" line.");
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

            Console.Write("Quantity of succsesfull deals: ");
            string succsesfullDealsString = Console.ReadLine();
            short succsesfullDeals;
            if (short.TryParse(succsesfullDealsString, out _))
            {
                succsesfullDeals = short.Parse(succsesfullDealsString, regionalSetting);
            }
            else
            {
                throw new ArgumentException("User's enter is not correct in \"Quantity of succsesfull deals: \" line.");
            }

            Console.Write("Addition сoefficient to salary: ");
            string additionCoefficientString = Console.ReadLine();
            decimal additionCoefficient;
            if (decimal.TryParse(succsesfullDealsString, out _))
            {
                additionCoefficient = decimal.Parse(additionCoefficientString, regionalSetting);
            }
            else
            {
                throw new ArgumentException("User's enter is not correct in \"Addition сoefficient to salary: \" line.");
            }

            Console.Write("Maneger Class: ");
            string manegerClassString = Console.ReadLine();
            char manegerClass;
            if (manegerClassString.Length < 2 || manegerClassString.Length != 0)
            {
                manegerClass = manegerClassString[0];
            }
            else
            {
                throw new ArgumentException("User's enter is not correct in \"Maneger Class: \" line.");
            }

            fileCabinetService.CreateRecord(firstName, lastName, dateTime, succsesfullDeals, additionCoefficient, manegerClass);
            Console.WriteLine($"Record #{fileCabinetService.GetStat()} is created.");
        }

        private static void Exit(string parameters)
        {
            Console.WriteLine("Exiting an application...");
            isRunning = false;
        }
    }
}