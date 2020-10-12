package com.company;

import com.google.gson.Gson;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Hashtable;


public class Server implements Runnable,IServerCommands{
    private ServerSocket server;
    private int port;
    private boolean tryToAccsept;
    private Hashtable<Integer,UserDetails> users;
    int usersCount;
    DatabaseServies ds;
    public  Server()
    {
        tryToAccsept= true;
        ds = new DatabaseServies();
        try {
            users = new Hashtable<>();
            //UserDetails user = new UserDetails(0,"f","f","f","gigiomri@gmail.com","2w2w2w147",0);
           // users.put(usersCount,user);
            usersCount =0;
            //usersCount++;
            port=2212;
            server = new ServerSocket(port);
            Thread accepetThread = new Thread(this,"AccseptTheard");
            accepetThread.start();
        } catch (IOException e) {
            e.printStackTrace();
        }

    }
    @Override
    public void run() {
        while (this.tryToAccsept) {
            try {
                Socket socket = server.accept();
                System.out.println("new connction");
                ClientHandler clinet = new ClientHandler(this,socket);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
    public String Register(String userjson)
    {
        String retuensrting = "";

        Gson gson = new Gson();
        UserDetails newUser = gson.fromJson(userjson,UserDetails.class);
        switch (ds.AddUserToDatabase(newUser)) {
            case -1:
                retuensrting= "User Name already exist";
                break;
            case -2:
                retuensrting =  "email already exist";
                break;
            case -3:

                retuensrting =  "we have some erro in our server try back later";
                break;
            default:
                newUser.setId(++this.usersCount);
                users.put(this.usersCount, newUser);
                retuensrting = "Succes&"+newUser.getId();

        }
            return  retuensrting;
        }
        public UserDetails Login(String loingComm)
        {
            UserDetails user= null;
            boolean found=false;
            String emailPassword[] = loingComm.split(" ");
            String email = emailPassword[0];
            String password = emailPassword[1];
            return ds.FindUser(email,password);
        }
    }

