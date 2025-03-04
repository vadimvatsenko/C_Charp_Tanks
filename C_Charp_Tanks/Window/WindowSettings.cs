using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Charp_Tanks.Window
{
    public class WindowSettings
    {
        private int _width;
        private int _height;
        public WindowSettings(int width, int height)
        {
            _height = height;
            _width = width;

            SetWindowSize(_width, _height);
        }

        private void SetWindowSize(int width, int height)
        {
            Console.WindowWidth = width;
            Console.WindowHeight = height;

            Console.BufferWidth = width;
            Console.BufferHeight = height;
        }
    }
}
