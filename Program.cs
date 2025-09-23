using Sprint2.Controllers;

UserController User = new UserController();
foreach (var user in User.Index())
{
  Console.WriteLine($"{user.Id} - {user.FirstName} {user.LastName} - {user.Email}");
}

