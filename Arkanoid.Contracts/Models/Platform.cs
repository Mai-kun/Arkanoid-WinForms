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
        /// Изображение
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// Положение в пространстве
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Скорость перемещения
        /// </summary>
        public int Speed { get; init; }

        public Platform(int width, int height, Bitmap image, Point location, int speed)
        {
            Width = width;
            Height = height;
            Image = image;
            Location = location;
            Speed = speed;
        }

        /// <summary>
        /// Перемещает <see cref="Platform"/> на новую позицию.
        /// </summary>
        public void Move(Direction direction)
        {
            Location = new Point(Location.X + Speed * (int)direction, Location.Y);
        }
    }
}
