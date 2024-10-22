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
        /// Положение в пространстве
        /// </summary>
        public Point Location { get; set; }

        public Platform(int width, int height, Point location)
        {
            Width = width;
            Height = height;
            Location = location;
        }
    }
}
