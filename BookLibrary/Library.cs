﻿// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: Library class
//  Version: 1.0.0
//
//  Description: Contains the Catalogue of Book objects.
//
//
// Library.cs
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
    public class Library
    {
        public Dictionary<_ISBNStruct, Book> Catalogue;

        public Library()
        {
            Catalogue = new Dictionary<_ISBNStruct, Book>();
        }

        /// <summary>
        /// Create and add a new book to the catalogue
        /// </summary>
        /// <param name="title">Book Title</param>
        /// <param name="afname">Author's First Name</param>
        /// <param name="alname">Author's Last Name</param>
        /// <param name="isbn">ISBN</param>
        /// <param name="publisher">Publisher Name</param>
        /// <param name="genre">Literary Genre</param>
        /// <param name="type">Litereary Category</param>
        /// <returns>Result string</returns>
        public string NewBook(string title, string afname, string alname, _ISBNStruct isbn, string publisher, _GenreEnum genre, _TypeEnum type)
        {
            try
            {
                Catalogue.Add(isbn, new Book(title, afname, alname, isbn, publisher, genre, type));
                return "Operation successful.";
            }
            catch (Exception e)
            {
                //TODO: Give option to overwrite existing record
                return $"ERROR: Failed to create. - {e.Message}";
            }
        }
    }
}
