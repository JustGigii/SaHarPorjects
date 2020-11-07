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
using Microsoft.Win32;
using System.IO;
using Firebase.Storage;
using Firebase.Auth;
using System.Threading;

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
            StorageConfig storage = new StorageConfig("1", "!", "1", "1", "1");
            string storagemess = new JavaScriptSerializer().Serialize(storage);


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
            message = "m^" + message;
            ShowNewMessage(message, true, DateTime.Now.ToString());
            UpdateDS(message);

        }

        private void UpdateDS(string message)
        {
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
            string[] messagesplit = message.Split('^');
            if (messagesplit[0] == "m")
            {
                TextBlock textBlock = new TextBlock(new Run(messagesplit[1]));
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
                dock.Children.Add(textBlock);
            }
            else
            {
                Image image = new Image();
                image.Width = 200;
                image.Height = 200;
                HorizontalAlignment = HorizontalAlignment.Left;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(messagesplit[1], UriKind.Absolute);
                bitmap.EndInit();
                image.Source = bitmap;
                dock.Children.Add(image);
            }
            Label labeltime = new Label();
            labeltime.Content = time;
            dock.Children.Add(labeltime);
            ViewContainer.Children.Add(dock);
        }

        private void comboBoxUser_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewContainer.Children.Clear();
            server.Send("showchat@" + user.Id + "," + comboBoxUser.SelectedValue);
            PopChat();
        }

        private async void buttonUplod_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            server.Send("GetConfig@Storage");
             StorageConfig config = JsonConvert.DeserializeObject<StorageConfig>(boardcastView.Recv()); ;
            if (openFileDialog.ShowDialog() == true)
            {
                var stream = File.Open(System.IO.Path.GetFullPath(openFileDialog.FileName), FileMode.Open);
                var auth = new FirebaseAuthProvider(new FirebaseConfig(config.Apikey));
                var a = await auth.SignInWithEmailAndPasswordAsync(config.Username, config.Password);

                var cancellation = new CancellationTokenSource();
                var task = new FirebaseStorage(
                    config.Storage,
                    new FirebaseStorageOptions
                    {
                        AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                        ThrowOnCancel = true // when you cancel the upload, exception is thrown. By default no exception is thrown
            })
                    .Child(config.File)
                    .Child(RandomName()+".png")
                    .PutAsync(stream, cancellation.Token);
                task.Progress.ProgressChanged += (s,f) => labelerr.Content =$"Progress: {f.Percentage} %";
                try
                {
                    // error during upload will be thrown when you await the task
                    string url = await task;
                    url = "i^" + url;
                    UpdateDS(url);
                    ShowNewMessage(url, true, DateTime.Now.ToString());
  
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private string RandomName()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            return new String(stringChars);
        }


    }
}
