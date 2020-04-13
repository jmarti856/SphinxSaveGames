using System;
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

        private void btn_cargarArchivo_Click(object sender, EventArgs e)
        {
            if (ofd_partida.ShowDialog() == DialogResult.OK)
            {
                //Obtener la ruta al archivo de la partida guadada
                txtb_rutaPartida.Text = ofd_partida.FileName;

                BinaryReaderBigEndian Lector = new BinaryReaderBigEndian(new FileStream(txtb_rutaPartida.Text, FileMode.Open, FileAccess.Read));

                // Variables:
                int length = (int)Lector.BaseStream.Length;
                int PosicionInicio = unchecked((int)0xEC);
                int NumeroObjectives = 1700;
                int count = 0;

                Lector.BaseStream.Seek(PosicionInicio, SeekOrigin.Begin);

                while (PosicionInicio < length && count < NumeroObjectives)
                {
                    //Obtener el 
                    uint y = SwapBytes(Lector.ReadUInt32());
                    MessageBox.Show(y.ToString("X4"));
                    PosicionInicio++;
                    count++;
                }
            }
        }

        public uint SwapBytes(uint x)
        {
            // swap adjacent 16-bit blocks
            x = (x >> 16) | (x << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
        }
    }
}
