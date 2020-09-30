package com.company;

import com.google.gson.Gson;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.util.Hashtable;

public class Server implements Runnable {
    private ServerSocket server;
    private int port;
    private boolean tryToAccsept;
    private Hashtable<Integer,UserDetails> users;
    int usersCount;

    public  Server()
    {
        tryToAccsept= true;
        try {
            users = new Hashtable<>();
            usersCount =0;
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
                Iclient clinet = new Iclient(this,socket);
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }
    public void AddNewUser(String UserJson)
    {
        Gson gson = new Gson();
        UserDetails newUser = gson.fromJson(UserJson,UserDetails.class);
        newUser.setId(++this.usersCount);
        users.put(this.usersCount,newUser);
        System.out.println(users.toString());
        }
    }

