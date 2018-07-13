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

namespace Tasks_2_and_3
{
    public partial class Form1 : Form
    {
        SqlConnection connection = null;

        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.connection = new SqlConnection();

            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = @"T5";
            connectionStringBuilder.InitialCatalog = "MyDB";
            connectionStringBuilder.UserID = "loginMyDBDz01";
            connectionStringBuilder.Password = "passwordMyDBDz01";

            this.connection.ConnectionString = connectionStringBuilder.ConnectionString;
        }

        private void buttonCreateTable_Click(object sender, EventArgs e)
        {
            try
            {
                this.connection?.Open();

                CreateTableInMyDB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
            finally
            {
                this.connection?.Close();
            }
        }

        private void CreateTableInMyDB()
        {
            string insertString = @"
                CREATE TABLE gruppa
                (
                    ID TINYINT NOT NULL PRIMARY KEY IDENTITY,
                    Name NVARCHAR(15) NOT NULL
                )";

            SqlCommand command = new SqlCommand();
            command.Connection = this.connection;
            command.CommandText = insertString;

            command.ExecuteNonQuery();
        }
    }
}
