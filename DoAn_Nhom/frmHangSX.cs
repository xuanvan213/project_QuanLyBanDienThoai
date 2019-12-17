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
    public partial class frmHangSX : Form
    {
        public frmHangSX()
        {
            InitializeComponent();
        }
        //khai bao lop xu ly du lieu
        XuLyDuLieu xldl = new XuLyDuLieu();

        //tra ca text box ve trang thai ban dau
        private void resetvalue()
        {
            txtMaHangSX.Clear();
            txtQuocGia.Clear();
            txtTenHangSX.Clear();
        }

        //xu kien load du lieu datagridview
        private void frmHangSX_Load(object sender, EventArgs e)
        {
            dgvHangSX.DataSource = xldl.getDataFromHangSX();
            btnBoQua.Enabled = false;
        }

        //xu kien lay du lieu khi click vao dong trong datagridview
        private void dgvHangSX_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            btnBoQua.Enabled = true;
            btnThem.Enabled = false;
            txtMaHangSX.Text = dgvHangSX.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtTenHangSX.Text = dgvHangSX.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtQuocGia.Text = dgvHangSX.Rows[e.RowIndex].Cells[2].Value.ToString();
        }

        //ham sua thong tin hang san xuat
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                dr = MessageBox.Show("Bạn có muốn sửa Hãng Sản Xuất không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string sMa = string.Empty;
                    string ten = string.Empty;
                    string diachi = string.Empty;
                    if (txtMaHangSX.Text != string.Empty && txtTenHangSX.Text != string.Empty && txtQuocGia.Text != string.Empty)
                    {
                        sMa = txtMaHangSX.Text;
                        ten = txtTenHangSX.Text;
                        diachi = txtQuocGia.Text;
                        if (xldl.updateHangSX(sMa, ten, diachi) == 1)
                        {
                            MessageBox.Show("Sửa thông tin thành công!!");
                            dgvHangSX.DataSource = xldl.getDataFromHangSX();
                            resetvalue();
                        }
                        else
                        {
                            MessageBox.Show("Sửa thông tin thất bại!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải chọn hãng sản xuất để xóa!!");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi sua du lieu " + ex.Message);
            }
        }

        //ham dong form chuyen ve form main
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

        //ham bo chon Hang San xuat
        private void btnBoQua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            resetvalue();
            btnBoQua.Enabled = false;
        }

        //hàm thêm hãng sản xuất
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                dr = MessageBox.Show("Bạn có muốn thêm Hãng Sản Xuất không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string sMa = string.Empty;
                    string ten = string.Empty;
                    string diachi = string.Empty;
                    if (txtMaHangSX.Text != string.Empty && txtTenHangSX.Text != string.Empty && txtQuocGia.Text != string.Empty)
                    {
                        sMa = txtMaHangSX.Text;
                        ten = txtTenHangSX.Text;
                        diachi = txtQuocGia.Text;
                        if (xldl.insertHangSX(sMa, ten, diachi) == 1)
                        {
                            MessageBox.Show("Thêm hãng sản xuẩt thành công!!");
                            dgvHangSX.DataSource = xldl.getDataFromHangSX();
                            resetvalue();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bạn phải điền đầy đủ thông tin!!");
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi them du lieu " + ex.Message);
            }
        }

        //hàm xóa hãng sản xuất
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                dr = MessageBox.Show("Bạn có muốn xóa Hãng Sản Xuất không ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    string sMa = string.Empty;
                    if (txtMaHangSX.Text != string.Empty)
                    {
                        sMa = txtMaHangSX.Text;
                        if (xldl.deleteHangSX(sMa) == 1)
                        {
                            MessageBox.Show("Xóa hãng sản xuất thành công!!");
                            dgvHangSX.DataSource = xldl.getDataFromHangSX();
                            resetvalue();
                        }
                        else
                        {
                            MessageBox.Show("Xóa hãng sản xuất thất bại!!");
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
                MessageBox.Show("Loi " + ex.Message);
            }
        }

        //hàm tìm kiếm hãng SX
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string key = string.Empty;
                if (txtTimKiem.Text != string.Empty)
                {
                    key = txtTimKiem.Text;
                    dgvHangSX.DataSource = xldl.searchHangSX(key);
                }
                else if (txtTimKiem.Text == string.Empty)
                {
                    dgvHangSX.DataSource = xldl.getDataFromHangSX();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tim kiem " + ex.Message);
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
                    if (xldl.searchHangSX(key).Rows.Count > 0)
                    {
                        dgvHangSX.DataSource = xldl.searchHangSX(key);
                    }
                    else if (xldl.searchHangSX(key).Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy hãng sản xuất!!");
                    }
                }
                else if (txtTimKiem.Text == string.Empty)
                {
                    dgvHangSX.DataSource = xldl.getDataFromHangSX();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi tim kiem " + ex.Message);
            }
        }
    }
}
