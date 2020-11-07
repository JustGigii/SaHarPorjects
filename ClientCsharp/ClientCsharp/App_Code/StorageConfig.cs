using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientCsharp.App_Code
{
    class StorageConfig
    {
        string apikey;
        string username;
        string password;
        string storage;
        string file;

        public StorageConfig(string apikey, string username, string password, string storage, string file)
        {
            this.Apikey = apikey ?? throw new ArgumentNullException(nameof(apikey));
            this.Username = username ?? throw new ArgumentNullException(nameof(username));
            this.Password = password ?? throw new ArgumentNullException(nameof(password));
            this.Storage = storage ?? throw new ArgumentNullException(nameof(storage));
            this.File = file ?? throw new ArgumentNullException(nameof(file));
        }

        public string Apikey { get => apikey; set => apikey = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
        public string Storage { get => storage; set => storage = value; }
        public string File { get => file; set => file = value; }

        
    }
}
