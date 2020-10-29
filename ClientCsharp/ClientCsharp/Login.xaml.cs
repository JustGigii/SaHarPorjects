using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using ClientCsharp.App_Code;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace ClientCsharp
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private const string Pattern = @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$";
        Connction con;
        UserDetails user;
        public Login(Connction con)
        {
            InitializeComponent();
            this.con = con;
        }
        
        private void button1_Click(object sender, RoutedEventArgs e)
        {
            errormessage.Text = "";
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, Pattern))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                con.Send("login@" + email + " " + password);
                string[] aswer = con.Recv().Split('&');
                
                if (aswer[1] != "None")
                {
                    string json = aswer[1].Substring(0, int.Parse(aswer[0]));
                    user = new JavaScriptSerializer().Deserialize<UserDetails>(json);
                    ChatInterFace chat = new ChatInterFace(con,user);
                    chat.Show();
                    this.Close();
                }
                else
                {
                    errormessage.Text = "Sorry! Please enter existing email/password.";
                }
               
            }
        }
        private void buttonRegister_Click(object sender, RoutedEventArgs e)
        {
            Register Reg = new Register(con);
            Reg.Show();
            Close();
        }
    }
}

