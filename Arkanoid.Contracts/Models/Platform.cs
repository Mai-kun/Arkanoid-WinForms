using System.Drawing;

namespace Arkanoid.Contracts.Models
{
    public class Platform
    {
        /// <summary>
        /// Ширина
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Скорость
        /// </summary>
        public int Speed { get; set; }

        /// <summary>
        /// Положение в пространстве
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Изображение
        /// </summary>
        public Bitmap Image { get; set; }

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
