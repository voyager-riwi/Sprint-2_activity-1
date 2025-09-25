using Sprint2.Controllers;
using Sprint2.Models; // <--- ESTA LÍNEA FALTABA

public class Program
{
    public static void Main(string[] args)
    {
        var userController = new UserController();
        string opcion;
        var user = new User();

        while (true)
        {
            Console.WriteLine("========== SISTEMA DE GESTIÓN DE USUARIOS ==========");
            Console.WriteLine("1. Registrar un nuevo usuario.");
            Console.WriteLine("2. Consultas");
            Console.WriteLine("3. Editar un usuario.");
            Console.WriteLine("4. Eliminar un usuario.");
            Console.WriteLine("5. Salir");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.WriteLine("\n--- Registrar un nuevo usuario ---");
                    var newUser = new User();

                    Console.Write("Nombre: ");
                    newUser.FirstName = Console.ReadLine();

                    Console.Write("Apellido: ");
                    newUser.LastName = Console.ReadLine();

                    Console.Write("Nombre de usuario: ");
                    newUser.Username = Console.ReadLine();

                    Console.Write("Correo electrónico: ");
                    newUser.Email = Console.ReadLine();

                    Console.Write("Contraseña: ");
                    newUser.Password = Console.ReadLine();

                    var message = userController.Create(newUser);
                    Console.WriteLine(message);
                    break;
                case "2":
                    Consultations();
                    break; 
                
                case "3":
                    Console.WriteLine("--- Módulo de Actualización de Datos ---");

                    Console.Write("Ingrese el ID del usuario que desea actualizar: ");
                    int userId;
                    if (!int.TryParse(Console.ReadLine(), out userId))
                    {
                        Console.WriteLine(" El ID debe ser un número.");
                        break;
                    }

                    var controller = new UserController();
                    var userToEdit = controller.GetUserForEditing(userId); 

                    if (userToEdit == null) 
                    {
                        Console.WriteLine("✘ Usuario no encontrado.");
                        break;
                    }

                    Console.WriteLine($"1. Nombre actual: {userToEdit.Username}");
                    Console.WriteLine($"2. Correo actual: {userToEdit.Email}");
                    Console.WriteLine($"3. Teléfono actual: {userToEdit.Phone}");
                    Console.WriteLine("4. Contraseña (oculta)");

                    Console.WriteLine("¿Qué campo desea actualizar?");
                    Console.WriteLine("  1. Nombre de usuario");
                    Console.WriteLine("  2. Correo electrónico");
                    Console.WriteLine("  3. Teléfono");
                    Console.WriteLine("  4. Contraseña");
                    Console.Write("Seleccione una opción: ");

                    string option = Console.ReadLine();
                    bool needsUpdate = true;

                    switch (option)
                    {
                        case "1":
                            Console.Write("Ingrese el nuevo nombre: ");
                            userToEdit.Username = Console.ReadLine();
                            break;

                        case "2":
                            Console.Write("Ingrese el nuevo correo: ");
                            userToEdit.Email = Console.ReadLine();
                            break;

                        case "3":
                            Console.Write("Ingrese el nuevo teléfono: ");
                            userToEdit.Phone = Console.ReadLine();
                            break;

                        case "4":
                            Console.Write("Ingrese la nueva contraseña: ");
                            string newPassword = Console.ReadLine();
                            Console.Write("Confirme la nueva contraseña: ");
                            string confirmPassword = Console.ReadLine();

                            if (newPassword == confirmPassword)
                            {
                                userToEdit.Password = newPassword;
                                Console.WriteLine("✔ Contraseña actualizada correctamente.");
                            }
                            else
                            {
                                Console.WriteLine("✘ Error: Las contraseñas no coinciden.");
                                needsUpdate = false;
                            }
                            break;

                        default:
                            Console.WriteLine("✘ Opción no válida. No se realizará ninguna actualización.");
                            needsUpdate = false;
                            break;
                    }

                    if (needsUpdate)
                    { 
                        bool updateSuccess = controller.UpdateUser(userToEdit.Id, userToEdit);

                        if (updateSuccess)
                        {
                            Console.WriteLine(" ¡Cambios guardados exitosamente en la base de datos!");
                           
                            Console.WriteLine("Información actualizada:");
                            Console.WriteLine($"1. Nombre: {userToEdit.Username}");
                            Console.WriteLine($"2. Correo: {userToEdit.Email}");
                            Console.WriteLine($"3. Teléfono: {userToEdit.Phone}"); 
                            Console.WriteLine("4. Contraseña: (oculta)");
                        }
                        else
                        {
                            Console.WriteLine(" Ocurrió un error. Los datos no pudieron ser guardados.");
                        }
                    }
                    break;


                case "4":
                        Console.WriteLine("\n--- Eliminar un usuario ---");
                    Console.WriteLine("1. Eliminar por ID");
                    Console.WriteLine("2. Eliminar por correo electrónico");
                    Console.Write("Seleccione una opción de eliminación: ");
                    string deleteOption = Console.ReadLine();

                    if (deleteOption == "1")
                    {
                        Console.Write("Ingrese el ID del usuario a eliminar: ");
                        if (int.TryParse(Console.ReadLine(), out int id))
                        {
                            var userToDelete = userController.GetUserById(id);
                            if (userToDelete != null)
                            {
                                Console.WriteLine($"\nDetalles del usuario a eliminar: \nID: {userToDelete.Id}\nNombre: {userToDelete.FirstName} {userToDelete.LastName}\nEmail: {userToDelete.Email}");
                                Console.Write("¿Está seguro de eliminar este usuario? (S/N): ");
                                var confirmation = Console.ReadLine();
                                if (confirmation?.ToUpper() == "S")
                                {
                                    if (userController.DeleteUser(id))
                                    {
                                        Console.WriteLine("Usuario eliminado correctamente.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error al eliminar el usuario.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Operación de eliminación cancelada.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Usuario no encontrado.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("ID inválido.");
                        }
                    }
                    else if (deleteOption == "2")
                    {
                        Console.Write("Ingrese el correo electrónico del usuario a eliminar: ");
                        string email = Console.ReadLine();
                        if (!string.IsNullOrEmpty(email))
                        {
                            var userToDelete = userController.GetUserByEmail(email);
                            if (userToDelete != null)
                            {
                                Console.WriteLine($"\nDetalles del usuario a eliminar: \nID: {userToDelete.Id}\nNombre: {userToDelete.FirstName} {userToDelete.LastName}\nEmail: {userToDelete.Email}");
                                Console.Write("¿Está seguro de eliminar este usuario? (S/N): ");
                                var confirmation = Console.ReadLine();
                                if (confirmation?.ToUpper() == "S")
                                {
                                    if (userController.DeleteUserByEmail(email))
                                    {
                                        Console.WriteLine("Usuario eliminado correctamente.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Error al eliminar el usuario.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Operación de eliminación cancelada.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Usuario no encontrado.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Correo electrónico inválido.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Opción no válida.");
                    }
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                    break;
            }
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
    }
    
  public static void Consultations()
    {
        var userController = new UserController();
        string subOpcion;

        do
        {
            Console.Clear();
            Console.WriteLine("========== CONSULTAS Y REPORTES ==========");
            Console.WriteLine("1. Listar todos los usuarios.");
            Console.WriteLine("2. Ver el detalle de un usuario por su Id.");
            Console.WriteLine("3. Ver el detalle de un usuario por su correo electrónico.");
            Console.WriteLine("4. Listar usuarios de una ciudad específica.");
            Console.WriteLine("5. Listar usuarios de un país específico.");
            Console.WriteLine("6. Listar usuarios mayores de una edad específica.");
            Console.WriteLine("7. Listar usuarios de un género específico.");
            Console.WriteLine("8. Mostrar solo nombres completos y correos.");
            Console.WriteLine("9. Contar el total de usuarios registrados.");
            Console.WriteLine("10. Contar cuántos usuarios hay en cada ciudad.");
            Console.WriteLine("11. Contar cuántos usuarios hay en cada país.");
            Console.WriteLine("12. Ver cuáles usuarios no tienen teléfono registrado.");
            Console.WriteLine("13. Ver cuáles usuarios no tienen dirección registrada.");
            Console.WriteLine("14. Listar los últimos usuarios registrados.");
            Console.WriteLine("15. Listar usuarios ordenados por apellido.");
            Console.WriteLine("x. Regresar al menú principal.");
            Console.Write("Seleccione una opción de consulta: ");
            subOpcion = Console.ReadLine()?.ToLower();

            switch (subOpcion)
            {
                case "1":
                    Console.WriteLine("\n--- Listado de todos los usuarios ---");
                    var allUsers = userController.GetAllUsers();
                    foreach (var user in allUsers)
                    {
                        Console.WriteLine($"ID: {user.Id}, Nombre: {user.FirstName} {user.LastName}, Email: {user.Email}");
                    }
                    break;

                case "2":
                    Console.WriteLine("\n--- Ver el detalle de un usuario por su Id ---");
                    Console.Write("Ingrese el ID del usuario: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        var userById = userController.GetUserById(id);
                        if (userById != null)
                        {
                            Console.WriteLine($"ID: {userById.Id}\nNombre: {userById.FirstName} {userById.LastName}\nCorreo: {userById.Email}\nTeléfono: {userById.Phone}\nCiudad: {userById.City}");
                        }
                        else
                        {
                            Console.WriteLine("Usuario no encontrado.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                    }
                    break;

                case "3":
                    Console.WriteLine("\n--- Ver el detalle de un usuario por su correo electrónico ---");
                    Console.Write("Ingrese el correo electrónico del usuario: ");
                    var email = Console.ReadLine();
                    var userByEmail = userController.GetUserByEmail(email);
                    if (userByEmail != null)
                    {
                        Console.WriteLine($"ID: {userByEmail.Id}\nNombre: {userByEmail.FirstName} {userByEmail.LastName}\nCorreo: {userByEmail.Email}");
                    }
                    else
                    {
                        Console.WriteLine("Usuario no encontrado.");
                    }
                    break;

                case "4":
                    Console.WriteLine("\n--- Listar usuarios de una ciudad específica ---");
                    Console.Write("Ingrese el nombre de la ciudad: ");
                    var city = Console.ReadLine();
                    var usersByCity = userController.GetUsersByCity(city);
                    if (usersByCity.Any())
                    {
                        foreach (var user in usersByCity)
                        {
                            Console.WriteLine($"Nombre: {user.FirstName} {user.LastName}, Ciudad: {user.City}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No se encontraron usuarios en la ciudad de '{city}'.");
                    }
                    break;

                case "5":
                    Console.WriteLine("\n--- Listar usuarios de un país específico ---");
                    Console.Write("Ingrese el nombre del país: ");
                    var country = Console.ReadLine();
                    var usersByCountry = userController.GetUsersByCountry(country);
                    if (usersByCountry.Any())
                    {
                        foreach (var user in usersByCountry)
                        {
                            Console.WriteLine($"Nombre: {user.FirstName} {user.LastName}, País: {user.Country}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No se encontraron usuarios en el país de '{country}'.");
                    }
                    break;

                case "6":
                    Console.WriteLine("\n--- Listar usuarios mayores de una edad específica ---");
                    Console.Write("Ingrese la edad mínima: ");
                    if (int.TryParse(Console.ReadLine(), out int minAge))
                    {
                        var usersByAge = userController.GetUsersOlderThan(minAge);
                        if (usersByAge.Any())
                        {
                            foreach (var user in usersByAge)
                            {
                                Console.WriteLine($"Nombre: {user.FirstName} {user.LastName}, Edad: {user.Age}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron usuarios con la edad especificada.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Edad inválida.");
                    }
                    break;

                case "7":
                    Console.WriteLine("\n--- Listar usuarios de un género específico ---");
                    Console.Write("Ingrese el género (M/F/Otro): ");
                    var gender = Console.ReadLine();
                    var usersByGender = userController.GetUsersByGender(gender);
                    if (usersByGender.Any())
                    {
                        foreach (var user in usersByGender)
                        {
                            Console.WriteLine($"Nombre: {user.FirstName} {user.LastName}, Género: {user.Gender}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No se encontraron usuarios con el género especificado.");
                    }
                    break;

                case "8":
                    Console.WriteLine("\n--- Mostrar solo nombres completos y correos ---");
                    var namesAndEmails = userController.GetNamesAndEmails();
                    foreach (dynamic item in namesAndEmails)
                    {
                        Console.WriteLine($"Nombre: {item.FirstName} {item.LastName}, Correo: {item.Email}");
                    }
                    break;

                case "9":
                    Console.WriteLine("\n--- Contar el total de usuarios registrados ---");
                    var totalUsers = userController.CountTotalUsers();
                    Console.WriteLine($"Total de usuarios registrados: {totalUsers}");
                    break;

                case "10":
                    Console.WriteLine("\n--- Contar cuántos usuarios hay en cada ciudad ---");
                    var usersByCityCount = userController.CountUsersByCity();
                    foreach (var item in usersByCityCount)
                    {
                        Console.WriteLine($"Ciudad: {item.Key}, Cantidad de usuarios: {item.Value}");
                    }
                    break;

                case "11":
                    Console.WriteLine("\n--- Contar cuántos usuarios hay en cada país ---");
                    var usersByCountryCount = userController.CountUsersByCountry();
                    foreach (var item in usersByCountryCount)
                    {
                        Console.WriteLine($"País: {item.Key}, Cantidad de usuarios: {item.Value}");
                    }
                    break;

                case "12":
                    Console.WriteLine("\n--- Ver cuáles usuarios no tienen teléfono registrado ---");
                    var usersWithoutPhone = userController.GetUsersWithoutPhone();
                    if (usersWithoutPhone.Any())
                    {
                        foreach (var user in usersWithoutPhone)
                        {
                            Console.WriteLine($"Nombre: {user.FirstName} {user.LastName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Todos los usuarios tienen un teléfono registrado.");
                    }
                    break;

                case "13":
                    Console.WriteLine("\n--- Ver cuáles usuarios no tienen dirección registrada ---");
                    var usersWithoutAddress = userController.GetUsersWithoutAddress();
                    if (usersWithoutAddress.Any())
                    {
                        foreach (var user in usersWithoutAddress)
                        {
                            Console.WriteLine($"Nombre: {user.FirstName} {user.LastName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Todos los usuarios tienen una dirección registrada.");
                    }
                    break;

                case "14":
                    Console.WriteLine("\n--- Listar los últimos usuarios registrados ---");
                    Console.Write("Ingrese la cantidad de usuarios a mostrar: ");
                    if (int.TryParse(Console.ReadLine(), out int count))
                    {
                        var lastUsers = userController.GetLastRegisteredUsers(count);
                        if (lastUsers.Any())
                        {
                            foreach (var user in lastUsers)
                            {
                                // Asume que tienes una propiedad RegistrationDate en tu modelo User
                                Console.WriteLine($"ID: {user.Id}, Nombre: {user.FirstName} {user.LastName}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No se encontraron usuarios.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Cantidad inválida.");
                    }
                    break;

                case "15":
                    Console.WriteLine("\n--- Listar usuarios ordenados por apellido ---");
                    var usersOrdered = userController.GetUsersOrderedByLastName();
                    foreach (var user in usersOrdered)
                    {
                        Console.WriteLine($"Nombre: {user.FirstName} {user.LastName}");
                    }
                    break;

                case "x":
                    return;

                default:
                    Console.WriteLine("Opción no válida. Inténtelo de nuevo.");
                    break;
            }
            Console.WriteLine("\nPresiona cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        } while (subOpcion != "x");
    }
}
