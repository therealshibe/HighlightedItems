﻿using HighlightedItems;
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace HighlightedItems.Utils
{
    internal class Keyboard
    {
        private const int KEYEVENTF_EXTENDEDKEY = 0x0001;
        private const int KEYEVENTF_KEYUP = 0x0002;
        private const int KEY_PRESSED = 0x8000;
        private const int KEY_TOGGLED = 0x0001;

        [DllImport("user32.dll")]
        private static extern uint keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern short GetKeyState(int nVirtKey);


        public static void KeyDown(Keys key)
        {
            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | 0, 0);
        }

        public static void KeyUp(Keys key)
        {
            keybd_event((byte)key, 0, KEYEVENTF_EXTENDEDKEY | KEYEVENTF_KEYUP, 0);
        }

        public static void KeyPress(Keys key)
        {
            KeyDown(key);
            Thread.Sleep(Constants.CLICK_DELAY);
            KeyUp(key);
        }

        public static bool IsKeyDown(Keys key)
        {
            return GetKeyState((int)key) < 0;
        }

        public static bool IsKeyPressed(Keys key)
        {
            return Convert.ToBoolean(GetKeyState((int)key) & KEY_PRESSED);
        }


        public static bool IsKeyToggled(Keys key)
        {
            return Convert.ToBoolean(GetKeyState((int)key) & KEY_TOGGLED);
        }
    }
}