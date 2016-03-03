using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;

namespace DropInspector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void Form1_DragOver(object sender, DragEventArgs e)
        {
            this.Text = e.ToString();
            string[] c=e.Data.GetFormats(true);
            string[] c2 = e.Data.GetFormats();

            /*
            FieldInfo info = e.Data.GetType().GetField("innerData", BindingFlags.NonPublic | BindingFlags.Instance);
            object obj = info.GetValue(e.Data);
            info = obj.GetType().GetField("innerData", BindingFlags.NonPublic | BindingFlags.Instance);
            DataObject dataObj = info.GetValue(obj) as DataObject;
            */

            this.Text = " : ) ";
            
        }
        
    }
}
