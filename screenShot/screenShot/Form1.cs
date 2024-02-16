using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace screenShot
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog saveDialog = new SaveFileDialog())
            {
                saveDialog.Filter = "PNG Dosyaları|*.png|JPEG Dosyaları|*.jpeg|Tüm Dosyalar|*.*";
                saveDialog.Title = "Ekran Görüntüsünü Kaydet";
                saveDialog.FileName = "screenshot.png";

                // Kullanıcı dosya seçtiyse
                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    string dosyaYolu = saveDialog.FileName;

                    Form2 form2 = new Form2(); // Form2'nin bir örneğini oluşturun
                    form2.Show(); // Form2'yi gösterin

                    // Biraz bekleyin, formun tamamen açılmasını sağlayın
                    form2.Activate();
                    form2.Refresh();
                    System.Threading.Thread.Sleep(500); // İstediğiniz kadar bekleyebilirsiniz, burada 1 saniye bekletiliyor

                    CaptureAndSaveScreenshot(form2, dosyaYolu);
                    form2.Close();
                }
            }
        }

        public void CaptureAndSaveScreenshot(Form form, string filePath)
        {
            Bitmap screenshot = new Bitmap(form.Width, form.Height);

            using (Graphics g = Graphics.FromImage(screenshot))
            {
                g.CopyFromScreen(form.PointToScreen(Point.Empty), Point.Empty, form.Size);
            }

            screenshot.Save(filePath);

            screenshot.Dispose();
        }
    }
}
