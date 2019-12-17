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


namespace DoAn_Nhom
{
    public partial class frmHoaDonBan : Form
    {
        public frmHoaDonBan()
        {
            InitializeComponent();
        }
        XuLyDuLieu xldl = new XuLyDuLieu();
        //xự kiến đóng form hiên tại quay về form main
        private void btnThoat_Click(object sender, EventArgs e)
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

        public int doanhthu()
        {
            int doanhthu = 0;
            int tinhtien = 0;
            if (txtTongTien.Text != string.Empty)
            {
                tinhtien = int.Parse(txtTongTien.Text);
                doanhthu += tinhtien;
            }
            return doanhthu;
        }

        private void frmHoaDonBan_Load(object sender, EventArgs e)
        {
            cbMaNV.DataSource = xldl.getDataFromNhanVien();
            cbMaNV.DisplayMember = "MaNV";
            cbMaNV.ValueMember = "MaNV";
            cbMaSP.DataSource = xldl.getDataFromSanPham();
            cbMaSP.DisplayMember = "MaSP";
            cbMaSP.ValueMember = "MaSP";
            cbMaHD.DataSource = xldl.getMaHDBDESC();
            cbMaHD.DisplayMember = "MaHDB";
            cbMaHD.ValueMember = "MaHDB";
            btnThem.Enabled = true;
            btnHuy.Enabled = false;
            txtDonGia.Enabled = false;
            cbMaSP.SelectedIndex = -1;
            cbMaNV.SelectedIndex = -1;
            txtTongTien.Enabled = false;
            txtThanhTien.Enabled = false;
            txtTongTien.Text = "0";
        }

        //reset value tat ca cac o nhap
        public void resetValue()
        {
            cbMaHD.Text = "";
            cbMaNV.Text = "";
            txtTenNhanVien.Clear();
            txtMaKH.Clear();
            txtTenKhachHang.Clear();
            txtDiaChi.Clear();
            mkbSoDT.Clear();
            cbMaSP.Text = "";
            txtTenSP.Clear();
            txtDonGia.Clear();
            txtSoLuong.Clear();
            txtGiamGia.Clear();
            txtThanhTien.Clear();
            txtTongTien.Clear();
            txtMaKH.Clear();
            txtTenKhachHang.Clear();
            txtDiaChi.Clear();
            mkbSoDT.Clear();
            
        }
        //ham chuyen so sang chu
        public string ChuyenSoSangChu(string sNumber)
        {
            int mLen, mDigit;
            string mTemp = "";
            string[] mNumText;
            //Xóa các dấu "," nếu có
            sNumber = sNumber.Replace(",", "");
            mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
            mLen = sNumber.Length - 1; // trừ 1 vì thứ tự đi từ 0
            for (int i = 0; i <= mLen; i++)
            {
                mDigit = Convert.ToInt32(sNumber.Substring(i, 1));
                mTemp = mTemp + " " + mNumText[mDigit];
                if (mLen == i) // Chữ số cuối cùng không cần xét tiếp break; 
                    switch ((mLen - i) % 9)
                    {
                        case 0:
                            mTemp = mTemp + " tỷ";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        case 6:
                            mTemp = mTemp + " triệu";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        case 3:
                            mTemp = mTemp + " nghìn";
                            if (sNumber.Substring(i + 1, 3) == "000") i = i + 3;
                            break;
                        default:
                            switch ((mLen - i) % 3)
                            {
                                case 2:
                                    mTemp = mTemp + " trăm";
                                    break;
                                case 1:
                                    mTemp = mTemp + " mươi";
                                    break;
                            }
                            break;
                    }
            }
            //Loại bỏ trường hợp x00
            mTemp = mTemp.Replace("không mươi không ", "");
            mTemp = mTemp.Replace("không mươi không", ""); //Loại bỏ trường hợp 00x 
            mTemp = mTemp.Replace("không mươi ", "linh "); //Loại bỏ trường hợp x0, x>=2
            mTemp = mTemp.Replace("mươi không", "mươi");
            //Fix trường hợp 10
            mTemp = mTemp.Replace("một mươi", "mười");
            //Fix trường hợp x4, x>=2
            mTemp = mTemp.Replace("mươi bốn", "mươi tư");
            //Fix trường hợp x04
            mTemp = mTemp.Replace("linh bốn", "linh tư");
            //Fix trường hợp x5, x>=2
            mTemp = mTemp.Replace("mươi năm", "mươi lăm");
            //Fix trường hợp x1, x>=2
            mTemp = mTemp.Replace("mươi một", "mươi mốt");
            //Fix trường hợp x15
            mTemp = mTemp.Replace("mười năm", "mười lăm");
            //Bỏ ký tự space
            mTemp = mTemp.Trim();
            //Viết hoa ký tự đầu tiên
            mTemp = mTemp.Substring(0, 1).ToUpper() + mTemp.Substring(1) + " đồng";
            return mTemp;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                string sMaHD = string.Empty;
                string NgayBan = string.Empty, MaNV = string.Empty, MaKH = string.Empty;
                int tongtien = 0;
                if (cbMaHD.Text != string.Empty)
                {
                    sMaHD = cbMaHD.Text;
                    if (!xldl.CheckKeyMHDB(sMaHD))
                    {
                        if (mkbNgayBan.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải nhập ngày bán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            mkbNgayBan.Focus();
                            return;
                        }
                        if (cbMaNV.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải nhập mã nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            cbMaNV.Focus();
                            return;
                        }
                        if (txtMaKH.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải nhập mã khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMaKH.Focus();
                            return;
                        }
                        if (txtTongTien.Text == string.Empty)
                        {
                            MessageBox.Show("Bạn phải mua sản phẩm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMaKH.Focus();
                            return;
                        }
                        MessageBox.Show("Luu chi tiet Hoa Don Ban Thanh Cong!!");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi Du Lieu: " + ex.Message);
            }

        }

        //chuyen sang  ngay thang
        public string ConvertDateTime(string date)
        {
            string[] elements = date.Split('/');
            string dt = string.Format("{0}/{1}/{2}", elements[0], elements[1], elements[2]);
            return dt;
        }

        //xoa san pham khi doubleclick trong datagridview
        private void dGvThongTin_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string maSP = string.Empty;
                string maHDB = string.Empty;
                if (cbMaHD.Text != string.Empty)
                {
                    maSP = dGvThongTin.Rows[e.RowIndex].Cells[0].Value.ToString();
                    maHDB = cbMaHD.Text;
                    if (xldl.deleteChiTietHDB(maSP, maHDB) == 1)
                    {
                        MessageBox.Show("Xoa san pham thanh cong!!");
                        dGvThongTin.DataSource = xldl.getChiTietHD(maHDB);
                    }
                    else
                    {
                        MessageBox.Show("Xoa san pham that bai!!");
                    }
                }
                else
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi Xoa du lieu!!" + ex.Message);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            try
            {
                resetValue();
                btnThem.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi : " + ex.Message);
            }
        }

        //su kien thay doi du lieu trong textbox Giam gia va tra ve thanh tien cho textbox thanh tien
        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            int tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToInt32(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToInt32(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToInt32(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        //su kien thay doi du lieu trong combobox Ma San Pham
        private void cbMaSP_TextChanged(object sender, EventArgs e)
        {
            string sMa = string.Empty;
            if (cbMaSP.Text == string.Empty)
            {
                txtDonGia.Text = "";
                txtTenSP.Text = "";
            }
            //lay du lieu tu combobox
            sMa = cbMaSP.Text;
            //gan du lieu cho textbox Ten
            txtTenSP.Text = xldl.getNameSP(sMa);
            //gan du lieu cho textbox Gia
            txtDonGia.Text = xldl.getPriceSP(sMa);

        }

        //su kien thay doi ma nhan vien 
        private void cbMaNV_TextChanged(object sender, EventArgs e)
        {
            string sMaNV = string.Empty;
            if (cbMaNV.Text == string.Empty)
            {
                txtTenNhanVien.Text = "";
            }
            //lay ten nhan vien theo ma nhan vien duoc chon
            sMaNV = cbMaNV.Text;
            txtTenNhanVien.Text = xldl.getNameNhanVien(sMaNV);
        }

        //su kien thay doi so luong san pham
        private void txtSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            //kiem tra du lieu nhap tu ban phim la so hay chu
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || Convert.ToInt32(e.KeyChar) == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        //xu ly du lieu nhap  vao o GiamGia
        private void txtGiamGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar >= '0' && e.KeyChar <= '9' || Convert.ToInt32(e.KeyChar) == 8)
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        //tinh Thanh tien khi thay doi so luong
        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            int tt, sl, dg, gg;
            if (txtSoLuong.Text == "")
                sl = 0;
            else
                sl = Convert.ToInt32(txtSoLuong.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToInt32(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToInt32(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;

            txtThanhTien.Text = tt.ToString();
        }

        //them san pham vao hoa don
        private void btnThemSP_Click(object sender, EventArgs e)
        {
            try
            {
                btnHuy.Enabled = true;
                string MaHDB = string.Empty, MaSP = string.Empty;
                int SoLuong = 0, DonGia = 0, GiamGia = 0, ThanhTien = 0;
                int tongtien = 0;
                if (cbMaHD.Text != string.Empty && cbMaSP.Text != string.Empty && txtSoLuong.Text != string.Empty && txtDonGia.Text != string.Empty && txtGiamGia.Text != string.Empty && txtThanhTien.Text != string.Empty)
                {
                    MaHDB = cbMaHD.Text;
                    MaSP = cbMaSP.SelectedValue.ToString();
                    SoLuong = int.Parse(txtSoLuong.Text);
                    DonGia = int.Parse(txtDonGia.Text);
                    GiamGia = int.Parse(txtGiamGia.Text);
                    ThanhTien = int.Parse(txtThanhTien.Text);
                    if (xldl.insertChiTietHDB(MaHDB, MaSP, SoLuong, DonGia, GiamGia, ThanhTien) == 1)
                    {
                        MessageBox.Show("Them san pham thanh cong!!");
                        dGvThongTin.DataSource = xldl.getChiTietHD(MaHDB);
                        if (txtTongTien.Text == string.Empty)
                        {
                            tongtien = tongtien + ThanhTien;
                            txtTongTien.Text = tongtien.ToString();
                        }
                        else
                        {
                            tongtien = int.Parse(txtTongTien.Text);
                            tongtien = tongtien + ThanhTien;
                            txtTongTien.Text = tongtien.ToString();
                        }
                        cbMaSP.Text = "";
                        txtSoLuong.Clear();
                        txtGiamGia.Clear();
                    }
                    else
                    {
                        MessageBox.Show("Them san pham that bai!!");
                    }
                }
                else
                {
                    MessageBox.Show("Ban phai dien du thong tin!! ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi : " + ex.Message);
            }
        }

        //neu ma khach hang da ton tai thi se lay thong tin
        private void mkbSoDT_TextChanged(object sender, EventArgs e)
        {
            int sdt = 0;
            if (mkbSoDT.Text != string.Empty)
            {
                sdt = int.Parse(mkbSoDT.Text);
                txtMaKH.Text = xldl.getMaKhachHang(sdt);
                txtTenKhachHang.Text = xldl.getNameKhachHang(sdt);
                txtDiaChi.Text = xldl.getAddressKhachHang(sdt);
            }

        }

        private void txtMaKH_TextChanged(object sender, EventArgs e)
        {
            string MaKH = string.Empty;
            string ngayThang = string.Empty;
            if (txtMaKH.Text != string.Empty)
            {
                MaKH = txtMaKH.Text;
                cbMaHD.Text = xldl.getMaHDB(MaKH);
                ngayThang = xldl.getNgayHDB(MaKH);
                mkbNgayBan.Text = ngayThang;
            }
        
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (cbMaHD.Text != string.Empty)
                {
                    resetValue();
                    btnHuy.Enabled = true;
                    btnLuu.Enabled = true;
                    btnThem.Enabled = true;
                }
                
                string MaHDB = string.Empty, MaNV = string.Empty, NgayBan = string.Empty, MaKH = string.Empty;
                int TongTien = 0;
                if (cbMaHD.Text != string.Empty && cbMaNV.Text != string.Empty && mkbNgayBan.Text != string.Empty && txtMaKH.Text != string.Empty)
                {
                    MaHDB = cbMaHD.Text;
                    MaNV = cbMaNV.Text;
                    NgayBan = mkbNgayBan.Text;
                    MaKH = txtMaKH.Text;
                    TongTien = int.Parse(txtTongTien.Text);
                    if (xldl.insertHoaDonBan(MaHDB, MaNV, NgayBan, MaKH, TongTien) == 1)
                    {
                        MessageBox.Show("Them thanh cong!! ");
                        dGvThongTin.DataSource = xldl.getChiTietHD(MaHDB);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi: " + ex.Message);
            }


        }

        private void cbMaHD_TextChanged(object sender, EventArgs e)
        {
            string maHDB = string.Empty, MaKH = string.Empty;
            if (cbMaHD.Text != string.Empty)
            {
                maHDB = cbMaHD.Text;
                dGvThongTin.DataSource = xldl.getChiTietHD(maHDB);
                txtMaKH.Text = xldl.getMaKHbyMaHDB(maHDB);
                MaKH = txtMaKH.Text;
                mkbSoDT.Text = xldl.getSDTHbyMaKH(MaKH).ToString();
            }
            else
            {
                dGvThongTin.DataSource = null;
            }
        }
    }
}
