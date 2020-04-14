using System.IO;
using System.Windows.Forms;

namespace SphinxSavedGameReader
{
    class FuncionesGenericas
    {
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
    }
}
