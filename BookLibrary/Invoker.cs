// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: Invoker class
//  Version: 1.0.0
//
//  Description: The invoker for the Command Design Pattern.
//               It tells the Commands to execute their actions.
//
//
// Invoker.cs
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

namespace BookLibrary
{
    /// <summary>
    /// The <c>Invoker</c> class tells the <c>Command</c> class
    /// to execute their actions.
    /// </summary>
    public class Invoker
    {
        private Command _command;
        private string[] _args;
        private string _id;

        /// <summary>
        /// Parse arguments to be passed to the command.
        /// </summary>
        /// <param name="command">Command Name</param>
        /// <param name="args">Array of Arguments</param>
        /// <param name="id">Workstation ID</param>
        public void SetCommand(Command command, string[] args, string id)
        {
            this._command = command;
            this._args = args;
            this._id = id;
        }

        public void ExecuteCommand() => _command.Execute(_args, _id);
    }
}
