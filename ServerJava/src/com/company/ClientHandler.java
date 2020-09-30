package com.company;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
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
        Thread Reader = new Thread(this,"MessageThread");
        Reader.start();
    }
    @Override
    public void run() {
        while (this.isruning)
        {
            try {
                BufferedReader  recv = new BufferedReader(new InputStreamReader(clientSocket.getInputStream()));
                String[] split = recv.readLine().split("@",2);
                String Command= split[0];
                String Massage = split[1];
                switch (Command)
                {
                    case "register":
                        serverInteface.AddNewUser(Massage);
                        break;
                }
            } catch (IOException e) {
                e.printStackTrace();
            }

        }
    }
}
