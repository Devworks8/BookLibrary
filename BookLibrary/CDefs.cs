// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: 
//  Version: 
//
//  Description: 
//
//
// CDefs.cs
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
using System.Linq;
using ManyConsole;

namespace BookLibrary
{


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
            HasAlias("quit");

        }

        public override int Run(string[] remainingArguments)
        {
            Environment.Exit(0);
            return 0;
        }
    }

    public class ListCommand : ConsoleCommand
    {
        public string arg;

        public ListCommand()
        {
            IsCommand("list", "List Genres or Catagories.");
            HasAlias("list");
            HasLongDescription(@"
List the genres or catagories
Expected usage at the CLI: list <options>.");
            HasRequiredOption("o=", "[genre|cat]", c => arg = c);
        }

        public override int Run(string[] remainingArguments)
        {
            if (!string.IsNullOrEmpty(arg))
            {
                if (arg.ToLower() == "genre")
                {
                    var values = Enum.GetValues(typeof(_GenreEnum)).Cast<_GenreEnum>();

                    Desktop.Workspaces["menu"].FlushBuffer(); 
                    Desktop.SendToWorkspace("menu", "Code \t<-> \tDescription\n");

                    byte count = 1;
                    foreach (var value in values)
                    {
                        
                        Desktop.SendToWorkspace("menu", $"{count} \t<-> \t" + value.ToString()) ;
                        count++;
                    }
                }
                if (arg.ToLower() == "cat")
                {
                    var values = Enum.GetValues(typeof(_TypeEnum)).Cast<_TypeEnum>();
                    Desktop.Workspaces["menu"].FlushBuffer();
                    Desktop.SendToWorkspace("menu", "Code \t<-> \tDescription\n");

                    byte count = 1;
                    foreach (var value in values)
                    {
                        Desktop.SendToWorkspace("menu", $"{count} \t<-> \t" + value.ToString());
                        count++;
                    }
                }
            }
            return 0;
        }
    }

    public class MenuCommand : ConsoleCommand
    {
        public MenuCommand()
        {
            IsCommand("menu", "Show the menu.");
            HasAlias("menu");
            HasLongDescription(@"Show menu.");
        }

        public override int Run(string[] remainingArguments)
        {
            Desktop.Workspaces["menu"].FlushBuffer();
            Desktop.SendToWorkspace("menu", CommandBot.strMenu);

            return 0;
        }
    }

    public class AddCommand : ConsoleCommand
    {
        public string Title { get; set; }
        public string AFName { get; set; }
        public string ALName { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Cat { get; set; }
        public string Publisher { get; set; }

        public AddCommand()
        {
            IsCommand("add", "Add a record.");
            HasAlias("add");
            HasLongDescription(@"
Add a new record to the database.
Expected usage at the CLI: add <options>");
            HasOption("t|title=", "Title.", t => Title = t);
            HasOption("f|first=", "Author's first name.", f => AFName = f);
            HasOption("l|last=", "Author's last name.", l => ALName = l);
            HasOption("i|isbn=", "ISBN.", i => ISBN = i);
            HasOption("g|genre=", "Literary genre.", g => Genre = g);
            HasOption("T|type=", "Literary type.", T => Cat = T);
            HasOption("p|publisher=", "Publisher name.", p => Publisher = p);
        }

        public override int Run(string[] remainingArguments)
        {
            //TODO: Add functionality
            Desktop.SendToWorkspace("info", "Add command entered");
            return 0;
        }
    }
    
    public class DeleteCommand : ConsoleCommand
    {
        public string Title { get; set; }
        public string AFName { get; set; }
        public string ALName { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Cat { get; set; }
        public string Publisher { get; set; }

        public DeleteCommand()
        {
            IsCommand("delete", "Delete a record.");
            HasAlias("delete");
            HasLongDescription(@"
Delete a new record to the database.
Expected usage at the CLI: delete <options>");
            HasOption("t|title=", "Title.", t => Title = t);
            HasOption("f|first=", "Author's first name.", f => AFName = f);
            HasOption("l|last=", "Author's last name.", l => ALName = l);
            HasOption("i|isbn=", "ISBN.", i => ISBN = i);
            HasOption("g|genre=", "Literary genre.", g => Genre = g);
            HasOption("T|type=", "Literary type.", T => Cat = T);
            HasOption("p|publisher=", "Publisher name.", p => Publisher = p);
        }

        public override int Run(string[] remainingArguments)
        {
            //TODO: Add functionality
            Desktop.SendToWorkspace("info", "Delete command entered");
            return 0;
        }
    }

    public class SearchCommand : ConsoleCommand
    {
        public string Title { get; set; }
        public string AFName { get; set; }
        public string ALName { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Cat { get; set; }
        public string Publisher { get; set; }

        public SearchCommand()
        {
            IsCommand("search", "Search for a record.");
            HasAlias("search");
            HasLongDescription(@"
Search for a record.
Expected usage at the CLI: search <options>");
            HasOption("t|title=", "Title.", t => Title = t);
            HasOption("f|first=", "Author's first name.", f => AFName = f);
            HasOption("l|last=", "Author's last name.", l => ALName = l);
            HasOption("i|isbn=", "ISBN.", i => ISBN = i);
            HasOption("g|genre=", "Literary genre.", g => Genre = g);
            HasOption("T|type=", "Literary type.", T => Cat = T);
            HasOption("p|publisher=", "Publisher name.", p => Publisher = p);

        }

        public override int Run(string[] remainingArguments)
        {
            //TODO: Add functionality
            Desktop.SendToWorkspace("info", " Search command entered");
            return 0;
        }
    }

    public class ModifyCommand : ConsoleCommand
    {
        public string Title { get; set; }
        public string AFName { get; set; }
        public string ALName { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Cat { get; set; }
        public string Publisher { get; set; }

        public ModifyCommand()
        {
            IsCommand("modify", "Modify a record.");
            HasAlias("modify");
            HasLongDescription(@"
Modify a record.
Expected usage at the CLI: search <options>");
            HasOption("t|title=", "Title.", t => Title = t);
            HasOption("f|first=", "Author's first name.", f => AFName = f);
            HasOption("l|last=", "Author's last name.", l => ALName = l);
            HasOption("i|isbn=", "ISBN.", i => ISBN = i);
            HasOption("g|genre=", "Literary genre.", g => Genre = g);
            HasOption("T|type=", "Literary type.", T => Cat = T);
            HasOption("p|publisher=", "Publisher name.", p => Publisher = p);
        }

        public override int Run(string[] remainingArguments)
        {
            //TODO: Add functionality
            Desktop.SendToWorkspace("info", " Modify command entered");
            return 0;
        }
    }
}
