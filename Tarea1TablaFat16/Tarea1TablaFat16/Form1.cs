using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tarea1TablaFat16
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MBR tabla = new MBR();
            tabla.executable_code = 1;
            Partition part = new Partition();
            
        }

        public bool escribirTabla(MBR tablaFAT)
        {
            try
            {
                string serializationFile = Path.Combine("MBRfile.ea");
                //serialize
                using (Stream stream = File.Open(serializationFile, FileMode.Create))
                {
                    var bformatter = new BinaryFormatter();
                    bformatter.Serialize(stream, tablaFAT);
                }
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        public MBR leerTabla()
        {
            MBR result = new MBR();
            string serializationFile = Path.Combine("MBRfile.ea");
            //deserialize
            using (Stream stream = File.Open(serializationFile, FileMode.Open))
            {
                var bformatter = new BinaryFormatter();

                result = (MBR)bformatter.Deserialize(stream);
            }
            return result;
        }
    }
    [Serializable]
    public class MBR
    {
        public int executable_code { get; set; }
        public Partition firstpartition { get; set; }
        public Partition secondpartition { get; set; }
        public Partition thirdpartition { get; set; }
        public Partition fourthpartition { get; set; }
        public char executable_marker { get; set; }
    }

    [Serializable]
    public class Partition
    {
        public byte state { get; set; }
        public byte headBegin { get; set; }
        public char cilinderSectorBegin { get; set; }
        public byte type { get; set; }
        public byte headEnd { get; set; }
        public char cilinderSectorEnd { get; set; }
        public int offset { get; set; }
        public int sectors { get; set; }
    }
}
