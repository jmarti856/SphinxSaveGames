using System;
using System.Collections.Generic;
using System.IO;

namespace SphinxSavedGameReader
{
    class FuncionesLecturaFicheros
    {
        //Leer seccion hahcodes.h y añadirlos a una lista
        public void LeerSeccionArchivoHashcodes(string ruta, string inicio, string final, List<Hashcodes> Lista)
        {
            string line;
            string [] LineSplit;
            bool read = false;
            StreamReader LectorArchivoHashcodes = File.OpenText(ruta);

            while ((line = LectorArchivoHashcodes.ReadLine()) != null)
            {
                //Empezar a leer
                if (line.Contains(inicio))
                {
                    read = true;
                }

                //Añadir objectives a la lista
                if (read)
                { 
                    if (!line.Contains("/*"))
                    {
                        LineSplit = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                        Lista.Add(new Hashcodes {Etiqueta = LineSplit[1], ValorHex = LineSplit [2]});
                    }
                }

                //Parar de leer y salir del bucle
                if (line.Contains(final))
                {
                    break;
                }
            }
        }
    }
}
