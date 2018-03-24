using SpeechToText.Business.Methods;
using SpeechToText.Business.Models;
using SpeechToText.Presentation_Layer.Preconditions.UserSettings;
using SpeechToText.Presentation_Layer.SessionData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpeechToText
{
    public partial class DeleteUser : Form
    {
        private bool _IsAdmin;
        public DeleteUser(bool IsAdmin)
        {
            _IsAdmin = IsAdmin;
            InitializeComponent();
            if (!_IsAdmin)
            {
                button1.Text = "Close";
                this.Text = "User details";
            }
                
        }

        private void DeleteUser_Load(object sender, EventArgs e)
        {
             
            UserBusinessModel businesslayer = new UserBusinessModel();
           // List<User> listUser = businesslayer.GetUsers();
            //List<User> listUser = GetAllUser();
           // var reducedList = listUser.Select(L => new { L.Name, L.EmailID }).ToList();
            //if(!_IsNotAdmin)
            var reducedList = GetAllUser(Isadmin());
           
            //var singleUser = reducedList.Select(U => U.EmailID == "admin@ycet.com");
            DataGridViewRow row;
            int i = 0;
            foreach (var _user in reducedList)
            {
                row = new DataGridViewRow();
                dataGridView1.Rows.Add();
                row = dataGridView1.Rows[i];
                row.Cells["ColName"].Value = _user.Name;
                row.Cells["ColEmail"].Value = _user.EmailID;
                i++;
            }

            
        }

        private bool Isadmin()
        {
            string GetUserName = LoginCredentials.LoggedEmailId.Substring(0, LoginCredentials.LoggedEmailId.LastIndexOf('@'));

            if (UserPrivilege.Isadmin(GetUserName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private IEnumerable<User> GetAllUser(bool Isadmin)
        {
             UserBusinessModel businesslayer = new UserBusinessModel();
            List<User> listUser = businesslayer.GetUsers();
            var SingleUser = listUser.Where(U => U.EmailID == LoginCredentials.LoggedEmailId);
            var AlUser = listUser;
            if (!Isadmin)
            {
                return SingleUser.ToList();
            }
              
            else
            {
                return AlUser.ToList();
            }
                
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!_IsAdmin)
            {
               // this.Close();
                List<Form> forms = new List<Form>();

                // All opened myForm instances
                foreach (Form f in Application.OpenForms)
                {
                    if (f.Name == "DeleteUser")
                        forms.Add(f);
                    if (f.Name == "Options")
                        forms.Add(f);
                    if (f.Name == "CreateUser")
                        forms.Add(f);
                }
                  

                // Now let's close opened myForm instances
                foreach (Form f in forms)
                {
                    f.Close(); 
                }
                   
            }
            else
            {
                if (dataGridView1.Rows.Count > 0)
                {
                    var UserName = dataGridView1.SelectedRows[0].Cells["ColName"].Value.ToString();
                    if (DialogResult.Yes == MessageBox.Show("Do you want to delete the user permanently ? \n all the mails of "+UserName+" will also get deleted Do you wish to continue?", "YCET User module", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                    {
                        
                        UserBusinessModel _busModel = new UserBusinessModel();
                        if (!UserBusinessModel.Isadmin(UserName))
                        {
                            if (_busModel.DeleteUser(UserName))
                            {
                                MessageBox.Show("User deleted successfully !!!", "YCET User module", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                dataGridView1.Rows.RemoveAt(this.dataGridView1.SelectedRows[0].Index);
                            }
                            else
                            {
                                MessageBox.Show("Unable to delete user", "YCET User module", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Admin user cannot be deleted", "Access denied", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }

                    }

                }
            }
            
        }







        public bool IsAdmin { get; set; }
    }
}
