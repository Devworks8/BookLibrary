// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: Command Definitions
//  Version: 1.0.0
//
//  Description: Logic for all commands when executed.
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
    /// <summary>
    /// Help Command
    /// </summary>
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

    /// <summary>
    /// Quit Command
    /// </summary>
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

    /// <summary>
    /// List Command
    /// </summary>
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
                // List Genres
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
                // List Categories
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

    /// <summary>
    /// Menu Command
    /// </summary>
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

    /// <summary>
    /// Add Command
    /// </summary>
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

            var genres = Enum.GetValues(typeof(_GenreEnum)).Cast<_GenreEnum>();

            Desktop.Workspaces["menu"].FlushBuffer();
            Desktop.SendToWorkspace("menu", "Code \t<-> \tDescription\n");

            byte count = 0;
            foreach (var value in genres)
            {

                Desktop.SendToWorkspace("menu", $"{count} \t<-> \t" + value.ToString());
                count++;
            }

            Desktop.DrawDesktop();
            Console.Write("Genre: > ");
            Genre = Console.ReadLine();

            var types = Enum.GetValues(typeof(_TypeEnum)).Cast<_TypeEnum>();
            Desktop.Workspaces["menu"].FlushBuffer();
            Desktop.SendToWorkspace("menu", "Code \t<-> \tDescription\n");

            count = 0;
            foreach (var value in types)
            {
                Desktop.SendToWorkspace("menu", $"{count} \t<-> \t" + value.ToString());
                count++;
            }

            Desktop.DrawDesktop();
            Console.Write("Type: > ");
            Cat = Console.ReadLine();


            if (!Book.ValidateISBN(ISBN)) Desktop.SendToWorkspace("cmd", "ERROR: Invalid ISBN."); // Send error message if invalid ISBN
            else Desktop.SendToWorkspace("cmd", CommandBot.library.NewBook(Title, AFName, ALName, Book.ParseISBN(ISBN), Publisher, (_GenreEnum)Convert.ToInt16(Genre), (_TypeEnum)Convert.ToInt16(Cat)));
            Desktop.DrawDesktop();
            return 0;
        }
    }

    /// <summary>
    /// Delete Command
    /// </summary>
    public class DeleteCommand : ConsoleCommand
    {
        public string Title { get; set; }
        public string AFName { get; set; }
        public string ALName { get; set; }
        public string ISBN { get; set; }
        public string Genre { get; set; }
        public string Cat { get; set; }
        public string Publisher { get; set; }
        public bool All { get; set; }

        public DeleteCommand()
        {
            IsCommand("delete", "Delete a record.");
            HasAlias("delete");
            HasLongDescription(@"Delete a record from the database.");
            HasOption("t|title=", "Title.", t => Title = t);
            HasOption("f|first=", "Author's first name.", f => AFName = f);
            HasOption("l|last=", "Author's last name.", l => ALName = l);
            HasOption("i|isbn=", "ISBN.", i => ISBN = i);
            HasOption("g|genre=", "Literary genre.", g => Genre = g);
            HasOption("T|type=", "Literary type.", T => Cat = T);
            HasOption("p|publisher=", "Publisher name.", p => Publisher = p);
            HasOption("A|ALL", "Delete all records", A => All = true);
        }

        /// <summary>
        /// Create list of tuples containing the name and value of the arguments.
        /// </summary>
        /// <returns>List of tuples</returns>
        private List<(string, string)> ConstructQuery()
        {
            List<(string, string)> query = new List<(string, string)>();

            if (!String.IsNullOrEmpty(Title)) query.Add(("Title", Title));
            if (!String.IsNullOrEmpty(AFName)) query.Add(("AFName", AFName));
            if (!String.IsNullOrEmpty(ALName)) query.Add(("ALName", ALName));
            if (!String.IsNullOrEmpty(ISBN)) query.Add(("ISBN", ISBN));
            if (!String.IsNullOrEmpty(Genre)) query.Add(("Genre", Genre));
            if (!String.IsNullOrEmpty(Cat)) query.Add(("Cat", Cat));
            if (!String.IsNullOrEmpty(Publisher)) query.Add(("Publisher", Publisher));

            return query;
        }

        public override int Run(string[] remainingArguments)
        {
            Desktop.DrawDesktop();

            var query = ConstructQuery();
            int count = 0;
            bool noMatch = false;
            Book result = new Book();

            // No ISBN provided
            if (String.IsNullOrEmpty(ISBN))
            {
                // No arguments provided, display all records
                if (All)
                {
                    Desktop.Workspaces["cmd"].FlushBuffer();
                    // Get user input
                    Desktop.DrawDesktop();
                    int total = CommandBot.library.Catalogue.Count;
                    Console.Write($"{total} Record(s) will be deleted. Proceed [y|N]: > ");
                    string answer = Console.ReadLine();
                    if (answer.ToLower() == "y")
                    {
                        CommandBot.library.Catalogue.Clear();
                        Desktop.SendToWorkspace("cmd", $"Operation Complete. - {total} records deleted");
                    }
                    else Desktop.SendToWorkspace("cmd", "Operation Aborted.");
                    
                    Desktop.DrawDesktop();
                }
                // Parse arguments
                else
                {
                    // Create a list of ISBNs to delete
                    List<_ISBNStruct> results = new List<_ISBNStruct>();

                    foreach (Book book in CommandBot.library.Catalogue.Values)
                    {
                        noMatch = false;
                        byte argCount = 0;

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
                            argCount++;
                        }
                        if (argCount == query.Count && !noMatch)
                        {
                            count++;
                            results.Add(book.ISBN); // Add ISBN of books that match criterias
                        }
                    }

                    if (results.Count == 0) Desktop.SendToWorkspace("cmd", "ERROR: Record(s) not found.");
                    else
                    {
                        Desktop.Workspaces["cmd"].FlushBuffer();

                        // Get user input
                        Desktop.DrawDesktop();
                        Console.Write($"{count} Record(s) will be deleted. Proceed [y|N]: > ");
                        string answer = Console.ReadLine();

                        if (answer.ToLower() == "y")
                        {
                            foreach(var rslt in results)
                            {
                                // Delete records
                                CommandBot.library.Catalogue.Remove(rslt);
                            }
                            Desktop.SendToWorkspace("cmd", $"Operation Complete. - {count} records deleted");
                        }
                        else Desktop.SendToWorkspace("cmd", "Operation Aborted.");

                        Desktop.DrawDesktop();
                    }
                }
            }
            else
            {
                if (Book.ValidateISBN(ISBN))
                {
                    Desktop.Workspaces["cmd"].FlushBuffer();
                    Desktop.Workspaces["info"].FlushBuffer();

                    try
                    {
                        Desktop.Workspaces["cmd"].FlushBuffer();

                        // Get user input
                        Desktop.DrawDesktop();
                        Console.Write($"ISBN {ISBN} will be deleted. Proceed [y|N]: > ");
                        string answer = Console.ReadLine();

                        if (answer.ToLower() == "y")
                        {
                            // Delete record
                            CommandBot.library.Catalogue.Remove(Book.ParseISBN(ISBN));
                            Desktop.SendToWorkspace("cmd", $"Operation Complete. - ISBN {ISBN} deleted");
                        }
                        else Desktop.SendToWorkspace("cmd", "Operation Aborted.");
                    }
                    catch (Exception e)
                    {
                        Desktop.SendToWorkspace("cmd", $"ERROR: Operation Failed. - {e.Message}");
                    }
                }
                else
                {
                    Desktop.SendToWorkspace("cmd", "ERROR: Invalid ISBN.");
                    Desktop.DrawDesktop();
                }
            }
            return 0;
        }
    }

    /// <summary>
    /// Modify Command
    /// </summary>
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
The ISBN is required.");
            HasOption("t|title=", "Title.", t => Title = t);
            HasOption("f|first=", "Author's first name.", f => AFName = f);
            HasOption("l|last=", "Author's last name.", l => ALName = l);
            HasOption("i|isbn=", "ISBN.", i => ISBN = i);
            HasOption("g|genre=", "Literary genre.", g => Genre = g);
            HasOption("T|type=", "Literary type.", T => Cat = T);
            HasOption("p|publisher=", "Publisher name.", p => Publisher = p);
        }

        /// <summary>
        /// Create list of tuples containing the name and value of the arguments.
        /// </summary>
        /// <returns>List of tuples</returns>
        private List<(string, string)> ConstructQuery()
        {
            List<(string, string)> query = new List<(string, string)>();

            if (!String.IsNullOrEmpty(Title)) query.Add(("Title", Title));
            if (!String.IsNullOrEmpty(AFName)) query.Add(("AFName", AFName));
            if (!String.IsNullOrEmpty(ALName)) query.Add(("ALName", ALName));
            if (!String.IsNullOrEmpty(ISBN)) query.Add(("ISBN", ISBN));
            if (!String.IsNullOrEmpty(Genre)) query.Add(("Genre", Genre));
            if (!String.IsNullOrEmpty(Cat)) query.Add(("Cat", Cat));
            if (!String.IsNullOrEmpty(Publisher)) query.Add(("Publisher", Publisher));

            return query;
        }

        public override int Run(string[] remainingArguments)
        {
            var query = ConstructQuery();

            Desktop.Workspaces["cmd"].FlushBuffer();

            if (String.IsNullOrEmpty(ISBN))
            {
                Desktop.SendToWorkspace("cmd", "ERROR: ISBN required.");
                Desktop.DrawDesktop();
            }
            else
            {
                try
                {
                    string original;
                    string modified;

                    if(Book.ValidateISBN(ISBN))
                    {
                        original = $"Original\n\n" +
                            $"Title:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Title}\n" +
                            $"Author:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].AuthorFirstName}, {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].AuthorLastName}\n" +
                            $"ISBN:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].ISBN.ToString()}\n" +
                            $"Publisher:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Publisher}\n" +
                            $"Genre:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Genre}\n" +
                            $"Category:\t {(_TypeEnum)CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Type}\n\n";

                        // Generate custom Modified string based of changes
                        modified = $"Modified\n\n";
                        modified += String.IsNullOrEmpty(Title) ? $"Title:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Title}\n" : $"Title:\t {Title}\n";
                        modified += String.IsNullOrEmpty(AFName) ? $"Author:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].AuthorFirstName}, " : $"Author:\t {AFName}, ";
                        modified += String.IsNullOrEmpty(ALName) ? $"{CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].AuthorLastName}" : $"{ALName}\n";
                        modified += String.IsNullOrEmpty(ISBN) ? $"ISBN:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].ISBN.ToString()}\n" : $"ISBN:\t {ISBN}\n";
                        modified += String.IsNullOrEmpty(Publisher) ? $"Publisher:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Publisher}\n" : $"Publisher:\t {Publisher}\n";
                        modified += String.IsNullOrEmpty(Title) ? $"Genre:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Genre}\n" : $"Genre:\t {Genre}\n";
                        modified += String.IsNullOrEmpty(Title) ? $"Category:\t {(_TypeEnum)CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Type}\n\n" : $"Category:\t {Cat}\n\n";

                        Desktop.Workspaces["info"].FlushBuffer();
                        Desktop.SendToWorkspace("info", original + modified);

                        // Get user input
                        Desktop.DrawDesktop();
                        Console.Write($"Proceed with modification [y|N]: > ");
                        string answer = Console.ReadLine();

                        if (answer.ToLower() == "y")
                        {
                            foreach (var arg in query)
                            {
                                switch (arg.Item1)
                                {
                                    case "Title":
                                        CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Title = Title;
                                        break;
                                    case "AFName":
                                        CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].AuthorFirstName = AFName;
                                        break;
                                    case "ALName":
                                        CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].AuthorLastName = ALName;
                                        break;
                                    case "Genre":
                                        CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Genre = (_GenreEnum)Convert.ToInt16(Genre);
                                        break;
                                    case "Cat":
                                        CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Type = (_TypeEnum)Convert.ToInt16(Cat);
                                        break;
                                    case "Publisher":
                                        CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Publisher = Publisher;
                                        break;
                                }
                            }

                            Desktop.SendToWorkspace("cmd", "Operation Complete.");
                            Desktop.DrawDesktop();
                        }
                        else
                        {
                            Desktop.SendToWorkspace("cmd", "Operation Aborted.");
                            Desktop.DrawDesktop();
                        }
                    }
                    else
                    {
                        Desktop.SendToWorkspace("cmd", "ERROR: Invalid ISBN.");
                    }

                }
                catch (Exception e)
                {
                    Desktop.SendToWorkspace("cmd", $"ERROR: Operation Failed. - {e.Message}");
                    Desktop.DrawDesktop();
                }
            }
            return 0;
        }
    }

    /// <summary>
    /// Fetch Command
    /// </summary>
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

        /// <summary>
        /// Create list of tuples containing the name and value of the arguments.
        /// </summary>
        /// <returns>List of tuples</returns>
        private List<(string, string)> ConstructQuery()
        {
            List<(string, string)> query = new List<(string, string)>();

            if (!String.IsNullOrEmpty(Title)) query.Add(("Title", Title));
            if (!String.IsNullOrEmpty(AFName)) query.Add(("AFName", AFName));
            if (!String.IsNullOrEmpty(ALName)) query.Add(("ALName", ALName));
            if (!String.IsNullOrEmpty(ISBN)) query.Add(("ISBN", ISBN));
            if (!String.IsNullOrEmpty(Genre)) query.Add(("Genre", Genre));
            if (!String.IsNullOrEmpty(Cat)) query.Add(("Cat", Cat));
            if (!String.IsNullOrEmpty(Publisher)) query.Add(("Publisher", Publisher));

            return query;
        }

        public override int Run(string[] remaingArguments)
        {
            Desktop.DrawDesktop();
            var query = ConstructQuery();

            int count = 0;
            bool noMatch = false;
            Book result = new Book();

            // No ISBN provided
            if(String.IsNullOrEmpty(ISBN))
            {
                // No arguments provided, display all records
                if (query.Count == 0)
                {

                    int blockSize = 7;
                    int workspaceSize = Desktop.Workspaces["info"].WorkspaceHeight;
                    int chunk = 0;
                    string results = "";

                    foreach (Book book in CommandBot.library.Catalogue.Values)
                    {
                        // Display results when workspace buffer is full
                        if (chunk + blockSize > workspaceSize)
                        {
                            Desktop.Workspaces["info"].FlushBuffer();
                            Desktop.SendToWorkspace("info", results);
                            Desktop.SendToWorkspace("cmd", "Press Any Key to Continu...");
                            Desktop.DrawDesktop();
                            Console.ReadKey(true);
                            results = $"Title:\t {book.Title}\n" +
                            $"Author:\t {book.AuthorFirstName}, {book.AuthorLastName}\n" +
                            $"ISBN:\t {book.ISBN.ToString()}\n" +
                            $"Publisher:\t {book.Publisher}\n" +
                            $"Genre:\t {book.Genre}\n" +
                            $"Category:\t {(_TypeEnum)book.Type}\n\n";
                            chunk = blockSize;
                            count++;
                        }
                        // Add to workspace buffer
                        else
                        {
                            chunk += blockSize;
                            results += $"Title:\t {book.Title}\n" +
                            $"Author:\t {book.AuthorFirstName}, {book.AuthorLastName}\n" +
                            $"ISBN:\t {book.ISBN.ToString()}\n" +
                            $"Publisher:\t {book.Publisher}\n" +
                            $"Genre:\t {book.Genre}\n" +
                            $"Category:\t {(_TypeEnum)book.Type}\n\n";
                            count++;
                        }
                    }
                    Desktop.Workspaces["info"].FlushBuffer();
                    Desktop.Workspaces["cmd"].FlushBuffer();
                    Desktop.SendToWorkspace("cmd", $"Operation Complete. - {count} record(s) found");
                    Desktop.SendToWorkspace("info", results);
                    Desktop.DrawDesktop();
                }
                // Parse arguments
                else
                {
                    string results = "";

                    foreach (Book book in CommandBot.library.Catalogue.Values)
                    {
                        noMatch = false;
                        byte argCount = 0;

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
                            argCount++;
                        }

                        if (argCount == query.Count && !noMatch) // Match found
                        {
                            count++;
                            results += $"Title:\t {book.Title}\n" +
                                $"Author:\t {book.AuthorFirstName}, {book.AuthorLastName}\n" +
                                $"ISBN:\t {book.ISBN.ToString()}\n" +
                                $"Publisher:\t {book.Publisher}\n" +
                                $"Genre:\t {result.Genre}\n" +
                                $"Category:\t {(_TypeEnum)book.Type}\n\n";
                        }
                    }

                    if (String.IsNullOrEmpty(results)) Desktop.SendToWorkspace("cmd", "ERROR: Record not found.");
                    else
                    {
                        Desktop.Workspaces["cmd"].FlushBuffer();
                        Desktop.Workspaces["info"].FlushBuffer();
                        Desktop.SendToWorkspace("cmd", $"Operation Complete. - {count} record(s) found");
                        Desktop.SendToWorkspace("info", results);
                        Desktop.DrawDesktop();
                    }
                }
            }
            else
            {
                // ISBN provided, skip search and get book using the ISBN key
                if (Book.ValidateISBN(ISBN))
                {
                    Desktop.Workspaces["cmd"].FlushBuffer();
                    Desktop.Workspaces["info"].FlushBuffer();

                    try
                    {
                        Desktop.SendToWorkspace("info", $"Title:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Title}\n" +
                                    $"Author:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].AuthorFirstName}, {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].AuthorLastName}\n" +
                                    $"ISBN:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].ISBN.ToString()}\n" +
                                    $"Publisher:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Publisher}\n" +
                                    $"Genre:\t {CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Genre}\n" +
                                    $"Category:\t {(_TypeEnum)CommandBot.library.Catalogue[Book.ParseISBN(ISBN)].Type}\n\n");
                        Desktop.SendToWorkspace("cmd", "Operation Complete. - 1 record found");
                    }
                    catch (Exception e)
                    {
                        Desktop.SendToWorkspace("cmd", $"ERROR: Operation Failed. - {e.Message}");
                    }
                }
                else
                {
                    Desktop.SendToWorkspace("cmd", "ERROR: Invalid ISBN.");
                    Desktop.DrawDesktop();
                }
            }
            return 0;
        }
    }
}
