using System.Drawing;

namespace Arkanoid.Models
{
    public class Platform
    {
        /// <summary>
        /// Ширина
        /// </summary>
        public int Width { get; init; }

        /// <summary>
        /// Высота
        /// </summary>
        public int Height { get; init; }

        /// <summary>
        /// Скорость
        /// </summary>
        public int Speed { get; init; }

        /// <summary>
        /// Положение в пространстве
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        public Bitmap Image { get; private set; }

        public Platform(int width, int height, int speed, Point location, Bitmap image)
        {
            Width = width;
            Height = height;
            Speed = speed;
            Location = location;
            Image = image;
        }
    }
}
