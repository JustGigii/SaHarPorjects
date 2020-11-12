package com.company;

public class MessageDetails {
    private int Massageid;
    private int Useridsend;
    private int Useridrecv;
    private int Status;
    private String Text;
    private String Date;


    // Getter Methods

    public int getMassageid() {
        return Massageid;
    }

    public int getUseridsend() {
        return Useridsend;
    }

    public int getUseridrecv() {
        return Useridrecv;
    }

    public int getStatus() {
        return Status;
    }

    public String getText() {
        return Text;
    }

    public String getDate() {
        return Date;
    }

    // Setter Methods

    public void setMassage(int Massage) {
        this.Massageid = Massage;
    }

    public void setUseridsend(int Useridsend) {
        this.Useridsend = Useridsend;
    }

    public void setUseridrecv(int Useridrecv) {
        this.Useridrecv = Useridrecv;
    }

    public void setStatus(int Status) {
        this.Status = Status;
    }

    public void setText(String Text) {
        this.Text = Text;
    }

    public void setDate(String Date) {
        this.Date = Date;
    }
}
