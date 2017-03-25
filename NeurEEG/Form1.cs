using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace NeurEEG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
            pictureBox2.BackColor = Color.Black;
            pictureBox1.Hide();
            pictureBox2.Hide();
            int a = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var files = Directory.GetFiles("D:/Projects/NeurEEG/NMN/true");
            //var files = Directory.GetFiles("D:/wp");
            string filePath = textBox2.Text;
            var files = Directory.GetFiles(filePath);
            int stopTime = Convert.ToInt32(textBox1.Text);
            int windWidth = Width;
            int windHeight = Height;
            hideItems();
            Thread.Sleep(stopTime);
            pictureBox2.Width = windWidth * 20 / 100;
            pictureBox2.Height = windHeight * 20 / 100;
            pictureBox1.Width = windWidth / 2;
            pictureBox1.Height = windHeight / 2;
            pictureBox1.Location = new Point((pictureBox1.Parent.ClientSize.Width / 2) - (pictureBox1.Width / 2),
                                            (pictureBox1.Parent.ClientSize.Height / 2) - (pictureBox1.Height / 2));
            Cursor.Hide();
            for (int i = 0; i < files.Length; i++)
            {
                pictureBox1.Image = Image.FromFile(files[i]);
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox2.BackColor = Color.White;
                pictureBox1.Show();
                pictureBox2.Show();
                Update();
                Thread.Sleep(stopTime);
                pictureBox1.Hide();
                pictureBox2.Hide();
                Thread.Sleep(stopTime);
            }
            missionEnd();
        }
        private void hideItems()
        {
            button1.Hide();
            label1.Hide();
            textBox1.Hide();
            SuspendLayout();
            textBox2.Hide();
            label2.Hide();
        }
        private void missionEnd()
        {
            Cursor.Show();
            pictureBox2.Hide();
            pictureBox1.Hide();
            label1.Show();
            textBox1.Show();
            button1.Show();
            label2.Show();
            textBox2.Show();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Close();                
            }
        }
    }
}
