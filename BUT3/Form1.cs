using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BUT3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SqlConnection connection;
        private string connectionString = "Data Source=MONSTER-HUMA;Initial Catalog=db_butunleme;Integrated Security=True";

        private void buttonSorgula_Click(object sender, EventArgs e)
        {
            string No = textBox1.Text.Trim(); // textbox1'den sipariş numarasını istedim.

            if (string.IsNullOrEmpty(No))
            {
                MessageBox.Show("Lütfen sipariş numarasını girdikten sonra sorgulayınız.");
                return;
            }
            string query = "select * from tblSiparisler where SiparisNo = @p1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@p1", No);

            try
            {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    dr.Read();
                    string Bilgi = $"Sipariş Numarası: {dr["SiparisNo"]}\n" +
                                       $"Müşteri Adı: {dr["Musteri"]}\n" +
                                       $"Teslimat Adresi: {dr["Adres"]}\n" +
                                       $"Tarih: {dr["Tarih"]}\n" +
                                       $"Durum: {dr["Durum"]}";

                    richTextBox1.Text = Bilgi; //richTextBox1'e sorguyu yazdırdım.
                }
                else
                {
                    MessageBox.Show("Bu sipariş numarasına göre bir sipariş bulunamadı.","Bulunamadı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

                dr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorgunuz hatalı !!!: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection = new SqlConnection(connectionString);
        }
    }
}
