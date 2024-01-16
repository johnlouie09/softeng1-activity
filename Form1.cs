using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Louie_s_Prelim_Exam
{
    public partial class Form1 : Form
    {
        // connection from db
        MySqlConnection connection = new MySqlConnection("Server=localhost;Database=bscs3db;User=root;Password='';");

        public Form1()
        {
            InitializeComponent();

            try
            {
                connection.Open();
                string query = "SELECT * FROM studentstbl ORDER by StudentID DESC";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridwithdb.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was an error: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                MySqlCommand cmd;

                if (button1.Text == "Add Student")
                {
                    // Insert new student data
                    query = "INSERT INTO studentstbl (StudentID, Firstname, Age, isSingle) VALUES (@value1, @value2, @value3, @value4);";
                    cmd = new MySqlCommand(query, connection);
                }
                else if (button1.Text == "Update Student")
                {
                    // Update existing student data
                    query = "UPDATE studentstbl SET Firstname = @value2, Age = @value3, isSingle = @value4 WHERE StudentID = @value1;";
                    cmd = new MySqlCommand(query, connection);
                }
                else
                {
                    // Handle an unexpected state
                    MessageBox.Show("Unexpected button state.");
                    return;
                }

                // Set parameters
                cmd.Parameters.AddWithValue("@value1", studentid.Text);
                cmd.Parameters.AddWithValue("@value2", studentname.Text);
                cmd.Parameters.AddWithValue("@value3", int.Parse(studentage.Text));
                cmd.Parameters.AddWithValue("@value4", checkBox1.Checked);

                // Execute query
                cmd.ExecuteNonQuery();

                // Get the last inserted/updated student ID
                int lastStudentId = Convert.ToInt32(cmd.LastInsertedId);

                // Generate QR code based on the student data
                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;
                string combinedData = $"{studentid.Text}, {studentname.Text}, {studentage.Text}, {checkBox1.Checked}";

                // Adjust the width and height as needed
                int width = 6;
                int height = 6;
                var qrCodeImage = qrcode.Draw(combinedData, width, height);

                // Insert QR code into qrcodetbl table
                query = "INSERT INTO qrcodetbl (StudentId, QrCodeImage) VALUES (@studentId, @qrCodeImage);";
                cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@studentId", lastStudentId);

                // Display the generated QR code
                qrcodebox.Image = qrCodeImage;

                // Refresh the DataGridView
                dataGridwithdb.DataSource = null;
                string query2 = "SELECT * FROM studentstbl ORDER BY StudentID DESC";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query2, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridwithdb.DataSource = dt;

                // Clear input fields
                studentid.Text = "";
                studentname.Text = "";
                studentage.Text = "";
                checkBox1.Checked = false;

                // Change the text of the button back to "Add Student"
                button1.Text = "Add Student";

                // Show a specific message indicating whether the record was inserted or updated
                if (button1.Text == "Add Student")
                {
                    MessageBox.Show("Student record inserted successfully.", "Success!");
                }
                else if (button1.Text == "Update Student")
                {
                    MessageBox.Show("Student record updated successfully.", "Success!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data not inserted/updated: " + ex.Message);
            }
        }

        private void dataGridwithdb_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Check if the clicked cell is in a valid row (not the header)
            if (e.RowIndex >= 0)
            {
                // Access the data in the clicked row
                DataGridViewRow clickedRow = dataGridwithdb.Rows[e.RowIndex];
                // Assuming you want to access the data in the first cell of the clicked row
                string rowDataStudentID = clickedRow.Cells[0].Value.ToString();
                string Studentid = clickedRow.Cells[0].Value.ToString();
                string Studentname = clickedRow.Cells[1].Value.ToString();
                string Age = clickedRow.Cells[2].Value.ToString();
                var isSingle = clickedRow.Cells[4].Value.ToString();

                // Populate UI elements with the data
                studentid.Text = Studentid;
                studentname.Text = Studentname;
                studentage.Text = Age;
                checkBox1.Checked = Convert.ToBoolean(isSingle);

                // Change the text of the button (assuming you have a button named button1)
                button1.Text = "Update Student";

                // Generate QR code based on the student data
                string combinedData = $"{Studentid}, {Studentname}, {Age}, {isSingle}";
                Zen.Barcode.CodeQrBarcodeDraw qrcode = Zen.Barcode.BarcodeDrawFactory.CodeQr;

                // Adjust the width and height as needed
                int width = 6;
                int height = 6;
                var qrCodeImage = qrcode.Draw(combinedData, width, height);

                // Display the generated QR code
                qrcodebox.Image = qrCodeImage;

                // Now you can use rowData as needed
            }
        }

        private void studentid_TextChanged(object sender, EventArgs e)
        {
            {
                if (!string.IsNullOrEmpty(studentname.Text) && !string.IsNullOrEmpty(studentage.Text) && int.TryParse(studentage.Text, out _))
                {
                    button1.Enabled = true;

                }
                else
                    button1.Enabled = false;
            }
        }

        private void studentname_TextChanged(object sender, EventArgs e)
        {
            {
                if (!string.IsNullOrEmpty(studentname.Text) && !string.IsNullOrEmpty(studentage.Text) && int.TryParse(studentage.Text, out _))
                {
                    button1.Enabled = true;

                }
                else
                    button1.Enabled = false;
            }

        }

        private void studentage_TextChanged(object sender, EventArgs e)
        {
            {
                if (!string.IsNullOrEmpty(studentname.Text) && !string.IsNullOrEmpty(studentage.Text) && int.TryParse(studentage.Text, out _))
                {
                    button1.Enabled = true;

                }
                else
                    button1.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            studentid.Text = "";
            studentname.Text = "";
            studentage.Text = "";
            checkBox1.Checked = false;

            // Change the text of the button
            button1.Text = "Add Student";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Check if a row is selected
            if (dataGridwithdb.SelectedRows.Count > 0)
            {
                // Get the selected row
                DataGridViewRow selectedRow = dataGridwithdb.SelectedRows[0];

                // Get the value of the primary key column (adjust this based on your table structure)
                int primaryKeyValue = Convert.ToInt32(selectedRow.Cells[0].Value);

                // Ask the user for confirmation
                DialogResult result = MessageBox.Show("Are you sure you want to delete this record?", "Alert!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Delete the row from the database
                    if (DeleteRowFromDatabase(primaryKeyValue))
                    {
                        // Delete the row from the DataGridView
                        dataGridwithdb.Rows.Remove(selectedRow);

                        // Display success message
                        MessageBox.Show("Record deleted successfully.", "Delete Record");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete record.", "Delete Record", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // If the user clicks No, do nothing
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Delete Row");
            }

            // The input fields will become empty after deleting
            studentid.Text = "";
            studentname.Text = "";
            studentage.Text = "";
            checkBox1.Checked = false;
        }

        private bool DeleteRowFromDatabase(int primaryKeyValue)
        {
            try
            {
                // Use your MySQL database connection and SQL command to delete the row
                string query = "DELETE FROM studentstbl WHERE StudentID = @PrimaryKeyValue";
                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PrimaryKeyValue", primaryKeyValue);
                    command.ExecuteNonQuery();

                    return true; // Deletion successful
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting row from database: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // Deletion failed
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            button1.Enabled = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Attendance2 attendance = new Attendance2();
            attendance.Show();
            this.Hide();
        }
    }
}