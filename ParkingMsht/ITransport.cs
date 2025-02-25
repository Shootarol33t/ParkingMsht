﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ParkingMsht.SportCar;

namespace ParkingMsht
{
    public interface ITransport
    {
        void SetPosition(int x, int y, int width, int height);
        void MoveTransport(Direction direction);
        void DrawCar(Graphics g);

        void SetMainColor(Color color);
    }
}
