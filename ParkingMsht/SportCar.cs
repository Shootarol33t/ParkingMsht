using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace ParkingMsht
{
    public class SportCar : Car
    {
        /// <summary>
        /// Дополнительный цвет
        /// </summary>
        public Color DopColor { private set; get; }
        /// <summary>
        /// Признак наличия переднего спойлера
        /// </summary>
        public bool FrontSpoiler { private set; get; }
        /// <summary>
        /// Признак наличия боковых спойлеров
        /// </summary>
        public bool SideSpoiler { private set; get; }
        /// <summary>
        /// Признак наличия заднего спойлера
        /// </summary>
        public bool BackSpoiler { private set; get; }
        public SportCar(int maxSpeed, float weight, Color mainColor, Color dopColor,bool frontSpoiler, bool sideSpoiler, bool backSpoiler) :
        base(maxSpeed, weight, mainColor)
        {
             
            DopColor = dopColor;
            FrontSpoiler = frontSpoiler;
            SideSpoiler = sideSpoiler;
            BackSpoiler = backSpoiler;

            Random rnd = new Random();
        }
        public SportCar(string info) : base(info)
        {
            string[] strs = info.Split(';');
            Console.WriteLine($"Получено info: \"{info}\"");
            Console.WriteLine($"Длина info: {info.Length}");
            Console.WriteLine($"Количество элементов после Split: {strs.Length}");

            for (int i = 0; i < strs.Length; i++)
            {
                Console.WriteLine($"strs[{i}] = \"{strs[i]}\"");
            }

            if (strs.Length == 7)  // Исправлено: 7 вместо 8
            {
                MaxSpeed = Convert.ToInt32(strs[0]);
                Weight = Convert.ToInt32(strs[1]);
                MainColor = Color.FromName(strs[2]);
                DopColor = Color.FromName(strs[3]);
                FrontSpoiler = Convert.ToBoolean(strs[4]);
                SideSpoiler = Convert.ToBoolean(strs[5]);
                BackSpoiler = Convert.ToBoolean(strs[6]);

                Console.WriteLine($"SportCar создан: {MaxSpeed}, {Weight}, {MainColor}, {DopColor}, {FrontSpoiler}, {SideSpoiler}, {BackSpoiler}");
            }
            else
            {
                Console.WriteLine("Ошибка загрузки SportCar: неправильное количество параметров!");
            }
        }
        public override void DrawCar(Graphics g)
        {
            Pen pen = new Pen(Color.Black);
            Brush dopBrush = new SolidBrush(DopColor);
            
            if (FrontSpoiler)
            {
                g.DrawEllipse(pen, _startPosX + 80, _startPosY - 6, 20, 20);
                g.DrawEllipse(pen, _startPosX + 80, _startPosY + 35, 20, 20);
                g.DrawEllipse(pen, _startPosX - 5, _startPosY - 6, 20, 20);
                g.DrawEllipse(pen, _startPosX - 5, _startPosY + 35, 20, 20);
                g.DrawRectangle(pen, _startPosX + 80, _startPosY - 6, 15, 15);
                g.DrawRectangle(pen, _startPosX + 80, _startPosY + 40, 15, 15);
                g.DrawRectangle(pen, _startPosX, _startPosY - 6, 14, 15);
                g.DrawRectangle(pen, _startPosX, _startPosY + 40, 14, 15);
                g.FillEllipse(dopBrush, _startPosX + 80, _startPosY - 5, 20, 20);
                g.FillEllipse(dopBrush, _startPosX + 80, _startPosY + 35, 20, 20);
                g.FillRectangle(dopBrush, _startPosX + 75, _startPosY, 25, 40);
                g.FillRectangle(dopBrush, _startPosX + 80, _startPosY - 5, 15, 15);
                g.FillRectangle(dopBrush, _startPosX + 80, _startPosY + 40, 15, 15);
                g.FillEllipse(dopBrush, _startPosX - 5, _startPosY - 5, 20, 20);
                g.FillEllipse(dopBrush, _startPosX - 5, _startPosY + 35, 20, 20);
                g.FillRectangle(dopBrush, _startPosX - 5, _startPosY, 25, 40);
                g.FillRectangle(dopBrush, _startPosX, _startPosY - 5, 15, 15);
                g.FillRectangle(dopBrush, _startPosX, _startPosY + 40, 15, 15);
            }
            // и боковые
            if (SideSpoiler)
            {
                g.DrawRectangle(pen, _startPosX + 25, _startPosY - 6, 39, 10);
                g.DrawRectangle(pen, _startPosX + 25, _startPosY + 45, 39, 10);
                g.FillRectangle(dopBrush, _startPosX + 25, _startPosY - 5, 40, 10);
                g.FillRectangle(dopBrush, _startPosX + 25, _startPosY + 45, 40, 10);
            }
            // теперь отрисуем основной кузов автомобиля
            base.DrawCar(g);
            g.FillRectangle(dopBrush, _startPosX + 65, _startPosY + 18, 25, 15);
            g.FillRectangle(dopBrush, _startPosX + 25, _startPosY + 18, 35, 15);
            g.FillRectangle(dopBrush, _startPosX, _startPosY + 18, 20, 15);
            // рисуем задний спойлер автомобиля
            if (BackSpoiler)
            {
                g.FillRectangle(dopBrush, _startPosX - 5, _startPosY, 10, 50);
                g.DrawRectangle(pen, _startPosX - 5, _startPosY, 10, 50);
            }
        }
        public void SetDopColor(Color color)
        {
            DopColor = color;
            if (!DopColor.IsKnownColor)
            {
                DopColor = Color.Gray;  // Если цвет неизвестен, установить серый
            }
        }
        public override string ToString()
        {
            string result = base.ToString() + ";" + DopColor.Name + ";" + FrontSpoiler + ";" +
                            SideSpoiler + ";" + BackSpoiler;
            Console.WriteLine($"ToString() вернул: \"{result}\"");
            return result;
        }

        public enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }
        public int CompareTo(SportCar other)
        {
            var res = (this is Car).CompareTo(other is Car);
            if (res != 0)
            {
                return res;
            }
            if (DopColor != other.DopColor)
            {
                DopColor.Name.CompareTo(other.DopColor.Name);
            }
            if (FrontSpoiler != other.FrontSpoiler)
            {
                return FrontSpoiler.CompareTo(other.FrontSpoiler);
            }
            if (SideSpoiler != other.SideSpoiler)
            {
                return SideSpoiler.CompareTo(other.SideSpoiler);
            }
            if (BackSpoiler != other.BackSpoiler)
            {
                return BackSpoiler.CompareTo(other.BackSpoiler);
            }
            return 0;
        }
        /// <summary>
        /// Метод интерфейса IEquatable для класса SportCar
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(SportCar other)
        {
            var res = (this as Car).Equals(other as Car);
            if (!res)
            {
                return res;
            }
            if (GetType().Name != other.GetType().Name)
            {
                return false;
            }
            if (DopColor != other.DopColor)
            {
                return false;
            }
            if (FrontSpoiler != other.FrontSpoiler)
            {
                return false;
            }
            if (SideSpoiler != other.SideSpoiler)
            {
                return false;
            }
            if (BackSpoiler != other.BackSpoiler)
            {
                return false;
            }
            return true;
        }
        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (!(obj is SportCar carObj))
            {
                return false;
            }
            else
            {
                return Equals(carObj);
            }
        }
        /// <summary>
        /// Перегрузка метода от object
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }





    }
}
  


