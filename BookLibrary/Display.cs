﻿// Name: Christian Lachapelle
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

        public Workspace(Point origin, int width, int height, int padding=0)
        {
            WorkspaceOrigin = origin;
            WorkspaceWidth = Convert.ToInt16(Convert.ToDouble(Console.WindowWidth) / 100 * width);
            WorkspaceHeight = Convert.ToInt16(Convert.ToDouble(Console.WindowHeight) / 100 * height);
            Padding = padding;
            FiFoBuffer = new Queue<string>();
        }

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

        public void UpdateBuffer(string msg)
        {
            if (FiFoBuffer.Count == WorkspaceHeight)
            {
                var _ = FiFoBuffer.Dequeue();
                FiFoBuffer.Enqueue(msg);
            }
            else FiFoBuffer.Enqueue(msg);
        }

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

        public Desktop()
        {
            ConsoleWidth = Console.WindowWidth;
            ConsoleHeight = Console.WindowHeight;
        }

        public static void AddWorkspace(string id, Workspace workspace)
        {
            Workspaces.Add(id, workspace);
        }

        public static void SendToWorkspace(string id, string msg)
        {
            if (Workspaces.ContainsKey(id))
            {
                foreach (var line in msg.Split('\n'))
                {
                    Workspaces[id].UpdateBuffer(line);
                }
            }
        }

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
