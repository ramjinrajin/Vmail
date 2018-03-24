using SpeechToText.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechToText.Presentation_Layer
{
    public partial class SQLDBConnection : Form
    {
        public SQLDBConnection()
        {
            InitializeComponent();
        }

        private void btnValidate_Click(object sender, EventArgs e)
        {
            string ConString = txtString.Text;
            if (ConString != "" && ConString != null)
            {
                if (ConString.Contains("master"))
                {
                    if (ConnectionVerified(ConString))
                    {
                        ConfigureSqlConnectionString(ConString);
                        LoadSQl();
                        MessageBox.Show("Application Configured sucessfully !!!", "Sucess!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        InitializeApplication();
                        // MessageBox.Show(ConString.Replace("master","YCET"));
                    }
                }
                else
                {
                    MessageBox.Show("Connection string should contain 'Initial Catalog=master'", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
               
            }
            else
            {
                MessageBox.Show("Connection string cannot be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void InitializeApplication()
        {
            // this.Close();
            List<Form> forms = new List<Form>();

            // All opened myForm instances
            foreach (Form f in Application.OpenForms)
            {
                // if (f.Name != "DeleteUser")              
                forms.Add(f);
            }


            // Now let's close opened myForm instances
            foreach (Form f in forms)
            {
                f.Hide();
            }

            UserLogin userloginfrm = new UserLogin(true);
            userloginfrm.Show();
        }

        private bool ConnectionVerified(string ConString)
        {
            try
            {
                SqlConnection con = new SqlConnection(ConString);
                con.Open();
                return true;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return false;

        }


        private void ConfigureSqlConnectionString(string ConString)
        {
            string Path = @"C:\\Ycet\\";
            if (!Directory.Exists(Path))
            {
                try
                {
                    CreateFileDirectory(Path, ConString);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Unable to create file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            }



        }

        private static void CreateFileDirectory(string Path, string ConString)
        {
            System.IO.Directory.CreateDirectory(Path);
            using (StreamWriter sw = File.AppendText(@"C:\\Ycet\\SqlDBConnect.ini"))
            {

                sw.WriteLine(ConString);

            }
        }

        private void LoadSQl()
        {

            //create database YCET 
            //Data Source=DESKTOP-VK8O6K0\SQLEXPRESS;Initial Catalog=master;User ID=sa;Password=admin@123
            //Data Source=DESKTOP-GENB0UD\SQLEXPRESS;Initial Catalog=master;User ID=sa;Password=admin@123
            // string path = Path.Combine(Environment.CurrentDirectory, @"SqlScripts\", "CreateDB.sql");

            try
            {

                

               
                string CreateDB = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\","CreateDB.sql");
                string CreateTables = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\", "CreateUserTable.sql");
                string CreateSuperUser = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\", "CreateSuperUser.sql");
                string CreateTablesMail = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\", "InsertMail.sql");
                string ProcSentMail = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\", "ProcSentMail.sql");
                string ProcSaveDraft = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\", "ProcSaveDraft.sql");
                string ProcSearchMail = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\", "ProcSearchMail.sql");
                //ProcSaveDraft

                List<string> ExecuteQueries = new List<string>();
                ExecuteQueries.Add(CreateDB);
                ExecuteQueries.Add(CreateTables);
                ExecuteQueries.Add(CreateSuperUser);
                ExecuteQueries.Add(CreateTablesMail);
                ExecuteQueries.Add(ProcSentMail);
                ExecuteQueries.Add(ProcSaveDraft);
                ExecuteQueries.Add(ProcSearchMail);



                string path2 = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts");
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path2);
                int count = dir.GetFiles().Length;

                foreach (string Query in ExecuteQueries)
                {
                    string connectionString = System.IO.File.ReadAllText(@"C:\\Ycet\\SqlDBConnect.ini");
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {

                        using (var streamReader = new StreamReader(Query, Encoding.UTF8))
                        {
                            string GetQuery = streamReader.ReadToEnd();
                            con.Open();
                            SqlCommand cmd = new SqlCommand(GetQuery, con);
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                            streamReader.Dispose();
                        }
                    }
                }




            }

            catch
            {
                throw;
            }



        }

        private void SQLDBConnection_Load(object sender, EventArgs e)
        {

        }

    }
}
