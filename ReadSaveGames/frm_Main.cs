using System;
using System.Collections.Generic;
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

        List<string> ListaObjectivesEncontrados = new List<string>();

        private void btn_cargarArchivo_Click(object sender, EventArgs e)
        {
            if (ofd_partida.ShowDialog() == DialogResult.OK)
            {
                //Obtener la ruta al archivo de la partida guadada
                txtb_rutaPartida.Text = ofd_partida.FileName;

                LeerPartida();

                string[] Nombres;
                foreach(var item in ListaObjectivesEncontrados)
                {
                    Nombres = item.Split(',');
                    rtbx_partidaBinario.Text += "Hashcode: " + Nombres[0] + " Valor: " + Nombres[1] + Environment.NewLine;
                }
            }
        }

        public void LeerPartida()
        {
            BinaryReaderBigEndian Lector = new BinaryReaderBigEndian(new FileStream(txtb_rutaPartida.Text, FileMode.Open, FileAccess.Read));

            // Variables:
            int NumeroObjectives = 1700;
            int count = 0;
            bool EsHashcode = false;
            string NombreHashcode = null;

            Lector.BaseStream.Seek(unchecked((int)0xEC), SeekOrigin.Begin);

            while (count < NumeroObjectives)
            {
                uint y = SwapBytes(Lector.ReadUInt32());

                if (y.ToString("X4").StartsWith("42"))
                {
                    EsHashcode = true;
                    NombreHashcode = y.ToString("X4");
                }
                else
                {
                    if (EsHashcode)
                    {
                        ListaObjectivesEncontrados.Add(NombreHashcode + "," + int.Parse(y.ToString("X4"), System.Globalization.NumberStyles.HexNumber));
                        EsHashcode = false;
                    }
                }

                if (y.ToString("X4").Equals("55555555"))
                {
                    break;
                }
                else
                {
                    count++;
                }
            }
            Lector.Close();

            rtbx_partidaBinario.Text += "Hashcodes encontrados: " + ListaObjectivesEncontrados.Count + Environment.NewLine;
        }
        public uint SwapBytes(uint x)
        {
            // swap adjacent 16-bit blocks
            x = (x >> 16) | (x << 16);
            // swap adjacent 8-bit blocks
            return ((x & 0xFF00FF00) >> 8) | ((x & 0x00FF00FF) << 8);
        }

        public static string InvertirString(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
