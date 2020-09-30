package com.company;

public class UserDetails {
    private int id;
    private String fname;
    private String lname;
    private String userName;
    private String mail;
    private String password;
    private int mode;


    public UserDetails(int id, String fname, String lname, String userName, String mail, String password, int mode) {
        this.id = id;
        this.fname = fname;
        this.lname = lname;
        this.userName = userName;
        this.mail = mail;
        this.password = password;
        this.mode = mode;
    }

    public String getFname() {
        return fname;
    }

    public void setFname(String fname) {
        this.fname = fname;
    }

    public String getLname() {
        return lname;
    }

    public void setLname(String lname) {
        this.lname = lname;
    }

    public String getUserName() {
        return userName;
    }

    public void setUserName(String userName) {
        this.userName = userName;
    }

    public String getMail() {
        return mail;
    }

    public void setMail(String mail) {
        this.mail = mail;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public int getMode() {
        return mode;
    }

    public void setMode(int mode) {
        this.mode = mode;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }
}
