// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: 
//  Version: 
//
//  Description: 
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

        public void SetCommand(Command command, string[] args, string id)
        {
            this._command = command;
            this._args = args;
            this._id = id;
        }

        public void ExecuteCommand() => _command.Execute(_args, _id);
    }
}
