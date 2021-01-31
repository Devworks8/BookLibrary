// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: CLI
//  Version: 1.0.0
//
//  Description: Command Line Interface
//
//
// Command.cs
//
//  Author:
//       Christian Lachapelle <lachapellec@gmail.com>
//
//  Copyright (c) 2021 
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using ManyConsole;

namespace BookLibrary
{
    /// <summary>
    /// The class <c>Command</c> executes the action from a <c>Receiver</c> class.
    /// </summary>
    abstract public class Command
    {
        protected Receiver Receiver;
        public Command(Receiver receiver)
        {
            this.Receiver = receiver;
        }

        public abstract void Execute(string[] args);
    }

    /// <summary>
    /// The following classes constitute the commands.
    /// <list type="bullet">
    ///<item>
    ///<description>
    ///<c>quit</c> => Exit the application
    ///</description>
    ///</item>
    /// </list>
    /// </summary>
    public class quit : Command
    {
        public quit(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args) => Receiver.quit(args);
    }

    public class help : Command
    {
        public help(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args) => Receiver.help(args);
    }

    public class add : Command
    {
        public add(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args) => Receiver.add(args);
    }

    public class delete : Command
    {
        public delete(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args) => Receiver.delete(args);
    }

    public class search : Command
    {
        public search(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args) => Receiver.search(args);
    }

    /// <summary>
    /// The class <c>Receiver</c> is the object that has an action
    /// to be executed from the <c>Command</c> class.
    /// </summary>
    public class Receiver
    {
        public static IEnumerable<ConsoleCommand> GetCommands()
        {
            return ConsoleCommandDispatcher.FindCommandsInSameAssemblyAs(typeof(Receiver));
        }

        public void quit(string[] args)
        {
            var commands = GetCommands();
            ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }

        public void help(string[] args)
        {
            var commands = GetCommands();

            if (args.Length > 1)
            {
                foreach (ConsoleCommand cmd in commands)
                {
                    if (!(cmd.Aliases is null) && cmd.Aliases[0] == args[1])
                    {
                        ManyConsole.Internal.ConsoleHelp.ShowCommandHelp(cmd, Console.Out);
                        break;
                    }
                    else
                    {
                        ManyConsole.Internal.ConsoleHelp.ShowSummaryOfCommands(commands, Console.Out);
                        break;
                    }
                }
            }
            else
            {
                ManyConsole.Internal.ConsoleHelp.ShowSummaryOfCommands(commands, Console.Out);
            }
        }

        public void add(string[] args)
        {
            var commands = GetCommands();
            ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);


        }

        public void delete(string[] args)
        {
            var commands = GetCommands();
            ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);


        }

        public void search(string[] args)
        {
            var commands = GetCommands();
            ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);


        }

    }


    /// <summary>
    /// The <c>Invoker</c> class tells the <c>Command</c> class
    /// to execute their actions.
    /// </summary>
    public class Invoker
    {
        private Command _command;
        private string[] _args;

        public void SetCommand(Command command, string[] args)
        {
            this._command = command;
            this._args = args;
        }

        public void ExecuteCommand() => _command.Execute(_args);
    }



    //////////////////////////////////////////////////////////////////////////
    /// Command Classes
    //////////////////////////////////////////////////////////////////////////
 

    public class HelpCommand : ConsoleCommand
    {
        public HelpCommand()
        {
            IsCommand("help", "Display command list.");
        }

        public override int Run(string[] remainingArguments)
        {
            return 0;
        }
    }

    public class QuitCommand : ConsoleCommand
    {
        public QuitCommand()
        {
            IsCommand("quit", "Quit the program.");
        }

        public override int Run(string[] remainingArguments)
        {
            Environment.Exit(0);
            return 0;
        }
    }

    public class AddCommand : ConsoleCommand
    {
        public AddCommand()
        {
            IsCommand("add", "Add a record.");
        }

        public override int Run(string[] remainingArguments)
        {
            Environment.Exit(0);
            return 0;
        }
    }

    public class DeleteCommand : ConsoleCommand
    {
        public DeleteCommand()
        {
            IsCommand("delete", "Delete a record.");
        }

        public override int Run(string[] remainingArguments)
        {
            Environment.Exit(0);
            return 0;
        }
    }

    public class SearchCommand : ConsoleCommand
    {
        public SearchCommand()
        {
            IsCommand("search", "Search for a record.");
        }

        public override int Run(string[] remainingArguments)
        {
            Environment.Exit(0);
            return 0;
        }
    }

    ///////////////////////////////////////////////////////////////////////////


    public static class CommandBot
    {
        static Receiver r = new Receiver();
        static Invoker invoker = new Invoker();
        static Desktop desktop = new Desktop();


        // List of valid commands to be created dynamically
        public static List<string> commandOptions =
            new List<string> { "quit", "add", "help", "delete", "search" };

        // Hold the valid command objects in a Dictionary
        public static Dictionary<string, Command> validCommands =
            new Dictionary<string, Command>();

        // Dynamically create a Command object className with the Receiver object argument
        public static Command CreateInstance(string className, Receiver r)
        {
            Type t = Type.GetType("BookLibrary." + className);

            Command o = (Command)Activator.CreateInstance(t, new Object[] { r });
            return o;
        }

        private static void GetUserInput(string prompt)
        {
            Console.Write(prompt);

            // Capture the cursor position just after the prompt
            var inputCursorLeft = Console.CursorLeft;
            var inputCursorTop = Console.CursorTop;

            // Now get user input
            string input = Console.ReadLine();

            string[] args = input.Split(' ');

            while (!String.IsNullOrEmpty(input))
            {
                desktop.DrawDesktop();
                if (commandOptions.Contains(args[0]))
                {
                    // Erase the last error message (if there was one)
                    //Console.Write(new string(' ', Console.WindowWidth));
                    desktop.SendToWorkspace("cmd", new string(' ', desktop.Workspaces["cmd"].WorkspaceWidth));
                    invoker.SetCommand(validCommands[args[0].ToLower()], args);
                    invoker.ExecuteCommand();
                    break;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    // PadRight ensures that this line extends the width
                    // of the console window so it erases itself each time
                    desktop.SendToWorkspace("cmd", $"Error! '{input}' is not a valid response".PadRight(Console.WindowWidth));
                    //Console.Write($"Error! '{input}' is not a valid response".PadRight(Console.WindowWidth));
                    Console.ResetColor();

                    // Set cursor position to just after the promt again, write
                    // a blank line, and reset the cursor one more time
                    Console.SetCursorPosition(inputCursorLeft, inputCursorTop);
                    Console.Write(new string(' ', input.Length));
                    Console.SetCursorPosition(inputCursorLeft, inputCursorTop);

                    input = Console.ReadLine();
                    args = input.Split(' ');
                }
            }

            // Erase the last error message (if there was one)
            Console.Write(new string(' ', Console.WindowWidth));

        }

        public static void run()
        {
            // Create individual workspaces
            Workspace title = new Workspace(new Point(0, 0), 100, 2);
            Workspace description = new Workspace(new Point(0, title.WorkspaceHeight + 1), 100, 12);
            Workspace headers = new Workspace(new Point(0, description.WorkspaceHeight + 1), 100, 5);
            Workspace menu = new Workspace(new Point(0, description.WorkspaceHeight + 5), 50, 30);
            Workspace info = new Workspace(new Point(menu.WorkspaceWidth + 1, menu.WorkspaceOrigin.Y), 50, 30);
            Workspace cmd = new Workspace(new Point(0, info.WorkspaceOrigin.Y + info.WorkspaceHeight + 3), 100, 3);

            // Add workspaces to desktop
            desktop.AddWorkspace("title", title);
            desktop.AddWorkspace("desc", description);
            desktop.AddWorkspace("headers", headers);
            desktop.AddWorkspace("menu", menu);
            desktop.AddWorkspace("info", info);
            desktop.AddWorkspace("cmd", cmd);

            // Static Workspace text
            string strTitle = "Book Database - Version 1.0.0".PadLeft(desktop.Workspaces["title"].WorkspaceWidth/2,' ');

            string strDescription =
                new String('-', desktop.Workspaces["desc"].WorkspaceWidth) + "\n" +
                "DESCRIPTION\n" +
                "\nA database that stores the Title, Author's Name, ISBN, Genre, Type, and Publisher of a piece of literature.\n" +
                "The program gives you the ability to Add, Delete, Search, Modify a record.\n" +
                "It also allows you to search using different criterias.\n";

            string strHeaders = new String('-',  desktop.Workspaces["headers"].WorkspaceWidth) + "\n"+
                "Menu".PadLeft((desktop.Workspaces["menu"].WorkspaceWidth-4)/2, ' ')+ "|".PadLeft(desktop.Workspaces["menu"].WorkspaceWidth - (desktop.Workspaces["menu"].WorkspaceWidth - 4) / 2, ' ')+
                "Information".PadLeft((desktop.Workspaces["info"].WorkspaceWidth - 11)- (desktop.Workspaces["menu"].WorkspaceWidth - 4) / 2, ' ')+"\n"+
                new String('-', desktop.Workspaces["headers"].WorkspaceWidth);

            string strMenu =
                $"\t\t\t\t\tCommand\t\t Deescription\n\n" +
                $"\t\t\t\t\tadd\t\t Add a record.\n" +
                $"\t\t\t\t\tdelete\t\t Delete a record.\n" +
                $"\t\t\t\t\tmodify\t\t Modify a record.\n" +
                $"\t\t\t\t\tsearch\t\t Search for a record.\n" +
                $"\t\t\t\t\thelp\t\t Extended help.\n" +
                $"\t\t\t\t\tquit\t\t Quit the program.\n\n";

            desktop.SendToWorkspace("title", strTitle);
            desktop.SendToWorkspace("desc", strDescription);
            desktop.SendToWorkspace("headers", strHeaders);
            desktop.SendToWorkspace("menu", strMenu);

            // Create command objects and assign to dictionary
            foreach (string command in commandOptions)
            {
                validCommands.Add(command, CreateInstance(command, r));
            }

            while (true)
            {
                desktop.DrawDesktop();
                GetUserInput("> ");
            }
        }
    }



}

