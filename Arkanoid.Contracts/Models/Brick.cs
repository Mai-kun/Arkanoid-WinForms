using System.Drawing;

namespace Arkanoid.Models
{
    public class Brick
    {
        /// <summary>
        /// Ширина.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота.
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Возможные изображения блока.
        /// </summary>
        public Bitmap[] Image { get; set; }

        public Brick(int width, int height, Bitmap[] image)
        {
            Width = width;
            Height = height;
            Image = image;
        }
    }
}
