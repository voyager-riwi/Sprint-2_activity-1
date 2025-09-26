using Sprint2.Data;
using Sprint2.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Sprint2.Controllers;

public class UserController
{
    private readonly MysqlDbContext _context;

    public UserController()
    {
        _context = new MysqlDbContext();
    }

    // CREATE
    public User Create(User newUser)
    {
        // Validamos que el correo no exista o el usuario
        var userExists = _context.users.Any(u => u.Username == newUser.Username || u.Email == newUser.Email);
        if (userExists)
        {
            return null;
        }

        newUser.CreatedAt = DateTime.Now;

        _context.users.Add(newUser);
        _context.SaveChanges();

        // Devolvemos el usuario recién creado.
        return newUser;
    }
    
    // Update
    public bool UpdateUser(int userId, User userDataToUpdate)
    {
        // Usamos el contexto de la clase (_context) en lugar de crear uno nuevo.
        var user = _context.users.FirstOrDefault(u => u.Id == userId);
        
        if (user == null)
        {
            return false; 
        }
        
        // Actualizamos los datos del usuario encontrado en la BD.
        user.Username = userDataToUpdate.Username;
        user.Email = userDataToUpdate.Email;
        user.Phone = userDataToUpdate.Phone;
        user.Password = userDataToUpdate.Password;

        try
        {
            _context.SaveChanges();
            return true; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error al actualizar el usuario: {ex.Message}");
            return false;
        }
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
    
    // 3 - Ver detalles por correo
    public User GetUserByEmail(string email)
    {
        return _context.users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
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
        var userToDelete = _context.users.Find(id);
        if (userToDelete == null)
        {
            return false;
        }

        _context.users.Remove(userToDelete);
        _context.SaveChanges();
        return true;
    }

    // Delete por Email
    public bool DeleteUserByEmail(string email)
    {
        var userToDelete = _context.users.FirstOrDefault(u => u.Email.ToLower() == email.ToLower());
        if (userToDelete == null)
        {
            return false;
        }

        _context.users.Remove(userToDelete);
        _context.SaveChanges();
        return true;
    }
}