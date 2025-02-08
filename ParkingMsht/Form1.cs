﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ParkingMsht.SportCar;


namespace ParkingMsht
{
    public partial class Form1 : Form
    {
        private SportCar car;

        public Form1()
        {
            InitializeComponent();
        }
        private void Draw()
        {
            Bitmap bmp = new Bitmap(pictureBoxCar.Width, pictureBoxCar.Height);
            Graphics gr = Graphics.FromImage(bmp);
            car.DrawCar(gr);
            pictureBoxCar.Image = bmp;
        }

        private void buttonCreate_Click(object sender, EventArgs e)
        {
            Random rnd = new Random();
            car = new SportCar(rnd.Next(100, 300), rnd.Next(1000, 2000), Color.Blue,
            Color.Yellow, true, true, true);
            car.SetPosition(rnd.Next(10, 100), rnd.Next(10, 100), pictureBoxCar.Width,
            pictureBoxCar.Height);
            Draw();

        }


        private void buttonUp_Click(object sender, EventArgs e)
        {
            if (car != null)
            {
                car.MoveTransport(Direction.Up);
                Draw();
            }
        }




        private void buttonRight_Click(object sender, EventArgs e)
        {
            if (car != null)
            {
                car.MoveTransport(Direction.Right);
                Draw();
            }
        }




        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (car != null)
            {
                car.MoveTransport(Direction.Down);
                Draw();
            }
        }




        private void buttonLeft_Click(object sender, EventArgs e)
        {
            if (car != null)
            {
                car.MoveTransport(Direction.Left);
                Draw();
            }
        }

        private void pictureBoxCar_Click(object sender, EventArgs e)
        {

        }
    }
}
