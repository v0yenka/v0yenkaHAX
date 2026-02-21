using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;

namespace SSO_Library_Test
{
    public partial class Form1 : Form
    {
        // Creating 'engine' (library instance)
        Mem mem = new Mem();

        // Saving the address which will be replaced
        private uint _savedAddress = 0;

        // Button in the HEX format, found thanks to AoB scanning
        private string originalHex = "67 6C 6F 62 61 6C 2F 4D 61 70 57 69 6E 64 6F 77 2E 53 74 61 72 74 28 29 3B";

        public Form1()
        {
            InitializeComponent();
            SetupGUI();
        }

        private async void BtnHack_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "Checking the game process...";

            if (!mem.OpenProcess("PXStudioRuntimeMMO"))
            {
                // Alternative name validation
                if (!mem.OpenProcess("SSOClient"))
                {
                    MessageBox.Show("Game not found.");
                    return;
                }
            }

            // Scanning in case the address is not saved yet (first time or after restore)
            if (_savedAddress == 0)
            {
                StatusLabel.Text = "Scanning memory in process...";

                var results = await mem.AoBScan(originalHex);
                _savedAddress = results.FirstOrDefault();
            }

            // Injecting
            if (_savedAddress != 0)
            {
                string myScript = TxtScript.Text;

                mem.WriteMemory(_savedAddress, myScript);

                StatusLabel.Text = $"COMPLETED (0x{_savedAddress:X})";
                StatusLabel.ForeColor = Color.Green;
                MessageBox.Show("You're all set ;3");
            }
            else
            {
                StatusLabel.Text = "Haven't found the HEX in game memory";
                StatusLabel.ForeColor = Color.Red;
                MessageBox.Show("Try 'Fix the map' button");
            }
        }

        // Restoring button previous functionality
        private void BtnRestore_Click(object sender, EventArgs e)
        {
            if (_savedAddress == 0)
            {
                

            }

            // Converting the original HEX string to byte array
            string hexClean = originalHex.Replace(" ", "");
            byte[] originalBytes = new byte[hexClean.Length / 2];
            for (int i = 0; i < originalBytes.Length; i++)
            {
                string byteStr = hexClean.Substring(i * 2, 2);
                originalBytes[i] = Convert.ToByte(byteStr, 16);
            }

            mem.WriteBytes(_savedAddress, originalBytes);

            StatusLabel.Text = "Successfully restored the button functionality";
            StatusLabel.ForeColor = Color.Blue;
            MessageBox.Show("Fixed!");
        }

        // GUI (will be upgraded)
        Label StatusLabel;
        Button BtnHack;
        Button BtnRestore;
        TextBox TxtScript;

        private void SetupGUI()
        {
            Color bgColor = Color.FromArgb(13, 2, 8);
            Color neonPink = Color.FromArgb(255, 0, 255);
            Color textColor = Color.White;

            this.Text = "★v0yenka hax★";
            this.Size = new System.Drawing.Size(420, 500);
            this.BackColor = bgColor;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            Label lbl = new Label()
            {
                Text = ">> SCRIPT INJECTOR",
                Top = 15,
                Left = 20,
                Width = 300,
                ForeColor = neonPink,
                Font = new Font("Consolas", 15, FontStyle.Bold)
            };
            this.Controls.Add(lbl);

            TxtScript = new TextBox()
            {
                Top = 45,
                Left = 20,
                Width = 360,
                Height = 120,
                Multiline = true,
                BackColor = Color.FromArgb(25, 25, 25),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Font = new Font("Consolas", 10)
            };
            TxtScript.Text = "global/Horse.AddRelativeForce(0,1,2.5f);"; // Just a default script for testing
            this.Controls.Add(TxtScript);

            BtnHack = new Button()
            {
                Text = "EXECUTE",
                Top = 180,
                Left = 20,
                Width = 360,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                BackColor = bgColor,
                ForeColor = neonPink,
                Font = new Font("Consolas", 12, FontStyle.Bold)
            };
            BtnHack.FlatAppearance.BorderColor = neonPink;
            BtnHack.FlatAppearance.BorderSize = 2;
            BtnHack.Click += BtnHack_Click;
            this.Controls.Add(BtnHack);

            BtnRestore = new Button()
            {
                Text = "RESTORE ORIGINAL",
                Top = 255,
                Left = 20,
                Width = 360,
                Height = 60,
                FlatStyle = FlatStyle.Flat,
                BackColor = bgColor,
                ForeColor = neonPink,
                Font = new Font("Consolas", 12, FontStyle.Bold)
            };
            BtnRestore.FlatAppearance.BorderColor = neonPink;
            BtnRestore.FlatAppearance.BorderSize = 2;
            BtnRestore.Click += BtnRestore_Click;
            this.Controls.Add(BtnRestore);
        }
    }
}
