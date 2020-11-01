package com.company;

import com.google.gson.Gson;
import org.json.JSONArray;
import org.json.JSONObject;

import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.sql.ResultSet;
import java.util.ArrayList;
import java.util.List;


public class Server implements Runnable,IServerCommands{
    private ServerSocket server;
    private int port;
    private boolean tryToAccsept;
    private List<ClientHandler> users;
    public  Server()
    {
        tryToAccsept= true;
        try {
            users = new ArrayList<>();
            //UserDetails user = new UserDetails(0,"f","f","f","gigiomri@gmail.com","2w2w2w147",0);
           // users.put(usersCount,user);
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
    public String Register(String userjson,ClientHandler client)
    {
        String retuensrting = "";

        Gson gson = new Gson();
        UserDetails newUser = gson.fromJson(userjson,UserDetails.class);
        switch (DatabaseServies.AddUserToDatabase(newUser)) {
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
                users.add(client);
                retuensrting = "Succes&"+newUser.getId();

        }
            return  retuensrting;
        }
        public UserDetails Login(String loingComm,ClientHandler clinent)
        {
            UserDetails user= null;
            boolean found=false;
            String emailPassword[] = loingComm.split(" ");
            String email = emailPassword[0];
            String password = emailPassword[1];
            user =DatabaseServies.FindUser(email,password);
            users.add(clinent);
            return user;
        }
        public String GetAllUser(int userid)
        {
                ResultSet rs = DatabaseServies.DsGetAllUser(userid);
            try {
                JSONArray jsonArray = convertToJSON(rs);
                JSONObject jsonObject = new JSONObject();
                jsonObject.put("UserTable",jsonArray);
                return jsonObject.toString();
            } catch (Exception e) {
                e.printStackTrace();
                return null;
            }


        }

    @Override
    public String SendPopChat(int userid1, int userid2) {
        ResultSet rs = DatabaseServies.PopChat(userid1,userid2);
        try {
            JSONArray jsonArray = convertToJSON(rs);
            JSONObject jsonObject = new JSONObject();
            jsonObject.put("ChatTable",jsonArray);
            return jsonObject.toString();
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }

    @Override
    public String AddMessage(String messagejson) {
        String retuensrting = "";

        Gson gson = new Gson();
        MessageDetails newmessage = gson.fromJson(messagejson,MessageDetails.class);
        if(DatabaseServies.AddMessage(newmessage)>0)
        {
                retuensrting = "Succes&"+newmessage.getMassageid();
        }
        else
            retuensrting = "we have some erro in our server try back later";
        return  retuensrting;
    }

    public void SendBoardCast(String message)
    {
        for (int i = 0; i < users.size(); i++) {
            ClientHandler client = users.get(i);
            client.SendBoardcast(message);
        }
    }

    private JSONArray convertToJSON(ResultSet resultSet)
            throws Exception {
        JSONArray jsonArray = new JSONArray();
        while (resultSet.next()) {
            int total_columns = resultSet.getMetaData().getColumnCount();
            JSONObject obj = new JSONObject();
            for (int i = 0; i < total_columns; i++) {
                obj.put(resultSet.getMetaData().getColumnLabel(i + 1).toLowerCase(), resultSet.getObject(i + 1));
            }
            jsonArray.put(obj);
        }
        return jsonArray;
    }

    }

