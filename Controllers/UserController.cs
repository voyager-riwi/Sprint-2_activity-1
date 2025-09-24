using Sprint2.Data;
using Sprint2.Models;


namespace Sprint2.Controllers;

public class UserController
{
    // Create
    public string Create(User newUser)
    {
        using (var db = new MysqlDbContext())
        {
            // Validación de que el username o email no existan
            var userExists = db.users.Any(u => u.Username == newUser.Username || u.Email == newUser.Email);
            if (userExists)
            {
                return "Error: El nombre de usuario o el correo electrónico ya existen.";
            }

            // Asignar la fecha de creación
            newUser.CreatedAt = DateTime.Now;

            // Añadir el nuevo usuario y guardar cambios
            
            db.users.Add(newUser);
            db.SaveChanges();

            return "¡Usuario creado correctamente!";
        }
    }

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
    // Delete por Id
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

    // Delete por Email
    public bool DeleteUserByEmail(string email)
    {
        using (var db = new MysqlDbContext())
        {
            var userToDelete = db.users.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
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
