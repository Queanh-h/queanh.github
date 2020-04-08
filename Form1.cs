using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QuanLyKhachSan
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection();

        public Form1()
        {
            InitializeComponent();
        }
        public void KhoaControl()
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string connectionString = @"Data Source =DESKTOP-SVLIFR8\SQLEXPRESS; Initial Catalog= QuanLyKhachSan;Itegrated Security=True";
            con.ConnectionString = connectionString;
            con.Open();
            loadDataGridView();

        }
        private void loadDataGridView()
        {
            string sql = "Select * FROM tblPhong";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tableQuanLyKhachSan = new DataTable();
            adp.Fill(tableQuanLyKhachSan);
            dataGridView1.DataSource = tableQuanLyKhachSan;
        }
        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaPhong.Text = dataGridView1.CurrentRow.Cells["Maphong"].Value.ToString();
            txtTenPhong.Text= dataGridView1.CurrentRow.Cells["Tenphong"].Value.ToString();
            txtDonGia.Text= dataGridView1.CurrentRow.Cells["DonGia"].Value.ToString();
            txtMaPhong.Enabled = true;
            txtTenPhong.Enabled = true;
            txtDonGia.Enabled = true;

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaPhong.Text = "";
            txtTenPhong.Text = "";
            txtDonGia.Text = "";
            txtMaPhong.Enabled = true;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql = "Delete From tblPhong where Maphong = '" + txtMaPhong.Text + "'";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();

            sql = "Select * From tblPhong";
            SqlDataAdapter adp = new SqlDataAdapter(sql, con);
            DataTable tableQuanLyKhachSan = new DataTable();
            adp.Fill(tableQuanLyKhachSan);
            dataGridView1.DataSource = tableQuanLyKhachSan;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaPhong.Text=="")
            {
                MessageBox.Show("Ban can nhap ma phong");
                txtMaPhong.Focus();
                return;
            }
            if (txtTenPhong.Text=="")
            {
                MessageBox.Show("Ban can nhap ten phong");
                txtTenPhong.Focus();
            }
            else
            {
                string sql = "insert into tblPhong values( '" + txtMaPhong.Text +"','" + txtTenPhong.Text +"') ";
                if (txtDonGia.Text != "")
                    sql = sql + "," + txtDonGia.Text.Trim();
                sql = sql + ")";
                MessageBox.Show(sql);
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    loadDataGridView();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                return;
                              

            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            btnHuy.Enabled = true;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = true;
            txtMaPhong.Enabled = false;

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            con.Close();
            this.Close();

        }
    }
}
