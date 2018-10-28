using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace DVD_open
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //Заполнение списка дисковых накопителей
            getDrives(ref cDrives);
        }

        [DllImport("winmm.dll", EntryPoint = "mciSendString")]
        public static extern int mciSendStringA(string lpstrCommand, string lpstrReturnString, int uReturnLength, int hwndCallback);

        private void getDrives(ref ComboBox cList)
        {
            //Очистить список
            cList.Items.Clear();

            //Добавить только CD-диски
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                if (drive.DriveType == DriveType.CDRom)
                    cList.Items.Add(drive.RootDirectory);
            }
        }

        private void openDrive(string driveRoot)
        {
            string driveLetter = driveRoot[0].ToString();
            string returnString = string.Empty;

            //Задаём имя для указанного диска
            mciSendStringA("open " + driveLetter + ": type CDaudio alias drive" + driveLetter, returnString, 0, 0);

            //Открыть лоток для диска
            mciSendStringA("set drive" + driveLetter + " door open", returnString, 0, 0);
        }

        private void closeDrive(string driveRoot)
        {
            string driveLetter = driveRoot[0].ToString();
            string returnString = string.Empty;

            //Задаём имя для указанного диска
            mciSendStringA("open " + driveLetter + ": type CDaudio alias drive" + driveLetter, returnString, 0, 0);

            //Закрываем лоток для диска
            mciSendStringA("set drive" + driveLetter + " door closed", returnString, 0, 0);
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (cDrives.Text != string.Empty)
                openDrive(cDrives.Text);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (cDrives.Text != string.Empty)
                closeDrive(cDrives.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://vk.com/jobbed");
        }
    }
}
