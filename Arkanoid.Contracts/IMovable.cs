using System.Drawing;

namespace Arkanoid.Contracts
{
    /// <summary>
    /// Описывает поведение объектов, которые могут изменять своё положение в пространстве
    /// </summary>
    internal interface IMovable
    {
        /// <summary>
        /// Указывает текущее положение
        /// </summary>
        public Point Location { get; set; }
    }
}
