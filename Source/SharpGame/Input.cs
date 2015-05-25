using System;

namespace SharpGame
{
    public class Input
    {
        /** 
         * A positional bit flag indicating the part of a key state denoting key pressed.
         */
        private const int KeyPressedMask = 0x8000;

        public static bool IsKeyDown(ConsoleKey key)
        {
            return (GetKeyState((int)key) & KeyPressedMask) != 0;
        }

        /**
         * Returns the status of the key. This is actually a binding to a native function inside
         * Windows standart DLL.
         * Found this solution here: http://stackoverflow.com/questions/4351258/c-sharp-arrow-key-input-for-a-console-app
         */
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetKeyState(int key);
    }
}
