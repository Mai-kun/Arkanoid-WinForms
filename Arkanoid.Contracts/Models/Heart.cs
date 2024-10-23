using System.Drawing;

namespace Arkanoid.Contracts.Models
{
    public class Heart
    {
        /// <summary>
        /// Изображение
        /// </summary>
        public Bitmap Image { get; set; }

        /// <summary>
        /// Ширина
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Высота
        /// </summary>
        public int Height { get; set; }

        public Heart(Bitmap image, int width, int height)
        {
            Image = image;
            Width = width;
            Height = height;
        }
    }
}
