using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_Nhom
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        XuLyDuLieu xldl = new XuLyDuLieu();
        private void mnuThoat_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có muốn thoát khỏi chương trình không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                xldl.disconnect();
                Application.Exit();              
            }
                
        }

        private void mnuDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có muốn đăng xuất khỏi chương trình không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Hide();
                frmLogin f = new frmLogin();
                f.ShowDialog();
            }
        }

        private void mnuKhachHang_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDMKH f = new frmDMKH();
            f.ShowDialog();
        }

        private void mnuNhanVien_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmDanhMucNV fnv = new frmDanhMucNV();
            fnv.ShowDialog();
        }

        private void mnuHoaDonBan_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHoaDonBan fhdb = new frmHoaDonBan();
            fhdb.ShowDialog();
        }

        private void mnuSanPham_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSanPham fsp = new frmSanPham();
            fsp.ShowDialog();
        }

        private void mnuHangSX_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmHangSX fhsx = new frmHangSX();
            fhsx.ShowDialog();

        }

        private void mnuHangTon_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSanPham fsp = new frmSanPham();
            fsp.ShowDialog();
        }

        private void mnuDoanhThu_Click(object sender, EventArgs e)
        {
            frmHoaDonBan fhdb = new frmHoaDonBan();
            MessageBox.Show("Doanh Thu: " + fhdb.doanhthu());
        }
    }
}
