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
    public partial class FormCarConfig : Form
    {
        ITransport car = null;
        private event carDelegate eventAddCar;

        public FormCarConfig()
        {
            InitializeComponent();
            panelBlack.MouseDown += panelColor_MouseDown;
            panelGold.MouseDown += panelColor_MouseDown;
            panelGray.MouseDown += panelColor_MouseDown;
            panelGreen.MouseDown += panelColor_MouseDown;
            panelRed.MouseDown += panelColor_MouseDown;
            panelWhite.MouseDown += panelColor_MouseDown;
            panelYellow.MouseDown += panelColor_MouseDown;
            panelBlue.MouseDown += panelColor_MouseDown;
            buttonCancel.Click += (object sender, EventArgs e) => { Close(); };
        }
        private void DrawCar()
        {
            if (car != null)
            {
                Bitmap bmp = new Bitmap(pictureBoxCar.Width, pictureBoxCar.Height);
                Graphics gr = Graphics.FromImage(bmp);
                car.SetPosition(5, 5, pictureBoxCar.Width, pictureBoxCar.Height);
                car.DrawCar(gr);
                pictureBoxCar.Image = bmp;
            }
        }
        public void AddEvent(carDelegate ev)
        {
            if (eventAddCar == null)
            {
                eventAddCar = new carDelegate(ev);
            }
            else
            {
                eventAddCar += ev;
            }
        }
        private void labelCar_MouseDown(object sender, MouseEventArgs e)
        {
            labelCar.DoDragDrop(labelCar.Text, DragDropEffects.Move |
            DragDropEffects.Copy);
        }
        private void labelSport_MouseDown(object sender, MouseEventArgs e)
        {
            labelSport.DoDragDrop(labelSport.Text, DragDropEffects.Move |
            DragDropEffects.Copy);
        }
        private void panelCar_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
        private void panelCar_DragDrop(object sender, DragEventArgs e)
        {
            switch (e.Data.GetData(DataFormats.Text).ToString())
            {
                case "Обычный автомобиль":
                    car = new Car(100, 500, Color.White);
                    break;
                case "Гоночный автомобиль":
                    car = new SportCar(100, 500, Color.White, Color.Black, true, true, true);
                    break;
            }
            DrawCar();
        }
        private void FormCarConfig_Load(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void panelColor_MouseDown(object sender, MouseEventArgs e)
        {
            (sender as Control).DoDragDrop((sender as Control).BackColor,
            DragDropEffects.Move | DragDropEffects.Copy);
        }
        private void labelBaseColor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Color)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }
        }
        private void labelBaseColor_DragDrop(object sender, DragEventArgs e)
        {
            if (car != null)
            {
                car.SetMainColor((Color)e.Data.GetData(typeof(Color)));
                DrawCar();
            }
        }
        private void labelDopColor_DragDrop(object sender, DragEventArgs e)
        {
            if (car != null)
            {
                if (car is SportCar)
                {
                    (car as
                    SportCar).SetDopColor((Color)e.Data.GetData(typeof(Color)));
                    DrawCar();
                }
            }
        }
        private void labelDopColor_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Color)))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void panelColor_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            eventAddCar?.Invoke(car);
            Close();
        }
    }
}
