using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Tarea4
{
    class Archivos
    {
        public static string directoryo = Directory.GetCurrentDirectory();
        protected string Ruta;
        protected string Nombre;
        //constructor
        public Archivos (string nombre)
        {
            Ruta = Directory.GetCurrentDirectory();
            Nombre = nombre;
        }
        //Metodo touch
        public void touch(string directorio = null)
        {
            //Si contiene \ es por uqe tiene la ruta completa, entonces pasa directo, de no contener,
            //es poque solo es el nombre del archivo, por lo tanto se busca en el directorio actual.
            if (!directorio.Contains("\\"))
            {
                string directorio2 = directoryo+"\\"+directorio;
                directorio = directorio2;
            }
            //Si no existe el archivo en el directorio especificado, se crea, de lo contrario no se 
            //crea y e imprime alerta
            if (!File.Exists(directorio))
            {
                StreamWriter archivo;
                string rutaCompleta = directorio;

                archivo = File.CreateText(rutaCompleta);
                archivo.Close();
            }
            else
            {
                Console.WriteLine("Archivo con el mismo nombre existente en la carpeta. ");
            }
        }
        //Método move que se puede utilizar sin necesidad de instanciar la clase.
        public static void move(string directorio = null, string directorioDestino=null)
        {
            //Si existe el directorio origen, entra
            if (File.Exists(directorio))
            {
                //Si existe el direcotrio destino, entonces imprime que ya existe el archivo,
                //por lo cual no se hace nada más
                if (File.Exists(directorioDestino))
                {
                    Console.WriteLine("No se puede sobreescribir el archivo. ");

                }
                //De no existir el archvo en el directorio destino, entonces muevelo y 
                //borra el origen ya que no es copia, es mover.

                else
                {
                    File.Move(directorio, directorioDestino);
                    File.Delete(directorio);
                }
            }
            //Si no encuentras el diretorio origen, checa si contiene \, de no contenerlo
            //puede ser que sea solo el nombre del archivo de la carpeta actual, por lo 
            //tanto concatena el nobmre del archivo con el directorio actual y checa si existe.
            else
            {
                if (!directorio.Contains("\\"))
                {
                    string directorio2 = directoryo + "\\" + directorio;
                    directorio = directorio2;
                    //Si existe, entonces  checa si el directorio destino existe, si existe 
                    //entonces manda alerta
                    if (File.Exists(directorio))
                    {

                        if (File.Exists(directorioDestino))
                        {
                            Console.WriteLine("No se puede sobreecribir el archivo. ");
                        }
                        //Si no existe, entonces mueve el archivo y borralo del direcotrio origen.
                        else
                        {
                            //Si la ruta destino contiene \ no existe y dieron la ruta, etonces 
                            //mueve le archivo y borra el archivo origen.
                            if (directorioDestino.Contains("\\"))
                            {
                                File.Move(directorio, directorioDestino);
                                File.Delete(directorio);
                            }
                            //De no tenerlas, manda error
                            else
                            {
                                Console.WriteLine("Ruta destino desconocida");
                            }
                            
                        }
                    }
                    else
                        Console.WriteLine("No se ha encontrado el archivo especificado. ");
                }
                //SI el archivo no contiene \ entonces no existe como se ha ingresado, manda alerta.
                else 
                    Console.WriteLine("No se ha encontrado el archivo especificado. ");
            }
        }
        //Método para dir.
        public static void dir(string directorio = null)
        {
            //Si no se recibe directorio, entonces el directorio es nulo, si es nulo entonces es el 
            //directorio actual.
            if (directorio == null)
            {
                directorio = Archivos.directoryo;
            }
            //Creamos dos objetos de estas clases para poder imprimir los archivos y directorios del
            //directorio especificado
            DirectoryInfo di = new DirectoryInfo(@directorio);
            FileInfo files = new FileInfo(directorio);
            //Para cada directorio en le ldirectorio, imprime su nombre.
            foreach (DirectoryInfo dir in di.GetDirectories())
                Console.WriteLine(dir.Name);
            //Para cada archivo en el directorio, imprime su nombre.
            foreach (FileInfo file in di.GetFiles())
                Console.WriteLine(file.Name);
        }
        //Método de cd
        public static string cd(string directorio = null)
        {
            //Si el directrio recibido es nulo, o sea que recibe .., entonces el directorio actual tomalo
            //con el que se trabajará
            string ruta = "";
            if (directorio == null)
            {
                directorio = directoryo;
                int i = directorio.Length;
                //Busca, del ultimo caracter hacia el 0, la primer \.
                while (directorio[i - 1] != '\\')
                {
                    i--;
                }
                //Una vez sabiendo la posicion de \, corta la ruta, y toma como tu nuevo directorio todo antes del ultimo \
                if (directorio[i - 1] == '\\')
                {
                    ruta = directorio.Substring(0, i - 1);
                }
                directoryo = ruta;
                return directoryo;
            }
            //Si no es nulo el directorio, o sea uqe te estan indicando una ruta tipo C:\Users\bla\bla\bla
            else
            {
                //Checa que exista la ruta, si existe, tomala como tu nueva ruta
                if (Directory.Exists(directorio))
                {
                    directoryo = directorio;
                    return directorio;
                }
                //Si no existe, supongamos que te dieron el nombre de una carpeta dentro del directorio actual, 
                //entonces concatena tu directorio actual con el nobmre dado
                else 
                {
                    string directorio2 = directoryo + "\\" + directorio;
                    //Si existe la ruta tomalo como tu nuevo directorio, de lo contrario manda una laerta que no se ha encontrado y recuerda que no te has movido del direcotrio inicial.
                    if (Directory.Exists(directorio2))
                    {
                        directoryo = directorio2;
                        return directoryo;
                    }
                    else
                    {
                        Console.WriteLine("No se encontró la ruta especificada.");
                        return directoryo;
                    }
                }
            }
           
        }
    }
}
