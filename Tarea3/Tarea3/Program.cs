using DAO;
using DAO.Entidades;
using Tarea3;

DAOUsuario db = new DAOUsuario();
ProgramHandler handler = new ProgramHandler(db);

Console.WriteLine("Inicio de programa");

bool continuar = true;

while (continuar)
{
    Console.WriteLine("Seleccione una opcion (0-5) y precione enter");
    Console.WriteLine("0- terminar programa");
    Console.WriteLine("1- traer todos los usuarios");
    Console.WriteLine("2- traer usuario por id");
    Console.WriteLine("3- crear un usuario");
    Console.WriteLine("4- eliminar un usuario por id");
    Console.WriteLine("5- actualizar un usuario por id");

    var opcion = Console.ReadLine();

    switch (opcion)
    {
        case "0":
            {
                continuar = false;
                Console.WriteLine("Fin de programa");
                break;
            }

        case "1":
            {
                Console.WriteLine("Los usuarios de la base de datos son:");
                handler.PrintAllUsers();
                break;
            }
            

        case "2":
            {
                Console.WriteLine("Inserte el id del usuario que quiere buscar (recuerde poner un entero mayor a cero)");
                
                //validacion para ids en el rango de la tabla
                int id = 0;
                while (id <= 0)
                {
                    id = Convert.ToInt32(Console.ReadLine());
                    if (id <= 0)
                    {
                        Console.WriteLine("Recuerde que el id debe ser mayor a 0, ingreselo nuevamente");
                    }
                }

                handler.PrintUserById(id);
                break;
            }

        case "3":
            {
                Console.WriteLine("Para crear un usuario debe ingresar el nombre (string), edad (int) y mail (string)" +
                "\nTodos los campos son obligatorios: ");

                //validacion para campos vacios o null y edades e ids en rango de tabla
                string? nombre; int edad; string? mail;
                bool condition;
                do
                {
                    Console.WriteLine("Nombre: ");
                    nombre = Console.ReadLine();
                    Console.WriteLine("Edad: ");
                    edad = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Mail: ");
                    mail = Console.ReadLine();
                    condition = false;
                    if (nombre == null || nombre.Trim() == "" || edad <= 0 || mail == null || mail.Trim() == "")
                    {
                        Console.WriteLine("No debes dejar campos vacios ni poner edades menores a 0");
                        condition = true;
                    }
                }
                while (condition);

                handler.CreateUser(nombre, edad, mail);
                break;
            }

        case "4":
            {
                Console.WriteLine("Inserte el id del usuario que quiere eliminar (recuerde poner un entero mayor a cero):");

                //validacion para ids en el rango de la tabla
                int id = 0;
                while (id <= 0)
                {
                    id = Convert.ToInt32(Console.ReadLine());
                    if (id <= 0)
                    {
                        Console.WriteLine("Recuerde que el id debe ser mayor a 0, ingreselo nuevamente");
                    }
                }

                handler.DeleteUserById(id);
                break;
            }

        case "5":
            {
                Console.WriteLine("Para actualizar un usuario debe ingresar sus datos: id (int), nombre (string), edad (int) y mail (string)" +
                "\nTodos los campos son obligatorios: ");

                //validacion para campos vacios o null y edades e ids en rango de tabla
                string? nombre; int edad; string? mail; int id;
                bool condition;
                do
                {
                    Console.WriteLine("Id:");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Nombre: ");
                    nombre = Console.ReadLine();
                    Console.WriteLine("Edad: ");
                    edad = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Mail: ");
                    mail = Console.ReadLine();
                    condition = false;
                    if (nombre == null || nombre.Trim() == "" || edad <= 0 || mail == null || mail.Trim() == "" || id <= 0)
                    {
                        Console.WriteLine("No debes dejar campos vacios ni poner edades menores a 0");
                        condition = true;
                    }
                }
                while (condition);

                handler.UpdateUserById(id, nombre, edad, mail);
                break;
            }
            
        default:
            {
                Console.WriteLine("Elija un número 0-5");
                break;
            }
    }

    Console.WriteLine();
}