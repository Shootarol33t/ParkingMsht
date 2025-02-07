using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMsht
{
    internal class Car
    {
        // Свойства автомобиля
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Speed { get; private set; }
        public Color Color { get; private set; }

        // Конструктор
        public Car(int x, int y, int speed, Color color)
        {
            X = x;
            Y = y;
            Speed = speed;
            Color = color;
        }

        // Метод перемещения
        public void Move(Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    Y -= Speed;
                    break;
                case Direction.Down:
                    Y += Speed;
                    break;
                case Direction.Left:
                    X -= Speed;
                    break;
                case Direction.Right:
                    X += Speed;
                    break;
            }
        }

        // Метод отрисовки автомобиля
        public void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            g.FillRectangle(brush, X, Y, 50, 30); // Основное тело машины
            g.FillRectangle(Brushes.Black, X + 5, Y + 25, 10, 10); // Колеса
            g.FillRectangle(Brushes.Black, X + 35, Y + 25, 10, 10);
        }
    }

    // Перечисление направлений движения
    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}

