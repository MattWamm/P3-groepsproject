using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EigenMaaltijd.Pages
{
    public class UserRepository : DBtools
    {
        //get a user from login information
        public User Login(string email, string password)
        {
            using IDbConnection _db = Connect();
            int userID = 0;
            userID = _db.QuerySingleOrDefault<int>
                (
                "SELECT UserID FROM loginusers WHERE Password = @password AND Email = @email",
                new { 
                    password = password,
                    email = email
                });

            if (userID == 0)
                return null;

            User user = _db.QuerySingleOrDefault<User>
                (
                "SELECT * FROM users WHERE UserID = @userID",
                new { userID = userID}
                );


            return user;
        }
        //get input in the model as a bindproperty and take that as input.
        // inserts a user and loginuser;
        // returns: UserID
        public int Register(User user, RegisterLUser loginUser)
        {
            using IDbConnection _db = Connect();
            int UserID = 0;
                int rows = 0;
            if (loginUser.Password != loginUser.PassConfirm)
                return 0;

            try
            {
                rows = _db.Execute
                (
                "INSERT INTO loginusers (UserID, Password, Email) VALUES (@userID, @password, @email)",
                new
                {
                    password = loginUser.Password,
                    email = loginUser.Email,
                    userID = UserID
                });
            } catch
            {
                return 0;
            }

            if (rows == 1)
                        {
                        UserID = _db.QuerySingleOrDefault<int>
                            (
                            "SELECT UserID FROM loginusers WHERE UserID = LAST_INSERT_ID()"
                            );
                        }

            if (UserID != 0)
                rows = _db.Execute
                        (
                        "INSERT INTO users (UserID, UserName, GeboorteDatum, Telefoon, Adres, Postcode) VALUES (@userID, @username, @geboortedatum, @telefoon, @adres, @postcode)",
                        new
                        {
                            userID = UserID,
                            username = user.UserName,
                            geboortedatum = user.GeboorteDatum,
                            telefoon = user.Telefoon,
                            adres = user.Adres,
                            postcode = user.Postcode
                        });

            return UserID;
        }
        //returns 1 user class
        public User getUserFromID(int userID)
        {
            using IDbConnection _db = Connect();
            User returnUser = _db.QuerySingleOrDefault<User>
                            (
                            "SELECT * FROM users WHERE UserID = @userID",
                            new {userID = userID}
                            );
            return returnUser;
        }
        //returns LoginUser class
        public LoginUser getLoginUserFromID(int userID)
        {
            using IDbConnection _db = Connect();
            LoginUser returnUser = _db.QuerySingleOrDefault<LoginUser>
                            (
                            "SELECT * FROM loginusers WHERE UserID = @userID",
                            new { userID = userID }
                            );
            return returnUser;
        }
        //deletes all information of a user including meals login and contactinformation.
        //returns: number of meals deleted
        public int DeleteUser(int userID)
        {
            using IDbConnection _db = Connect();
            _db.Execute
                (
                "DELETE FROM users WHERE UserID = @userID",
                new { userID = userID}
                );
            _db.Execute
                (
                "DELETE FROM loginusers WHERE UserID = @userID",
                new { userID = userID }
                );
            int rows = _db.Execute
                (
                "DELETE FROM maaltijden WHERE UserID = @userID",
                new { userID = userID }
                );
            return rows ;
        }

        //public void Plaatsbestelling()
        //{ 
        //    using IDbConnection _db = Connect();
        //        int rows = _db.Execute
        //                (
        //                "INSERT INTO bestelinformatie()",
        //                new
        //                {
        //                    Hoeveelheid = Hoeveelheid,
        //                });

        //}
    }
}
