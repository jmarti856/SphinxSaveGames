using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ReadSaveGames
{
    public partial class frm_Main : Form
    {
        public frm_Main()
        {
            InitializeComponent();

            //Habilitar menú contextual a los richtextbox
            rtbx_partidaBinario.EnableContextMenu();
            rtbx_PartidaTexto.EnableContextMenu();
        }

        //Variables globales
        List<string> ObjectivesEncontradosPartida = new List<string>();
        List<string> InventarioEncontradoPartida = new List<string>();
        List<string> ListaSeccionArchivoHashcodes = new List<string>();
        List<string> ArchivoFinal = new List<string>();
       
        Stopwatch Cronometro = new Stopwatch();
       
        string RutaArchivoHashcodes;
        string RutaGuardarTXT;

        //Clases
        CategoriasPartidaGuardada ClasseFunciones = new CategoriasPartidaGuardada();
        FuncionesGenerales FuncionesGenerales = new FuncionesGenerales();

        //Mostrar los hashcodes encontrados en la partida guardada.
        private void btn_cargarArchivo_Click(object sender, EventArgs e)
        {
            //Abrir buscador archivos windows.
            if (ofd_partida.ShowDialog() == DialogResult.OK)
            {
                //Obtener la ruta al archivo de la partida guadada.
                txtb_rutaPartida.Text = ofd_partida.FileName;

                //Vaciar la lista y el control del texto.
                rtbx_partidaBinario.Clear();
                ObjectivesEncontradosPartida.Clear();
                InventarioEncontradoPartida.Clear();

                //Leer objectives y añadirlos a la lista.
                Thread LecturaPartida = new Thread(LeerPartida);
                LecturaPartida.Start();
            }
        }

        //Lee los objectives y los añade a la lista.
        public void LeerPartida()
        {
            int cuenta;
            //Iniciar cronometro.
            Cronometro.Start();

            //ObtenerListaObjectives
            ObjectivesEncontradosPartida = ClasseFunciones.ObtenerListaObjectives(txtb_rutaPartida.Text);
            InventarioEncontradoPartida = ClasseFunciones.ObtenerListaInventario(txtb_rutaPartida.Text);

            //Parar cronometro.
            Cronometro.Stop();

            cuenta = ObjectivesEncontradosPartida.Count + InventarioEncontradoPartida.Count;
            //Mostrar numero de objectives.
            rtbx_partidaBinario.Invoke((MethodInvoker)delegate
            {
                rtbx_partidaBinario.Text += "Hashcodes found: " + cuenta + Environment.NewLine;
                rtbx_partidaBinario.Text += "Time elapsed: " + Cronometro.Elapsed.TotalSeconds.ToString() + "ms" + Environment.NewLine;
            });

            //Mostrar resultados busqueda
            FuncionesGenerales.ImprimirListaRichTextBox(rtbx_partidaBinario, ObjectivesEncontradosPartida, chbx_mostrarEtiquetas);
            FuncionesGenerales.ImprimirListaRichTextBox(rtbx_partidaBinario, InventarioEncontradoPartida, chbx_mostrarEtiquetas);
        }

        //Cambiar las los números por las etiquetas.
        private void btn_convertir_Click(object sender, EventArgs e)
        {
            //Limpiar los textbox y las listas.
            rtbx_PartidaTexto.Clear();
            ArchivoFinal.Clear();
            ListaSeccionArchivoHashcodes.Clear();

            Thread Etiquetar = new Thread(EtiquetarHashcodes);
            Etiquetar.Start();
        }

        private void EtiquetarHashcodes()
        {
            //Asegurar que hay un archivo hashcode.h.
            RutaArchivoHashcodes = FuncionesGenerales.ComprobarRutaArchivoHashcodes(chbx_hashcodesX);

            //Comprobar si existe el archivo y etiquetar los hashcodes encontrados.
            if (File.Exists(RutaArchivoHashcodes))
            {
                //Iniciar cronometro.
                Cronometro.Start();

                //Objectives
                FuncionesGenerales.LeerSeccionArchivoHashcodes(RutaArchivoHashcodes, "HT_Objective_HASHCODE_BASE", "HT_Objective_HASHCODE_END", ListaSeccionArchivoHashcodes);
                FuncionesGenerales.ObtenerEtiquetasHashcodes(RutaArchivoHashcodes, ListaSeccionArchivoHashcodes, ObjectivesEncontradosPartida, ArchivoFinal);

                //Inventario
                FuncionesGenerales.LeerSeccionArchivoHashcodes(RutaArchivoHashcodes, "HT_Item_Pickup_BronzeScarab", "HT_Item_Object_HASHCODE_END", ListaSeccionArchivoHashcodes);
                FuncionesGenerales.ObtenerEtiquetasHashcodes(RutaArchivoHashcodes, ListaSeccionArchivoHashcodes, InventarioEncontradoPartida, ArchivoFinal);

                //Parar cronometro.
                Cronometro.Stop();

                //Mostrar numero de objectives.
                rtbx_PartidaTexto.Invoke((MethodInvoker)delegate
                {
                    rtbx_PartidaTexto.Text += "Hashcodes tagged: " + ArchivoFinal.Count + Environment.NewLine;
                    rtbx_PartidaTexto.Text += "Time elapsed: " + Cronometro.Elapsed.TotalSeconds.ToString() + "ms" + Environment.NewLine;
                });

                //Mostrar resultados.
                FuncionesGenerales.ImprimirListaRichTextBox(rtbx_PartidaTexto, ArchivoFinal, chbx_mostrarEtiquetas);
            }
            else
            {
                MessageBox.Show("File \"hashcodes.h\" not found.", "Error reading", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btn_seleccionarRuta_Click(object sender, EventArgs e)
        {
            if (sfd_save.ShowDialog() == DialogResult.OK)
            {
                RutaGuardarTXT = sfd_save.FileName;
                txbx_guardar.Text = RutaGuardarTXT;
            }
        }

        private void btn_guardar_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(Path.GetDirectoryName(RutaGuardarTXT)))
            {
                TextWriter tw = new StreamWriter(RutaGuardarTXT);
                string [] cadenaDividida;
                string prefix = "";

                try
                {
                    foreach (string s in ArchivoFinal)
                    {
                        cadenaDividida = s.Split(',');
                        if (cadenaDividida[0].StartsWith("HT_Objective"))
                        {
                            prefix = "SETOBJECTIVE ";
                        }
                        if (cadenaDividida[0].StartsWith("HT_Item"))
                        {
                            prefix = "INVENTORYSET ";
                        }

                        if (chbx_EuroLand.Checked)
                        {
                            tw.WriteLine(prefix + cadenaDividida[0] + " " + cadenaDividida[1]);
                        }
                        else
                        {
                            tw.WriteLine(cadenaDividida[0] + " " + cadenaDividida[1]);
                        }
                    }

                    tw.Close();

                    MessageBox.Show("File saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("An error occurred while writing the file.", "Error saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("The directory does not exist", "Error saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}