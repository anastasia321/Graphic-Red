using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graphic_Red
{
    public partial class Form1 : Form
    {
        
        int x1, y1, x2, y2;
        Bitmap snapshot, tempDraw;
        Color foreColor;
        int lineWidth;
      
        Color color = Color.Black; //Создаем переменную типа Color присваиваем ей черный цвет.
        bool isPressed = false; //логическая переменная понадобиться для опеределения когда можно рисовать на panel
        Point CurrentPoint; //Текущая точка ресунка.
        Point PrevPoint; //Это начальная точка рисунка.
        Graphics g; //Создаем графический элемент.
        ColorDialog colorDialog = new ColorDialog(); //диалоговое окно для выбора цвета.

        public Form1()
        {
            InitializeComponent();
            label1.BackColor = Color.Black; //По умолчанию для пера задан черный цвет, поэтому мы зададим такой же фон для label2
            g = pictureBox.CreateGraphics(); //Создаем область для работы с графикой на элементе panel
            snapshot = new Bitmap(pictureBox.ClientRectangle.Width, pictureBox.ClientRectangle.Height);
            tempDraw = (Bitmap)snapshot.Clone();
            foreColor = Color.Black;
            lineWidth = 2;

            //По умолчанию для пера задан черный цвет, поэтому мы зададим такой же фон для label2
            g = pictureBox.CreateGraphics(); //Создаем область для работы с графикой на элементе panel
        }

        private void pictureBox_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
           
            {
                if (colorDialog.ShowDialog() == DialogResult.OK) //Если окно закрылось с OK, то меняем цвет для пера и фона label2
                {
                    color = colorDialog.Color; //меняем цвет для пера
                    label1.BackColor = colorDialog.Color; //меняем цвет для Фона label2
                }
            }
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            CurrentPoint = e.Location;
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(openFileDialog1.FileName);
                snapshot = (Bitmap)pictureBox.Image;
                tempDraw = (Bitmap)snapshot.Clone();
                this.Text = openFileDialog1.FileName;
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                string strFilExtn = fileName.Remove(0, fileName.Length - 3);
                this.Text = saveFileDialog1.FileName;

                switch (strFilExtn)
                {
                    case "bmp": snapshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Bmp); break;
                    case "jpg": snapshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Jpeg); break;
                    case "gif": snapshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Gif); break;
                    case "tif": snapshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Tiff); break;
                    case "png": snapshot.Save(fileName, System.Drawing.Imaging.ImageFormat.Png); break;
                    default: break;
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox.Image = Image.FromFile(openFileDialog1.FileName);
                snapshot = (Bitmap)pictureBox.Image;
                tempDraw = (Bitmap)snapshot.Clone();
                this.Text = openFileDialog1.FileName;
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            
            
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private void pictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                PrevPoint = CurrentPoint;
                CurrentPoint = e.Location;
                myPen();
            }
        }
        private void myPen()
        {
            Pen pen = new Pen(color, (float)numericUpDown1.Value); //Создаем перо, задаем ему цвет и толщину.
            g.DrawLine(pen, CurrentPoint, PrevPoint); //Соединияем точки линиями
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox.CreateGraphics();
            g.Clear(Color.White);
            pictureBox.Image = null;
            snapshot = new Bitmap(pictureBox.ClientRectangle.Width, pictureBox.ClientRectangle.Height);
            tempDraw = (Bitmap)snapshot.Clone();
            g.Dispose();
        }
    }
}
