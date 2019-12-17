using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DoAn_Nhom
{
    public partial class frmDMKH : Form
    {
        public frmDMKH()
        {
            InitializeComponent();
        }
        XuLyDuLieu xldl = new XuLyDuLieu();
        //xu kien load form
        private void frmDMKH_Load(object sender, EventArgs e)
        {
            dgvKhachHang.DataSource = xldl.getDataFromKhachHang();
            txtMakhach.Enabled = true;
            btnBoQua.Enabled = false;
        }

        //ham dong form hien tai quay vef form main 
        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dl = MessageBox.Show("Bạn muốn thoát chương trình không?",
                   "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dl == DialogResult.Yes)
            {

                this.Hide();
                frmMain f = new frmMain();
                f.ShowDialog();
            }
        }

        //ham them Khach Hang
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn muốn thêm khách hàng không?",
                   "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    string sMaKH = string.Empty;
                    string sTenKh = string.Empty;
                    string sDiaChi = string.Empty;
                    int iSoDT = 0;
                    string sdt = string.Empty;
                    if (txtDiachi.Text != string.Empty && txtMakhach.Text != string.Empty && txtTenkhach.Text != string.Empty && mskDienthoai.Text != string.Empty)
                    {
                        sMaKH = txtMakhach.Text;
                        sTenKh = txtTenkhach.Text;
                        sDiaChi = txtDiachi.Text;
                        sdt = mskDienthoai.Text;
                        iSoDT = int.Parse(sdt);
                        if (xldl.insertKH(sMaKH, sTenKh, sDiaChi, iSoDT) == 1)
                        {
                            MessageBox.Show("Thêm thành công!!");
                            dgvKhachHang.DataSource = xldl.getDataFromKhachHang();
                            resetValue();
                        }
                        else
                        {
                            MessageBox.Show("Thêm không thành công!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("bạn phải điền đầy đủ thông tin!!");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi them du lieu: " + ex);
            }
        }

        //ham xóa khách hàng
        private void btnXoa_Click(object sender, EventArgs e)
        {
            string ma = string.Empty;
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn muốn xóa khách hàng này không?",
                   "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    if (txtDiachi.Text != string.Empty && txtMakhach.Text != string.Empty && txtTenkhach.Text != string.Empty && mskDienthoai.Text != string.Empty)
                    {
                        ma = txtMakhach.Text;
                        if (xldl.deleteKhachHang(ma) == 1)
                        {
                            MessageBox.Show("Xóa thành công!!");
                            dgvKhachHang.DataSource = xldl.getDataFromKhachHang();
                            resetValue();
                        }
                        else
                        {
                            MessageBox.Show("Xóa không thành công!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("bạn phải chọn khách hàng để xóa!!");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }
        }

        //xự kiện click vào hàng trong datagridview sẽ truyền dữ liệu lên bảng
        private void dgvKhachHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            txtMakhach.Text = dgvKhachHang.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenkhach.Text = dgvKhachHang.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtDiachi.Text = dgvKhachHang.Rows[e.RowIndex].Cells[2].Value.ToString();
            mskDienthoai.Text = dgvKhachHang.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        //hàm sửa thông tin khách hàng
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn muốn Sửa thông tin khách hàng?",
                   "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    string sMaKH = string.Empty;
                    string sTenKh = string.Empty;
                    string sDiaChi = string.Empty;
                    int iSoDT = 0;
                    string sdt = string.Empty;
                    if (txtDiachi.Text != string.Empty && txtMakhach.Text != string.Empty && txtTenkhach.Text != string.Empty && mskDienthoai.Text != string.Empty)
                    {
                        sMaKH = txtMakhach.Text;
                        sTenKh = txtTenkhach.Text;
                        sDiaChi = txtDiachi.Text;
                        sdt = mskDienthoai.Text;
                        iSoDT = int.Parse(sdt);
                        if (xldl.updateKhachHang(sMaKH, sTenKh, sDiaChi, iSoDT) == 1)
                        {
                            MessageBox.Show("Sửa thành công!!");
                            dgvKhachHang.DataSource = xldl.getDataFromKhachHang();
                        }
                        else
                        {
                            MessageBox.Show("Sửa không thành công!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải chọn khách hàng để sửa thông tin!!");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }
        }

        //hàm trả về trạng thái ban đầu cho các textbox
        private void resetValue()
        {
            txtMakhach.Clear();
            txtTenkhach.Clear();
            txtDiachi.Clear();
            mskDienthoai.Clear();
        }

        //bỏ chọn dòng đang chọn trong datagridview
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            resetValue();
            btnThem.Enabled = true;
        }

        //tìm kiếm khách hàng
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string key = string.Empty;
                if (txtTimKiem.Text != string.Empty)
                {
                    key = txtTimKiem.Text;
                    dgvKhachHang.DataSource = xldl.searchKhachHang(key);
                }
                else if (txtTimKiem.Text == string.Empty)
                {
                    dgvKhachHang.DataSource = xldl.getDataFromKhachHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }
        }

        //tìm kiếm khách hàng
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string key = string.Empty;
                if (txtTimKiem.Text != string.Empty)
                {
                    key = txtTimKiem.Text;
                    if (xldl.searchKhachHang(key).Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy khách hàng!!");
                    }
                    else
                    {
                        dgvKhachHang.DataSource = xldl.searchKhachHang(key);
                    }

                }
                else if (txtTimKiem.Text == string.Empty)
                {
                    dgvKhachHang.DataSource = xldl.getDataFromKhachHang();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }
        }
    }
}
