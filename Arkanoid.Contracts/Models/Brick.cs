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

        public Brick(int width, int height)
        {
            Width = width;
            Height = height;
        }
    }
}
