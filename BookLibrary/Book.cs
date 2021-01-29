// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: Book Class
//  Version: 1.0.0
//
//  Description: Book class implementation
//
//
// Book.cs
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
namespace BookLibrary
{
    // ISBN struct, seperating it's key components to ease parsing and validation.
    public struct _ISBNStruct
    {
        public int Prefix { get; }
        public int Group { get; }
        public int Registrant { get; }
        public int Publication { get; }
        public int Check { get; }

        public _ISBNStruct(int prefix, int group, int registrant, int publication, int check)
        {
            this.Prefix = prefix;
            this.Group = group;
            this.Registrant = registrant;
            this.Publication = publication;
            this.Check = check;
        }
    }

    // Literary Genres
    public enum _GenreEnum
    {
        Drama,
        Fable,
        Fantasy,
        Fiction,
        FictionInVerse,
        Folklore,
        HistoricalFiction,
        Horror,
        Legend,
        Mystery,
        Mythology,
        Poetry,
        RealisticFiction,
        ScienceFiction,
        ShortStory,
        TallTale,
        BiographyAutobiography,
        Essay,
        NarrativeNonfiction,
        Nonfiction,
        Speech
    }

    // Literary Categories
    public enum _TypeEnum
    {
        Fiction,
        NonFiction
    }

    public class Book
    {
        private _ISBNStruct _ISBN;
        private _GenreEnum _Genre;
        private _TypeEnum _Type;
        

        private string _Title;
        public string Title
        {
            get => _Title;
            set => _Title = value;
        }

        private string _AuthorFirstName;
        public string AuthorFirstName
        {
            get => _AuthorFirstName;
            set => _AuthorFirstName = value;
        }

        private string _AuthorLastName;
        public string AuthorLastName
        {
            get => _AuthorLastName;
            set => _AuthorLastName = value;
        }

        // Return concatenated author name
        public string Author
        {
            get => $"{_AuthorFirstName}, {_AuthorLastName}";
        }

        public _ISBNStruct ISBN
        {
            get => _ISBN;
            set => _ISBN = value;
        }

        private string _Publisher;
        public string Publisher
        {
            get => _Publisher;
            set => _Publisher = value;
        }

        public _GenreEnum Genre
        {
            get => _Genre;
            set => _Genre = value;
        }

        public _TypeEnum Type
        {
            get => _Type;
            set => _Type = value;
        }

        public Book()
        {
        }

        public Book(string title, string afname, string alname, _ISBNStruct isbn, string publisher, _GenreEnum genre, _TypeEnum type)
        {
            Title = title;
            AuthorFirstName = afname;
            AuthorLastName = alname;
            ISBN = isbn;
            Publisher = publisher;
            Genre = genre;
            Type = type;
        }
    }
}
