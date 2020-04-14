using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SphinxSavedGameReader
{
    class FuncionesConversion
    {
        public void EtiquetarObjectivesEncontrados(List<Objectives> ListaObjectivesEncontrados, List<Hashcodes> ArchivoHashcodes, List<ArchivoTXT> ArchivoFinal)
        {
            string ObjectiveHex, ObjectiveEtiqueta; 

            foreach (Objectives ObjectiveEncontrado in ListaObjectivesEncontrados)
            {
                ObjectiveHex = "0x" + ObjectiveEncontrado.ObjectiveNumHex;
                ObjectiveEtiqueta = ObtenerEtiquetaValorHex(ObjectiveHex, ArchivoHashcodes);

                if (!String.IsNullOrEmpty(ObjectiveEtiqueta))
                {
                    ArchivoFinal.Add(new ArchivoTXT { Etiqueta = ObjectiveEtiqueta, Valor = ObjectiveEncontrado.ValorObjective, ItemValorMax = "" });
                }
            }
        }

        public void EtiquetarInventarioEncontrado(List<InventoryItems> ListaInventarioEncontrado, List<Hashcodes> ArchivoHashcodes, List<ArchivoTXT> ArchivoFinal)
        {
            string InventarioHex, InventarioEtiqueta;

            foreach (InventoryItems ItemEncontrado in ListaInventarioEncontrado)
            {
                InventarioHex = "0x" + ItemEncontrado.ItemNumeroHex;
                InventarioEtiqueta = ObtenerEtiquetaValorHex(InventarioHex, ArchivoHashcodes);

                if (!String.IsNullOrEmpty(InventarioEtiqueta))
                {
                    ArchivoFinal.Add(new ArchivoTXT { Etiqueta = InventarioEtiqueta, Valor = ItemEncontrado.ItemMinValue, ItemValorMax = ItemEncontrado.ItemMaxValue });
                }
            }
        }

        private string ObtenerEtiquetaValorHex(string valorHex, List<Hashcodes> ArchivoHashcodes)
        {
            string Etiqueta = null;

            foreach (Hashcodes item in ArchivoHashcodes)
            {
                if (item.ValorHex.Equals(valorHex, StringComparison.OrdinalIgnoreCase))
                {
                    Etiqueta = item.Etiqueta;
                }
            }

            if (String.IsNullOrEmpty(Etiqueta))
            {
                MessageBox.Show("The hashcode: \"" + valorHex + "\"has not found in hashcodes.h", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return Etiqueta;
        }
    }
}
