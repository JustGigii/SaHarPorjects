package com.company;
import java.io.BufferedInputStream;
import java.io.DataInputStream;
import java.io.IOException;
import java.net.Socket;



public class Iclient implements Runnable {
    private Server serverInteface;
    private Socket clientSocket;
    private Boolean isruning;
    Iclient(Server serverInteface,Socket clientSocket)
    {
        this.serverInteface= serverInteface;
        this.clientSocket=clientSocket;
        isruning = true;
        Thread Reader = new Thread(this);
        Reader.start();
    }
    @Override
    public void run() {
        while (this.isruning)
        {
            try {
                DataInputStream in =new DataInputStream(new BufferedInputStream (clientSocket.getInputStream()));
                System.out.println(in.readUTF());
            } catch (IOException e) {
                e.printStackTrace();
            }

        }
    }
}
