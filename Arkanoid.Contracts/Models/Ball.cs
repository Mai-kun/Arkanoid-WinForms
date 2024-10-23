using System.Drawing;
using Arkanoid.Contracts;

namespace Arkanoid.Models
{
    public class Ball : IMovable
    {
        /// <summary>
        /// Изображение
        /// </summary>
        public Bitmap Image { get; private set; }

        /// <summary>
        /// Размер
        /// </summary>
        public int Size { get; private set; }

        /// <summary>
        /// Текущая скорость по оси X
        /// </summary>
        public int SpeedX { get; set; }

        /// <summary>
        /// Текущая скорость по оси Y
        /// </summary>
        public int SpeedY { get; set; }

        /// <inheritdoc cref="IMovable.Location"/>
        public Point Location { get; set; }

        public Ball(int size, int speedX, int speedY, Point location, Bitmap image)
        {
            Size = size;
            SpeedX = speedX;
            SpeedY = speedY;
            Location = location;
            Image = image;
        }

        /// <summary>
        /// Меняет направление движения по оси X
        /// </summary>
        public void ChangeVelocityX()
        {
            SpeedX = -SpeedX;
        }

        /// <summary>
        /// Меняет направление движения по оси Y
        /// </summary>
        public void ChangeVelocityY()
        {
            SpeedY = -SpeedY;
        }
    }
}
