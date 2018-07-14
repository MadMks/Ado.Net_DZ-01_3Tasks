using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Task_4_Selling
{
    public partial class MainForm : Form
    {
        SqlConnection sqlConnection = null;

        public MainForm()
        {
            InitializeComponent();

            this.comboBoxNameTable.SelectedIndexChanged += ComboBoxNameTable_SelectedIndexChanged;
            this.FormClosing += MainForm_FormClosing;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            sqlConnection = new SqlConnection();
            sqlConnection.ConnectionString
                = ConfigurationManager.ConnectionStrings["SellingDBConnectString"].ConnectionString;

            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error connection");
            }

            FillingComboboxTableNames();

            this.comboBoxNameTable.SelectedIndex = 2;
        }

        private void ComboBoxNameTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.FillingGridViewFromSelectedTable();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            sqlConnection?.Close();
        }

        /// <summary>
        /// Заполнение gridView данными из выбранной таблицы.
        /// </summary>
        private void FillingGridViewFromSelectedTable()
        {
            SqlDataReader sqlDataReader = null;
            DataTable dataTable = null;

            try
            {
                // New query
                string selectedTableName = this.comboBoxNameTable.SelectedItem.ToString();
                string insertStringDataSelectTable
                    = @"SELECT * FROM " + selectedTableName;

                // Command
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = this.sqlConnection;
                sqlCommand.CommandText = insertStringDataSelectTable;

                sqlDataReader = sqlCommand.ExecuteReader();

                // Read to DataTable
                dataTable = new DataTable();
                int line = 0;
                
                while (sqlDataReader.Read())
                {
                    if (line == 0)
                    {
                        for (int i = 0; i < sqlDataReader.FieldCount; i++)
                        {
                            dataTable.Columns.Add(sqlDataReader.GetName(i));
                        }
                    }
                    line++;

                    DataRow row = dataTable.NewRow();
                    for (int i = 0; i < sqlDataReader.FieldCount; i++)
                    {
                        row[i] = sqlDataReader[i];
                    }
                    dataTable.Rows.Add(row);
                }

                this.dataGridView.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error read");
            }
            finally
            {
                sqlDataReader?.Close();
            }
        }

        /// <summary>
        /// Заполнение comboBox названиями таблиц.
        /// </summary>
        private void FillingComboboxTableNames()
        {
            SqlDataReader sqlDataReader = null;

            try
            {
                string insertStringNameTables
                    = @"SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES";

                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = this.sqlConnection;
                sqlCommand.CommandText = insertStringNameTables;

                sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    this.comboBoxNameTable.Items.Add(
                        sqlDataReader[0]);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error read");
            }
            finally
            {
                sqlDataReader?.Close();
            }
        }

    }
}
