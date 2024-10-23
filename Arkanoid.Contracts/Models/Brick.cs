﻿using System.Drawing;

namespace Arkanoid.Models
{
    public class brick
    {
        /// <summary>
        /// Изображение
        /// </summary>
        public Bitmap[] Images { get; private set; }

        /// <summary>
        /// Ширина.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота.
        /// </summary>
        public int Height { get; set; }

        public brick(int width, int height, Bitmap[] image)
        {
            Width = width;
            Height = height;
            Images = image;
        }
    }
}
