using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace EigenMaaltijd.Pages
{
    public class MealRepository : DBtools
    {
        //get meal information with bindpropperty and logged user information.

        //add a meal to the list


        public int AddMeal(Meal meal)
        {
            using IDbConnection _db = Connect();

            int rows = _db.Execute
                (
                "INSERT INTO maaltijden (UserID, Name, Ingredients, Portions, PortionSize, Rating, Ingevroren, Betalingsmethode) VALUES (@userID, @Name, @ingredients, @portions, @portionSize, @rating, @ingevroren, @betalingsmethode)",
                new { userID = meal.UserID, name = meal.Name, ingredients = meal.Ingredients, portions = meal.Portions, portionSize = meal.PortionSize, rating = meal.Rating, ingevroren = meal.Ingevroren, betalingsmethode = meal.Betalingsmethode }
                );
            return 0;
        }


        public int DeleteMeal(int MealID)
        {
            using IDbConnection _db = Connect();
            int rows = _db.Execute
                (
                "DELETE FROM maaltijden WHERE MealID = @mealID",
                new { mealID = MealID }
                );
            return rows;
        }

        public int ChangeMeal(Meal Meal)
        {
            using IDbConnection _db = Connect();
            int rows = _db.Execute
                (
                "UPDATE maaltijden SET Name = @name, Ingredients = @ingredients, Portions = @portions, PortionSize = @portionSize WHERE MealID = @mealID ",
                new { 
                    mealID = Meal.MealID,
                    name = Meal.Name,
                    ingredients = Meal.Ingredients,
                    portions = Meal.Portions,
                    portionSize = Meal.PortionSize 
                });
            return rows;
        }


        public List<Meal> GetAllMeals()
        {
            using IDbConnection _db = Connect();
            List<Meal> returnList = _db.Query<Meal>
                ("SELECT * FROM maaltijden").ToList();
            return returnList;
        }


        public List<Meal> GetMealsFromUserID(int UserID)
        {
            using IDbConnection _db = Connect();
            List<Meal> returnList = _db.Query<Meal>
                ("SELECT * FROM maaltijden WHERE UserID = @userID",
                new {userID = UserID }).ToList();
            return returnList;
        }


        public int AddFavourite(int MealID, int UserID)
        {
            using IDbConnection _db = Connect();

            int rows = _db.Execute
                (
                "INSERT INTO favourite (UserID, MealID) VALUES (@userID, @mealID))",
                new {userID = UserID, mealID = MealID}
                );
            return rows;
        }

        public int RemoveFavourite(int MealID, int UserID)
        {
            using IDbConnection _db = Connect();
            int rows = _db.Execute
                (
                "DELETE FROM favourite WHERE MealID = @mealID AND UserID = @userID",
                new {mealID = MealID, userID = UserID}
                );
            return rows;
        }

        public List<Meal> GetFavourites(int UserID)
        {
            using IDbConnection _db = Connect();
            List<Meal> returnList = null;
            List<int> favouriteIdList = _db.Query<int>
                ("SELECT MealID FROM favourite WHERE UserID = @userID",
                new {userID = UserID} ).ToList();
            foreach (int mealID in favouriteIdList)
            {
                Meal favouriteMeal = _db.QuerySingleOrDefault<Meal>
                ("SELECT * FROM maaltijden WHERE MealID = @MealID",
                new { MealID = mealID});
            }

            return returnList;
        }

        public int NewOrder(User user, Meal meal)
        {


            return 0;
        }

        public int RemoveOrder(User user, Meal meal)
        {


            return 0;
        }

        public float addRating(int mealID, int Rating)
        {

            return 0;
        }

        public float ChangeRating(int mealID, int Rating)
        {

            return 0;
        }



          

        public List<Meal> Search(string searchTerm)
        {
        
           List<Meal> _mealRepository = new MealRepository().GetAllMeals();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                return _mealRepository.Where(m => m.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }
            else
            {
                return _mealRepository;
            }


        }


    }
}
