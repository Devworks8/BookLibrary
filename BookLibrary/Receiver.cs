// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: 
//  Version: 
//
//  Description: 
//
//
// Receiver.cs
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
using System.IO;
using System.Collections.Generic;
using ManyConsole;

namespace BookLibrary
{
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

        public void menu(string[] args)
        {
            //// Redirect standard output to stringwriter object
            //var sw = new StringWriter();
            //Console.SetOut(sw);
            //Console.SetError(sw);

            var commands = GetCommands();
            ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
            //desktop.Workspaces[id].FlushBuffer();
            //desktop.SendToWorkspace(id, sw.ToString());

            //// Close previous output stream and redirect output to standard output.
            //var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            //standardOutput.AutoFlush = true;
            //Console.SetOut(standardOutput);
        }

        public void help(string[] args, string id)
        {
            var commands = GetCommands();

            // Redirect standard output to stringwriter object
            var sw = new StringWriter();
            Console.SetOut(sw);
            Console.SetError(sw);

            if (args.Length > 1)
            {
                bool found = false;
                foreach (ConsoleCommand cmd in commands)
                {
                    if (!(cmd.Aliases is null) && cmd.Aliases[0] == args[1])
                    {
                        ManyConsole.Internal.ConsoleHelp.ShowCommandHelp(cmd, Console.Out);
                        Desktop.Workspaces[id].FlushBuffer();
                        Desktop.SendToWorkspace(id, sw.ToString());
                        found = true;
                        break;
                    }
                }
                if(!found)
                {
                    ManyConsole.Internal.ConsoleHelp.ShowSummaryOfCommands(commands, Console.Out);
                    Desktop.Workspaces[id].FlushBuffer();
                    Desktop.SendToWorkspace(id, sw.ToString());
                }
            }
            else
            {
                ManyConsole.Internal.ConsoleHelp.ShowSummaryOfCommands(commands, Console.Out);
                Desktop.Workspaces[id].FlushBuffer();
                Desktop.SendToWorkspace(id, sw.ToString());
            }

            // Close previous output stream and redirect output to standard output.
            var standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
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

        public void modify(string[] args)
        {
            var commands = GetCommands();
            ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }

        public void list(string[] args)
        {
            var commands = GetCommands();
            ConsoleCommandDispatcher.DispatchCommand(commands, args, Console.Out);
        }
    }
}
