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

namespace ClientCsharp 
{
    /// <summary>
    /// Interaction logic for ChatInterFace.xaml
    /// </summary>
    public partial class ChatInterFace : Window 
    {
        Connction server;
        UserDetails user;
        public ChatInterFace(Connction server, UserDetails user)
        {
            this.server = server;
            this.user = user;
            server.Send("GetAllUser@n");
            InitializeComponent();
            PopUser();
        }
      
        void PopUser()
        {
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(server.Recv());
            comboBoxUser.ItemsSource = ds.Tables[0].DefaultView;
            comboBoxUser.DisplayMemberPath = ds.Tables[0].Columns[0].ToString();
            comboBoxUser.SelectedValuePath = ds.Tables[0].Columns[1].ToString();
           
            
        }
        private void PopChat()
        {
            DataSet ds = JsonConvert.DeserializeObject<DataSet>(server.Recv());
            foreach (TableRow i in ds.Tables[0].Rows)
            {
                //ShowNew(i.Cells[""])
            }
        }
        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            string message = textBoxChat.Text;
           
            ShowNew(message,true,DateTime.Now.ToString());
            
        }


        public void ShowNew(string message, bool isyou,string time)
        {
            textBoxChat.Clear();
            DockPanel dock = new DockPanel(); 
            TextBlock textBlock = new TextBlock(new Run(message));
            textBlock.FontSize = 20;
            if (isyou)
            {
                textBlock.Background = Brushes.Cyan;
                textBlock.TextAlignment = TextAlignment.Right;
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
