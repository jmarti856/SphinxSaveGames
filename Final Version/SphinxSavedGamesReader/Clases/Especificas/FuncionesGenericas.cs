using System.IO;
using System.Windows.Forms;

namespace SphinxSavedGameReader
{
    class FuncionesGenericas
    {
        public string ComprobarRutaArchivoHashcodes(CheckBox chbx_hashcodesX)
        {
            string ruta = "";

            if (chbx_hashcodesX.Checked)
            {
                if (File.Exists(@"S:\Sphinx\Project\hashcodes.h"))
                {
                    ruta = @"S:\Sphinx\Project\hashcodes.h";
                }
                else
                {
                    MessageBox.Show("File \"S:\\Sphinx\\Project\\hashcodes.h\" not found. Select a hashcodes file.", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                if (File.Exists(Application.StartupPath + "//hashcodes.h"))
                {
                    ruta = Application.StartupPath + "//hashcodes.h";
                }
                else
                {
                    MessageBox.Show("Hashcodes file not found", "Caution", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return ruta;
        }
    }
}
