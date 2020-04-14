using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace ReadSaveGames
{
    class FuncionesGenerales
    {
        //Mostar Datos lista en un RichTextBox
        public void ImprimirListaRichTextBox(RichTextBox textbox, List<string> ListaItems, CheckBox Etiquetas, bool items)
        {
            string[] cadenaResultadoLeerPartida;

            foreach (string item in ListaItems)
            {
                if (!item.StartsWith("/*"))
                {
                    cadenaResultadoLeerPartida = item.Split(',');

                    textbox.Invoke((MethodInvoker)delegate
                    {
                        if (Etiquetas.Checked)
                        {
                            if (cadenaResultadoLeerPartida.Length == 3)
                            {
                                textbox.Text += "Hashcode: " + cadenaResultadoLeerPartida[0] + " Value: " + cadenaResultadoLeerPartida[1] + " Item max: " + cadenaResultadoLeerPartida[2] + Environment.NewLine;
                            }
                            else
                            {
                                textbox.Text += "Hashcode: " + cadenaResultadoLeerPartida[0] + " Value: " + cadenaResultadoLeerPartida[1] + Environment.NewLine;
                            }
                        }
                        else
                        {
                            if (cadenaResultadoLeerPartida.Length == 3)
                            {
                                textbox.Text += cadenaResultadoLeerPartida[0] + " " + cadenaResultadoLeerPartida[1] + " " + cadenaResultadoLeerPartida[2] + Environment.NewLine;
                            }
                            else
                            {
                                textbox.Text += cadenaResultadoLeerPartida[0] + " " + cadenaResultadoLeerPartida[1] + Environment.NewLine;
                            }
                        }
                    });
                }
                else
                {
                    textbox.Invoke((MethodInvoker)delegate
                    {
                        textbox.Text += item + Environment.NewLine;

                    });
                }
            }
        }

        //Asegurar que siempre hay un archivo hashcodes.h seleccionado
        public string ComprobarRutaArchivoHashcodes(CheckBox chbx_hashcodesX)
        {
            string ruta;

            if (chbx_hashcodesX.Checked)
            {
                ruta = @"S:\Sphinx\Project\hashcodes.h";
            }
            else
            {
                if (File.Exists(Application.StartupPath + "//hashcodes.h"))
                {
                    ruta = Application.StartupPath + "//hashcodes.h";
                }
                else
                {
                    MessageBox.Show("File not found, \"S:\\Sphinx\\Project\\hashcodes.h\" file will be used.");
                    ruta = @"S:\Sphinx\Project\hashcodes.h";
                }
            }

            return ruta;
        }

        //Leer seccion hahcodes.h y añadirlos a una lista
        public void LeerSeccionArchivoHashcodes(string ruta, string inicio, string final, List<string>Lista)
        {
            string line;
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
                        Lista.Add(line);
                    }
                }

                //Parar de leer y salir del bucle
                if (line.Contains(final))
                {
                    read = false;
                    break;
                }
            }
        }

        //Obtener la etiqueta de un hashcode buscando en el archivo hashcode.h
        public void ObtenerArchivoFinal(string ID, string value, List<string>ArchivoFinal, List<string> ListaSeccionHashcodesH)
        {
            string[] ListaObjectivesSplit;
            string Element = null;

            foreach (string item in ListaSeccionHashcodesH)
            {
                if (item.Contains(ID, StringComparison.OrdinalIgnoreCase))
                {
                    Element = item;
                    break;
                }
            }

            if(!String.IsNullOrEmpty(Element))
            {
                ListaObjectivesSplit = Element.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                ArchivoFinal.Add(ListaObjectivesSplit[1] + "," + value);
            }
            else
            {
                MessageBox.Show("The hashcode: \"" + ID + "\"has not found in hashcodes.h", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ObtenerArchivoFinal(string ID, string value, string max, List<string> ArchivoFinal, List<string> ListaSeccionHashcodesH)
        {
            string[] ListaObjectivesSplit;
            string Element = null;

            foreach (string item in ListaSeccionHashcodesH)
            {
                if (item.Contains(ID, StringComparison.OrdinalIgnoreCase))
                {
                    Element = item;
                    break;
                }
            }

            if (!String.IsNullOrEmpty(Element))
            {
                ListaObjectivesSplit = Element.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                ArchivoFinal.Add(ListaObjectivesSplit[1] + "," + value + "," + max);
            }
            else
            {
                MessageBox.Show("The hashcode: \"" + ID + "\"has not found in hashcodes.h", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void ObtenerEtiquetasHashcodes(string RutaArchivoHashcodes, List<string> ListaSeccionArchivoHashcodes, List<string> HashcodesParaEtiquetar, List<string> ArchivoFinal, bool inventory)
        {
            string[] ObjectivesEncontradosSplit;
            foreach (string item in HashcodesParaEtiquetar)
            {
                if (!item.StartsWith("/*"))
                {
                    ObjectivesEncontradosSplit = item.Split(',');
                    if (inventory)
                    {
                        if (ObjectivesEncontradosSplit.Length == 3)
                        {
                            ObtenerArchivoFinal("0x" + ObjectivesEncontradosSplit[0], ObjectivesEncontradosSplit[1], ObjectivesEncontradosSplit[2], ArchivoFinal, ListaSeccionArchivoHashcodes);
                        }
                    }
                    else
                    {
                        ObtenerArchivoFinal("0x" + ObjectivesEncontradosSplit[0], ObjectivesEncontradosSplit[1], ArchivoFinal, ListaSeccionArchivoHashcodes);
                    }
                }
                else
                {
                    ArchivoFinal.Add(item);
                }
            }
        }
    }
}
