using Sprint2.Data;
using Sprint2.Models;

namespace Sprint2.Controllers;

public class UserController
{
    private readonly MysqlDbContext _context;

    public UserController()
    {
        _context = new MysqlDbContext();
    }

    // CREATE
    public string Create(User newUser)
    {
        // Validación de que el username o email no existan
        var userExists = _context.users.Any(u => u.Username == newUser.Username || u.Email == newUser.Email);
        if (userExists)
        {
            return "Error: El nombre de usuario o el correo electrónico ya existen.";
        }

        newUser.CreatedAt = DateTime.Now;

        _context.users.Add(newUser);
        _context.SaveChanges();

        return "¡Usuario creado correctamente!";
    }

    // READ / CONSULTAS
    // 1 - Listar todos los usuarios
    public List<User> GetAllUsers()
    {
        return _context.users.ToList();
    }

    // 2 - Ver detalle por Id
    public User GetUserById(int id)
    {
        return _context.users.Find(id);
    }
    
    // 3 - Ver detalle por correo
    public User GetUserByEmail(string email)
    {
        return _context.users.FirstOrDefault(u => u.Email == email);
    }

    // 4 - Listar por ciudad
    public List<User> GetUsersByCity(string city)
    {
        return _context.users.Where(u => u.City == city).ToList();
    }

    // 5 - Listar por país
    public List<User> GetUsersByCountry(string country)
    {
        return _context.users.Where(u => u.Country == country).ToList();
    }
    
    // 6 - Listar por edad
    public List<User> GetUsersOlderThan(int minAge)
    {
        return _context.users.Where(u => u.Age > minAge).ToList();
    }

    // 7 - Listar por género
    public List<User> GetUsersByGender(string gender)
    {
        return _context.users.Where(u => u.Gender == gender).ToList();
    }

    // 8 - Seleccionar campos específicos (nombres y correos)
    public List<UserNameAndEmailDto> GetNamesAndEmails()
    {
        return _context.users
                       .Select(u => new UserNameAndEmailDto
                       {
                           FirstName = u.FirstName,
                           LastName = u.LastName,
                           Email = u.Email
                       })
                       .ToList();
    }

    // 9 - Contar el total de usuarios
    public int CountTotalUsers()
    {
        return _context.users.Count();
    }

    // 10 - Contar usuarios por ciudad
    public Dictionary<string, int> CountUsersByCity()
    {
        return _context.users.GroupBy(u => u.City)
                             .ToDictionary(g => g.Key, g => g.Count());
    }

    // 11 - Contar usuarios por país
    public Dictionary<string, int> CountUsersByCountry()
    {
        return _context.users.GroupBy(u => u.Country)
                             .ToDictionary(g => g.Key, g => g.Count());
    }

    // 12 - Usuarios sin teléfono
    public List<User> GetUsersWithoutPhone()
    {
        return _context.users.Where(u => string.IsNullOrEmpty(u.Phone)).ToList();
    }

    // 13 - Usuarios sin dirección
    public List<User> GetUsersWithoutAddress()
    {
        return _context.users.Where(u => string.IsNullOrEmpty(u.City) || string.IsNullOrEmpty(u.Country)).ToList();
    }

    // 14 - Últimos usuarios registrados
    public List<User> GetLastRegisteredUsers(int count)
    {
        return _context.users.OrderByDescending(u => u.CreatedAt)
                             .Take(count)
                             .ToList();
    }

    // 15 - Ordenar por apellido
    public List<User> GetUsersOrderedByLastName()
    {
        return _context.users.OrderBy(u => u.LastName).ToList();
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
