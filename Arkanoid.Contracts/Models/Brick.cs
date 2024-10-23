using System.Drawing;

namespace Arkanoid.Contracts.Models
{
    public class Brick
    {
        /// <summary>
        /// Возможные изображения
        /// </summary>
        public Bitmap[] Images { get; init; }

        /// <summary>
        /// Ширина.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота.
        /// </summary>
        public int Height { get; set; }

        public Brick(int width, int height, Bitmap[] image)
        {
            Width = width;
            Height = height;
            Images = image;
        }
    }
}
