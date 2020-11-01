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
using MaterialDesignThemes.Wpf;
using ClientCsharp.App_Code;
using System.Data;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace ClientCsharp 
{
    /// <summary>
    /// Interaction logic for ChatInterFace.xaml
    /// </summary>
    public partial class ChatInterFace : Window 
    {
        Connction server;
        UserDetails user;
        BoardcastView boardcastView;
        public ChatInterFace(Connction server, UserDetails user)
        {
            InitializeComponent();
            this.server = server;
            this.user = user;
            boardcastView = new BoardcastView(server,user);
            boardcastView.Show();
            server.Send("GetAllUser@"+user.Id);
            PopUser();
           
        }
      
        void PopUser()
        {
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(boardcastView.Recv());
            comboBoxUser.ItemsSource = ds.Tables[0].DefaultView;
            comboBoxUser.DisplayMemberPath = ds.Tables[0].Columns[0].ToString();
            comboBoxUser.SelectedValuePath = ds.Tables[0].Columns[1].ToString();
           
            
        }
        private void PopChat()
        {
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(boardcastView.Recv());
            foreach (DataRow i in ds.Tables[0].Rows)
            {
                ShowNewMessage(i[1].ToString(), int.Parse(i[0].ToString()) == user.Id, i[2].ToString());
            }
        }
        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            string message = textBoxChat.Text;
            ShowNewMessage(message,true,DateTime.Now.ToString());
            var messageDetail = new MessageDetail(user.Id, int.Parse(comboBoxUser.SelectedValue.ToString()), 1, message, DateTime.Now.ToString());
            var json = new JavaScriptSerializer().Serialize(messageDetail);
            server.Send("AddMessage@" + json + "\n");
            string[] answer = boardcastView.Recv().Split('&');
            messageDetail.Massageid = int.Parse(answer[1]);

        }


        public void ShowNewMessage(string message, bool isyou,string time)
        {
            textBoxChat.Clear();
            DockPanel dock = new DockPanel(); 
            TextBlock textBlock = new TextBlock(new Run(message));
            textBlock.FontSize = 20;
            if (isyou)
            {
                textBlock.TextAlignment = TextAlignment.Right;
                 textBlock.Background = Brushes.Cyan;
            }
            else
            {
                textBlock.Background = Brushes.White;
                textBlock.TextAlignment = TextAlignment.Left;
            }
            Label labeltime = new Label();
            labeltime.Content = time;
            dock.Children.Add(textBlock);
            dock.Children.Add(labeltime);
            ViewContainer.Children.Add(dock);
        }

        private void comboBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewContainer.Children.Clear();
            server.Send("showchat@" + user.Id + "," + comboBoxUser.SelectedValue);
            PopChat();
        }
    }
}
