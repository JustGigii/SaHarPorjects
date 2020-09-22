package com.company;
import java.net.ServerSocket;
import java.net.Socket;
import java.io.*;

public class Server implements Runnable {
    private ServerSocket server;
    private int port=2212;
    private boolean tryToAccsept;

    public  Server()
    {
        tryToAccsept= true;
        try {
            server = new ServerSocket(port);
            Thread accepetThread = new Thread(this);
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
}
