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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Louie_s_Prelim_Exam
{
    public partial class Attendance2 : Form
    {
        private FilterInfoCollection filterInfoCollection;
        private VideoCaptureDevice videoCaptureDevice;
        private BarcodeReader barcodeReader;
        private MySqlConnection dbConnection;
        private DataTable attendanceTable;

        public Attendance2()
        {
            InitializeComponent();
            InitializeComponents();
        }

        private void InitializeComponents()
        {
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
            attendanceTable.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Student Name", typeof(string)),
                new DataColumn("Status", typeof(string)),
                new DataColumn("Date/Time", typeof(DateTime))
            });

            // Set DataGridView's DataSource to the DataTable
            attendanceDataGrid.DataSource = attendanceTable;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();

            // Close the camera
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
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
            StartCamera();
            StartDecoding();
            label2.Text = "QR Scanner is running...";
            label2.Visible = true;
        }

        private void StartCamera()
        {
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboCam.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += (s, args) => scannerBox.Image = (System.Drawing.Image)args.Frame.Clone();
            videoCaptureDevice.Start();
        }

        private void StartDecoding()
        {
            barcodeReader = new BarcodeReader();
            barcodeReader.Options.PossibleFormats = new BarcodeFormat[] { BarcodeFormat.QR_CODE };
            timer1.Interval = 2000; // Set the interval to 2 seconds (adjust as needed)
            timer1.Start();
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
                ProcessQRCode(result.ToString());
            }
        }

        private void ProcessQRCode(string decodedText)
        {
            try
            {
                dbConnection.Open();

                string query = "SELECT Firstname FROM studentstbl WHERE StudentID = @studentID";
                using (MySqlCommand cmd = new MySqlCommand(query, dbConnection))
                {
                    cmd.Parameters.AddWithValue("@studentID", decodedText);
                    string studentName = cmd.ExecuteScalar()?.ToString();

                    if (studentName != null)
                    {
                        int attendanceCount = GetAttendanceCount(decodedText);
                        string status = (attendanceCount == 0) ? "IN" : "OUT";

                        attendanceTable.Rows.Add(studentName, status, DateTime.Now);
                    }
                    else
                    {
                        MessageBox.Show("Student not found in the database.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private int GetAttendanceCount(string studentID)
        {
            string checkAttendanceQuery = "SELECT COUNT(*) FROM studentstbl WHERE StudentID = @studentID AND DATE(LastScanDateTime) = CURDATE()";
            using (MySqlCommand cmd = new MySqlCommand(checkAttendanceQuery, dbConnection))
            {
                cmd.Parameters.AddWithValue("@studentID", studentID);
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
        }

        private void Attendance2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice != null && videoCaptureDevice.IsRunning)
            {
                videoCaptureDevice.Stop();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Visible = !label2.Visible; 
        }
    }
}
