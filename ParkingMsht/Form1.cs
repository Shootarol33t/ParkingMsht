using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParkingMsht
{
    public partial class Form1 : Form
    {
        private Car car;
        public Form1()
        {
            InitializeComponent();
        }
        private void Draw()
        {
            if (car == null) return;

            Bitmap bmp = new Bitmap(pictureBoxCar.Width, pictureBoxCar.Height);
            Graphics g = Graphics.FromImage(bmp);

            car.Draw(g);
            pictureBoxCar.Image = bmp;
        }
        private void buttonCreate_Click(object sender, EventArgs e)
        {
            car = new Car(200, 150, 10, Color.Blue);
            Draw();
        }
        private void MoveCar(Direction direction)
        {
            if (car == null) return;

            car.Move(direction);
            Draw();
        }

        private void buttonUp_Click(object sender, EventArgs e) => MoveCar(Direction.Up);




        private void buttonRight_Click(object sender, EventArgs e) => MoveCar(Direction.Right);
        

        

        private void buttonDown_Click(object sender, EventArgs e) => MoveCar(Direction.Down);




        private void buttonLeft_Click(object sender, EventArgs e) => MoveCar(Direction.Left);

        private void pictureBoxCar_Click(object sender, EventArgs e)
        {

        }
    }
}
