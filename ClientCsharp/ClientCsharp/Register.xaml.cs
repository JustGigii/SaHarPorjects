using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Web.Script.Serialization;
using ClientCsharp.App_Code;
using System.Threading;
using Microsoft.Win32;
using Newtonsoft.Json;
using Firebase.Auth;
using System.IO;
using Firebase.Storage;

namespace ClientCsharp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Register : Window
    {
        Connction server;
        string url;
        public Register(Connction server)
        {
            this.server = server;
            InitializeComponent();
            url = @"https://firebasestorage.googleapis.com/v0/b/fir-project-50920.appspot.com/o/sharpoject%2Funknow.png?alt=media&token=e19506fe-7ecf-4b80-b262-ce5d0132d2f3";
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login(server);
            login.Show();
            Close();
        }
        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetBoxs();
        }
        private void ResetBoxs()
        {
            textBoxFirstName.Text = "";
            textBoxLastName.Text = "";
            textBoxEmail.Text = "";
            textBoxUserName.Text = "";
            passwordBox1.Password = "";
            passwordBoxConfirm.Password = "";
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxEmail.Text.Length == 0)
            {
                errormessage.Text = "Enter an email.";
                textBoxEmail.Focus();
            }
            else if (!Regex.IsMatch(textBoxEmail.Text, @"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"))
            {
                errormessage.Text = "Enter a valid email.";
                textBoxEmail.Select(0, textBoxEmail.Text.Length);
                textBoxEmail.Focus();
            }
            else
            {
                string firstname = textBoxFirstName.Text;
                string lastname = textBoxLastName.Text;
                string email = textBoxEmail.Text;
                string password = passwordBox1.Password;
                if (passwordBox1.Password.Length == 0)
                {
                    errormessage.Text = "Enter password.";
                    passwordBox1.Focus();
                }
                else if (passwordBoxConfirm.Password.Length == 0)
                {
                    errormessage.Text = "Enter Confirm password.";
                    passwordBoxConfirm.Focus();
                }
                else if (passwordBox1.Password != passwordBoxConfirm.Password)
                {
                    errormessage.Text = "Confirm password must be same as password.";
                    passwordBoxConfirm.Focus();
                }
                else
                {
                    errormessage.Text = "";
                    UserDetails User = new UserDetails(0, textBoxFirstName.Text, textBoxLastName.Text, textBoxUserName.Text, textBoxEmail.Text, passwordBox1.Password, 0, url);
                    var json = new JavaScriptSerializer().Serialize(User);
                    server.Send("register@"+json+"\n");
                    string[] answer = server.Recv().Split('&');
                    errormessage.Text = answer[0];
                    if (answer.Length > 1) ;
                    User.Id = int.Parse(answer[1]);
                }
            }
        }

        private async void UploudImage_Click(object sender, RoutedEventArgs e)
        {


            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*";
            server.Send("GetConfig@Storage");
            string[] send = server.Recv().Split('¨');
            StorageConfig config = JsonConvert.DeserializeObject<StorageConfig>(send[1]); ;
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
                    .Child(RandomName() + ".png")
                    .PutAsync(stream, cancellation.Token);
                task.Progress.ProgressChanged += (s, f) => labelerr.Content = $"Progress: {f.Percentage} %";
                try
                {
                    // error during upload will be thrown when you await the task
                     url = await task;
                    labelerr.Content = "";
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(url, UriKind.Absolute);
                    bitmap.EndInit();
                    imagePorfile.Source = bitmap;


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

