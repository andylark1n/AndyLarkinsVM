using System;
using System.IO;
using System.Windows.Forms;
using KVM;
using dnlib.DotNet;


namespace AndyLarkinsProtector
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        internal static string filepath;

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(filepath) || String.IsNullOrWhiteSpace(filepath))
            {
                MessageBox.Show("Enter valid path!", "Error!");
            }
            else
            {
                if (File.Exists(filepath))
                {
                    {
                        try
                        {
                            new KVMTask().Exceute(ModuleDefMD.Load(filepath), filepath+"_P.exe", "", null);
                            MessageBox.Show("Done!");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString(), "Error!");
                        }
                    }
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("File doesn't exist!", "Error!");
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            filepath = textBox1.Text;
        }
        private void textBox1DragEnter(object sender, DragEventArgs e)
        {
            {
                if (e.Data.GetDataPresent(DataFormats.FileDrop))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            }
            
        }
        private void textBox1DragDrop(object sender, DragEventArgs e)
        {
            {
                try
                {
                    Array a = (Array)e.Data.GetData(DataFormats.FileDrop);
                    if (a != null)
                    {
                        string s = a.GetValue(0).ToString();
                        int lastoffsetpoint = s.LastIndexOf(".");
                        if (lastoffsetpoint != -1)
                        {
                            string Extension = s.Substring(lastoffsetpoint);
                            Extension = Extension.ToLower();
                            if (Extension == ".exe" || Extension == ".dll")
                            {
                                this.Activate();
                                textBox1.Text = s;
                                int lastslash = s.LastIndexOf("\\");
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

    }
}
