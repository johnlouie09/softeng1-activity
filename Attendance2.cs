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
            isUserIn = false; // Set the intial state

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

        private void ProcessQRCode(string decodedText)
        {
            try
            {
                dbConnection.Open();

                string query = "SELECT * FROM attendance WHERE qr_code = @qr_code";
                MySqlCommand cmd = new MySqlCommand(query, dbConnection);
                cmd.Parameters.AddWithValue("@qr_code", decodedText);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string userName = reader["user_name"].ToString();
                        DateTime dateTime = DateTime.Now;

                        if (!isUserIn)
                        {
                            string insertQuery = "INSERT INTO attendance (user_name, qr_code, status, date_time) VALUES (@user_name, @qr_code, 'IN', @date_time)";
                            MySqlCommand insertCmd = new MySqlCommand(insertQuery, dbConnection);
                            insertCmd.Parameters.AddWithValue("@user_name", userName);
                            insertCmd.Parameters.AddWithValue("@qr_code", decodedText);
                            insertCmd.Parameters.AddWithValue("@date_time", dateTime);
                            insertCmd.ExecuteNonQuery();

                            // Add a new row to the DataTable
                            attendanceTable.Rows.Add(userName, decodedText, "IN", dateTime);
                            UpdateStatus("IN");
                            isUserIn = true;
                        }
                        else
                        {
                            string updateQuery = "UPDATE attendance SET status = 'OUT' WHERE qr_code = @qr_code AND status = 'IN'";
                            MySqlCommand updateCmd = new MySqlCommand(updateQuery, dbConnection);
                            updateCmd.Parameters.AddWithValue("@qr_code", decodedText);
                            updateCmd.ExecuteNonQuery();

                            // Update the status in the DataTable
                            DataRow[] rows = attendanceTable.Select($"[QR Code] = '{decodedText}' AND [Status] = 'IN'");
                            if (rows.Length > 0)
                            {
                                rows[0]["Status"] = "OUT";
                                UpdateStatus("OUT");
                            }
                            isUserIn = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("User not found in the database.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing QR code: " + ex.Message);
            }
            finally
            {
                dbConnection.Close();
            }
        }

        private void UpdateStatus(string status)
        {
            textBox1.Text = $"Status: {status}";
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
