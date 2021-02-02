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
    public static class Display
    {
        /// <summary>
        /// Center align string
        /// </summary>
        /// <param name="s">String to align</param>
        /// <param name="width">Width if line to center on</param>
        /// <returns>Padded string</returns>
        public static string CenterAligned(string s, int width)
        {
            if (s.Length >= width)
            {
                return s;
            }

            int leftPadding = (width - s.Length) / 2;
            int rightPadding = width - s.Length - leftPadding;

            return new string(' ', leftPadding) + s + new string(' ', rightPadding);
        }

        /// <summary>
        /// Shift string to the right
        /// </summary>
        /// <param name="s">String to shift</param>
        /// <param name="width">Number of spaces to shift</param>
        /// <returns>Padded string</returns>
        public static string AddPadding(string s, int width)
        {
            return new string(' ', width) + s;
        }
    }
}
