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
        bool exit = false;
        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            TopMost = true;
            pictureBox2.BackColor = Color.Black;
            pictureBox1.Hide();
            pictureBox2.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string filePath = textBox2.Text;
            if (Directory.Exists(filePath))
            {
                exit = false;
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
                for (int i = 0; i < files.Length; i++)
                {
                    if (!(files[i].Contains(".jpg") || files[i].Contains(".bmp") || files[i].Contains(".png"))) i++;
                    else
                    {
                        Application.DoEvents();
                        if (exit)
                        {
                            missionEnd();
                            break;
                        }
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
                }
                missionEnd();
            }
            else
            {
                MessageBox.Show("Wrong Directory!!!");
            }
        }
        private void hideItems()
        {
            Cursor.Hide();
            button1.Hide();
            button2.Hide();
            button3.Hide();
            label1.Hide();
            label2.Hide();
            textBox1.Hide();
            textBox2.Hide();
            SuspendLayout();
        }
        private void missionEnd()
        {
            Cursor.Show();
            pictureBox1.Hide();
            pictureBox2.Hide();
            label1.Show();
            label2.Show();
            textBox1.Show();
            textBox2.Show();
            button1.Show();
            button2.Show();
            button3.Show();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                exit = true;
                //Close();                
            }
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    string[] files = Directory.GetFiles(fbd.SelectedPath);
                    string directoryName = "";
                    for (int i = 0; i < fbd.SelectedPath.Length; i++)
                    {
                        if (fbd.SelectedPath[i] == '\u005C') directoryName += '\u002F';
                        else directoryName += fbd.SelectedPath[i];
                    }
                    textBox2.Text = directoryName;
                }
            }
        }

        private void button1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
