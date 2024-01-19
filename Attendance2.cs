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
            isUserIn = false; // Set the intial 

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

            // Close the camera
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
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
            // turn on the camera
            videoCaptureDevice = new VideoCaptureDevice(filterInfoCollection[comboCam.SelectedIndex].MonikerString);
            videoCaptureDevice.NewFrame += FinalFrame_NewFrame;
            videoCaptureDevice.Start();

            // start the decoding process
            timer1.Start();

            label2.Text = "QR Scanner running...";
            label2.Visible = true;
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
                Console.WriteLine($"Decoded Text: {decodedText}");
                ProcessQRCode(decodedText);
            }
        }

        private void ProcessQRCode(string decodedText)
        {
            try
            {
                dbConnection.Open();

                // Modified query to retrieve information from related tables
                string query = "SELECT s.Firstname, a.status, a.date_time " +
                                "FROM attendance a " +
                                "JOIN qrcodetbl q ON a.qr_code = q.QrCodeId " +
                                "JOIN studentstbl s ON q.StudentId = s.StudentID " +
                                "WHERE a.qr_code = @qr_code";

                MySqlCommand cmd = new MySqlCommand(query, dbConnection);
                cmd.Parameters.AddWithValue("@qr_code", decodedText);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string studentName = reader["Firstname"].ToString();
                        string status = reader["status"].ToString();
                        DateTime dateTime = Convert.ToDateTime(reader["date_time"]);

                        if (!isUserIn)
                        {
                            // Add a new row to the DataTable
                            attendanceTable.Rows.Add(studentName, decodedText, status, dateTime);
                            isUserIn = true;
                        }
                        else
                        {
                            // Update the status in the DataTable
                            DataRow[] rows = attendanceTable.Select($"[QR Code] = '{decodedText}' AND [Status] = 'IN'");
                            if (rows.Length > 0)
                            {
                                rows[0]["Status"] = "OUT";
                            }
                            isUserIn = false;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"User not found in the database. Decoded Text: {decodedText}");
                        MessageBox.Show("User not found in the database.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing QR code: {ex.Message}");
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void Attendance2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (videoCaptureDevice.IsRunning == true)
                videoCaptureDevice.Stop();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            label2.Visible = !label2.Visible; 
        }
    }
}
