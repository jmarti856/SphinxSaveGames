﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace SphinxSavedGameReader
{
    public partial class Frm_Main : Form
    {
        public Frm_Main()
        {
            InitializeComponent();

            //Habilitar menú contextual a los richtextbox
            rtbx_partidaBinario.EnableContextMenu();
            rtbx_PartidaTexto.EnableContextMenu();
        }

        //Listas necesarias:
        List<InventoryItems> InventarioPartidaGuardada = new List<InventoryItems>();
        List<Objectives> ObjectivesPartidaGuardada = new List<Objectives>();
        List<Hashcodes> HashcodesH = new List<Hashcodes>();
        List<ArchivoTXT> ArchivoFinal = new List<ArchivoTXT>();

        //Clases:
        FuncionesLecturaPartida FuncionesL = new FuncionesLecturaPartida();
        FuncionesImpression FuncionesI = new FuncionesImpression();
        FuncionesGenericas FuncionesG = new FuncionesGenericas();
        FuncionesLecturaFicheros FuncionesLF = new FuncionesLecturaFicheros();
        FuncionesConversion FuncionesC = new FuncionesConversion();
        FuncionesEscritura FuncionesE = new FuncionesEscritura();

        Stopwatch Cronometro = new Stopwatch();

        string RutaArchivoHashcodes;
        string RutaArchivoTXT;
        string[] Ankhs;
        bool ArchivoCorrecto;

        private void Btn_cargarArchivo_Click(object sender, EventArgs e)
        {
            //Abrir buscador archivos windows.
            if (opfd_abrirPartidaGuardada.ShowDialog() == DialogResult.OK)
            {
                //Obtener la ruta al archivo de la partida guadada.
                txtb_rutaPartida.Text = opfd_abrirPartidaGuardada.FileName;

                //Vaciar la lista y el control del texto.
                rtbx_partidaBinario.Clear();
                ObjectivesPartidaGuardada.Clear();
                InventarioPartidaGuardada.Clear();

                ArchivoCorrecto = FuncionesL.ComprobarArchivo(txtb_rutaPartida.Text);
                if (ArchivoCorrecto)
                {
                    //Leer objectives y añadirlos a la lista.
                    Thread LecturaPartida = new Thread(LeerPartida);
                    LecturaPartida.Start();
                }
                else
                {
                    MessageBox.Show("Invalid file, select another file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        //Lee los objectives y los añade a la lista.
        public void LeerPartida()
        {
            int TotalObjectives;

            //Iniciar cronometro.
            Cronometro.Start();

            //ObtenerListaObjectives
            ObjectivesPartidaGuardada = FuncionesL.ObtenerObjectivesPartidaGuardada(0xEC, "42", txtb_rutaPartida.Text);
            InventarioPartidaGuardada = FuncionesL.ObtenerInventarioPartidaGuardada(txtb_rutaPartida.Text);
            Ankhs = FuncionesL.LeerAnkhs(txtb_rutaPartida.Text);

            //Parar cronometro.
            Cronometro.Stop();

            TotalObjectives = ObjectivesPartidaGuardada.Count + InventarioPartidaGuardada.Count + Ankhs.Length;

            //Mostrar numero de objectives.
            rtbx_partidaBinario.Invoke((MethodInvoker)delegate
            {
                rtbx_partidaBinario.Text += "Hashcodes found: " + TotalObjectives + Environment.NewLine;
                rtbx_partidaBinario.Text += "Time elapsed: " + Cronometro.Elapsed.TotalSeconds.ToString() + "ms" + Environment.NewLine;
            });

            //Mostrar resultados busqueda
            FuncionesI.ImprimirListaObjectivesPartidaGuardada(rtbx_partidaBinario, ObjectivesPartidaGuardada, chbx_mostrarEtiquetas);
            FuncionesI.ImprimirListaInventarioPartidaGuardada(rtbx_partidaBinario, InventarioPartidaGuardada, chbx_mostrarEtiquetas);
            FuncionesI.ImprimirAnkhs(rtbx_partidaBinario, Ankhs, chbx_mostrarEtiquetas);
        }

        private void Btn_convertir_Click(object sender, EventArgs e)
        {
            //Limpiar los textbox y las listas.
            rtbx_PartidaTexto.Clear();
            ArchivoFinal.Clear();
            HashcodesH.Clear();

            Thread Etiquetar = new Thread(EtiquetarHashcodes);
            Etiquetar.Start();
        }

        private void EtiquetarHashcodes()
        {
            //Asegurar que hay un archivo hashcode.h.
            RutaArchivoHashcodes = FuncionesG.ComprobarRutaArchivoHashcodes(chbx_hashcodesX);

            //Comprobar si existe el archivo y etiquetar los hashcodes encontrados.
            if (File.Exists(RutaArchivoHashcodes))
            {
                //Iniciar cronometro.
                Cronometro.Start();

                //Objectives
                FuncionesLF.LeerSeccionArchivoHashcodes(RutaArchivoHashcodes, "HT_Objective_HASHCODE_BASE", "HT_Objective_HASHCODE_END", HashcodesH);
                FuncionesC.EtiquetarObjectivesEncontrados(ObjectivesPartidaGuardada, HashcodesH, ArchivoFinal);

                //Inventario
                FuncionesLF.LeerSeccionArchivoHashcodes(RutaArchivoHashcodes, "HT_Item_Pickup_BronzeScarab", "HT_Item_Object_HASHCODE_END", HashcodesH);
                FuncionesC.EtiquetarInventarioEncontrado(InventarioPartidaGuardada, HashcodesH, ArchivoFinal);

                //Parar cronometro.
                Cronometro.Stop();

                //Mostrar numero de objectives.
                rtbx_PartidaTexto.Invoke((MethodInvoker)delegate
                {
                    rtbx_PartidaTexto.Text += "Hashcodes tagged: " + (ArchivoFinal.Count + 2) + Environment.NewLine;
                    rtbx_PartidaTexto.Text += "Time elapsed: " + Cronometro.Elapsed.TotalSeconds.ToString() + "ms" + Environment.NewLine;
                });

                //Imprimir Resultado final
                FuncionesI.ImprimirArchivoFinal(rtbx_PartidaTexto, ArchivoFinal, chbx_mostrarEtiquetas);
                FuncionesI.ImprimirAnkhs(rtbx_PartidaTexto, Ankhs, chbx_mostrarEtiquetas);
            }
            else
            {
                MessageBox.Show("File \"hashcodes.h\" not found.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Btn_seleccionarRuta_Click(object sender, EventArgs e)
        {
            if (sfd_guardarArchivoTxt.ShowDialog() == DialogResult.OK)
            {
                RutaArchivoTXT = sfd_guardarArchivoTxt.FileName;
                txbx_guardar.Text = RutaArchivoTXT;
            }
        }

        private void Btn_guardar_Click(object sender, EventArgs e)
        {
            //Si el directorio existe escribir el txt.
            if (Directory.Exists(Path.GetDirectoryName(RutaArchivoTXT)))
            {
                //Escribir archivo Txt con la configuración adequada. 
                FuncionesE.EscribirArchivo(RutaArchivoTXT, ArchivoFinal, Ankhs, chbx_EuroLand);
            }
            else
            {
                MessageBox.Show("The directory does not exist", "Error saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
