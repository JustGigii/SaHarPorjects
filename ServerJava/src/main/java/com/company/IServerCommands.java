package com.company;

public interface IServerCommands {
    public String Register(String userjson,ClientHandler client);
    public  UserDetails Login(String loingComm,ClientHandler client);
    public String GetAllUser(int userid);
    public String SendPopChat(int userid1 , int userid2);
    public String AddMessage(String message);
    public void SendBoardCast(String messagejson);
    public String StorageConfig();
}
