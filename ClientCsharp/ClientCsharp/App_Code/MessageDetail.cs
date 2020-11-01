using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCsharp.App_Code
{
  public  class MessageDetail
    {
        int massageid;
        int useridsend;
        int useridrecv;
        int status;
        string text;
        string date;

        public MessageDetail(int useridsend, int useridrecv, int status, string text, string date)
        {
            this.Massageid =0;
            this.Useridsend = useridsend;
            this.Useridrecv = useridrecv;
            this.Status = status;
            this.Text = text ?? throw new ArgumentNullException(nameof(text));
            this.Date = date;
        }

        public int Massageid { get => massageid; set => massageid = value; }
        public int Useridsend { get => useridsend; set => useridsend = value; }
        public int Useridrecv { get => useridrecv; set => useridrecv = value; }
        public int Status { get => status; set => status = value; }
        public string Text { get => text; set => text = value; }
        public string Date { get => date; set => date = value; }
    }
}
