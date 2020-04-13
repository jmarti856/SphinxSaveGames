using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        List<string> ListaObjectives = new List<string>();
        List<string> ArchivoFinal = new List<string>();
        Stopwatch Cronometro = new Stopwatch();
        string RutaArchivosHashcodes = Application.StartupPath + "//hashcodes.h";


        //Mostrar los hashcodes encontrados en la partida guardada.
        private void btn_cargarArchivo_Click(object sender, EventArgs e)
        {
            //Array para dividir el ID objective y valor.
            string[] cadenaResultadoLeerPartida;

            //Abrir buscador archivos windows.
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
                foreach(string item in ListaObjectivesEncontrados)
                {
                    cadenaResultadoLeerPartida = item.Split(',');
                    rtbx_partidaBinario.Text += "Hashcode: " + cadenaResultadoLeerPartida[0] + " Valor: " + cadenaResultadoLeerPartida[1] + Environment.NewLine;
                }
            }
        }

        //Lee los objectives y los añade a la lista.
        public void LeerPartida()
        {
            //Iniciar cronometro.
            Cronometro.Start();

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

                //Comprobar si lo que ha leido es un objective.
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

            //Parar cronometro.
            Cronometro.Stop();

            //Mostrar numero de objectives.
            rtbx_partidaBinario.Text += "Hashcodes encontrados: " + ListaObjectivesEncontrados.Count + Environment.NewLine;
            rtbx_partidaBinario.Text += "Tiempo transcurrido: " + Cronometro.Elapsed.TotalSeconds.ToString() + "s" + Environment.NewLine;
        }

        //Invertir el número.
        public uint SwapBytes(uint x)
        {
            // swap adjacent 16-bit blocks
            x = (x >> 16) | (x << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
        }

        //Cambiar las los números por las etiquetas.
        private void btn_convertir_Click(object sender, EventArgs e)
        {
            string [] ObjectivesEncontradosSplit;

            if (File.Exists(RutaArchivosHashcodes))
            {
                //Iniciar cronometro.
                Cronometro.Start();

                AddObjectivesArchivoLista(RutaArchivosHashcodes);
                foreach (string item in ListaObjectivesEncontrados)
                {
                    ObjectivesEncontradosSplit = item.Split(',');
                    ObtenerEtiquetaHashcode("0x" + ObjectivesEncontradosSplit[0], ObjectivesEncontradosSplit[1]);
                }

                //Parar cronometro.
                Cronometro.Stop();

                //Mostrar numero de objectives.
                rtbx_PartidaTexto.Text += "Hashcodes convertidos: " + ArchivoFinal.Count + Environment.NewLine;
                rtbx_PartidaTexto.Text += "Tiempo transcurrido: " + Cronometro.Elapsed.TotalSeconds.ToString() + "s" + Environment.NewLine;

                //Mostrar resultados
                foreach (string item in ArchivoFinal)
                {
                    rtbx_PartidaTexto.Text += item + Environment.NewLine;
                }

            }
            else
            {
                MessageBox.Show("No se ha encontrado el archivo \"hashcodes.h\".","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
        }

        public void ObtenerEtiquetaHashcode(string ID, string value)
        {
            string[] ListaObjectivesSplit;

            foreach (string item in ListaObjectives)
            {
                ListaObjectivesSplit = item.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                if (ListaObjectivesSplit[2].Equals(ID, StringComparison.InvariantCultureIgnoreCase))
                {
                    ArchivoFinal.Add(ListaObjectivesSplit[1] + "     " + value);
                }
            }

        }

        public void AddObjectivesArchivoLista(string ruta)
        {
            string line;
            bool read = false;
            StreamReader reader = File.OpenText(ruta);

            while ((line = reader.ReadLine()) != null)
            {
                //Empezar a leer
                if (line.Contains("HT_Objective_HASHCODE_BASE"))
                {
                    read = true;
                }

                //Añadir objectives a la lista
                if (read)
                {
                    ListaObjectives.Add(line);
                }

                //Parar de leer y salir del bucle
                if (line.Contains("HT_Objective_HASHCODE_END"))
                {
                    read = false;
                    break;
                }
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (sfd_save.ShowDialog() == DialogResult.OK)
            {
                txbx_guardar.Text = Path.GetFullPath(sfd_save.FileName);

                TextWriter tw = new StreamWriter(sfd_save.FileName);

                foreach (string s in ArchivoFinal)
                {
                    if (chbx_EuroLand.Checked)
                    {
                        tw.WriteLine("setobjective " + s);
                    }
                    else
                    {
                        tw.WriteLine(s);
                    }
                }

                tw.Close();
            }
        }

    }
}
