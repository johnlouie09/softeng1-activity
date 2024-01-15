using AForge.Video;
using AForge.Video.DirectShow;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZXing;

namespace Louie_s_Prelim_Exam
{
    public partial class Attendance2 : Form
    {
        FilterInfoCollection filterInfoCollection;
        VideoCaptureDevice videoCaptureDevice;
        BarcodeReader barcodeReader;
        MySqlConnection dbConnection;

        private DataTable attendanceTable;

        private bool isUserIn = false;

        public Attendance2()
        {
            InitializeComponent();
            InitializeDatabase();
            InitializeDataGridView();
        }

        private void InitializeDatabase()
        {
            // Set your MySQL connection string here
            string connectionString = "Server=localhost;Database=bscs3db;User=root;Password='';";
            dbConnection = new MySqlConnection(connectionString);
        }

        private void InitializeDataGridView()
        {
            attendanceTable = new DataTable();

            // Define columns for the DataTable
            attendanceTable.Columns.Add("User Name", typeof(string));
            attendanceTable.Columns.Add("QR Code", typeof(string));
            attendanceTable.Columns.Add("Status", typeof(string));
            attendanceTable.Columns.Add("Date/Time", typeof(DateTime));

            // Set DataGridView's DataSource to the DataTable
            attendanceDataGrid.DataSource = attendanceTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void Attendance2_Load(object sender, EventArgs e)
        {
            filterInfoCollection = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo Device in filterInfoCollection)
                comboCam.Items.Add(Device.Name);
            comboCam.SelectedIndex = 0;
            videoCaptureDevice = new VideoCaptureDevice();

            barcodeReader = new BarcodeReader();
            barcodeReader.Options.PossibleFormats = new BarcodeFormat[] { BarcodeFormat.QR_CODE };
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboCam.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            videoCaptureDevice.Start();
        }

        private void FinalFrame_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            scannerBox.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Result result = barcodeReader.Decode((Bitmap)scannerBox.Image);
            if (result != null)
            {
                string decodedText = result.ToString();
                ProcessQRCode(decodedText);
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void Attendance2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }
    }
}
