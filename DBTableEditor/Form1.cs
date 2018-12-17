using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DBTableEditor
{
    public partial class Form1 : Form
    {
        SqlConnection sqlConnection;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\C#exercises\DBTableEditor\DBTableEditor\Database1.mdf;Integrated Security=True";
            sqlConnection = new SqlConnection(connectionString);
            sqlConnection.Open();
            SqlDataReader sqlReader = null;
            SqlCommand command = new SqlCommand("SELECT * FROM [Subject]", sqlConnection);
            sqlReader = command.ExecuteReader();
            while (sqlReader.Read())
            {
                listBox1.Items.Add(Convert.ToString(sqlReader["Id"]) + "     " + Convert.ToString(sqlReader["Name"]));
            }
            sqlReader.Close();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sqlConnection.Close();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [Subject] (Name) VALUES (@Name)", sqlConnection);
            command.Parameters.AddWithValue("Name", textBox1.Text);
            command.ExecuteNonQuery();
            refreshToolStripMenuItem_Click(sender, e);
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            Form1_Load(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("UPDATE [Subject] SET [Name] = (@Name) WHERE [Id] = @Id", sqlConnection);
            command.Parameters.AddWithValue("Id", textBox3.Text);
            command.Parameters.AddWithValue("Name", textBox2.Text);
            command.ExecuteNonQuery();
            refreshToolStripMenuItem_Click(sender, e);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [Subject] WHERE [Id] = @Id", sqlConnection);
            command.Parameters.AddWithValue("Id", textBox4.Text);
            command.ExecuteNonQuery();
            refreshToolStripMenuItem_Click(sender, e);
        }
    }
}
