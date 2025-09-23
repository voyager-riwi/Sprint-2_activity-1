using Sprint2.Data;
using Sprint2.Models;

namespace Sprint2.Controllers;

public class UserController
{
    
    // 🔹 READ (todos)
    public List<User> Index()
    {
        using (var db = new MysqlDbContext())
        {
            return db.users.ToList();
        }
    }
}