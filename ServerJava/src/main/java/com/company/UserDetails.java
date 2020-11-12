package com.company;

public class UserDetails {
    private int Id;
    private String Fname;
    private String Lname;
    private String UserName;
    private String Mail;
    private String Password;
    private int Mode;
    private String Picture;

    public UserDetails( int Id,String Fname, String Lname, String UserName,String Mail, String Password,int Mode,String Picture)
    {
        this.setId(Id);
        this.setFname(Fname);
        this.setLname(Lname);
        this.setUserName(UserName);
        this.setMail(Mail);
        this.setPassword(Password);
        this.setMode(Mode);
        this.setPicture(Picture);
    }

    // Getter Methods

    public int getId() {
        return Id;
    }

    public String getFname() {
        return Fname;
    }

    public String getLname() {
        return Lname;
    }

    public String getUserName() {
        return UserName;
    }

    public String getMail() {
        return Mail;
    }

    public String getPassword() {
        return Password;
    }

    public int getMode() {
        return Mode;
    }

    // Setter Methods

    public void setId(int Id) {
        this.Id = Id;
    }

    public void setFname(String Fname) {
        this.Fname = Fname;
    }

    public void setLname(String Lname) {
        this.Lname = Lname;
    }

    public void setUserName(String UserName) {
        this.UserName = UserName;
    }

    public void setMail(String Mail) {
        this.Mail = Mail;
    }

    public void setPassword(String Password) {
        this.Password = Password;
    }

    public void setMode(int Mode) {
        this.Mode = Mode;
    }

    public String getPicture() {
        return Picture;
    }

    public void setPicture(String picture) {
        Picture = picture;
    }
}
