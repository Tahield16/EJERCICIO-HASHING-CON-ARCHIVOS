using System;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using static MyApp.Program;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {   public struct Trabajador
        {
            public int id;
            public string name;
            public string surname;
            public float salary;
            public string rol;
        }
        static int hashId( int id)
        {
            return (id - 1)%3;
        }
        static Trabajador[] hashTrabajadores(List<Trabajador> trabajadoresList)
        {
            int posTrabajador = 0;
            Trabajador[] trabajadoresArray=new Trabajador[trabajadoresList.Count];
            for (int i = 0; i < trabajadoresArray.Length; i++)
            {
                posTrabajador = Math.Abs(hashId(trabajadoresList[i].id));
                if (trabajadoresArray[posTrabajador].id == 0)
                {
                    trabajadoresArray[posTrabajador]=trabajadoresList[i];
                }
                else
                {
                    bool Agregado = false;
                    for(int j = 0; j < posTrabajador; j++)
                    {
                        if (trabajadoresArray[j].id == 0)
                        {
                            trabajadoresArray[j] = trabajadoresList[i];
                            Agregado = true;
                            break;
                        }
                    }
                    if (!Agregado)
                    {
                        for(int j = posTrabajador + 1; j < trabajadoresArray.Length; j++)
                        {
                            if (trabajadoresArray[j].id == 0)
                            {
                                trabajadoresArray[j] = trabajadoresList[i];
                                break;
                            }
                        }
                    }
                }
                
            }

            return trabajadoresArray;
        }
        
        static List<Trabajador> inputWorkers()
        {
           Trabajador trabajador = new Trabajador();
            List<Trabajador> TrabajadoresInput=new List<Trabajador>();
            do
            {
                trabajador = new Trabajador();
                Console.WriteLine("Ingrese el id de empleado, si desea finalizar el ingreso de datos, ingrese un id menor o igual a 0.");
                trabajador.id=int.Parse(Console.ReadLine());
                if (trabajador.id <= 0)
                {
                    break;
                }
                Console.WriteLine($"Ingrese el nombre del empleado con id {trabajador.id}");
                trabajador.name = Console.ReadLine();
                Console.WriteLine($"Ingrese el apellido del empleado con id {trabajador.id}");
                trabajador.surname = Console.ReadLine();
                Console.WriteLine($"Ingrese el salario del empleado con id {trabajador.id}");
                trabajador.salary=float.Parse(Console.ReadLine());
                Console.WriteLine($"Ingrese el rol del empleado con id {trabajador.id}");
                trabajador.rol = Console.ReadLine();
                TrabajadoresInput.Add(trabajador);
            } while (trabajador.id >= 0);
            return TrabajadoresInput;
        }
        static void Main(string[] args)
        {   List<Trabajador>ListaTrabajadores=inputWorkers();
            Trabajador[] trabajadores = hashTrabajadores(ListaTrabajadores);
            
            Console.WriteLine("Archivo creado en la carpeta bin de este proyecto.");

            using (StreamWriter sw = new StreamWriter("empladosOrdenados.csv",false))
            {
                sw.WriteLine("ID;NOMBRE;APELLIDO;SALARIO;ROL");
                foreach (var trabajador in trabajadores)
                {
                    sw.WriteLine($"{trabajador.id};{trabajador.name};{trabajador.surname};{trabajador.salary};{trabajador.rol}");

                }
                sw.Close();
                Console.WriteLine("Archivo creado correctamente.");
            }
            
        }
    }
}