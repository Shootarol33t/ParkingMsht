using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingMsht
{
    public class MultiLevelParking
    {
        /// <summary>
        /// Список с уровнями парковки
        /// </summary>
        List<Parking<ITransport>> parkingStages;
        /// <summary>
        /// Сколько мест на каждом уровне
        /// </summary>
        private const int countPlaces = 20;
        /// <summary>
        /// Ширина окна отрисовки
        /// </summary>
        private int pictureWidth;
        /// <summary>
        /// Высота окна отрисовки
        /// </summary>
        private int pictureHeight;
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="countStages">Количество уровенй парковки</param>
        /// <param name="pictureWidth"></param>
        /// <param name="pictureHeight"></param>
        public MultiLevelParking(int countStages, int pictureWidth, int pictureHeight)
        {
            parkingStages = new List<Parking<ITransport>>();
            this.pictureWidth = pictureWidth;
            this.pictureHeight = pictureHeight;
            for (int i = 0; i < countStages; ++i)
            {
                parkingStages.Add(new Parking<ITransport>(countPlaces, pictureWidth, pictureHeight));
            }
        }
        public Parking<ITransport> this[int ind]
        {
            get
            {
                if (ind > -1 && ind < parkingStages.Count)
                {
                    return parkingStages[ind];
                }
                return null;
            }
        }
        public bool SaveData(string filename)
        {
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                //Записываем количество уровней
                WriteToFile("CountLeveles:" + parkingStages.Count +
                Environment.NewLine, fs);
                foreach (var level in parkingStages)
                {
                    //Начинаем уровень
                    WriteToFile("Level" + Environment.NewLine, fs);
                    for (int i = 0; i < countPlaces; i++)
                    {
                        var car = level[i];
                        if (car != null)
                        {
                            //если место не пустое
                            //Записываем тип мшаины
                            if (car.GetType().Name == "Car")
                            {
                                WriteToFile(i + ":Car:", fs);
                            }
                            if (car.GetType().Name == "SportCar")
                            {
                                WriteToFile(i + ":SportCar:", fs);
                            }
                            //Записываемые параметры
                            WriteToFile(car + Environment.NewLine, fs);
                        }
                    }
                }
            }
            return true;
        }
        private void WriteToFile(string text, FileStream stream)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(text);
            stream.Write(info, 0, info.Length);
        }
        public bool LoadData(string filename)
        {
            if (!File.Exists(filename))
            {
                return false;
            }

            string[] lines;
            try
            {
                lines = File.ReadAllLines(filename);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении файла: {ex.Message}");
                return false;
            }

            if (lines.Length == 0 || !lines[0].StartsWith("CountLeveles:"))
            {
                Console.WriteLine("Ошибка: Некорректный формат файла.");
                return false;
            }

            // Читаем количество уровней
            if (!int.TryParse(lines[0].Split(':')[1], out int levelCount))
            {
                Console.WriteLine("Ошибка: Некорректное значение CountLeveles.");
                return false;
            }

            if (parkingStages != null)
            {
                parkingStages.Clear();
            }
            parkingStages = new List<Parking<ITransport>>(levelCount);

            int currentLevel = -1;
            ITransport car = null;

            for (int i = 1; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (string.IsNullOrEmpty(line)) continue; // Пропуск пустых строк

                if (line == "Level")
                {
                    currentLevel++;
                    parkingStages.Add(new Parking<ITransport>(countPlaces, pictureWidth, pictureHeight));
                    continue;
                }

                string[] parts = line.Split(':');
                if (parts.Length < 3)
                {
                    Console.WriteLine($"Ошибка: Некорректная строка {line}");
                    continue;
                }

                int place;
                if (!int.TryParse(parts[0], out place))
                {
                    Console.WriteLine($"Ошибка: Некорректный номер места {parts[0]}");
                    continue;
                }

                string type = parts[1];
                string param = parts[2];

                if (type == "Car")
                {
                    car = new Car(param);
                }
                else if (type == "SportCar")
                {
                    car = new SportCar(param);
                }
                else
                {
                    Console.WriteLine($"Ошибка: Неизвестный тип транспорта {type}");
                    continue;
                }
                Console.WriteLine($"Level: {currentLevel}, Place: {place}, Type: {type}, Param: {param}");
                parkingStages[currentLevel][place] = car;
            }

            return true;
        }

    }
}
    
   


