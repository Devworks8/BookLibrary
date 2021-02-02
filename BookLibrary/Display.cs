// Name: Christian Lachapelle
//  Student #: A00230066
//
//  Title: Display class
//  Version: 1.0.0
//
//  Description: Segment the display into workspaces.
///              Encapsulated into a Desktop class.
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
    public struct Point
    {
        public int X;
        public int Y;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class Workspace
    {
        public Point WorkspaceOrigin { get; }
        public int WorkspaceWidth { get; }
        public int WorkspaceHeight { get; }
        public int Padding { get; }
        public Queue<string> FiFoBuffer;

        /// <summary>
        /// Instantiate a new Workspace
        /// </summary>
        /// <param name="origin">Top left corner coordinate</param>
        /// <param name="width">Workspace width percentage of window</param>
        /// <param name="height">Workspace height percentage of window</param>
        /// <param name="padding">Left border padding</param>
        public Workspace(Point origin, int width, int height, int padding=0)
        {
            WorkspaceOrigin = origin;
            WorkspaceWidth = Convert.ToInt16(Convert.ToDouble(Console.WindowWidth) / 100 * width);
            WorkspaceHeight = Convert.ToInt16(Convert.ToDouble(Console.WindowHeight) / 100 * height);
            Padding = padding;
            FiFoBuffer = new Queue<string>();
        }

        // Print buffer content to screen
        public void PrintBuffer()
        {
            int counter = 1;
            foreach (String msg in FiFoBuffer)
            {
                Console.WriteLine(Display.AddPadding(msg, Padding));
                Console.SetCursorPosition(WorkspaceOrigin.X, WorkspaceOrigin.Y+counter);
                counter++;
            }
        }

        // Add to buffer
        public void UpdateBuffer(string msg)
        {
            if (FiFoBuffer.Count == WorkspaceHeight)
            {
                // Make room in buffer for new content.
                var _ = FiFoBuffer.Dequeue();
                FiFoBuffer.Enqueue(msg);
            }
            else FiFoBuffer.Enqueue(msg);
        }

        // Reset buffer content.
        public void FlushBuffer()
        {
            FiFoBuffer.Clear();
        }
    }

    public class Desktop
    {
        public static Dictionary<string, Workspace> Workspaces = new Dictionary<string, Workspace>();
        public int ConsoleWidth { get; }
        public int ConsoleHeight { get; }

        /// <summary>
        /// Create new Desktop and get window height and width
        /// </summary>
        public Desktop()
        {
            ConsoleWidth = Console.WindowWidth;
            ConsoleHeight = Console.WindowHeight;
        }

        /// <summary>
        /// Add new workspace to the desktop.
        /// </summary>
        /// <param name="id">Workspace ID</param>
        /// <param name="workspace">Workspace object</param>
        public static void AddWorkspace(string id, Workspace workspace)
        {
            Workspaces.Add(id, workspace);
        }

        /// <summary>
        /// Send data to a workspace
        /// </summary>
        /// <param name="id">Workspace ID</param>
        /// <param name="msg">String to send.</param>
        public static void SendToWorkspace(string id, string msg)
        {
            if (Workspaces.ContainsKey(id))
            {
                // Each line has its own buffer location.
                foreach (var line in msg.Split('\n'))
                {
                    Workspaces[id].UpdateBuffer(line);
                }
            }
        }

        /// <summary>
        /// Update the screen.
        /// </summary>
        public static void DrawDesktop()
        {
            Console.Clear();
            foreach(KeyValuePair<string, Workspace> kvp in Workspaces)
            {
                Console.SetCursorPosition(kvp.Value.WorkspaceOrigin.X, kvp.Value.WorkspaceOrigin.Y);

                kvp.Value.PrintBuffer();
            }
        }
    }
}
