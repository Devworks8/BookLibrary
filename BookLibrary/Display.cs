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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        public Origin CursorPosition;
        public Origin WorkspaceOrigin { get; }
        public int WorkspaceWidth { get; }
        public int WorkspaceHeight { get; }
        public Queue<string> FiFoBuffer { get; set; }

        public Workspace(Origin origin, int width, int height)
        {
            WorkspaceOrigin = origin;
            CursorPosition = origin;
            WorkspaceWidth = WorkspaceOrigin.X + width > Console.WindowWidth ? Console.WindowWidth - WorkspaceOrigin.X : width;
            WorkspaceHeight = WorkspaceOrigin.Y + height > Console.WindowHeight ? Console.WindowHeight - WorkspaceOrigin.Y : height;
            FiFoBuffer = new Queue<string>();
        }

        public void PrintBuffer()
        {
            foreach (Object msg in FiFoBuffer)
            {
                Console.WriteLine(msg);
                Console.SetCursorPosition(WorkspaceOrigin.X, CursorPosition.Y+1);
            }
        }
    }

    public class Desktop
    {
        public Dictionary<string, Workspace> Workspaces = new Dictionary<string, Workspace>();
        public int ConsoleWidth = Console.WindowWidth;
        public int ConsoleHeight = Console.WindowHeight;
        private bool _ShowBorder = true;
        public bool ShowBorder
        {
            get => _ShowBorder;
            set => _ShowBorder = value;
        }

        static IEnumerable<string> Split(string str, int chunckSize)
        {
            return Enumerable.Range(0, str.Length / chunckSize).Select(i => str.Substring(i * chunckSize, chunckSize));
        }

        public void AddWorkspace(string id, Workspace workspace)
        {
            Workspaces.Add(id, workspace);
        }

        public void SendToWorkspace(string id, string msg, string color="")
        {
            if (Workspaces.ContainsKey(id))
            {
                // Calculate total lines required
                var totalLines = msg.Length > Workspaces[id].WorkspaceWidth ? msg.Length / Workspaces[id].WorkspaceWidth : 0;
                string[] wordWrap;

                if (totalLines == 0)
                {
                    wordWrap = new string[1];
                }
                else
                {
                    wordWrap = new string[totalLines+1];
                }

                //FIXME: It's splitting the text, but only storring the first line in the buffer.
                // Split the message if needed to fit in the workspace.
                if (totalLines > 0)
                {
                    for (int i = 0; i < totalLines; i++)
                    {
                        wordWrap = Split(msg, Workspaces[id].WorkspaceWidth).ToArray();
                    }
                }
                else
                {
                    wordWrap[0] = msg;
                }

                // Add message to workspace buffer. Move text up if needed.
                if (Workspaces[id].FiFoBuffer.Count + totalLines <= Workspaces[id].WorkspaceHeight)
                {
                    foreach (string str in wordWrap)
                    {
                        Workspaces[id].FiFoBuffer.Enqueue(str);
                    }
                }
                else
                {
                    for (int _=0; _ < Workspaces[id].FiFoBuffer.Count+totalLines-Workspaces[id].WorkspaceHeight; _++)
                    {
                        foreach (string str in wordWrap)
                        {
                            Workspaces[id].FiFoBuffer.Dequeue();
                            Workspaces[id].FiFoBuffer.Enqueue(str);
                        }
                    }
                }
            }
        }

        //FIXME: The buffer only fills to a maximum of 2 lines.
        public void DrawDesktop()
        {
            Console.Clear();
            foreach(KeyValuePair<string, Workspace> kvp in Workspaces)
            {
                Console.SetCursorPosition(kvp.Value.WorkspaceOrigin.X, kvp.Value.WorkspaceOrigin.Y);
                kvp.Value.CursorPosition.X = kvp.Value.WorkspaceOrigin.X;
                kvp.Value.CursorPosition.Y = kvp.Value.WorkspaceOrigin.Y;

                kvp.Value.PrintBuffer();
            }
        }
    }
}
