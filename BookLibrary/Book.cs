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
        public int Prefix { get; set; }
        public int Group { get; set; }
        public int Registrant { get; set; }
        public int Publication { get; set; }
        public int Check { get; set; }

       public override string ToString()
        {
            return $"{Prefix}-{Group}-{Registrant}-{Publication}-{Check}";
        }
    }

    // Literary Genres
    public enum _GenreEnum
    {
        BiographyAutobiography,
        Drama,
        Essay,
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
        NarrativeNonfiction,
        Nonfiction,
        Poetry,
        RealisticFiction,
        ScienceFiction,
        ShortStory,
        Speech,
        TallTale,
    }

    // Literary Categories
    public enum _TypeEnum
    {
        Audio,
        Hardcover,
        Magazine,
        Paperback 
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

        /// <summary>
        /// Book object
        /// </summary>
        /// <param name="title">Book Title</param>
        /// <param name="afname">Author's First Name</param>
        /// <param name="alname">Author's Last Name</param>
        /// <param name="isbn">ISBN</param>
        /// <param name="publisher">Publisher Name</param>
        /// <param name="genre">Literary Genre</param>
        /// <param name="type">Literary Category</param>
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

        /// <summary>
        /// Validate ISBN format
        /// </summary>
        /// <param name="isbn">ISBN</param>
        /// <returns>True if valid</returns>
        static public bool ValidateISBN(string isbn)
        {
            string[] Parts = isbn.Split('-');

            int counter = 1;
            foreach (string part in Parts)
            {
                switch (counter)
                {
                    case 1:
                        if (part.Length != 3) return false;
                        break;
                    case 2:
                        if (part.Length < 1 || part.Length > 5) return false;
                        break;
                    case 3:
                        if (part.Length < 1 || part.Length > 7) return false;
                        break;
                    case 4:
                        if (part.Length < 1 || part.Length > 6) return false;
                        break;
                    case 5:
                        if (part.Length != 1) return false;
                        break;
                }
                counter++;
            }
            return true;
        }

        /// <summary>
        /// Convert ISBN string to ISBNStruct
        /// </summary>
        /// <param name="isbn">ISBN</param>
        /// <returns>ISBNStruct</returns>
        static public _ISBNStruct ParseISBN(string isbn)
        {
            _ISBNStruct parsedISBN = new _ISBNStruct();
            string[] Parts = isbn.Split('-');

            int counter = 1;
            foreach(string part in Parts)
            {
                switch (counter)
                {
                    case 1:
                        parsedISBN.Prefix = Convert.ToInt32(part);
                        break;
                    case 2:
                        parsedISBN.Group = Convert.ToInt32(part);
                        break;
                    case 3:
                        parsedISBN.Registrant = Convert.ToInt32(part);
                        break;
                    case 4:
                        parsedISBN.Publication = Convert.ToInt32(part);
                        break;
                    case 5:
                        parsedISBN.Check = Convert.ToInt32(part);
                        break;
                }
                counter++;
            }

            return parsedISBN;
        }
    }
}
