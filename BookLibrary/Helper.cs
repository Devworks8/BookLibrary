// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: Helper class
//  Version: 1.0.0
//
//  Description: Collection of helper methods
//
//
// Helper.cs
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
    public static class User
    {
        /// <summary>
        /// Display message to user in a specific color.
        /// </summary>
        /// <param name="msg">Message to display</param>
        /// <param name="color">Color to use. White is the default</param>
        public static void ShowMessage(string msg, string color="")
        {
            if (!string.IsNullOrEmpty(color))
            {
                Console.ForegroundColor = (ConsoleColor) Enum.Parse(typeof(ConsoleColor), color);
                Console.WriteLine(msg);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else Console.WriteLine(msg);
        }

    }
}
