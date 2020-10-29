package com.company;

import java.sql.*;

class DatabaseServies {
     private static String Url="jdbc:oracle:thin:@localhost:1521:XE";
    private static String UserName="MultyGigson";
    private static String Password="2w2w2w147";
    private  static Connection con = null;


    private static Connection GetConnection() {
        synchronized (Class.class) {
            if(con == null)
            try {
                Class.forName("oracle.jdbc.driver.OracleDriver");
                con = DriverManager.getConnection(Url, UserName, Password);
            } catch (SQLException | ClassNotFoundException throwables) {
                throwables.printStackTrace();
            }
            return con;
        }
    }
    public static int AddUserToDatabase(UserDetails user) {
        try {
             con = GetConnection();
            CallableStatement pst = con.prepareCall("{? = call AddNewUser(?,?,?,?,?,?)}");
            //Registering the out parameter of the function (return type)

            //Executing the statement
            pst.registerOutParameter(1,Types.INTEGER);
            pst.setString(2, user.getFname());
            pst.setString(3, user.getLname());
            pst.setString(4, user.getUserName());
            pst.setString(5, user.getMail());
            pst.setString(6, user.getPassword());
            pst.setInt(7, user.getMode());
            pst.execute();
            user.setId(pst.getInt(1));
            return user.getId();

        } catch (SQLException throwables) {
            throwables.printStackTrace();
            return -3;
        }

    }
    public static UserDetails FindUser(String username, String password) {
        UserDetails user = null;
         con = GetConnection();
        try {
            PreparedStatement  statement =con.prepareStatement("select * from users Where (MAIL=? or USERNAME_NAME = ?) and PASSWORD= ? ");   //executing statement
            statement.setString(1, username);
            statement.setString(2, username);
            statement.setString(3, password);
            ResultSet rs = statement.executeQuery();
            while(rs.next()){
                 user = new UserDetails(rs.getInt(1),rs.getString(2),rs.getString(3),rs.getString(4),rs.getString(5),rs.getString(6),rs.getInt(7));
            }
        }
        catch (SQLException throwables) {
            throwables.printStackTrace();
        }

        return user;
    }
    public static ResultSet DsGetAllUser()
    {
        try {
         con = GetConnection();
        Statement statement = con.createStatement();
        ResultSet results = statement.executeQuery("SELECT USERNAME_NAME,USER_ID FROM USERS");
            return results;
        }
        catch (SQLException e) {
            e.printStackTrace();
            return  null;
        }
    }
    public static ResultSet PopChat(int user1,int user2)
    {
        try {
            con = GetConnection();
            PreparedStatement statement = con.prepareStatement("SELECT TRANSFROMMASSAGE.USERIDSEND,MASSGES.TEXT,MASSGES.SEND_IN,TRANSFROMMASSAGE.STATUS FROM MASSGES INNER JOIN TRANSFROMMASSAGE ON MASSGES.MESSGE_ID= TRANSFROMMASSAGE.USERIDSEND WHERE TRANSFROMMASSAGE.USERIDSEND=? or TRANSFROMMASSAGE.USERIDSEND=?");
            statement.setInt(1, user1);
            statement.setInt(2, user2);
            ResultSet results = statement.executeQuery();
            return results;
        }
        catch (SQLException e) {
            e.printStackTrace();
            return  null;
        }
    }

}

