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
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
        }
        //khai báo lớp xử lý dữ liệu
        XuLyDuLieu xldl = new XuLyDuLieu();

        //sự kiện load form
        private void frmSanPham_Load(object sender, EventArgs e)
        {
            txtMaSP.Enabled = true;
            btnBoQua.Enabled = false;
            dgvSanPham.DataSource = xldl.getDataFromSanPham();
            fillCombobox();
        }
        //đẩy cơ sở dữ liệu vào combobox
        private void fillCombobox()
        {
            cbHangSX.DataSource = xldl.getDataFromHangSX();
            cbHangSX.DisplayMember = "TenHang";
            cbHangSX.ValueMember = "MaHangSX";
        }

        //đóng form hien tại quay về form main
        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dr;
            dr = MessageBox.Show("Bạn có muốn đóng chương trình không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                this.Hide();
                frmMain fm = new frmMain();
                fm.ShowDialog();
            }
        }

        //bỏ qua sản phẩm vùa chọn  
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            resetValue();
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnBoQua.Enabled = false;
            txtMaSP.Enabled = true;
            fillCombobox();
        }

        //xóa dữ liệu trong các ô textbox và combobõ quay về trạng thái ban đầu
        private void resetValue()
        {
            txtMaSP.Clear();
            txtTenSP.Clear();
            txtMoTa.Clear();
            txtSoLuong.Clear();
            cbHangSX.Text = "";
            txtGiaNhap.Clear();
            txtGiaBan.Clear();
            cbPhanLoai.Text = "";

        }

        //them sản phẩm
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                dr = MessageBox.Show("Bạn có muốn thêm sản phẩm không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string MaSP = string.Empty;
                    string tenSP = string.Empty;
                    string MoTa = string.Empty;
                    string MaHangSX = string.Empty;
                    string PhanLoai = string.Empty;
                    int SoLuong = 0;
                    int GiaNhap = 0;
                    int GiaBan = 0;
                    if (txtMaSP.Text != string.Empty && txtTenSP.Text != string.Empty && txtMoTa.Text != string.Empty && cbHangSX.Text != string.Empty && cbPhanLoai.Text != string.Empty && txtGiaNhap.Text != string.Empty && txtGiaBan.Text != string.Empty)
                    {
                        MaSP = txtMaSP.Text;
                        tenSP = txtTenSP.Text;
                        MoTa = txtMoTa.Text;
                        MaHangSX = cbHangSX.SelectedValue.ToString();
                        PhanLoai = cbPhanLoai.Text;
                        SoLuong = int.Parse(txtSoLuong.Text);
                        GiaNhap = int.Parse(txtGiaNhap.Text);
                        GiaBan = int.Parse(txtGiaBan.Text);
                        if (xldl.insertSanPham(MaSP, tenSP, MoTa, MaHangSX, PhanLoai, SoLuong, GiaNhap, GiaBan) == 1)
                        {
                            MessageBox.Show("Thêm sản phẩm thành công!!");
                            dgvSanPham.DataSource = xldl.getDataFromSanPham();
                            resetValue();
                        }
                        else
                        {
                            MessageBox.Show("Thêm sản phẩm thất bại!!");
                            resetValue();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải điền đầy đủ thông tin!!!");
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi them du lieu!!!" + ex.Message);
            }
        }

        //khi click vào sản phẩm trong datagridview thì sẽ đưa dữ liệu của sản phẩm lên các ô text bõ
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string maHangSX = string.Empty;
            btnBoQua.Enabled = true;
            txtMaSP.Enabled = false;
            txtMaSP.Text = dgvSanPham.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenSP.Text = dgvSanPham.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMoTa.Text = dgvSanPham.Rows[e.RowIndex].Cells[2].Value.ToString();
            //lay ten hang
            string ma = string.Empty;
            ma = dgvSanPham.Rows[e.RowIndex].Cells[3].Value.ToString();
            cbHangSX.Text = xldl.getTenHangSXByMa(ma);
            cbPhanLoai.Text = dgvSanPham.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtSoLuong.Text = dgvSanPham.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtGiaNhap.Text = dgvSanPham.Rows[e.RowIndex].Cells[6].Value.ToString();
            txtGiaBan.Text = dgvSanPham.Rows[e.RowIndex].Cells[7].Value.ToString();
        }

        //ham xoa san pham theo tên
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                dr = MessageBox.Show("Bạn có muốn xóa sản phẩm này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string maSP = string.Empty;
                    if (txtMaSP.Text != string.Empty)
                    {
                        maSP = txtMaSP.Text;
                        if (xldl.deleteSanPham(maSP) == 1)
                        {
                            MessageBox.Show("Xóa sản phẩm thành công!!");
                            dgvSanPham.DataSource = xldl.getDataFromSanPham();
                            resetValue();
                        }
                        else
                        {
                            MessageBox.Show("Xóa sản phẩm thất bại!!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Vui lòng chọn sản phẩm để xóa!!!");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi xoa du lieu!!!" + ex.Message);
            }
        }

        //ham sửa thông tin sản phẩm
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                dr = MessageBox.Show("Bạn có muốn sửa thông tin sản phẩm này không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string MaSP = string.Empty;
                    string tenSP = string.Empty;
                    string MoTa = string.Empty;
                    string MaHangSX = string.Empty;
                    string PhanLoai = string.Empty;
                    int SoLuong = 0;
                    int GiaNhap = 0;
                    int GiaBan = 0;
                    if (txtMaSP.Text != string.Empty && txtTenSP.Text != string.Empty && txtMoTa.Text != string.Empty && cbHangSX.Text != string.Empty && cbPhanLoai.Text != string.Empty && txtGiaNhap.Text != string.Empty && txtGiaBan.Text != string.Empty)
                    {
                        MaSP = txtMaSP.Text;
                        tenSP = txtTenSP.Text;
                        MoTa = txtMoTa.Text;
                        MaHangSX = cbHangSX.SelectedValue.ToString();
                        PhanLoai = cbPhanLoai.Text;
                        SoLuong = int.Parse(txtSoLuong.Text);
                        GiaNhap = int.Parse(txtGiaNhap.Text);
                        GiaBan = int.Parse(txtGiaBan.Text);
                        if (xldl.updateSanPham(MaSP, tenSP, MoTa, MaHangSX, PhanLoai, SoLuong, GiaNhap, GiaBan) == 1)
                        {
                            MessageBox.Show("Sửa sản phẩm thành công!!!");
                            dgvSanPham.DataSource = xldl.getDataFromSanPham();
                            resetValue();
                        }
                        else
                        {
                            MessageBox.Show("Sửa sản phẩm thất bại!!!");
                        }

                    }
                    else
                    {
                        MessageBox.Show("vui lòng chọn sản phẩm để sửa!!!");
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi du lieu: " + ex.Message);
            }
        }


        //tim kiem sản phẩm
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string key = string.Empty;
                if (txtTimKiem.Text != string.Empty)
                {
                    key = txtTimKiem.Text;
                    dgvSanPham.DataSource = xldl.searchSanPham(key);
                }
                else if (txtTimKiem.Text == string.Empty)
                {
                    dgvSanPham.DataSource = xldl.getDataFromSanPham();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi " + ex.Message);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string key = string.Empty;
                if (txtTimKiem.Text != string.Empty)
                {
                    key = txtTimKiem.Text;
                    if (xldl.searchSanPham(key).Rows.Count > 0)
                    {
                        dgvSanPham.DataSource = xldl.searchSanPham(key);
                    }
                    else if (xldl.searchSanPham(key).Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy sản phẩm!!");
                        dgvSanPham.DataSource = xldl.getDataFromSanPham();
                    }
                }
                else if (txtTimKiem.Text == string.Empty)
                {
                    dgvSanPham.DataSource = xldl.getDataFromSanPham();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi " + ex.Message);
            }
        }
    }
}
