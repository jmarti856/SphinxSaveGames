using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SphinxSavedGameReader
{
    class FuncionesEscritura
    {
        public void EscribirArchivo(string Ruta, List<ArchivoTXT> ArchivoFinal, CheckBox FormatoGamescript)
        {
            string Linea;

            TextWriter Escritor = new StreamWriter(Ruta);

            try
            {
                foreach (ArchivoTXT item in ArchivoFinal)
                {
                    if (item.Etiqueta.StartsWith("HT_Objective"))
                    {
                        if (FormatoGamescript.Checked)
                        {
                            Linea = "SetObjective " + item.Etiqueta + " " + item.Valor;
                        }
                        else
                        {
                            Linea = item.Etiqueta + " " + item.Valor;
                        }

                        Escritor.WriteLine(Linea);
                    }
                    else if (item.Etiqueta.StartsWith("HT_Item"))
                    {
                        if (FormatoGamescript.Checked)
                        {
                            Escritor.WriteLine("InventorySet " + item.Etiqueta + " " + item.Valor);

                            if (item.Etiqueta.Equals("HT_Item_Pickup_BronzeScarab") || item.Etiqueta.Equals("HT_Item_Weapon_IceDart") ||
                            item.Etiqueta.Equals("HT_Item_Pickup_MummyMoney") || item.Etiqueta.Equals("HT_Item_Weapon_AcidDart") ||
                            item.Etiqueta.Equals("HT_Item_Weapon_FireDart"))
                            {
                                Escritor.WriteLine("InventoryMaxSet " + item.Etiqueta + " " + item.ItemValorMax);
                            }
                        }
                        else
                        {
                            Linea = item.Etiqueta + " " + item.Valor + " " + item.ItemValorMax;
                            Escritor.WriteLine(Linea);
                        }
                    }
                }
                Escritor.Close();

                MessageBox.Show("File saved successfully.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("An error occurred while writing the file.", "Error saving", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
