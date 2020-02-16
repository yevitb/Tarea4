using System;
using System.IO;
using System.Collections.Generic;

namespace Tarea4
{
    class Program
    {
       
        public static string opc;
        public static string path = Directory.GetCurrentDirectory();
        public static List<string> history = new List<string>();
        public static int bandera;


        static void Main(string[] args)
        {
            do {
                
                //obtener el directorio actual para empezar a trabajar.
                //string path = Directory.GetCurrentDirectory();
                //Trabajar con los comandos que se ingresan
                int intermedio = 0;
                int count = 0;
                int j = 0;
                //Bandera para recibir cadenas con espacios delimitados por "
                bool AntesComillas = false;
                //Imprimir el directorio "actual"
                Console.Write(path+"\\");
                //recibir comandos
                opc = Console.ReadLine();
                history.Add(opc);
                opc = opc + ' ';
                string argumento = "";
                List<string> argumentos = new List<string>();
                //Separaremos los comandos usando un espacio como delimitador
                for (int i=0;i<opc.Length;i++)
                {
                    //Si antes de este se recivio un argumento entre comillas, entra aqui para que norepita 
                    //la cadena al entronctar el espacio que nuevamente trabaja como delimiador de argumentos
                    if (opc[i]==' ' && AntesComillas == true)
                    {
                        i++;
                        AntesComillas = false;
                    }
                    //si se encuentra una comill doble, se junta ocmo una cadena, inclueyendo espacios
                    //Para que todo conforme una cadena hasta que se necuentre comill adoble de nuevo
                    // deno encontrar comila doble que cierra manda error
                    else if (opc[i] == '\"' && AntesComillas==false)
                    {
                        j = i + 1;
                        i++;
                            while (opc[i] != '\"')
                        {
                            i++;
                            count++;
                            if (i == opc.Length - 1 && opc[i] != '\"')
                            {
                                throw new ArgsError();
                            }
                        }
                        argumento = opc.Substring(j, i - 1 - intermedio);
                        intermedio = i + 1;
                        argumentos.Add(argumento);
                        AntesComillas = true;
                    }
                    //Si el anterior no fue con comillas, se hacer el corte del argumetno
                    else if (opc[i] == ' '&& AntesComillas!=true)
                    {
                        argumento = opc.Substring(intermedio, i-intermedio);
                        intermedio = i + 1;
                        argumentos.Add(argumento);
                        count++;
                    }
                }
                //El argumento 0 de la lista es el comando, entonces lo leemos para saber qué quiere hacer el usuario.
                //Dependiendo del comando dse manda a llamar a al metodo correcto, sin antes checar el numero de argumentos, que este sea correcto.
                switch (argumentos[0])
                {
                    case "touch":
                        if (argumentos.Count > 2 || argumentos.Count<2)
                            Console.WriteLine("Error en el nunero de argumentos, touch acepta 1.");
                        else
                        {
                            Archivos f1 = new Archivos(argumentos[1]);
                            f1.touch(argumentos[1]);
                        }
                        break;
                    case "dir":
                        if (argumentos.Count > 2)
                            Console.WriteLine("Demasiados argumentos, se espera 1.");
                        else
                        {
                            if (argumentos.Count == 1)
                                Archivos.dir();
                            else if (argumentos.Count == 2)
                                Archivos.dir(argumentos[1]);
                        }
                        break;
                    case "cd":
                        if (argumentos.Count > 2 || argumentos.Count < 2)
                            Console.WriteLine("Error en el nunero de argumentos, cd espera un argumento. ");
                        else
                        {
                            if (argumentos[1] == "..")
                                path = Archivos.cd();
                            else
                            {
                                path = Archivos.cd(argumentos[1]);
                            }
                        }
                        break;
                    case "move":
                        if (argumentos.Count > 4 || argumentos.Count<3)
                            Console.WriteLine("ERROR: Número de argumentos incorrecto, move recibe 2 argumentos");
                        else
                        {
                            Archivos.move(argumentos[1], argumentos[2]);
                        }
                        break;
                    case "history":
                        if (argumentos.Count > 1)
                            Console.WriteLine("Demasiados argumentos, history no recibe argumentos.");
                        else
                        {
                            foreach (string command in history)
                                Console.WriteLine(command);
                        }
                        break;
                    case "cls":
                        if (argumentos.Count > 1)
                            Console.WriteLine("Demasiados argumentos, cls no recibe mas argumentos.");
                        else
                        {
                            Console.Clear();
                        }
                        break;
                    case "exit":
                        bandera = 18;
                        break;
                }

            } while (bandera !=18);
        }
        
    }

}
