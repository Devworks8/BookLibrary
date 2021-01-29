// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: Book Library
//  Version: 1.0.0
//
//  Description: Applied Activity 1 - Create a .NET Core console application
//               witht the purpose of being a database.
//
//
// Program.cs
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
    class MainClass
    {
        public static void Main(string[] args)
        {
            Desktop desktop = new Desktop();

            Workspace title = new Workspace(new Origin(0, 0), desktop.ConsoleWidth, 2);
            Workspace description = new Workspace(new Origin(0, title.DisplayHeight + 1), desktop.ConsoleWidth, 10);
            Workspace menu = new Workspace(new Origin(0, description.DisplayHeight + 1), desktop.ConsoleWidth / 2, 30);
            Workspace info = new Workspace(new Origin(menu.DisplayWidth + 1, description.DisplayHeight + 1), desktop.ConsoleWidth, 30);
            Workspace command = new Workspace(new Origin(0, info.DisplayHeight + 1), desktop.ConsoleWidth, desktop.ConsoleHeight);

            desktop.AddWorkspace("title", title);
            desktop.AddWorkspace("desc", description);
            desktop.AddWorkspace("menu", menu);
            desktop.AddWorkspace("info", info);
            desktop.AddWorkspace("command", command);

            desktop.SendToWorkspace("title", "Title", "Red");
            desktop.SendToWorkspace("desc", "This is the description panel...");
            desktop.SendToWorkspace("menu", "1) Item 1\n2) I tem 2\n3) Item 3");
            desktop.SendToWorkspace("info", "Information panel");
            desktop.SendToWorkspace("command", "Command panel");


            Console.ReadKey(true);
        }
    }
}
