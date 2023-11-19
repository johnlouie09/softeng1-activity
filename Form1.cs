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
                string query = "SELECT * FROM studentstbl ORDER by studentid DESC";
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
                string query = "INSERT INTO studentstbl (Name, Age, isSingle) VALUES (@value1, @value2, @value3);";
                MySqlCommand cmd = new MySqlCommand(query, connection);

                cmd.Parameters.AddWithValue("@value1", studentname.Text);
                cmd.Parameters.AddWithValue("@value2", int.Parse(studentage.Text));
                cmd.Parameters.AddWithValue("@value3", checkBox1.Checked);

                cmd.ExecuteNonQuery();

                MessageBox.Show("Data inserted succesfully. ");

                dataGridwithdb.DataSource = null;
                string query2 = "SELECT * FROM studentstbl ORDER by studentid DESC";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query2, connection);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                dataGridwithdb.DataSource = dt;

                studentname.Text = "";
                studentage.Text = "";
                checkBox1.Checked = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Data not inserted" + ex.Message);
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
                string Studentname = clickedRow.Cells[1].Value.ToString();
                string Age = clickedRow.Cells[2].Value.ToString();
                var isSingle = clickedRow.Cells[4].Value;

                checkBox1.Checked = Convert.ToBoolean(isSingle);

                studentname.Text = Studentname;
                studentage.Text = Age;

                // Now you can use rowData as needed
            }
        }

        private void studentname_TextChanged(object sender, EventArgs e)
        {
            {
                if ((!string.IsNullOrEmpty(studentname.Text)) && !(string.IsNullOrEmpty(studentage.Text)))
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
                if ((!string.IsNullOrEmpty(studentname.Text)) && !(string.IsNullOrEmpty(studentage.Text)))
                {
                    button1.Enabled = true;

                }
                else
                    button1.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            studentname.Text = "";
            studentage.Text = "";
            checkBox1.Checked = false;
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
                DialogResult result = MessageBox.Show("Are you sure you want to delete this row?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Delete the row from the database
                    if (DeleteRowFromDatabase(primaryKeyValue))
                    {
                        // Delete the row from the DataGridView
                        dataGridwithdb.Rows.Remove(selectedRow);

                        // Display success message
                        MessageBox.Show("Row deleted successfully.", "Delete Row");
                    }
                    else
                    {
                        MessageBox.Show("Failed to delete row.", "Delete Row", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                // If the user clicks No, do nothing
            }
            else
            {
                MessageBox.Show("Please select a row to delete.", "Delete Row");

                // The input fields will become empty after deleting
                studentname.Text = "";
                studentage.Text = "";
                checkBox1.Checked = false;
            }
        }

        private bool DeleteRowFromDatabase(int primaryKeyValue)
        {
            try
            {
                // Use your MySQL database connection and SQL command to delete the row
                string query = "DELETE FROM studentstbl WHERE studentid = @PrimaryKeyValue";
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
    }
}
