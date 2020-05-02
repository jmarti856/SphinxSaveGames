using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SphinxSavedGameReader
{
    public class FuncionesLecturaPartida
    {
        BinaryReaderBigEndian Lector;

        public List<Objectives> ObtenerObjectivesPartidaGuardada(int PosicionObjectives, string PrefijoObjectives, string RtuaPartidaGuardada)
        {
            //Crear lector.
            Lector = new BinaryReaderBigEndian(new FileStream(RtuaPartidaGuardada, FileMode.Open, FileAccess.Read));

            //Crear Lista
            List<Objectives> ObjectivesEncontrados = new List<Objectives>();

            //Variables importantes.
            int NumeroInventario, Contador = 0;
            uint DatosLeidos;

            //Ir a la sección donde están los objectives.
            Lector.BaseStream.Seek(unchecked((int)PosicionObjectives), SeekOrigin.Begin);

            //Número de objectives que hay.
            NumeroInventario = checked((int)SwapBytes(Lector.ReadUInt32()));

            //Buscar los objectives.
            while (Contador < NumeroInventario)
            {
                //Leer datos
                DatosLeidos = SwapBytes(Lector.ReadUInt32());

                //Comprobar si lo que ha leido es un objective.
                if (DatosLeidos.ToString("X4").StartsWith(PrefijoObjectives))
                {
                    if (DatosLeidos.ToString("X4").Length == 8)
                    {
                        ObjectivesEncontrados.Add(new Objectives { ObjectiveNumHex = DatosLeidos.ToString("X4"), ValorObjective = int.Parse(SwapBytes(Lector.ReadUInt32()).ToString("X4"), System.Globalization.NumberStyles.HexNumber).ToString() });
                        Contador++;
                    }
                }

                //Si son datos de relleno o se han hecho las 1700 iteraciones sale del bucle.
                if (DatosLeidos.ToString("X4").Equals("55555555") || Contador == 1700)
                {
                    break;
                }
            }
            Lector.Close();

            return ObjectivesEncontrados;
        }

        public List<InventoryItems> ObtenerInventarioPartidaGuardada(string RtuaPartidaGuardada)
        {
            //Crear Lista
            List<InventoryItems> Inventario = new List<InventoryItems>();

            //InventarioEnLaPartida.Add("/*------------[Sphinx Inventory]------------*/");
            LectorInventario(Inventario, 0x3630, "40", RtuaPartidaGuardada); //Esta sección es para los Dardos.
            LectorInventario(Inventario, 0x3EF8, "40", RtuaPartidaGuardada); //Esta sección es para los Items.
            LectorInventario(Inventario, 0x47C0, "40", RtuaPartidaGuardada); //Esta sección es para las Habilidades.

            // InventarioEnLaPartida.Add("/*------------[Mummy Inventory]------------*/");
            LectorInventario(Inventario, 0x73BC, "40", RtuaPartidaGuardada); //Items Momia. 

            return Inventario;
        }

        private List<InventoryItems> LectorInventario(List<InventoryItems> ListaInventario, int PosicionObjectives, string PrefijoObjectives, string RtuaPartidaGuardada)
        {
            //Crear lector.
            Lector = new BinaryReaderBigEndian(new FileStream(RtuaPartidaGuardada, FileMode.Open, FileAccess.Read));

            //Variables importantes.
            int NumeroInventario, Contador = 0;
            uint DatosLeidos;

            //Ir a la sección donde están los objectives.
            Lector.BaseStream.Seek(unchecked((int)PosicionObjectives), SeekOrigin.Begin);

            //Número de objectives que hay.
            NumeroInventario = checked((int)SwapBytes(Lector.ReadUInt32()));

            //Buscar los objectives.
            while (Contador < NumeroInventario)
            {
                //Leer datos
                DatosLeidos = SwapBytes(Lector.ReadUInt32());

                //Comprobar si lo que ha leido es un objective.
                if (DatosLeidos.ToString("X4").StartsWith(PrefijoObjectives))
                {
                    if (DatosLeidos.ToString("X4").Length == 8)
                    {
                        ListaInventario.Add(new InventoryItems { ItemNumeroHex = DatosLeidos.ToString("X4"), ItemMinValue = int.Parse(SwapBytes(Lector.ReadUInt32()).ToString("X4"), System.Globalization.NumberStyles.HexNumber).ToString(), ItemMaxValue = int.Parse(SwapBytes(Lector.ReadUInt32()).ToString("X4"), System.Globalization.NumberStyles.HexNumber).ToString() });
                        Contador++;
                    }
                }

                //Si son datos de relleno o se han hecho las 1700 iteraciones sale del bucle.
                if (DatosLeidos.ToString("X4").Equals("55555555") || Contador == 350)
                {
                    break;
                }
            }
            Lector.Close();

            return ListaInventario;
        }

        public string [] LeerAnkhs(string RtuaPartidaGuardada)
        {
            string [] Ankhs = new string[2];

            //Crear lector
            Lector = new BinaryReaderBigEndian(new FileStream(RtuaPartidaGuardada, FileMode.Open, FileAccess.Read));

            //Numero de ankhs
            Lector.BaseStream.Seek(unchecked((int)0x3610), SeekOrigin.Begin);
            Ankhs[0] = checked((int)SwapBytes(Lector.ReadUInt32())).ToString();

            //Número total de ankhs
            Lector.BaseStream.Seek(unchecked((int)0x3614), SeekOrigin.Begin);
            Ankhs[1] = (checked((int)SwapBytes(Lector.ReadUInt32())) / 3).ToString();

            return Ankhs;
        }

        public bool ComprobarArchivo(string RtuaPartidaGuardada)
        {
            bool correcto;
            StringBuilder TextCheck = new StringBuilder();

            //Crear lector
            Lector = new BinaryReaderBigEndian(new FileStream(RtuaPartidaGuardada, FileMode.Open, FileAccess.Read));

            //Comprobar el primer byte
            if (Lector.Read().ToString().Equals("4"))
            {
                Lector.BaseStream.Seek(unchecked((int)0x12), SeekOrigin.Begin);

                for (int i = 0; i < 6; i++)
                {
                    TextCheck.Append(Lector.ReadChar());
                }

                if (TextCheck.ToString().Equals("SPHINX"))
                {
                    correcto = true;
                }
                else
                {
                    correcto = false;
                }
            }
            else
            {
                correcto = false;
            }

            return correcto;
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
