using Sprint2.Data;
using Sprint2.Models;


namespace Sprint2.Controllers;

public class UserController
{
    // Create
    
    // Update 
    
    // 🔹 READ (todos)
    public List<User> Index()
    {
        using (var db = new MysqlDbContext())
        {
            return db.users.ToList();
        }
    }
    
    
    // Delete 
    public bool DeleteUser(int id)
    {
        using (var db = new MysqlDbContext())
        {
            var userToDelete = db.users.Find(id);
            if (userToDelete == null)
            {
                return false; 
            }

            db.users.Remove(userToDelete);
            db.SaveChanges();
            return true; 
        }
    }
}


    
