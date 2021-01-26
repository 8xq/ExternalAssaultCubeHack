using Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace AssaultCube_trainer_v1
{
    public partial class Form1 : Form
    {
        public Mem memLib = new Mem();
        bool ProcessStatus = false;

        public Form1()
        {
            InitializeComponent();
        }
        private void BGthread_DoWork(object sender, DoWorkEventArgs e)
        {
            for(; ;)
            {
                if (memLib.OpenProcess(Offsets.ProcessName) ==  true)
                {
                    ProcessStatus = true;
                    Thread.Sleep(1000);
                }
                else
                {
                    Thread.Sleep(1000);
                    return;
                }
                BGthread.ReportProgress(1);
            }
        }

        private void BGthread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label9.Text = ProcessStatus.ToString();
            label13.Text = Offsets.ProcessName;
        }

        private void BGthread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BGthread.RunWorkerAsync();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            BGthread.RunWorkerAsync();
        }
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while (ProcessStatus == true)
                {
                    if (checkBox1.Checked == true)
                    {
                        memLib.WriteMemory(Offsets.BaseAddress + "," + Offsets.DualPistolOffset, "long", "1"); // Enable dual pistols (1 = on)
                    }
                    else if (checkBox1.Checked == false)
                    {
                        memLib.WriteMemory(Offsets.BaseAddress + "," + Offsets.DualPistolOffset, "long", "0"); // Disable dual pistols (0 = off)
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void thirteenButton1_Click(object sender, EventArgs e)
        {
            string selectedWeapon = thirteenComboBox1.Text;
            string ammoWeapon = thirteenTextBox6.Text;
            if (string.IsNullOrEmpty(selectedWeapon))
            {
                MessageBox.Show("Please select a weapon from the dropdown !", "Error");
            }
            else
            {
                if (string.IsNullOrEmpty(ammoWeapon))
                {
                    MessageBox.Show("Please enter how much ammo you would like to add !", "Error");
                }
                else
                {
                    switch (selectedWeapon.ToLower())
                    {
                        case "assault rifle":
                            memLib.WriteMemory("base+" + Offsets.ARammo, "long", ammoWeapon); // Change AR ammo 
                            break;
                        case "sniper rifle":
                            memLib.WriteMemory("base+" + Offsets.SRammo, "long", ammoWeapon); // Change SR ammo 
                            break;
                        case "submachine gun":
                            memLib.WriteMemory("base+" + Offsets.SMGammo, "long", ammoWeapon); // Change SMG ammo 
                            break;
                        case "shotgun":
                            memLib.WriteMemory("base+" + Offsets.SGammo, "long", ammoWeapon); // Change SG ammo 
                            break;
                        case "dual pistols":
                            memLib.WriteMemory(Offsets.DPammo, "long", ammoWeapon); // Change DP ammo 
                            break;
                        default:
                            MessageBox.Show("Error with weapon selection !", "Error");
                            break;

                    }
                }
            }
        }

        private void thirteenButton5_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while (ProcessStatus == true)
                {
                    if (Convert.ToInt32(IGkills.Text) > 0)
                    {
                        memLib.WriteMemory("base+" + Offsets.FragKills, "long", IGkills.Text); // Ingame kills
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void thirteenButton2_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while (ProcessStatus == true)
                {
                    if(Convert.ToInt32(HealthTXT.Text) > 0)
                    {
                        memLib.WriteMemory("base+"+Offsets.Health, "long", HealthTXT.Text); // Health
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void thirteenButton3_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while (ProcessStatus == true)
                {
                    if (Convert.ToInt32(ArmourTXT.Text) > 0)
                    {
                        memLib.WriteMemory("base+" + Offsets.Armour, "long", ArmourTXT.Text); // Armour
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void thirteenButton4_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while (ProcessStatus == true)
                {
                    if (Convert.ToInt32(GrenadeTXT.Text) > 0)
                    {
                        memLib.WriteMemory("base+" + Offsets.Grenades, "long", GrenadeTXT.Text); // Grandes
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void thirteenButton6_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                while (ProcessStatus == true)
                {
                    if (Convert.ToInt32(IGdeaths.Text) > 0)
                    {
                        memLib.WriteMemory("base+" + Offsets.Deaths, "long", IGdeaths.Text); // Ingame deaths
                    }
                    Thread.Sleep(1000);
                }
            });
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                if (checkBox2.Checked == true)
                {
                    memLib.WriteMemory(Offsets.Recoil, "long", "0"); // Remove bullet recoil from gun
                }
                else
                {
                    memLib.WriteMemory(Offsets.Recoil, "long", "981668463"); // Appears to set recoil to default value
                }
                Thread.Sleep(1000);
            });
            
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
               if(checkBox3.Checked == true)
               {
                 memLib.WriteMemory("0x50F20C", "long", "0"); // Remove crosshair
               }
               else
               {
                 memLib.WriteMemory("0x50F20C", "long", "15"); // Redraw crosshair
               }
               Thread.Sleep(1000);
            });
        }
    }
}
