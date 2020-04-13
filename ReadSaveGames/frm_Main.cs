using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ReadSaveGames
{
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();
        }

        //Lista donde se guardaran los objectives.
        List<string> ListaObjectivesEncontrados = new List<string>();

        private void btn_cargarArchivo_Click(object sender, EventArgs e)
        {
            //Array para dividir el ID objective y valor.
            string[] Nombres;

            if (ofd_partida.ShowDialog() == DialogResult.OK)
            {
                //Obtener la ruta al archivo de la partida guadada.
                txtb_rutaPartida.Text = ofd_partida.FileName;

                //Vaciar la lista y el control del texto.
                rtbx_partidaBinario.Clear();
                ListaObjectivesEncontrados.Clear();

                //Leer objectives y añadirlos a la lista.
                LeerPartida();

                //Mostrar resultados busqueda
                foreach(var item in ListaObjectivesEncontrados)
                {
                    Nombres = item.Split(',');
                    rtbx_partidaBinario.Text += "Hashcode: " + Nombres[0] + " Valor: " + Nombres[1] + Environment.NewLine;
                }
            }
        }

        public void LeerPartida()
        {
            //Crear lector.
            BinaryReaderBigEndian Lector = new BinaryReaderBigEndian(new FileStream(txtb_rutaPartida.Text, FileMode.Open, FileAccess.Read));

            //Variables importantes.
            int NumeroObjectives = 1700;
            int Contador = 0;
            bool EsHashcode = false;
            string NombreHashcode = "";

            //Ir a la sección donde están los objectives.
            Lector.BaseStream.Seek(unchecked((int)0xEC), SeekOrigin.Begin);

            //Buscar los 1700 objectives.
            while (Contador < NumeroObjectives)
            {
                //
                uint DatosLeidos = SwapBytes(Lector.ReadUInt32());

                //Comprovar si lo que ha leido es un objective.
                if (DatosLeidos.ToString("X4").StartsWith("42"))
                {
                    EsHashcode = true;
                    NombreHashcode = DatosLeidos.ToString("X4");
                }
                else
                {
                    //Si lo que había leido es un objective lo añade a la lista junto con su valor.
                    if (EsHashcode)
                    {
                        ListaObjectivesEncontrados.Add(NombreHashcode + "," + int.Parse(DatosLeidos.ToString("X4"), System.Globalization.NumberStyles.HexNumber));
                        EsHashcode = false;
                    }
                }

                //Si son datos de relleno sale del bucle.
                if (DatosLeidos.ToString("X4").Equals("55555555"))
                {
                    break;
                }
                else
                {
                    Contador++;
                }
            }

            //Cerrar el lector.
            Lector.Close();

            //Mostrar numero de objectives.
            rtbx_partidaBinario.Text += "Hashcodes encontrados: " + ListaObjectivesEncontrados.Count + Environment.NewLine;
        }

        //Invertir el número.
        public uint SwapBytes(uint x)
        {
            // swap adjacent 16-bit blocks
            x = (x >> 16) | (x << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
        }
    }
}
