using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace ChocoHelper
{
    public partial class ChocoHelper : Form
    {
        Boolean is64bit;
        String AppPath;
        String dump;

        public ChocoHelper()
        {
            InitializeComponent();
        }

        private void ChocoHelper_Load(object sender, EventArgs e)
        {
            this.is64bit = (IntPtr.Size == 8 ? true : false);
            this.AppPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            this.dump = this.AppPath + @"\dump.tmp";
            lblPCName.Text = String.Concat("Computer name: ", System.Environment.MachineName);
        }

        private void btnFindUUID_Click(object sender, EventArgs e)
        {
            btnFindUUID.Enabled = false;
            if (this.findKakaoProcess())
            {
                this.deleteDump();

                textUUID.Text = "Please wait.. [D]";
                String dumpCommand = Path.Combine(this.AppPath, this.is64bit ? "x64" : "x86", "userdump.exe");
                String dumpArgument = "KakaoTalk.exe \"" + this.dump + "\"";
                ExecuteCommandSync(dumpCommand, dumpArgument);

                if (this.checkDump())
                {
                    textUUID.Text = "Please wait.. [F]";
                    string uuid = this.getUUID();
                    this.deleteDump();
                    if (uuid == "")
                    {
                        MessageBox.Show("Failed to get UUID from KakaoTalk.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    btnFindUUID.Text = "BASE64:";
                    textUUID.Text = uuid;
                }
                else
                {
                    MessageBox.Show("Failed to dump KakaoTalk process.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Failed to find KakaoTalk process.\nPlease run KakaoTalk, then hit the button again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string getUUID()
        {
            int size = (int)getDumpSize();
            int uuidPos = 0;
            int uuidSize = 0;

            Byte[] data = new byte[size];
            Byte[] find = new byte[] { 0x00, 0x64, 0x65, 0x76, 0x69, 0x63, 0x65, 0x5F, 0x75, 0x75, 0x69, 0x64, 0x00 };
            Byte[] wrongContent = new byte[] { 0x6C, 0x61, 0x6E };
            Byte[] last = new byte[] { 0x00, 0x00 };
            StringBuilder uuid = new StringBuilder();
            StreamReader reader = new StreamReader(this.dump);
            Stream stream = reader.BaseStream;

            using (BinaryReader s = new BinaryReader(stream, Encoding.ASCII))
            {
                data = s.ReadBytes(size);
                for (int i = 0; i < size; i ++)
                {
                    if(compareBytes(data, find, i))
                    {
                        uuidPos = i + find.Length;

                        if (compareBytes(data, wrongContent, uuidPos)) continue;
                        for (int j = uuidPos; !compareBytes(data, last, j); j++)
                        {
                            uuid.Append(ConvertHexToString(data[j].ToString("x")));
                            uuidSize++;
                        }

                        break;
                    }
                }

                s.Close();
            }

            stream.Close();
            reader.Close();
            return uuid.ToString();
        }

        private bool compareBytes(byte[] a, byte[] b, int seek)
        {
            bool match = false;
            for (int i = 0; i < b.Length; i++)
            {
                if (i > 0 && !match)
                {
                    break;
                }
                if (a[seek + i] == b[i])
                {
                    match = true;
                }
                else
                {
                    match = false;
                }
            }

            return match;
        }

        public string ConvertHexToString(string HexValue)
        {
            if (HexValue == "0") HexValue = "00";
            string StrValue = "";
            while (HexValue.Length > 0)
            {
                StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
                HexValue = HexValue.Substring(2, HexValue.Length - 2);
            }
            return StrValue;
        }

        private bool findKakaoProcess()
        {
            Process[] procList = Process.GetProcessesByName("KakaoTalk");
            if (procList.Length > 0)
            {
                return true;
            }
            return false;
        }

        private bool checkDump()
        {
            return File.Exists(this.dump);
        }

        private void deleteDump()
        {
            if(checkDump())
                File.Delete(this.dump);
        }

        private long getDumpSize()
        {
            return new FileInfo(this.dump).Length;
        }

        public void ExecuteCommandSync(string command, string args)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            startInfo.Arguments = args;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;
            Process.Start(startInfo).WaitForExit();
        }
    }
}
