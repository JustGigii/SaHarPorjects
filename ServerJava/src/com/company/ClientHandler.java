package com.company;

import com.google.gson.Gson;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.net.Socket;



public class ClientHandler implements Runnable {
    private IServerCommands serverInteface;
    private Socket clientSocket;
    private Boolean isruning;
    BufferedReader  recv;
    PrintWriter send;
    ClientHandler(IServerCommands serverInteface, Socket clientSocket)
    {
        this.serverInteface= serverInteface;
        this.clientSocket=clientSocket;
        isruning = true;
        Thread Reader = new Thread(this,"MessageThread");
        Reader.start();
    }
    @Override
    public void run() {
        while (this.isruning)
        {
            try {
                  recv = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
                 // send = new BufferedWriter(new OutputStreamWriter(clientSocket.getOutputStream()));
                 send = new PrintWriter(clientSocket.getOutputStream(), true);
                String[] split = recv.readLine().split("@",2);
                CommandHandler(split[0],split[1]);
            } catch (IOException e) {
                e.printStackTrace();
                isruning =false;
            }

        }
    }
    private void CommandHandler(String Command,String Massage)
    {
        switch (Command)
        {
            case "register":
                send.println(serverInteface.Register(Massage));
                break;
            case "login":
             UserDetails User = serverInteface.Login(Massage);
             if(User != null) {
                 Gson gson = new Gson();
                 String tosend=gson.toJson(User);
                 send.println(tosend.length()+"&"+tosend);
             }
             else
                 send.println("we can't find your user");

        }
    }
}
