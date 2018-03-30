using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Speech.Synthesis;
using System.Configuration;
using SpeechToText.Presentation_Layer.SessionData;
using SpeechToText.ApplicationLayer;
using SpeechToText.Business.Models;
using SpeechToText.Presentation_Layer;
using SpeechToText.Business.Methods;
using SpeechToText.Presentation_Layer.Preconditions.UserSettings;
using SpeechToText.Presentation_Layer.Preconditions;
using Vmail._6.Infrastructure.MainModule;
using System.Speech.Recognition;
using System.Threading;

namespace SpeechToText
{
    public partial class Mailbox : Form
    {

        //   MailView _view = new MailView(0, false, "", "", "");
        // public EventHandler ReloadMail { get; set; }


        public bool MailTab_Inbox = false;
        public bool MailTab_SentMails = false;
        public bool MailTab_Draft = false;
        public bool MailTab_Recycle = false;
        SpeechSynthesizer synth = new SpeechSynthesizer();
        public Mailbox()
        {



            InitializeComponent();
            WelcomeMessage();

        }


        public async Task WelcomeMessage()
        {
            LanguagePackageConfig.Explore();
            SpeechRecognitionWithDictationGrammar();
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();

            await Task.Delay(100);
            if (SpeechModule.GetStatus())
            {
                GreetingMsg();
            }

        }


        SpeechRecognitionEngine _recognizer = null;
        ManualResetEvent manualResetEvent = null;
        void SpeechRecognitionWithDictationGrammar()
        {
            _recognizer = new SpeechRecognitionEngine();
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("compose mail")));
            _recognizer.LoadGrammar(new Grammar(new GrammarBuilder("Open compose Window")));
            _recognizer.LoadGrammar(new DictationGrammar());
            _recognizer.SpeechRecognized += speechRecognitionWithDictationGrammar_SpeechRecognized;
            _recognizer.SetInputToDefaultAudioDevice();
            _recognizer.RecognizeAsync(RecognizeMode.Multiple);
        }

        string SearchWord = "";
        public void speechRecognitionWithDictationGrammar_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();

            // MessageBox.Show(string.Format("you just told {0}",e.Result.Text));
            if (e.Result.Text.Trim().Contains("compose mail")
                || e.Result.Text.Trim().Contains("Open compose Window")
                || e.Result.Text.Trim().Contains("Open compose Window"))
            {
                ComposeMail composeMail = new ComposeMail(SpeechToText.ComposeMail.Mailtype.ComposeMail, "", "", "", 0);
                composeMail.ShowDialog();
                //DoAction(e.Result.Text);

            }
            else if (e.Result.Text.Trim().Contains("ok vmail can you read"))
            {
                DoAction(e.Result.Text);
            }
            else if (e.Result.Text.Trim().Contains("Open the draft"))
            {
                // DraftMailLoad();
            }
            else if (e.Result.Text.ToLower().Contains("Reply to this mail".ToLower()))
            {
                if (ChooseMail)
                {
                    
                     MailView _mailViewForm = new MailView(FilterMail.MsgId, false, FilterMail.FromEmailId,FilterMail.Subject, FilterMail.Message, FilterMail.FileName,true);
                    _mailViewForm.Show();
                }
                else
                {
                    synth.Speak("Please read a mail before reply");
                }
            }
            else if (e.Result.Text.Trim().Contains("signout") || e.Result.Text.Trim().Contains("seven"))
            {
                Signout();
            }
            else
            {
                txtSearch.Text = e.Result.Text;
            }
        }

        private static void DoAction(string text)
        {
            string RecognizedText = text;







            string Number = testc(RecognizedText);// StaticConvertToNumberic(RecognizedText.ToLower());
            if (Number != "" && Number != "NIL")
            {
                int n;
                bool isNumeric = int.TryParse(Number, out n);

                if (isNumeric)
                {
                    StaticMailNo = Convert.ToInt32(Number);
                    StaticReadMailByNo(StaticMailNo);
                    RecognizedText = "";
                }



            }











        }

        private static string testc(string v)
        {
            string Replacer;
            string Number = v.Replace("ok vmail can you read the mail", "");
            string[] words = v.Split(' ');
            int Count = words.Count();
            Number = words[Count - 1];
            if (Number == "" || Number == " ")
            {
                return "NIL";
            }
            Replacer = Number.Trim();
            List<string> SingleDigit = new List<string>
            {
                "zero0", "one1", "two2", "three3", "four4", "five5", "six6", "seven7", "eight8", "nine9", "ten10", "eleven11", "twelve12", "thirteen13", "fourteen14", "fifteen15", "sixteen16", "seventeen17", "eighteen18", "nineteen19"
            };

            //  string[] words = NumberAlphabets.Split(' ');

            //  if (words.Count() == 1)
            {
                Number = SingleDigit.FirstOrDefault(x => x.ToString().Contains(Number.Trim().ToLower()));
                if (Number != "" || Number != " ")
                {
                    Number = Number.Replace(Replacer, "");
                }

                else
                {
                    return "NIL";
                }



            }


            return Number;
        }

        private void GreetingMsg()
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Hi Welcome to YCET mail ");
            //synth.Speak("This application developed specially for blind people ");
            //synth.Speak("Here is some commands that help to intract with this application");
            synth.Speak("Say Compose to compose a new mail");
            //synth.Speak("say read my mails to read all unread mails for you");
            //synth.Speak("say close to terminate this application");
            //synth.Speak("Say next to focus the next control");
            ReadMail();
        }

        public EventHandler ReloadMail(bool IssentMail)
        {
            if (IssentMail)
            {
                LoadSentMails();
            }
            else
            {
                LoadInboxMails();
            }
            MessageBox.Show("hi");
            return new EventHandler(onEvent);
        }

        private void onEvent(object sender, EventArgs e)
        {

        }



        string CmdReader = "Nil";
        int MailNo = 0;
        static int StaticMailNo = 0;
        private string FilterCommand(string ReadText)
        {
            string RecognizedText = ReadText;

            List<string> ListCommands = new List<string>
        {

                "Read the mail "//dont remove space
        };

            foreach (string cmd in ListCommands)
            {

                if (RecognizedText.ToLower().Contains(cmd.ToLower()))
                {
                    if (cmd == "Read the mail ")
                    {
                        string Number = StaticConvertToNumberic(RecognizedText.ToLower());
                        if (Number != "" && Number != "NIL")
                        {
                            int n;
                            bool isNumeric = int.TryParse(Number, out n);

                            if (isNumeric)
                            {
                                MailNo = Convert.ToInt32(Number);
                                ReadMailByNo(MailNo);
                                RecognizedText = "";
                            }


                            return RecognizedText;
                        }
                    }
                    else
                    {
                        CmdReader = cmd;
                        RecognizedText = RecognizedText.ToLower().Replace(cmd.ToLower(), "");
                        return RecognizedText;
                    }



                }

            }



            return RecognizedText;

        }


        public string StaticConvertToNumberic(string NumberAlphabets)
        {
            string Replacer;
            string Number = NumberAlphabets.Replace("read the mail ", "");
            if (Number == "" || Number == " ")
            {
                return "NIL";
            }
            Replacer = Number.Trim();
            List<string> SingleDigit = new List<string>
            {
                "zero0", "one1", "two2", "three3", "four4", "five5", "six6", "seven7", "eight8", "nine9", "ten10", "eleven11", "twelve12", "thirteen13", "fourteen14", "fifteen15", "sixteen16", "seventeen17", "eighteen18", "nineteen19"
            };

            string[] words = NumberAlphabets.Split(' ');

            //  if (words.Count() == 1)
            {
                Number = SingleDigit.FirstOrDefault(x => x.ToString().Contains(Number.Trim().ToLower()));
                if (Number != "" || Number != " ")
                {
                    Number = Number.Replace(Replacer, "");
                }

                else
                {
                    return "NIL";
                }



            }


            return Number;

        }

        private void Mailbox_Load(object sender, EventArgs e)
        {
            btnCompose.Focus();
            //1202, 707
            this.Size = new System.Drawing.Size(1202, 707);
            label2.Text = "Total Mails : " + MailServer.GetMailCount(LoginCredentials.LoggedEmailId);
            //757
            //btnAccountSettings.Enabled = false;
            NoMailLabel.Visible = false;
            //InitTimer();

            // LoadSQl();
            SetLogin();
            LoadInboxMails();
            //

            txtSearch.Focus();

        }



        private void SetLogin()
        {

            string NewString = LoginCredentials.LoggedEmailId.Substring(0, LoginCredentials.LoggedEmailId.LastIndexOf('@'));


            label6.Text = "hi " + NewString + " !!!";
        }



        private void LoadSQl()
        {

            //create database YCET 

            //Data Source=DESKTOP-GENB0UD\\SQLEXPRESS;Initial Catalog=master;User ID=sa;Password=admin@123

            try
            {
                string CreateDB = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\CreateDB.sql");
                string CreateTables = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\CreateUserTable.sql");
                string CreateTablesMail = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\InsertMail.sql");
                string ProcSentMail = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts\ProcSentMail.sql");

                List<string> ExecuteQueries = new List<string>();
                ExecuteQueries.Add(CreateDB);
                ExecuteQueries.Add(CreateTables);
                ExecuteQueries.Add(CreateTablesMail);
                ExecuteQueries.Add(ProcSentMail);



                string path2 = Path.Combine(Environment.CurrentDirectory, @"SQL Scripts");
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path2);
                int count = dir.GetFiles().Length;

                foreach (string Query in ExecuteQueries)
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["Master"].ConnectionString.ToString();
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

        private System.Windows.Forms.Timer timer1;
        public void InitTimer()
        {
            System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 2000; // in miliseconds
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SpeechSynthesizer synth = new SpeechSynthesizer();
            synth.SetOutputToDefaultAudioDevice();
            synth.Speak("Hi Welcome to Speech to text ");

        }

        private void btnCompose_Click(object sender, EventArgs e)
        {
            ComposeMail composeMail = new ComposeMail(SpeechToText.ComposeMail.Mailtype.ComposeMail, "", "", "", 0);
            composeMail.ShowDialog();
        }

        private void btnAccountSettings_Click(object sender, EventArgs e)
        {

            string GetUserName = LoginCredentials.LoggedEmailId.Substring(0, LoginCredentials.LoggedEmailId.LastIndexOf('@'));

            if (UserPrivilege.Isadmin(GetUserName))
            {
                Options frmOptions = new Options(true);
                frmOptions.ShowDialog();
            }
            else
            {
                Options frmOptions = new Options(false);
                frmOptions.ShowDialog();
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PopUpMail();
        }



        //  _mailViewForm.SomeEvent += new EventHandler(this.HandleSomeEvent);
        //public void HandleSomeEvent(object sender, EventArgs e)
        //{
        //}

        private void PopUpMail()
        {
            if (MailTab_Inbox)
            {
                if (gridviewMails.SelectedRows.Count > 0)
                {
                    string FromMail = gridviewMails.SelectedRows[0].Cells["ColFrom"].Value.ToString();
                    string Subject = gridviewMails.SelectedRows[0].Cells["ColSubject"].Value.ToString();
                    string Msg = gridviewMails.SelectedRows[0].Cells["ColMsg"].Value.ToString();
                    int MsgId = (int)gridviewMails.SelectedRows[0].Cells["ColMsgId"].Value;
                    string FileName = gridviewMails.SelectedRows[0].Cells["ColFileName"].Value.ToString();
                    MakeMailAsRead();
                    MailView _mailViewForm = new MailView(MsgId, false, FromMail, Subject, Msg, FileName, false);
                    _mailViewForm.ShowDialog();
                    LoadInboxMails();

                }
            }
            else if (MailTab_Draft)
            {
                if (gridviewMails.SelectedRows.Count > 0)
                {

                    string FromMail = gridviewMails.SelectedRows[0].Cells["ColTo"].Value.ToString();
                    string Subject = gridviewMails.SelectedRows[0].Cells["ColSubject"].Value.ToString();
                    string Msg = gridviewMails.SelectedRows[0].Cells["ColMsg"].Value.ToString();
                    int MsgId = (int)gridviewMails.SelectedRows[0].Cells["ColMsgId"].Value;
                    ComposeMail _composeMail = new ComposeMail(ComposeMail.Mailtype.DraftMail, FromMail, Subject, Msg, MsgId);
                    _composeMail.ShowDialog();


                }
            }
            else if (MailTab_SentMails)
            {
                if (gridviewMails.SelectedRows.Count > 0)
                {
                    string FromMail = gridviewMails.SelectedRows[0].Cells["ColTo"].Value.ToString();
                    string Subject = gridviewMails.SelectedRows[0].Cells["ColSubject"].Value.ToString();
                    string Msg = gridviewMails.SelectedRows[0].Cells["ColMsg"].Value.ToString();
                    int MsgId = (int)gridviewMails.SelectedRows[0].Cells["ColMsgId"].Value;
                    string FileName = gridviewMails.SelectedRows[0].Cells["ColFileName"].Value.ToString();
                    MailView _mailViewForm = new MailView(MsgId, true, FromMail, Subject, Msg, FileName, false);
                    _mailViewForm.ShowDialog();
                    LoadSentMails();
                }
            }
            else if (MailTab_Recycle)
            {
                this.Size = new System.Drawing.Size(1202, 757);
            }
        }

        private void MakeMailAsRead()
        {
            if (gridviewMails.SelectedRows.Count > 0)
            {
                MailServer.MarkMsgAsRead((int)gridviewMails.SelectedRows[0].Cells["ColMsgId"].Value);
                gridviewMails.SelectedRows[0].DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular);
            }
        }

        private void btnInbox_Click(object sender, EventArgs e)
        {
            // SpeakTest();
            LoadInboxMails();


        }

        private void ReadMail()
        {
            if (SpeechModule.GetStatus())
            {
                synth.SetOutputToDefaultAudioDevice();
                MailServer mailserver = new MailServer();
                List<Mail> listmails = mailserver.ReceivedMailsList(LoginCredentials.LoggedEmailId);
                if (listmails != null)
                {
                    bool UnreadCount = listmails.Any(X => X.IsRead == 0);
                    if (UnreadCount)
                    {
                        synth.SetOutputToDefaultAudioDevice();
                        synth.Speak("You have some unread mails");
                        synth.Speak("I will read one by one for you");
                        IEnumerable<Mail> Unreadmails = listmails.Where(X => X.IsRead == 0);
                        int k = 0;
                        foreach (var Msg in Unreadmails)
                        {
                            k++;
                            synth.Speak("Message " + k.ToString());
                            synth.Speak("From " + Msg.FromEmailId);
                            synth.Speak("Subject" + Msg.Subject);
                            synth.Speak("Message " + Msg.Message);
                            MailServer.MarkMsgAsRead(Msg.MsgId);
                            LoadInboxMails();
                        }


                    }
                    else
                    {
                        synth.Speak("You dont have any unread mails");
                    }

                }

            }

        }

        private static void StaticReadMailByNo(int i)
        {
            if (i != 0)
            {
                if (SpeechModule.GetStatus())
                {
                    SpeechSynthesizer synth = new SpeechSynthesizer();
                    synth.SetOutputToDefaultAudioDevice();
                    MailServer mailserver = new MailServer();
                    List<Mail> listmails = mailserver.ReceivedMailsList(LoginCredentials.LoggedEmailId);
                    if (listmails != null)
                    {

                        if (listmails.Count >= i)
                        {
                            synth.SetOutputToDefaultAudioDevice();
                            Mail FilterMail = listmails.ElementAt(i - 1);
                            synth.Speak("Message " + i.ToString());
                            synth.Speak("From " + FilterMail.FromEmailId);
                            synth.Speak("Subject" + FilterMail.Subject);
                            synth.Speak("Message " + FilterMail.Message);
                        }
                        else
                        {
                            synth.Speak("You have only " + listmails.Count + " mail");
                        }

                    }

                }
            }



        }


        bool ChooseMail = false;
        Mail FilterMail = null;
        private void ReadMailByNo(int i)
        {
            string text;
            if (i != 0)
            {
                if (SpeechModule.GetStatus())
                {
                    synth.SetOutputToDefaultAudioDevice();
                    MailServer mailserver = new MailServer();
                    List<Mail> listmails = mailserver.ReceivedMailsList(LoginCredentials.LoggedEmailId);
                    if (listmails != null)
                    {

                        if (listmails.Count >= i)
                        {
                            FilterMail = listmails.ElementAt(i - 1);

                            synth.SetOutputToDefaultAudioDevice();

                            synth.Speak("Message " + i.ToString());
                            synth.Speak("From " + FilterMail.FromEmailId);
                            synth.Speak("Subject" + FilterMail.Subject);
                            synth.Speak("Message " + FilterMail.Message);
                            if(FilterMail.FileName!="NIL")
                            {
                                var fileStream = new FileStream(string.Format(@"D:\test\{0}.txt", FilterMail.FileName), FileMode.Open, FileAccess.Read);
                                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                                {
                                    text = streamReader.ReadToEnd();
                                    synth.Speak(text);
                                }
                            }
                           
                            ChooseMail = true;
                            //   MailView _mailViewForm = new MailView(FilterMail.MsgId, false, FilterMail.FromEmailId,FilterMail.Subject, FilterMail.Message, FilterMail.FileName);
                            //_mailViewForm.ShowDialog();
                        }
                        else
                        {
                            synth.Speak("You have only " + listmails.Count + " mail");
                        }

                    }

                }
            }



        }

        private void LoadInboxMails()
        {

            MailTab_Inbox = true;
            MailTab_SentMails = false;
            MailTab_Draft = false;
            MailTab_Recycle = false;
            gridviewMails.Columns["ColFrom"].Visible = true;
            gridviewMails.Columns["ColTo"].Visible = false;
            gridviewMails.Rows.Clear();
            MailServer mailserver = new MailServer();
            List<Mail> listmails = mailserver.ReceivedMailsList(LoginCredentials.LoggedEmailId);
            DataGridViewRow row;
            int i = 0;
            this.Size = new System.Drawing.Size(1202, 707);
            if (listmails.Count != 0)
            {
                NoMailLabel.Visible = false;
                foreach (var _mails in listmails)
                {
                    row = new DataGridViewRow();
                    gridviewMails.Rows.Add();
                    row = gridviewMails.Rows[i];
                    if (_mails.IsRead == 0)// Make the Mail bold if the mail is unread
                        row.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                    row.Cells["ColFrom"].Value = _mails.FromEmailId;
                    row.Cells["ColSubject"].Value = _mails.Subject;
                    row.Cells["ColMsgId"].Value = _mails.MsgId;
                    row.Cells["ColMsg"].Value = _mails.Message;
                    row.Cells["ColFileName"].Value = _mails.FileName;
                    i++;
                }
                gridviewMails.ClearSelection();
            }

            else
            {
                NoMailLabel.Visible = true;

            }

        }

        private void LoadSearchResults(string SearchTerm)
        {
            MailTab_Inbox = true;
            MailTab_SentMails = false;
            MailTab_Draft = false;
            MailTab_Recycle = false;
            gridviewMails.Columns["ColFrom"].Visible = true;
            gridviewMails.Columns["ColTo"].Visible = false;
            gridviewMails.Rows.Clear();
            MailServer mailserver = new MailServer();
            List<Mail> listmails = mailserver.SearchMailsList(LoginCredentials.LoggedEmailId, SearchTerm);//Main method to get search result
            DataGridViewRow row;
            int i = 0;
            this.Size = new System.Drawing.Size(1202, 707);
            if (listmails.Count != 0)
            {
                NoMailLabel.Visible = false;
                foreach (var _mails in listmails)
                {

                    row = new DataGridViewRow();
                    gridviewMails.Rows.Add();
                    row = gridviewMails.Rows[i];
                    row.Cells["ColFrom"].Value = _mails.FromEmailId;
                    row.Cells["ColSubject"].Value = _mails.Subject;
                    row.Cells["ColMsgId"].Value = _mails.MsgId;
                    row.Cells["ColMsg"].Value = _mails.Message;
                    if (_mails.IsRead == 0)
                        row.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                    i++;
                }
                gridviewMails.ClearSelection();
            }

            else
            {
                NoMailLabel.Visible = true;

            }

        }



        private void lbSignout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Signout();

        }

        private static void Signout()
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

            //UserLogin userloginfrm = new UserLogin(true);
            //userloginfrm.Show();
            ApiAuthenticate auth = new ApiAuthenticate();
            auth.Show();
        }

        private void btnSent_Click(object sender, EventArgs e)
        {
            LoadSentMails();


        }

        private void LoadSentMails()
        {
            MailTab_Inbox = false;
            MailTab_SentMails = true;
            MailTab_Draft = false;
            MailTab_Recycle = false;
            gridviewMails.Columns["ColFrom"].Visible = false;
            gridviewMails.Columns["ColTo"].Visible = true;
            gridviewMails.Rows.Clear();
            MailServer mailserver = new MailServer();
            List<Mail> listmails = mailserver.SentMailsList(LoginCredentials.LoggedEmailId);
            DataGridViewRow row;
            int i = 0;
            this.Size = new System.Drawing.Size(1202, 707);
            if (listmails.Count != 0)
            {
                NoMailLabel.Visible = false;
                foreach (var _mails in listmails)
                {
                    row = new DataGridViewRow();
                    gridviewMails.Rows.Add();
                    row = gridviewMails.Rows[i];
                    row.Cells["ColTo"].Value = _mails.EmailID;
                    row.Cells["ColSubject"].Value = _mails.Subject;
                    row.Cells["ColMsg"].Value = _mails.Message;
                    row.Cells["ColMsgId"].Value = _mails.MsgId;
                    row.Cells["ColFileName"].Value = _mails.FileName;
                    i++;
                }
                gridviewMails.ClearSelection();
            }

            else
                NoMailLabel.Visible = true;
        }

        private void btnDraft_Click(object sender, EventArgs e)
        {
            DraftMailLoad();
        }

        private void DraftMailLoad()
        {
            MailTab_Inbox = false;
            MailTab_SentMails = false;
            MailTab_Draft = true;
            MailTab_Recycle = false;
            gridviewMails.Columns["ColFrom"].Visible = false;
            gridviewMails.Columns["ColTo"].Visible = true;
            gridviewMails.Rows.Clear();
            MailServer mailserver = new MailServer();
            List<Mail> listmails = mailserver.DraftMailsList(LoginCredentials.LoggedEmailId);
            DataGridViewRow row;
            int i = 0;
            this.Size = new System.Drawing.Size(1202, 707);
            if (listmails.Count != 0)
            {
                NoMailLabel.Visible = false;
                foreach (var _mails in listmails)
                {
                    row = new DataGridViewRow();
                    gridviewMails.Rows.Add();
                    row = gridviewMails.Rows[i];
                    row.Cells["ColTo"].Value = _mails.EmailID;
                    row.Cells["ColSubject"].Value = _mails.Subject;
                    row.Cells["ColMsg"].Value = _mails.Message;
                    row.Cells["ColMsgId"].Value = _mails.MsgId;
                    row.Cells["ColFileName"].Value = _mails.FileName;
                    i++;
                }
                gridviewMails.ClearSelection();
            }

            else
                NoMailLabel.Visible = true;
        }

        private void btnRecycle_Click(object sender, EventArgs e)
        {
            LoadRecycleMails();
        }

        private void LoadRecycleMails()
        {
            MailTab_Inbox = false;
            MailTab_SentMails = false;
            MailTab_Draft = false;
            MailTab_Recycle = true;
            gridviewMails.Columns["ColFrom"].Visible = true;
            gridviewMails.Columns["ColTo"].Visible = false;
            gridviewMails.Rows.Clear();
            MailServer mailserver = new MailServer();
            List<Mail> listmails = mailserver.DeletedMailsList(LoginCredentials.LoggedEmailId);
            DataGridViewRow row;
            int i = 0;
            this.Size = new System.Drawing.Size(1202, 707);
            if (listmails.Count != 0)
            {

                NoMailLabel.Visible = false;
                foreach (var _mails in listmails)
                {
                    row = new DataGridViewRow();
                    gridviewMails.Rows.Add();
                    row = gridviewMails.Rows[i];
                    if (_mails.IsRead == 0)
                        row.DefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold);
                    row.Cells["ColFrom"].Value = _mails.FromEmailId;
                    row.Cells["ColSubject"].Value = _mails.Subject;
                    row.Cells["ColMsgId"].Value = _mails.MsgId;
                    row.Cells["ColMsg"].Value = _mails.Message;
                    row.Cells["ColFileName"].Value = _mails.FileName;
                    i++;
                }
                gridviewMails.ClearSelection();
            }

            else
            {
                NoMailLabel.Visible = true;

            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {

            if (gridviewMails.SelectedRows.Count > 0)
            {
                if (MailServer.MarkMailAsUnDelete((int)gridviewMails.SelectedRows[0].Cells["ColMsgId"].Value))
                {
                    MessageBox.Show("Mail restored sucessfully", "Done!!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    LoadRecycleMails();
                }
                else
                {
                    MessageBox.Show("Unable to restore mail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            else
            {
                MessageBox.Show("Please select a mail to restore", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void btnDeleteMail_Click(object sender, EventArgs e)
        {
            if (gridviewMails.SelectedRows.Count > 0)
            {
                if (DialogResult.Yes == MessageBox.Show("Do you want to delete the mail permanently ? \n It cant be recovered Do you wish to continue?", "YCET User module", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    if (MailServer.MailHardDelete((int)gridviewMails.SelectedRows[0].Cells["ColMsgId"].Value))
                    {
                        MessageBox.Show("Mail deleted sucessfully", "Done!!!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        LoadRecycleMails();
                    }
                    else
                    {
                        MessageBox.Show("Unable to delete mail", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtSearch.Text != "")
            {
                LoadSearchResults(txtSearch.Text.Trim());
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            txtSearch.Text = FilterCommand(txtSearch.Text);
            CmdReader = "Nil";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            synth.Speak("Someone is trying to minimize this application");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            synth.Speak("Someone is trying to close this application");
        }
    }
}
