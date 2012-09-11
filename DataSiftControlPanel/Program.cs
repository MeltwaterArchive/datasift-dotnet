using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using datasift;

namespace DataSiftControlPanel
{
    public class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Inst().Run();
        }

        static private Program sm_program = null;

        static public Program Inst()
        {
            if (sm_program == null)
            {
                sm_program = new Program();
            }
            return sm_program;
        }

        private string m_username = "";
        private string m_api_key = "";
        private bool m_remember_credentials = false;
        private User m_user = null;
        private frmMenu m_form_menu = null;

        public Program()
        {
        }

        public void Run()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            m_form_menu = new frmMenu();
            LoadCredentials();
            Application.Run(m_form_menu);
        }

        public string username { get { return m_username; } }
        public string api_key { get { return m_api_key; } }
        public bool remember_credentials { get { return username == Properties.Settings.Default.username && api_key == Properties.Settings.Default.api_key; } }
        public User dsuser { get { return m_user; } }
        public frmMenu menu { get { return m_form_menu; } }

        public bool LoadCredentials()
        {
            m_username = Properties.Settings.Default.username;
            m_api_key = Properties.Settings.Default.api_key;

            // Update the main menu button with our username
            menu.btnAccount.Text = m_username.Length == 0 ? "DataSift Account" : username;

            // Create the user object
            if (m_username.Length > 0 && m_api_key.Length > 0)
            {
                m_user = new User(username, api_key);
            }

            return m_username.Length > 0 && m_api_key.Length > 0;
        }

        public bool SaveCredentials()
        {
            if (m_remember_credentials)
            {
                Properties.Settings.Default.username = m_username;
                Properties.Settings.Default.api_key = m_api_key;
            }
            else
            {
                Properties.Settings.Default.username = "";
                Properties.Settings.Default.api_key = "";
            }
            Properties.Settings.Default.Save();

            // Update/create the user object
            if (m_user != null)
            {
                m_user = null;
            }
            m_user = new User(username, api_key);

            // Validate the user credentials
            try
            {
                m_user.getUsage();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool UpdateCredentials(string username, string api_key, bool remember = false)
        {
            bool retval = true;

            bool changed = false;
            if (m_username.CompareTo(username) != 0)
            {
                m_username = username;
                changed = true;
            }
            if (m_api_key.CompareTo(api_key) != 0)
            {
                m_api_key = api_key;
                changed = true;
            }
            m_remember_credentials = remember;

            if (changed)
            {
                retval = SaveCredentials();
            }

            return retval;
        }
    }
}
