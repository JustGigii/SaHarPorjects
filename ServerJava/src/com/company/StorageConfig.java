package com.company;

public class StorageConfig {
    private String Apikey;
    private String Username;
    private String Password;
    private String Storage;
    private String File;

    public StorageConfig()
    {
        this.Apikey = "AIzaSyA5sJ0YcMpegWpAI8wOIySho7QPmvFtlZ4";
        this.Username = "gigiomri@gmail.com";
        this.Password = "2w2w2w147";
        this.Storage = "fir-project-50920.appspot.com";
        this.File = "sharpoject";

    }
    // Getter Methods

    public String getApikey() {
        return Apikey;
    }

    public String getUsername() {
        return Username;
    }

    public String getPassword() {
        return Password;
    }

    public String getStorage() {
        return Storage;
    }

    public String getFile() {
        return File;
    }

    // Setter Methods

    public void setApikey(String Apikey) {
        this.Apikey = Apikey;
    }

    public void setUsername(String Username) {
        this.Username = Username;
    }

    public void setPassword(String Password) {
        this.Password = Password;
    }

    public void setStorage(String Storage) {
        this.Storage = Storage;
    }

    public void setFile(String File) {
        this.File = File;
    }
}
