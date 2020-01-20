using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;
namespace Internal_Assessment_Database
{
    public partial class newrecords : Form
    {
        public newrecords()
        {
            InitializeComponent();
            autocomplete_collegename();
            autocomplete_programcode();
        }

        void fillcombobox_collegecode()
        {

            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select distinct CollegeCode from internals.college;", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {

                    string college = myReader.GetString("CollegeCode");
                    comboBox1.Items.Add(college);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void fillcombobox_batch()
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select distinct Batch from internals.student;", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {

                    string sbatch = myReader.GetString("Batch");
                    comboBox3.Items.Add(sbatch);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void fillcombobox_program()
        {
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select distinct ProgramCode from internals.program;", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string sprogram = myReader.GetString("ProgramCode");
                    comboBox2.Items.Add(sprogram);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void autocomplete_collegename()
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col1 = new AutoCompleteStringCollection();
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select CollegeCode from internals.college", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string cname = myReader.GetString("CollegeCode");
                    col1.Add(cname);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboBox1.AutoCompleteCustomSource = col1;


        }

        void autocomplete_programcode()
        {
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection col1 = new AutoCompleteStringCollection();
            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select ProgramCode from internals.program", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                while (myReader.Read())
                {
                    string cname = myReader.GetString("ProgramCode");
                    col1.Add(cname);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            comboBox2.AutoCompleteCustomSource = col1;


        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opemfiledialog = new OpenFileDialog();
            if (opemfiledialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.textBox1.Text = opemfiledialog.FileName;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

            dataGridView1.DataSource = null;


            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                dataGridView1.Rows.RemoveAt(j);
                j--;
                while (dataGridView1.Rows.Count == 0)
                    continue;
            }

            Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbooks workbooks = app.Workbooks;

            Excel.Workbook workbook = workbooks.Open(textBox1.Text);
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            try
            {
                int rcount = worksheet.UsedRange.Rows.Count;

                int i = 0;

                //Initializing Columns
                dataGridView1.ColumnCount = worksheet.UsedRange.Columns.Count;



                for (i = 1; i < rcount; i++)
                {

                    //dataGridView1.Rows.Add(worksheet.Cells[i + 1, 1].Value, worksheet.Cells[i + 1, 2].Value, worksheet.Cells[i + 1, 3].Value, worksheet.Cells[i + 1, 4].Value, worksheet.Cells[i + 1, 5].Value, worksheet.Cells[i + 1, 6].Value, worksheet.Cells[i + 1, 7].Value, worksheet.Cells[i + 1, 8].Value, worksheet.Cells[i + 1, 9].Value);
                    dataGridView1.Rows.Add(worksheet.Cells[i + 1, 1].Value, worksheet.Cells[i + 1, 2].Value, worksheet.Cells[i + 1,3].Value, worksheet.Cells[i + 1, 4].Value, worksheet.Cells[i + 1, 5].Value);
                }

                workbook.Close();
                app.Quit();
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(workbooks);
                Marshal.ReleaseComObject(worksheet);

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                workbook.Close();
                app.Quit();
                Marshal.ReleaseComObject(workbook);
                Marshal.ReleaseComObject(workbooks);
                Marshal.ReleaseComObject(worksheet);
            }

        }

        void add_newstudent()
        {

            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    MySqlCommand cmdDataBase = new MySqlCommand("insert into internals.student (RollNo,Fname,Lname,Email,ProgramCode,Batch,GroupName,ContactNo,CollegeCode) values('" + this.dataGridView1.Rows[i].Cells[0].Value + "','" + this.dataGridView1.Rows[i].Cells[1].Value + "','" + this.dataGridView1.Rows[i].Cells[2].Value + "','" + this.dataGridView1.Rows[i].Cells[3].Value + "','" + this.comboBox2.Text + "','" + this.comboBox3.Text + "','" + this.comboBox4.Text + "','" + this.dataGridView1.Rows[i].Cells[4].Value + "','" + this.comboBox1.Text + "')", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();

                        while (myReader.Read())
                        {
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    conDataBase.Close();

                }
                MessageBox.Show("Records successfully Added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        void add_newsubject()
        {

            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    MySqlCommand cmdDataBase = new MySqlCommand("insert into internals.subject (SubjectCode,SubjectName,ProgramCode,TheoryFull,TheoryPass,PracticalFull,PracticalPass) values('" + this.dataGridView1.Rows[i].Cells[0].Value + "','" + this.dataGridView1.Rows[i].Cells[1].Value + "','" + this.dataGridView1.Rows[i].Cells[2].Value + "','" + this.dataGridView1.Rows[i].Cells[3].Value + "','" + this.dataGridView1.Rows[i].Cells[4].Value + "','" + this.dataGridView1.Rows[i].Cells[5].Value + "','"+this.dataGridView1.Rows[i].Cells[6].Value+"')", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();

                        while (myReader.Read())
                        {
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    conDataBase.Close();

                }
                MessageBox.Show("Records successfully Added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        void add_newcollege()
        {

            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    MySqlCommand cmdDataBase = new MySqlCommand("insert into internals.college (CollegeName,CollegeCode) values('" + this.dataGridView1.Rows[i].Cells[0].Value + "','" + this.dataGridView1.Rows[i].Cells[1].Value + "')", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();

                        while (myReader.Read())
                        {
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    conDataBase.Close();

                }
                MessageBox.Show("Records successfully Added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        void add_newprogram()
        {

            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    MySqlCommand cmdDataBase = new MySqlCommand("insert into internals.program (ProgramCode,ProgramName) values('" + this.dataGridView1.Rows[i].Cells[0].Value + "','" + this.dataGridView1.Rows[i].Cells[1].Value + "')", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();

                        while (myReader.Read())
                        {
                        }
                    }

                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    conDataBase.Close();

                }
                MessageBox.Show("Records successfully Added.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void button1_Click(object sender, EventArgs e)
        {
            if(label1.Text=="Add New Subject")
            {
                add_newsubject();
            }
            else if(label1.Text=="Add New Program")
            {
                add_newprogram();
            }
            else if(label1.Text=="Add New College")
            {
                add_newcollege();
            }
            else if(label1.Text=="Add Student Record")
            {
                add_newstudent();
            }
            
           
        }

        void update_newstudent()
        {

            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("update internals.student set Fname='" + this.dataGridView1.Rows[i].Cells[1].Value + "',Lname='" + this.dataGridView1.Rows[i].Cells[2].Value + "',Email='" + this.dataGridView1.Rows[i].Cells[3].Value + "',ProgramCode='" + this.dataGridView1.Rows[i].Cells[4].Value + "',Batch='" + this.dataGridView1.Rows[i].Cells[5].Value + "',GroupName='" + this.dataGridView1.Rows[i].Cells[6].Value + "',ContactNo='" + this.dataGridView1.Rows[i].Cells[7].Value + "',CollegeCode='" + this.dataGridView1.Rows[i].Cells[8].Value + "' where RollNo='" + this.dataGridView1.Rows[i].Cells[0].Value + "';", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        while (myReader.Read())
                        {
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Informations successfully Updated.");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void update_newsubject()
        {

            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("update internals.subject set SubjectName='" + this.dataGridView1.Rows[i].Cells[1].Value + "',ProgramCode='" + this.dataGridView1.Rows[i].Cells[2].Value + "',TheoryFull='" + this.dataGridView1.Rows[i].Cells[3].Value + "',TheoryPass='" + this.dataGridView1.Rows[i].Cells[4].Value + "',PracticalFull='" + this.dataGridView1.Rows[i].Cells[5].Value + "',PracticalPass='" + this.dataGridView1.Rows[i].Cells[6].Value + "' where SubjectCode='" + this.dataGridView1.Rows[i].Cells[0].Value + "';", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        while (myReader.Read())
                        {
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Informations successfully Updated.");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void update_newprogram()
        {

            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("update internals.program set ProgramName='" + this.dataGridView1.Rows[i].Cells[1].Value + "' where ProgramCode='" + this.dataGridView1.Rows[i].Cells[0].Value + "';", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        while (myReader.Read())
                        {
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Informations successfully Updated.");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        void update_newcollege()
        {

            try
            {
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    String constring = "datasource=localhost;port=3306;username=root;password=root";
                    MySqlConnection conDataBase = new MySqlConnection(constring);
                    MySqlCommand cmdDataBase = new MySqlCommand("update internals.college set CollegeName='" + this.dataGridView1.Rows[i].Cells[0].Value + "' where CollegeCode='" + this.dataGridView1.Rows[i].Cells[1].Value + "';", conDataBase);
                    MySqlDataReader myReader;
                    try
                    {
                        conDataBase.Open();
                        myReader = cmdDataBase.ExecuteReader();
                        while (myReader.Read())
                        {
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                MessageBox.Show("Informations successfully Updated.");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (label1.Text == "Add New Subject")
            {
                update_newsubject();
            }
            else if (label1.Text == "Add New Program")
            {
                update_newprogram();
            }
            else if (label1.Text == "Add New College")
            {
                update_newcollege();
            }
            else if (label1.Text == "Add Student Record")
            {
                update_newstudent();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;


            for (int j = 0; j < dataGridView1.Rows.Count - 1; j++)
            {
                dataGridView1.Rows.RemoveAt(j);
                j--;
                while (dataGridView1.Rows.Count == 0)
                    continue;
            }
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            Color temp = Color.FromArgb(0x0066CC);
            panel3.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0xFFFFFF);
            label3.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
        }

        private void panel4_MouseMove(object sender, MouseEventArgs e)
        {
            Color temp = Color.FromArgb(0x0066CC);
            panel4.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0xFFFFFF);
            label4.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
        }

        private void panel5_MouseMove(object sender, MouseEventArgs e)
        {
            Color temp = Color.FromArgb(0x0066CC);
            panel5.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0xFFFFFF);
            label5.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
        }

        private void panel6_MouseMove(object sender, MouseEventArgs e)
        {
            Color temp = Color.FromArgb(0x0066CC);
            panel6.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0xFFFFFF);
            label6.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Color temp = Color.FromArgb(0xFFFFFF);
            panel3.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);
            panel4.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);
            panel5.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);
            panel6.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);
            panel7.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0x000000);
            label3.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
            label4.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
            label5.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
            label6.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
            label7.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
        }

        private void panel7_Move(object sender, EventArgs e)
        {
            Color temp = Color.FromArgb(0x0066CC);
            panel7.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);
        }

        private void panel7_MouseMove(object sender, MouseEventArgs e)
        {
            Color temp = Color.FromArgb(0x0066CC);
            panel7.BackColor = Color.FromArgb(temp.R, temp.G, temp.B);

            Color temp1 = Color.FromArgb(0xFFFFFF);
            label7.ForeColor = Color.FromArgb(temp1.R, temp1.G, temp1.B);
        }
        DataTable dbdataset;
        private void label7_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "Username");
            dataGridView1.Columns.Add("column1", "Old Password");
            dataGridView1.Columns.Add("column2", "New Password");
            label1.Text = label7.Text;
            panel9.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            panel1.Height = 389;
            panel8.Location = new Point(3, 358);
        }

        private void label3_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "Roll No.");
            dataGridView1.Columns.Add("column1", "Fname");
            dataGridView1.Columns.Add("column2", "Lname");
            dataGridView1.Columns.Add("column3", "Email");
            // dataGridView1.Columns.Add("column4", "Program Code");
            //  dataGridView1.Columns.Add("column5", "Batch");
            // dataGridView1.Columns.Add("column6", "Group Name");
            dataGridView1.Columns.Add("column7", "Contact No.");
            //   dataGridView1.Columns.Add("column8", "College Code");
            // dataGridView1.DataSource = null;

            panel1.Height = 565;
            panel8.Location = new Point(4, 534);

            label1.Text = label3.Text;
            panel9.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            textBox1.Visible = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "Subject Code");
            dataGridView1.Columns.Add("column1", "Subject Name");
            dataGridView1.Columns.Add("column2", "Program Code");
            dataGridView1.Columns.Add("column3", "Theory Full");
            dataGridView1.Columns.Add("column4", "Theory Pass");
            dataGridView1.Columns.Add("column5", "Practical Full");
            dataGridView1.Columns.Add("column6", "Practical Pass");
            label1.Text = label4.Text;
            panel9.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            panel1.Height = 389;
            panel8.Location = new Point(3, 358);
        }

        private void label5_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "Program Code");
            dataGridView1.Columns.Add("column1", "Program Name");
            label1.Text = label5.Text;
            panel9.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            panel1.Height = 389;
            panel8.Location = new Point(3, 358);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "College Name");
            dataGridView1.Columns.Add("column1", "College Code");
            label1.Text = label6.Text;
            panel9.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            panel1.Height = 389;
            panel8.Location = new Point(3, 358);
        }

        private void panel7_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "Username");
            dataGridView1.Columns.Add("column1", "Old Password");
            dataGridView1.Columns.Add("column2", "New Password");
            label1.Text = label7.Text;
            panel9.Visible = true;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            panel1.Height = 389;
            panel8.Location = new Point(3, 358);
        }

        private void panel3_Click(object sender, EventArgs e)
        {

            // dataGridView1.DataSource = null;
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "Roll No.");
            dataGridView1.Columns.Add("column1", "Fname");
            dataGridView1.Columns.Add("column2", "Lname");
            dataGridView1.Columns.Add("column3", "Email");
            //dataGridView1.Columns.Add("column4", "Program Code");
            //dataGridView1.Columns.Add("column5", "Batch");
           // dataGridView1.Columns.Add("column6", "Group Name");
            dataGridView1.Columns.Add("column7", "Contact No.");
           // dataGridView1.Columns.Add("column8", "College Code");

            panel1.Height = 565;
            panel8.Location = new Point(4, 534);

            label1.Text = label3.Text;
            panel9.Visible = false;
            button2.Visible = true;
            button3.Visible = true;
            textBox1.Visible = true;
        }

        private void panel6_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "College Name");
            dataGridView1.Columns.Add("column1", "College Code");
            label1.Text = label6.Text;
            panel9.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            panel1.Height = 389;
            panel8.Location = new Point(3, 358);
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "Program Code");
            dataGridView1.Columns.Add("column1", "Program Name");
            label1.Text = label5.Text;
            panel9.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            panel1.Height = 389;
            panel8.Location = new Point(3, 358);
        }

        private void panel4_Click(object sender, EventArgs e)
        {
            int n = dataGridView1.Columns.Count + 1;

            for (int j = 1; j < n; j++)
            {
                dataGridView1.Columns.RemoveAt(0);
            }

            dataGridView1.Columns.Add("column0", "Subject Code");
            dataGridView1.Columns.Add("column1", "Subject Name");
            dataGridView1.Columns.Add("column2", "Program Code");
            dataGridView1.Columns.Add("column3", "Theory Full");
            dataGridView1.Columns.Add("column4", "Theory Pass");
            dataGridView1.Columns.Add("column5", "Practical Full");
            dataGridView1.Columns.Add("column6", "Practical Pass");
            label1.Text = label4.Text;
            panel9.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            textBox1.Visible = false;
            panel1.Height = 389;
            panel8.Location = new Point(3, 358);
        }

        void update_password()
        {

            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("update internals.login set Password='" + this.dataGridView1.Rows[0].Cells[2].Value + "' where UserName='" + this.dataGridView1.Rows[0].Cells[0].Value + "';", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();

                myReader = cmdDataBase.ExecuteReader();
                
                while (myReader.Read())
                {
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {

            String constring = "datasource=localhost;port=3306;username=root;password=root";
            MySqlConnection conDataBase = new MySqlConnection(constring);
            MySqlCommand cmdDataBase = new MySqlCommand("select * from internals.login where UserName='" + this.dataGridView1.Rows[0].Cells[0].Value + "' and Password='" + this.dataGridView1.Rows[0].Cells[1].Value + "';", conDataBase);
            MySqlDataReader myReader;
            try
            {
                conDataBase.Open();
                myReader = cmdDataBase.ExecuteReader();
                int count = 0;
                while (myReader.Read())
                {
                    count = count + 1;
                }
                
                if (count == 1)
                {
                    update_password();
                    MessageBox.Show("Password Successfully Updated.");
                    
                }
                
                else
                {
                    MessageBox.Show("Your username or password didn't match.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void newrecords_Load(object sender, EventArgs e)
        {
            fillcombobox_batch();
            fillcombobox_collegecode();
            fillcombobox_program();

            dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            // dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            // dataGridView1.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dataGridView1.BackgroundColor = Color.White;
            dataGridView1.EnableHeadersVisualStyles = false;
            dataGridView1.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            //dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            //dataGridView1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            
        }

        private void newrecords_DoubleClick(object sender, EventArgs e)
        {
            fillcombobox_batch();
            fillcombobox_program();
            fillcombobox_collegecode();
            autocomplete_programcode();
            autocomplete_collegename();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

		private void panel8_Paint(object sender, PaintEventArgs e)
		{

		}

		

		private void comboBox1_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
				comboBox2.Focus();
			}
		}

		private void comboBox2_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
				comboBox3.Focus();
			}
		}

		private void comboBox3_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				e.Handled = true;
				e.SuppressKeyPress = true;
				comboBox4.Focus();
			}
		}
	}

}
