using System.Collections.Generic;
using System.IO;

namespace ReadSaveGames
{
    public class CategoriasPartidaGuardada
    {
        BinaryReaderBigEndian Lector;

        public List<string> ObtenerListaObjectives(string archivo)
        {
            //Lista Objectives
            List<string> ObjectivesEnLaPartida = new List<string>();

            //Crear lector.
            Lector = new BinaryReaderBigEndian(new FileStream(archivo, FileMode.Open, FileAccess.Read));

            BuscadorPartida(Lector, ObjectivesEnLaPartida, 0xEC, "42");

            //Cerrar el lector.
            Lector.Close();

            return ObjectivesEnLaPartida;
        }

        public List<string> ObtenerListaInventario(string archivo)
        {
            //Lista Objectives
            List<string> InventarioEnLaPartida = new List<string>();

            //Crear lector.
            Lector = new BinaryReaderBigEndian(new FileStream(archivo, FileMode.Open, FileAccess.Read));

            BuscadorPartida(Lector, InventarioEnLaPartida, 0x3EF8, "40");
            BuscadorPartida(Lector, InventarioEnLaPartida, 0x73BC, "40");

            //Cerrar el lector.
            Lector.Close();

            return InventarioEnLaPartida;
        }

        private void BuscadorPartida(BinaryReaderBigEndian Lector, List<string> ListaDatos, int num, string prefijo)
        {
            //Variables importantes.
            int NumeroInventario, Contador = 0;
            uint DatosLeidos;

            //Ir a la sección donde están los objectives.
            Lector.BaseStream.Seek(unchecked((int)num), SeekOrigin.Begin);

            //Número de objectives que hay.
            NumeroInventario = checked((int)SwapBytes(Lector.ReadUInt32()));

            //Buscar los objectives.
            while (Contador < NumeroInventario)
            {
                //Leer datos
                DatosLeidos = SwapBytes(Lector.ReadUInt32());

                //Comprobar si lo que ha leido es un objective.
                if (DatosLeidos.ToString("X4").StartsWith(prefijo))
                {
                    if (DatosLeidos.ToString("X4").Length == 8)
                    {
                        ListaDatos.Add(DatosLeidos.ToString("X4") + "," + int.Parse(SwapBytes(Lector.ReadUInt32()).ToString("X4"), System.Globalization.NumberStyles.HexNumber));
                        Contador++;
                    }
                }

                //Si son datos de relleno o se han hecho las 1700 iteraciones sale del bucle.
                if (DatosLeidos.ToString("X4").Equals("55555555") || Contador == 1700)
                {
                    break;
                }
            }
        }

        //Invertir el número.
        private uint SwapBytes(uint x)
        {
            // swap adjacent 16-bit blocks
            x = (x >> 16) | (x << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
        }
    }
}
