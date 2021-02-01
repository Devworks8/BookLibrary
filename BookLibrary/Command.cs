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

namespace BookLibrary
{
    abstract public class Command
    {
        protected Receiver Receiver;
        public Command(Receiver receiver)
        {
            this.Receiver = receiver;
        }

        public abstract void Execute(string[] args, string id);
    }

    public class quit : Command
    {
        public quit(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args, string id) => Receiver.quit(args);
    }

    public class help : Command
    {
        public help(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args, string id) => Receiver.help(args, id);
    }

    public class add : Command
    {
        public add(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args, string id) => Receiver.add(args);
    }

    public class delete : Command
    {
        public delete(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args, string id) => Receiver.delete(args);
    }

    public class menu : Command
    {
        public menu(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args, string id) => Receiver.menu(args);
    }

    public class modify : Command
    {
        public modify(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args, string id) => Receiver.modify(args);
    }

    public class list : Command
    {
        public list(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args, string id) => Receiver.list(args);
    }

    public class fetch : Command
    {
        public fetch(Receiver receiver) : base(receiver) { }
        public override void Execute(string[] args, string id) => Receiver.fetch(args);
    }

    public static class CommandBot
    {
        static Receiver r = new Receiver();
        static Invoker invoker = new Invoker();
        static Desktop desktop = new Desktop();
        static public Library library = new Library();

        // Static Workspace text
        static public string strTitle;
        static public string strDescription;
        static public string strHeaders;
        static public string strMenu;

        // List of valid commands to be created dynamically
        public static List<string> commandOptions =
            new List<string> { "quit", "add", "help", "delete", "modify", "menu", "list", "fetch" };

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

            // Get user input
            string input = Console.ReadLine();

            string[] args = input.Split(' ');

            while (!String.IsNullOrEmpty(input))
            {
                
                if (commandOptions.Contains(args[0]))
                {
                    // CLear Command Window
                    Desktop.Workspaces["cmd"].FlushBuffer();

                    if (args[0].ToLower() == "help") invoker.SetCommand(validCommands[args[0].ToLower()], args, "menu");
                    else invoker.SetCommand(validCommands[args[0].ToLower()], args, "info");

                    invoker.ExecuteCommand();

                    Desktop.DrawDesktop();
                    break;
                }
                else
                {
                    Desktop.SendToWorkspace("cmd", $"Error! '{input}' is not a valid response".PadRight(Console.WindowWidth));

                    Desktop.DrawDesktop();

                    Console.Write(prompt);
                    input = Console.ReadLine();
                    args = input.Split(' ');
                }
            }

            // CLear Command Window
            //Desktop.Workspaces["cmd"].FlushBuffer();

        }

        public static void run()
        {
            // Create individual workspaces
            Workspace title = new Workspace(new Point(0, 0), 100, 2);
            Workspace description = new Workspace(new Point(0, title.WorkspaceHeight + 1), 100, 15);
            Workspace headers = new Workspace(new Point(0, description.WorkspaceHeight + 1), 100, 6);
            Workspace menu = new Workspace(new Point(0, description.WorkspaceHeight + 5), 50, 60);
            Workspace info = new Workspace(new Point(menu.WorkspaceWidth + 1, menu.WorkspaceOrigin.Y), 50, 60, 2);
            Workspace cmd = new Workspace(new Point(0, info.WorkspaceOrigin.Y + info.WorkspaceHeight + 3), 100, 6);

            // Add workspaces to desktop
            Desktop.AddWorkspace("title", title);
            Desktop.AddWorkspace("desc", description);
            Desktop.AddWorkspace("headers", headers);
            Desktop.AddWorkspace("menu", menu);
            Desktop.AddWorkspace("info", info);
            Desktop.AddWorkspace("cmd", cmd);

            // Static Workspace text
            strTitle = Display.CenterAligned("Book Database - Version 1.0.0", Desktop.Workspaces["title"].WorkspaceWidth);

            strDescription =
                new String('-', Desktop.Workspaces["desc"].WorkspaceWidth) + "\n" +
                "DESCRIPTION\n" +
                "\nA database that stores the Title, Author's Name, ISBN, Genre, Type, and Publisher of a piece of literature.\n" +
                "The program gives you the ability to Add, Delete, Search, Modify a record.\n" +
                "It also allows you to search using different criterias.\n";

            strHeaders = new String('-', Desktop.Workspaces["headers"].WorkspaceWidth) + "\n" +
                Display.CenterAligned("Menu", Desktop.Workspaces["menu"].WorkspaceWidth) + "|" +
                Display.CenterAligned("Information", Desktop.Workspaces["info"].WorkspaceWidth) + "\n" +
                new String('-', Desktop.Workspaces["headers"].WorkspaceWidth);

            strMenu =
                $"Command\t\t Deescription\n\n" +
                $"add\t\t Add a record.\n" +
                $"delete\t\t Delete a record.\n" +
                $"modify\t\t Modify a record.\n" +
                $"search\t\t Search for a record.\n" +
                $"list\t\t List genre or categories\n" +
                $"help\t\t Extended help.\n" +
                $"menu\t\t Show this menu.\n" +
                $"quit\t\t Quit the program.\n\n";


            Desktop.SendToWorkspace("title", strTitle);
            Desktop.SendToWorkspace("desc", strDescription);
            Desktop.SendToWorkspace("headers", strHeaders);
            Desktop.SendToWorkspace("menu", strMenu);

            // Create command objects and assign to dictionary
            foreach (string command in commandOptions)
            {
                validCommands.Add(command, CreateInstance(command, r));
            }

            while (true)
            {
                Desktop.DrawDesktop();
                GetUserInput("> ");
            }
        }
    }



}

