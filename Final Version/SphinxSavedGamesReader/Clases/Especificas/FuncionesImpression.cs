﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SphinxSavedGameReader
{
    class FuncionesImpression
    {
        public void ImprimirListaObjectivesPartidaGuardada(RichTextBox textbox, List<Objectives> ListaObjectives, CheckBox MostarEtiquetas)
        {
            foreach (Objectives item in ListaObjectives)
            {
                textbox.Invoke((MethodInvoker)delegate
                {
                    if (MostarEtiquetas.Checked)
                    {
                        textbox.Text += "Hashcode: " + item.ObjectiveNumHex + " Valor: " + item.ValorObjective + Environment.NewLine;
                    }
                    else
                    {
                        textbox.Text += item.ObjectiveNumHex + " " + item.ValorObjective + Environment.NewLine;
                    }
                });
            }
        }

        public void ImprimirListaInventarioPartidaGuardada(RichTextBox textbox, List<InventoryItems> ListaItems, CheckBox MostarEtiquetas)
        {
            foreach (InventoryItems item in ListaItems)
            {
                textbox.Invoke((MethodInvoker)delegate
                {
                    if (MostarEtiquetas.Checked)
                    {
                        textbox.Text += "Hashcode: " + item.ItemNumeroHex + " Min: " + item.ItemMinValue + " Max: " + item.ItemMaxValue + Environment.NewLine;
                    }
                    else
                    {
                        textbox.Text += item.ItemNumeroHex + " " + item.ItemMinValue + item.ItemMaxValue + Environment.NewLine;
                    }
                });
            }
        }

        public void ImprimirArchivoFinal(RichTextBox textbox, List<ArchivoTXT> ArchivoFinal, CheckBox MostarEtiquetas)
        {
            foreach (ArchivoTXT item in ArchivoFinal)
            {
                textbox.Invoke((MethodInvoker)delegate
                {
                    if (MostarEtiquetas.Checked)
                    {
                        if (!String.IsNullOrEmpty(item.ItemValorMax))
                        {
                            textbox.Text += "Hashcode: " + item.Etiqueta + " Value: " + item.Valor + " Max: " + item.ItemValorMax + Environment.NewLine;
                        }
                        else
                        {
                            textbox.Text += "Hashcode: " + item.Etiqueta + " Value: " + item.Valor + Environment.NewLine;
                        }
                    }
                    else
                    {
                        if (!String.IsNullOrEmpty(item.ItemValorMax))
                        {
                            textbox.Text += item.Etiqueta + " " + item.Valor + " " + item.ItemValorMax + Environment.NewLine;
                        }
                        else
                        {
                            textbox.Text += item.Etiqueta + " " + item.Valor + Environment.NewLine;
                        }
                    }
                });
            }
        }
    }
}
