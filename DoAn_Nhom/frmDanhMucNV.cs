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
    public partial class frmDanhMucNV : Form
    {
        public frmDanhMucNV()
        {
            InitializeComponent();
        }

        //tao luong du lieu
        XuLyDuLieu xldl = new XuLyDuLieu();

        //sự kiện load form
        private void frmDanhMucNV_Load(object sender, EventArgs e)
        {
            dgvNhanVien.DataSource = xldl.getDataFromNhanVien();
            btnXoa.Enabled = false;
            btnSua.Enabled = false;

        }

        //ham thêm nhân  viên
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn muốn thêm nhân viên?",
                   "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    string sMaNv = string.Empty;
                    string sTen = string.Empty;
                    string gioitinh = string.Empty;
                    string diaChi = string.Empty;
                    int soDT = 0;
                    string NgaySinh = string.Empty;
                    if (txtManhanvien.Text != string.Empty && txtTennhanvien.Text != string.Empty && txtDiachi.Text != string.Empty && mskDienthoai.Text != string.Empty && mskNgaysinh.Text != string.Empty)
                    {
                        sMaNv = txtManhanvien.Text;
                        sTen = txtTennhanvien.Text;
                        diaChi = txtDiachi.Text;
                        if (chkbNam.Checked == true)
                        {
                            gioitinh = "Nam";
                        }
                        else if (chkbNu.Checked == true)
                        {
                            gioitinh = "Nu";
                        }
                        else
                        {
                            MessageBox.Show("Hãy chọn giới tính!!");
                        }
                        soDT = int.Parse(mskDienthoai.Text);
                        NgaySinh = mskNgaysinh.Text;

                        //MessageBox.Show("Ngay sinh: " + NgaySinh);
                        if (xldl.insertNhanVien(sMaNv, sTen, gioitinh, diaChi, soDT, NgaySinh) == 1)
                        {
                            MessageBox.Show("thêm Nhân Viên thành công!!!");
                            dgvNhanVien.DataSource = xldl.getDataFromNhanVien();
                            resetValue();
                        }
                        else
                        {
                            MessageBox.Show("thêm Không thành công!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hay dien day du thong tin nhan vien!!!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }


        }

        //ham xóa nhân viên
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn muốn xóa nhân viên?",
                   "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    string ma = string.Empty;
                    if (txtManhanvien.Text != string.Empty && txtTennhanvien.Text != string.Empty && txtDiachi.Text != string.Empty && mskDienthoai.Text != string.Empty && mskNgaysinh.Text != string.Empty)
                    {
                        ma = txtManhanvien.Text;
                        if (xldl.deleteNhanVien(ma) == 1)
                        {
                            MessageBox.Show("Xóa thành công!!!");
                            dgvNhanVien.DataSource = xldl.getDataFromNhanVien();
                            resetValue();
                        }
                        else
                        {
                            MessageBox.Show("Xóa không thành công!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hay chon nhan vien!!!");
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi xoa du lieu: " + ex.Message);
            }
        }

        //ham đóng form
        private void btnDong_Click(object sender, EventArgs e)
        {
            DialogResult dlr = MessageBox.Show("Bạn muốn đóng form không?",
                  "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlr == DialogResult.Yes)
            {
                this.Hide();
                frmMain fm = new frmMain();
                fm.ShowDialog();
            }
            else
            {
                return;
            }

        }

        //hàm sửa thông tin nhân viên
        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dlr = MessageBox.Show("Bạn muốn Sửa thông tin nhân viên không?",
                   "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dlr == DialogResult.Yes)
                {
                    string sMaNv = string.Empty;
                    string sTen = string.Empty;
                    string gioitinh = string.Empty;
                    string diaChi = string.Empty;
                    int soDT = 0;
                    string NgaySinh = string.Empty;
                    if (txtManhanvien.Text != string.Empty && txtTennhanvien.Text != string.Empty && txtDiachi.Text != string.Empty && mskDienthoai.Text != string.Empty && mskNgaysinh.Text != string.Empty)
                    {
                        sMaNv = txtManhanvien.Text;
                        sTen = txtTennhanvien.Text;
                        diaChi = txtDiachi.Text;
                        if (chkbNam.Checked == true)
                        {
                            gioitinh = "Nam";
                        }
                        else if (chkbNu.Checked == true)
                        {
                            gioitinh = "Nu";
                        }
                        else
                        {
                            MessageBox.Show("Hãy chọn giới tính!!");
                        }
                        soDT = int.Parse(mskDienthoai.Text);

                        NgaySinh = mskNgaysinh.Text;
                        if (xldl.updateNhanVien(sMaNv, sTen, gioitinh, diaChi, soDT, NgaySinh) == 1)
                        {
                            MessageBox.Show("Sửa Nhân Viên thành công!!!");
                            dgvNhanVien.DataSource = xldl.getDataFromNhanVien();
                            resetValue();
                        }
                        else
                        {
                            MessageBox.Show("Sửa thông tin Không thành công!!!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hay chon nhan vien!!!");
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

        //ham xử lý sự kiện khi click chọn nhân viên
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                txtManhanvien.Text = dgvNhanVien.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTennhanvien.Text = dgvNhanVien.Rows[e.RowIndex].Cells[1].Value.ToString();
                if (dgvNhanVien.Rows[e.RowIndex].Cells[2].Value.ToString() == "nam" || dgvNhanVien.Rows[e.RowIndex].Cells[2].Value.ToString() == "Nam")
                {
                    chkbNam.Checked = true;
                    chkbNu.Checked = false;
                }
                else
                {
                    chkbNu.Checked = true;
                    chkbNam.Checked = false;
                }
                txtDiachi.Text = dgvNhanVien.Rows[e.RowIndex].Cells[3].Value.ToString();
                mskDienthoai.Text = dgvNhanVien.Rows[e.RowIndex].Cells[4].Value.ToString();

                //xu ly ngay
                string ngay = string.Empty;
                ngay = dgvNhanVien.Rows[e.RowIndex].Cells[5].Value.ToString();
                string[] ngaysinh = ngay.Split(' ');
                string[] ntn = ngaysinh[0].ToString().Split('/');
                int ngays = int.Parse(ntn[0]);
                int thang = int.Parse(ntn[1]);
                int nam = int.Parse(ntn[2]);
                string sDate = string.Empty;
                if (ngays < 10 && thang > 10)
                {
                    sDate = nam.ToString() + thang.ToString() + "0" + ngays.ToString();
                }
                else if (thang < 10 && ngays > 10)
                {
                    sDate = nam.ToString() + "0" + thang.ToString() + ngays.ToString();
                }
                else if (ngays < 10 && thang < 10)
                {
                    sDate = nam.ToString() + "0" + thang.ToString() + "0" + ngays.ToString();
                }
                else
                {
                    sDate = nam.ToString() + thang.ToString() + ngays.ToString();
                }
                mskNgaysinh.Text = sDate;
                btnSua.Enabled = true;
                btnXoa.Enabled = true;
                btnThem.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }

        }

        //hàm trả về trạng thái ban đầu cho text box
        private void resetValue()
        {
            txtManhanvien.Clear();
            txtTennhanvien.Clear();
            txtDiachi.Clear();
            txtTimKiem.Clear();
            mskDienthoai.Clear();
            mskNgaysinh.Clear();
            chkbNam.Checked = false;
            chkbNu.Checked = false;
        }
        //hàm tìm kiếm nhân viên
        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string key = string.Empty;
                if(txtTimKiem.Text != string.Empty)
                {
                    key = txtTimKiem.Text;
                    dgvNhanVien.DataSource = xldl.searchNhanVien(key);
                } 
                else if(txtTimKiem.Text == string.Empty)
                {
                    dgvNhanVien.DataSource = xldl.getDataFromNhanVien();
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Loi " + ex.Message);
            }
        }

        private void btnBoQua_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = true;           
            resetValue();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string key = string.Empty;
                if (txtTimKiem.Text != string.Empty)
                {
                    key = txtTimKiem.Text;
                    dgvNhanVien.DataSource = xldl.searchNhanVien(key);
                }
                else if (txtTimKiem.Text == string.Empty)
                {
                    dgvNhanVien.DataSource = xldl.getDataFromNhanVien();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi " + ex.Message);
            }
        }
    }
}
