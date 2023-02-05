using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9IHTE5N\MSSQLSERVER01;Initial Catalog=master;Integrated Security=True");
        //SqlConnection con = new SqlConnection(@"")
        public int Id;
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetContants();
        }

        private void GetContants()
        {
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-9IHTE5N\MSSQLSERVER01;Initial Catalog=master;Integrated Security=True");
            // SqlCommand cmd = new SqlCommand("exec ContactSP", con);
            SqlCommand cmd = new SqlCommand("select * from TableProject", con);
            
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            dataGridView1.DataSource = dt;
            
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
             
           // textBox7.Text = Convert.ToString(Convert.ToInt32(textBox4.Text )+ Convert.ToInt32(textBox6.Text));
            textBox7.Text = Convert.ToString((textBox4.Text )+ (textBox6.Text));
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Isvalid())
            {
                //SqlCommand com = conn.CreateCommand();
                //com.CommandType = CommandType.StoredProcedure;
                //com.CommandText = "ProductSetup_SP";
                //SqlDataAdapter adapter = new SqlDataAdapter(com);
                //DataTable dt = new DataTable();
                //conn.Open();

                SqlCommand cmd = new SqlCommand("Insert into TableProject values (@Name, @Date,@RegFee,@Examfee,@CollegeFees,@Total,@Address,@MobileNo,@Email)",con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Date", textBox2.Text);
                cmd.Parameters.AddWithValue("@RegFee", textBox3.Text);
                cmd.Parameters.AddWithValue("@Examfee", textBox4.Text);
                cmd.Parameters.AddWithValue("@CollegeFees", textBox6.Text);
                cmd.Parameters.AddWithValue("@Total", textBox7.Text);
                cmd.Parameters.AddWithValue("@Address", textBox8.Text);
                cmd.Parameters.AddWithValue("@MobileNo", textBox9.Text);
                cmd.Parameters.AddWithValue("@Email", textBox5.Text);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Inserted", "save", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetContants();
            }
        }

        private bool Isvalid()
        {
            Regex phoneNumpattern = new Regex(@"\+[0-9]{3}\s+[0-9]{3}\s+[0-9]{5}\s+[0-9]{3}");
            if (phoneNumpattern.IsMatch(textBox9.Text))
            {
                MessageBox.Show("No is Incorrect", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            return true;
        }
        private bool Isvalidemail()
        {
            Regex email= new Regex(@"^[a-zA-Z][\w\.-]{2,28}[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-][a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.][a-zA-Z]$");
            if (email.IsMatch(textBox5.Text))
            {
                MessageBox.Show("InValid EmailID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
            return true;
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Id >0)
            {
                SqlCommand cmd = new SqlCommand("update  TableProject set Name=@Name, Date=@Date,RegFee=@RegFee,Examfee=@Examfee,CollegeFees=@CollegeFees,Address=@Address,Total=@Total,MobileNo=@MobileNo,Email=@Email where Name=@Name", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", textBox1.Text);
                cmd.Parameters.AddWithValue("@Date", textBox2.Text);
                

                cmd.Parameters.AddWithValue("@RegFee", textBox3.Text);
                cmd.Parameters.AddWithValue("@Examfee", textBox4.Text);
                cmd.Parameters.AddWithValue("@CollegeFees", textBox6.Text);
                cmd.Parameters.AddWithValue("@Total", textBox7.Text);
                cmd.Parameters.AddWithValue("@MobileNo", textBox9.Text);
                cmd.Parameters.AddWithValue("@Address", textBox8.Text);
                cmd.Parameters.AddWithValue("@Email", textBox5.Text);
                cmd.Parameters.AddWithValue("@Id",this.Id);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Updated", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                GetContants();
            }
            else
            {
                MessageBox.Show("Data Error", "Not Update", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (Id >0 )
            {

                if (MessageBox.Show("Are you sure?", "CRUD", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("Delete TableProject where Id = @Id", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Id", this.Id);


                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Data Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    GetContants();
                }
                
            }
            else
            {
                MessageBox.Show("Error", "Not Delete", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Id = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value);
            textBox1.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.SelectedRows[0].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.SelectedRows[0].Cells[3].Value.ToString();
            textBox4.Text = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
            textBox5.Text = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
            textBox6.Text = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
            textBox7.Text = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
            textBox8.Text = dataGridView1.SelectedRows[0].Cells[8].Value.ToString();
            textBox9.Text = dataGridView1.SelectedRows[0].Cells[9].Value.ToString();
           
        }

        private void Reset(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Id = 0;
            
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();

            textBox1.Focus();
        }
    }
}
