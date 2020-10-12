package com.company;



import java.sql.*;

class DatabaseServies {
    String Url;
    String UserName;
    String Password;

    public DatabaseServies() {

    }

    private Connection GetConnection() {
        try {
            Class.forName("oracle.jdbc.driver.OracleDriver");
            return DriverManager.getConnection("jdbc:oracle:thin:@localhost:1521:XE", "MultyGigson", "2w2w2w147");
        } catch (SQLException | ClassNotFoundException throwables) {
            throwables.printStackTrace();
        }
        return null;
    }

    public int AddUserToDatabase(UserDetails user) {
        try {
            Connection con = GetConnection();
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
            con.close();    //closing connection
            user.setId(pst.getInt(1));
            return user.getId();

        } catch (SQLException throwables) {
            throwables.printStackTrace();
        }
        return -3;
    }
    public UserDetails FindUser(String username, String password) {
        UserDetails user = null;
        Connection con = GetConnection();
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
}

