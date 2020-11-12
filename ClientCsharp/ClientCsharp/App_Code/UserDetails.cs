using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCsharp.App_Code
{
    public class UserDetails
    {
        private int id;
        private string fname;
        private string lname;
        private string userName;
        private string mail;
        private string password;
        private int mode;
        private string picture;

        public UserDetails()
        {
        }
        public UserDetails(int id, string fname, string lname, string userName, string mail, string password, int mode,string picture)
        {
            this.id = id;
            this.fname = fname;
            this.lname = lname;
            this.userName = userName;
            this.mail = mail;
            this.password = password;
            this.mode = mode;
            this.picture = picture;
        }

        public int Id
        {
            get { return this.id; }
            set { this.id = value; }
        }
        public string Fname
        {
            get { return this.fname; }
            set { this.fname = value; }
        }
        public string Lname
        {
            get { return this.lname; }
            set { this.lname = value; }
        }
        public string UserName { get => userName; set => userName = value; }
        public string Mail { get => mail; set => mail = value; }
        public string Password { get => password; set => password = value; }
        public int Mode { get => mode; set => mode = value; }
        public string Picture { get => picture; set => picture = value; }
    }
}
