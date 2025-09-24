using Sprint2.Controllers;
using Sprint2.Models; // <--- ESTA LÍNEA FALTABA

public class Program
{
    public static void Main(string[] args)
    {
        var userController = new UserController();
        string opcion;

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
                    
                    // Aquí puedes agregar la solicitud de los demás campos opcionales si lo deseas
                    // newUser.Phone = ...
                    // newUser.City = ...

                    var message = userController.Create(newUser);
                    Console.WriteLine(message);
                    break;
                case "2":
                    Consultations();
                    break;
                case "3": 
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
                                    Console.WriteLine("Usuario no encontrado o error al eliminar.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Operación de eliminación cancelada.");
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
                                    Console.WriteLine("Usuario no encontrado o error al eliminar.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Operación de eliminación cancelada.");
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
                    break;
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    break;
                case "5":
                    break;
                case "6":
                    break;
                case "7":
                    break;
                case "8":
                    break;
                case "9":
                    break;
                case "10":
                    break;
                case "11":
                    break;
                case "12":
                    break;
                case "13":
                    break;
                case "14":
                    break;
                case "15":
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
