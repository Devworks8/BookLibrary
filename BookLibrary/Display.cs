// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: Display class
//  Version: 1.0.0
//
//  Description: Display format class
//
//
// Display.cs
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
    public struct Origin
    {
        public int X;
        public int Y;

        public Origin(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Workspace
    {
        public Origin CursorPosition { get; set; }
        public Origin DisplayOrigin { get; }
        public int DisplayWidth { get; }
        public int DisplayHeight { get; }

        public Workspace(Origin origin, int width, int height)
        {
            DisplayOrigin = origin;
            CursorPosition = origin;
            DisplayWidth = DisplayOrigin.X + width > Console.WindowWidth ? Console.WindowWidth : width;
            DisplayHeight = DisplayOrigin.Y + height > Console.WindowHeight ? Console.WindowHeight : height;
        }
    }

    public class Desktop
    {
        public Dictionary<string, Workspace> Workspaces = new Dictionary<string, Workspace>();
        public int ConsoleWidth = Console.WindowWidth;
        public int ConsoleHeight = Console.WindowHeight;

        public void AddWorkspace(string id, Workspace workspace)
        {
            Workspaces.Add(id, workspace);
        }

        public void SendToWorkspace(string id, string msg, string color="")
        {
            if (Workspaces.ContainsKey(id))
            {
                Console.SetCursorPosition(Workspaces[id].CursorPosition.X, Workspaces[id].CursorPosition.Y);
                User.ShowMessage(msg, color);
                Workspaces[id].CursorPosition = new Origin(Console.CursorLeft, Console.CursorTop);

            }
        }
    }
}
