using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using system;
using System.Security.Cryptography;

namespace EncryptFile
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSelFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выберите Файл для Шифровки или расшифровки", "Выберите Файл для Шифровки или расшифровки");
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtFilePath.Text = openFile.FileName;
                MessageBox.Show("Выбран Файл для Шифровки или расшифровки", "Выбран Файл для Шифровки или расшифровки");
            }
        }
dfdfdff
        private void btnEncFile_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Выберите Файл Ключа", "Выберите Файл Ключа");
            if (saveKeyFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Выбран Файл Ключа", "Выбран Файл Ключа");
                MessageBox.Show("Выберите Файл для записи шифра", "Выберите Файл для записи шифра");
                if (saveEncFile.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Выбран Файл для записи шифра", "Выбран Файл для записи шифра");
                    FileStream fsFileOut = File.Create(saveEncFile.FileName);
                        TripleDESCryptoServiceProvider cryptAlgorithm = new TripleDESCryptoServiceProvider(); 
                        CryptoStream csEncrypt = new CryptoStream(fsFileOut, cryptAlgorithm.CreateEncryptor(), CryptoStreamMode.Write);
                        StreamWriter swEncStream = new StreamWriter(csEncrypt, System.Text.Encoding.GetEncoding(1251));
                        StreamReader srFile = new StreamReader(txtFilePath.Text, System.Text.Encoding.GetEncoding(1251));
                    string currLine = srFile.ReadLine();
                    while (currLine != null)
                    {
                        swEncStream.Write(currLine);
                        currLine = srFile.ReadLine();
                    }
                    srFile.Close();
                    swEncStream.Flush();
                    swEncStream.Close();
                    FileStream fsFileKey = File.Create(saveKeyFile.FileName);
                    BinaryWriter bwFile = new BinaryWriter(fsFileKey);
                    bwFile.Write(cryptAlgorithm.Key);
                    bwFile.Write(cryptAlgorithm.IV);
                    bwFile.Flush();
                    bwFile.Close();
                    MessageBox.Show("Зашифрованно", "Зашифрованно");
                }
            }
        }

        private void btnDecFile_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Выберите Файл Ключа", "Выберите Файл Ключа");
            if (openKeyFile.ShowDialog() == DialogResult.OK)
            {
                MessageBox.Show("Выбран Файл Ключа", "Выбран Файл Ключа");
                MessageBox.Show("Выберте Файл для записи расшифрованного текста", "Выберте Файл для записи расшифрованного текста");
                if (saveDecFile.ShowDialog() == DialogResult.OK)
                {
                    MessageBox.Show("Выбран Файл для записи расшифрованного текста", "Выбран Файл для записи расшифрованного текста");
                    
                    FileStream fsFileIn = File.OpenRead(txtFilePath.Text);
                    FileStream fsKeyFile = File.OpenRead(openKeyFile.FileName);
                    FileStream fsFileOut = File.Create(saveDecFile.FileName);
                    TripleDESCryptoServiceProvider cryptAlgorithm = new TripleDESCryptoServiceProvider();
                    BinaryReader brFile = new BinaryReader(fsKeyFile);
                    cryptAlgorithm.Key = brFile.ReadBytes(24);
                    cryptAlgorithm.IV = brFile.ReadBytes(8);
                    CryptoStream csEncrypt = new CryptoStream(fsFileIn, cryptAlgorithm.CreateDecryptor(), CryptoStreamMode.Read);
                    StreamReader srCleanStream = new StreamReader(csEncrypt, System.Text.Encoding.GetEncoding(1251));
                    StreamWriter swCleanStream = new StreamWriter(fsFileOut, System.Text.Encoding.GetEncoding(1251));
                    swCleanStream.Write(srCleanStream.ReadToEnd());
                    swCleanStream.Close();
                    fsFileOut.Close();
                    srCleanStream.Close();
                    MessageBox.Show("Расшифрованно", "Расшифрованно");
                }
            }
        }
    }
        }
    
