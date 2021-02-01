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
using System.Collections.Generic;
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
        public ListCommand()
        {
            IsCommand("list", "List Genres or Catagories.");
            HasAlias("list");
            HasLongDescription(@"List the genres or catagories");
            HasAdditionalArguments(1,"[genre|cat]");
        }

        public override int Run(string[] remainingArguments)
        {
            if (!string.IsNullOrEmpty(remainingArguments[0]))
            {
                if (remainingArguments[0].ToLower() == "genre")
                {
                    var values = Enum.GetValues(typeof(_GenreEnum)).Cast<_GenreEnum>();

                    Desktop.Workspaces["menu"].FlushBuffer(); 
                    Desktop.SendToWorkspace("menu", "Code \t<-> \tDescription\n");

                    byte count = 0;
                    foreach (var value in values)
                    {
                        
                        Desktop.SendToWorkspace("menu", $"{count} \t<-> \t" + value.ToString()) ;
                        count++;
                    }
                }
                if (remainingArguments[0].ToLower() == "cat")
                {
                    var values = Enum.GetValues(typeof(_TypeEnum)).Cast<_TypeEnum>();
                    Desktop.Workspaces["menu"].FlushBuffer();
                    Desktop.SendToWorkspace("menu", "Code \t<-> \tDescription\n");

                    byte count = 0;
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
        private string Title { get; set; }
        private string AFName { get; set; }
        private string ALName { get; set; }
        private string ISBN { get; set; }
        private string Genre { get; set; }
        private string Cat { get; set; }
        private string Publisher { get; set; }

        public AddCommand()
        {
            IsCommand("add", "Add a record.");
            HasAlias("add");
            HasLongDescription(@"
Add a new record to the database.

The ISBN is to follow the following format:
xxx-xxxxx-xxxxxxx-xxxxxx-x
      |      |      |
      |      |      |
      |      |      --> Between 1-6 digits long
      |      --> Between 1-7 digits long
      --> Between 1-5 digits long");
        }

        public override int Run(string[] remainingArguments)
        {
            // Get user input
            Desktop.DrawDesktop();
            Console.Write("Title: > ");
            Title = Console.ReadLine();
            Desktop.DrawDesktop();
            Console.Write("Author's First Name: > ");
            AFName = Console.ReadLine();
            Desktop.DrawDesktop();
            Console.Write("Author's Last Name: > ");
            ALName = Console.ReadLine();
            Desktop.DrawDesktop();
            Console.Write("ISBN: > ");
            ISBN = Console.ReadLine();
            Desktop.DrawDesktop();
            Console.Write("Publisher: > ");
            Publisher = Console.ReadLine();
            Desktop.DrawDesktop();
            Console.Write("Genre: > ");
            Genre = Console.ReadLine();
            Desktop.DrawDesktop();
            Console.Write("Type: > ");
            Cat = Console.ReadLine();


            if (!Book.ValidateISBN(ISBN)) Desktop.SendToWorkspace("cmd", "ERROR: Invalid ISBN.");
            else Desktop.SendToWorkspace("cmd", CommandBot.library.NewBook(Title, AFName, ALName, Book.ParseISBN(ISBN), Publisher, (_GenreEnum)Convert.ToInt16(Genre), (_TypeEnum)Convert.ToInt16(Cat)));
            Desktop.DrawDesktop();
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
            HasLongDescription(@"Delete a new record to the database.");
        }

        public override int Run(string[] remainingArguments)
        {
            //TODO: Add functionality
            Desktop.SendToWorkspace("info", "Delete command entered");
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
Expected usage at the CLI: modify <options>");
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

    public class FetchCommand : ConsoleCommand
    {
        public string Title { get; set; }
        public string AFName { get; set; }
        public string ALName { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Cat { get; set; }
        public string Publisher { get; set; }

        public FetchCommand()
        {
            IsCommand("fetch", "Fetch and display a record.");
            HasAlias("fetch");
            HasLongDescription(@"
Fetch and display a record.
Expected usage at the CLI: fetch <options>");
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
            Desktop.DrawDesktop();
            var query = ConstructQuery();

            bool noMatch = false;
            Book result = new Book();

            if (query.Count == 0)
            {

                int blockSize = 7;
                int workspaceSize = Desktop.Workspaces["info"].WorkspaceHeight;
                int chunk = 0;
                string results = "";

                foreach (Book book in CommandBot.library.Catalogue.Values)
                {
                    if (chunk + blockSize > workspaceSize)
                    {
                        Desktop.Workspaces["info"].FlushBuffer();
                        Desktop.SendToWorkspace("info", results);
                        Desktop.SendToWorkspace("cmd", "Press Any Key to Continue...");
                        Desktop.DrawDesktop();
                        Console.ReadKey(true);
                        results = $"Title:\t {book.Title}\n" +
                        $"Author:\t {book.AuthorFirstName}, {book.AuthorLastName}\n" +
                        $"ISBN:\t {book.ISBN.ToString()}\n" +
                        $"Publisher:\t {book.Publisher}\n" +
                        $"Genre:\t {book.Genre}\n" +
                        $"Category:\t {(_TypeEnum)book.Type}\n\n";
                        chunk = blockSize;
                    }
                    else
                    {
                        chunk += blockSize;
                        results += $"Title:\t {book.Title}\n" +
                        $"Author:\t {book.AuthorFirstName}, {book.AuthorLastName}\n" +
                        $"ISBN:\t {book.ISBN.ToString()}\n" +
                        $"Publisher:\t {book.Publisher}\n" +
                        $"Genre:\t {book.Genre}\n" +
                        $"Category:\t {(_TypeEnum)book.Type}\n\n";
                    }
                }
                Desktop.Workspaces["info"].FlushBuffer();
                Desktop.Workspaces["cmd"].FlushBuffer();
                Desktop.SendToWorkspace("info", results);
                Desktop.DrawDesktop();
            }
            else
            {
                foreach (Book book in CommandBot.library.Catalogue.Values)
                {
                    byte count = 1;
                    noMatch = false;

                    foreach (var arg in query)
                    {
                        if (noMatch) break;
                        switch (arg.Item1)
                        {
                            case "Title":
                                if (book.Title != arg.Item2) noMatch = true;
                                break;
                            case "AFName":
                                if (book.AuthorFirstName != arg.Item2) noMatch = true;
                                break;
                            case "ALName":
                                if (book.AuthorLastName != arg.Item2) noMatch = true;
                                break;
                            case "ISBN":
                                if (!book.ISBN.Equals(Book.ParseISBN(arg.Item2))) noMatch = true;
                                break;
                            case "Genre":
                                if (!book.Genre.Equals(arg.Item2)) noMatch = true;
                                break;
                            case "Cat":
                                if (!book.Type.Equals(arg.Item2)) noMatch = true;
                                break;
                            case "Publisher":
                                if (book.Publisher != arg.Item2) noMatch = true;
                                break;
                        }
                    }
                    if (count == query.Count && !noMatch)
                    {
                        result = book;
                        break;
                    }
                }
                if (noMatch) Desktop.SendToWorkspace("cmd", "ERROR: Record not found.");
                else
                {
                    Desktop.Workspaces["info"].FlushBuffer();
                    Desktop.SendToWorkspace("info",
                        $"Title:\t {result.Title}\n" +
                        $"Author:\t {result.AuthorFirstName}, {result.AuthorLastName}\n" +
                        $"ISBN:\t {result.ISBN.ToString()}\n" +
                        $"Publisher:\t {result.Publisher}\n" +
                        $"Genre:\t {result.Genre}\n" +
                        $"Category:\t {(_TypeEnum)result.Type}\n");
                }
            }
            return 0;
        }

     private List<(string, string)> ConstructQuery()
        { 
            List<(string, string)> query = new List<(string, string)>();

            if (!String.IsNullOrEmpty(Title)) query.Add(("Title", Title));
            if (!String.IsNullOrEmpty(AFName)) query.Add(("AFName", AFName));
            if (!String.IsNullOrEmpty(ALName)) query.Add(("ALNAME", ALName));
            if (!String.IsNullOrEmpty(ISBN)) query.Add(("ISBN", ISBN));
            if (!String.IsNullOrEmpty(Genre)) query.Add(("Genre", Genre));
            if (!String.IsNullOrEmpty(Cat)) query.Add(("Cat", Cat));
            if (!String.IsNullOrEmpty(Publisher)) query.Add(("Publisher", Publisher));

            return query;
        }
    }
}
