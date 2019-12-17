using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DoAn_Nhom
{

    public class XuLyDuLieu
    {
        private SqlConnection connectDB = new SqlConnection();

        //tao luong du lieu
        public XuLyDuLieu()
        {
            connectDB.ConnectionString = @"Data Source=DESKTOP-P90O70J;Initial Catalog=QuanLyBanDiDong;Integrated Security=True";
            try
            {
                connectDB.Open();
            }
            catch (Exception ex)
            {
                connectDB.Close();
                System.Windows.Forms.MessageBox.Show(" Lỗi kết nối " + ex.Message);
            }
        }
        //dong ket noi 
        public void disconnect()
        {
            if(connectDB.State == ConnectionState.Open)
            {
                connectDB.Close();
                connectDB.Dispose();
            }
        }
        //lay tat ca du lieu trong bang san pham
        public DataTable getDataFromSanPham()
        {
            DataTable dtaSp = new DataTable();
            SqlCommand cmdgetSP = new SqlCommand("spgetAllSP", connectDB);
            cmdgetSP.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daSanPham = new SqlDataAdapter(cmdgetSP);
            daSanPham.Fill(dtaSp);
            return dtaSp;

        }

        //thêm sản phẩm
        public int insertSanPham(string maSP, string tenSP, string moTa, string MaSX, string phanloai, int soluong, int gianhap, int giaban)
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdInsertSP = new SqlCommand("spInsertSanPham", connectDB);
                cmdInsertSP.CommandType = CommandType.StoredProcedure;
                cmdInsertSP.Parameters.AddWithValue("@maSp", maSP);
                cmdInsertSP.Parameters.AddWithValue("@tenSp", tenSP);
                cmdInsertSP.Parameters.AddWithValue("@moTa", moTa);
                cmdInsertSP.Parameters.AddWithValue("@maHang", MaSX);
                cmdInsertSP.Parameters.AddWithValue("@phanloai", phanloai);
                cmdInsertSP.Parameters.AddWithValue("@soluong", soluong);
                cmdInsertSP.Parameters.AddWithValue("@giaNhap", gianhap);
                cmdInsertSP.Parameters.AddWithValue("@giaban", giaban);
                ketqua = cmdInsertSP.ExecuteNonQuery();
                if(ketqua > 0)
                {
                    ketqua = 1;
                }
                else
                {
                    ketqua = 0;
                }
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi them du lieu: " + ex.Message);
                return 0;
            }
        }
       
        //xoa sản phẩm theo mã sản phẩm
        public int deleteSanPham(string maSP)
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdDelSP = new SqlCommand("spDeleteSanPham", connectDB);
                cmdDelSP.CommandType = CommandType.StoredProcedure;
                cmdDelSP.Parameters.AddWithValue("@maSp", maSP);
                ketqua = cmdDelSP.ExecuteNonQuery();
                if (ketqua > 0)
                {
                    ketqua = 1;
                }
                else
                {
                    ketqua = 0;
                }
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi xoa du lieu: " + ex.Message);
                return 0;
            }
        }
        
        //sửa sản phẩm theo mã sản phẩm
        public int updateSanPham(string maSP, string tenSP, string moTa, string MaSX, string phanloai, int soluong, int gianhap, int giaban)
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdUpdateSanPham = new SqlCommand("spUpdateSanPham", connectDB);
                cmdUpdateSanPham.CommandType = CommandType.StoredProcedure;
                cmdUpdateSanPham.Parameters.AddWithValue("@maSp", maSP);
                cmdUpdateSanPham.Parameters.AddWithValue("@tenSp", tenSP);
                cmdUpdateSanPham.Parameters.AddWithValue("@moTa", moTa);
                cmdUpdateSanPham.Parameters.AddWithValue("@maHang", MaSX);
                cmdUpdateSanPham.Parameters.AddWithValue("@phanloai", phanloai);
                cmdUpdateSanPham.Parameters.AddWithValue("@soluong", soluong);
                cmdUpdateSanPham.Parameters.AddWithValue("@giaNhap", gianhap);
                cmdUpdateSanPham.Parameters.AddWithValue("@giaban", giaban);
                ketqua = cmdUpdateSanPham.ExecuteNonQuery();
                if(ketqua > 0)
                {
                    ketqua = 1;
                }             
                return ketqua;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi du lieu!! "+ex.Message);
                return 0;
            }
        }
        //xu ly dang nhap
        public bool Login(string username, string pass)
        {

            DataTable dtLogin = new DataTable();

            SqlCommand cmdLogin = new SqlCommand("spLogin", connectDB);
            cmdLogin.Parameters.AddWithValue("@user", username);
            cmdLogin.Parameters.AddWithValue("@pass", pass);
            cmdLogin.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daLogin = new SqlDataAdapter(cmdLogin);
            daLogin.Fill(dtLogin);
            return dtLogin.Rows.Count > 0;
        }


        //Cac cau truy van bang Khach Hang
        //lay ra tat ca Khach Hang trong bang Khach Hang

        public DataTable getDataFromKhachHang()
        {

            DataTable dtaKH = new DataTable();
            SqlCommand cmdgetKH = new SqlCommand("spGetAllKhachHang", connectDB);
            cmdgetKH.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daKhachHang = new SqlDataAdapter(cmdgetKH);
            daKhachHang.Fill(dtaKH);
            return dtaKH;
        }

        //them khach hang vao bang Khach Hang
        public int insertKH(string MaKH, string TenKH, string DiaChi, int SDT)
        {
            try
            {
                SqlCommand cmdInsertKH = new SqlCommand("spInsertKhachHang", connectDB);
                cmdInsertKH.CommandType = CommandType.StoredProcedure;
                cmdInsertKH.Parameters.AddWithValue("@ma", MaKH);
                cmdInsertKH.Parameters.AddWithValue("@ten", TenKH);
                cmdInsertKH.Parameters.AddWithValue("@diachi", DiaChi);
                cmdInsertKH.Parameters.AddWithValue("@sdt", SDT);

                int ketqua = 0;
                ketqua = cmdInsertKH.ExecuteNonQuery();
                if (ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi: " + ex.Message);
                return 0;
            }

        }

        //xoa khach hang theo Ma KH
        public int deleteKhachHang(string sMaKH)
        {
            try
            {
                SqlCommand cmdDeleteKH = new SqlCommand("spDeletKhachHang", connectDB);
                cmdDeleteKH.CommandType = CommandType.StoredProcedure;
                cmdDeleteKH.Parameters.AddWithValue("@makh", sMaKH); ;

                int ketqua = 0;
                ketqua = cmdDeleteKH.ExecuteNonQuery();
                if (ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi: " + ex.Message);
                return 0;
            }
        }

        //Sua thong tin khach hang
        public int updateKhachHang(string MaKH, string TenKH, string DiaChi, int SDT)
        {
            try
            {
                SqlCommand cmdInsertKH = new SqlCommand("spUpdateKhachHang", connectDB);
                cmdInsertKH.CommandType = CommandType.StoredProcedure;
                cmdInsertKH.Parameters.AddWithValue("@maKH", MaKH);
                cmdInsertKH.Parameters.AddWithValue("@ten", TenKH);
                cmdInsertKH.Parameters.AddWithValue("@diachi", DiaChi);
                cmdInsertKH.Parameters.AddWithValue("@sdt", SDT);
                int ketqua = 0;
                ketqua = cmdInsertKH.ExecuteNonQuery();
                if (ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi: " + ex.Message);
                return 0;
            }
        }

        //Cac cau truy van bang Nhan Vien

        //lay tat ca nhan vien tu bang NhanVien
        public DataTable getDataFromNhanVien()
        {
            DataTable dtNhanVien = new DataTable();
            SqlCommand cmdgetNV = new SqlCommand("spGetAllNhanVien", connectDB);
            cmdgetNV.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daNhanVien = new SqlDataAdapter(cmdgetNV);
            daNhanVien.Fill(dtNhanVien);
            return dtNhanVien;
        }
        
        //them nhan vien vao bang Nhan Vien

        public int insertNhanVien(string sMa, string  sTen, string gioiTinh, string diachi, int SoDT, string NgaySinh)
        {
            try
            {
                SqlCommand cmdInsertNV = new SqlCommand("spInsertNhanVien", connectDB);
                cmdInsertNV.CommandType = CommandType.StoredProcedure;
                cmdInsertNV.Parameters.AddWithValue("@maNV", sMa);
                cmdInsertNV.Parameters.AddWithValue("@TenNV", sTen);
                cmdInsertNV.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                cmdInsertNV.Parameters.AddWithValue("@DiaChi", diachi);
                cmdInsertNV.Parameters.AddWithValue("@soDienThoai", SoDT);
                cmdInsertNV.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                int ketqua = 0;
                ketqua = cmdInsertNV.ExecuteNonQuery();
                if (ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi them du lieu: "+ex.Message);
                return 0;
            }
        }

        //xoa nhan Vien trong bang Nhan Vien theo MaNV
        
        public int deleteNhanVien(string sMa)
        {
            try
            {
                SqlCommand cmdDelNhanVien = new SqlCommand("spDeleteNhanVien", connectDB);
                cmdDelNhanVien.CommandType = CommandType.StoredProcedure;
                cmdDelNhanVien.Parameters.AddWithValue("@maNV", sMa);
                int ketqua = 0;
                ketqua = cmdDelNhanVien.ExecuteNonQuery();
                if(ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi Xoa Du Lieu: " + ex.Message);
                return 0;
            }

        }

        //sua thong tin nhan vien trong bang nhan vien

        public int updateNhanVien(string sMa, string sTen, string gioiTinh, string diachi, int SoDT, string NgaySinh)
        {
            try
            {
                SqlCommand cmdUpdateNhanVien = new SqlCommand("spUpdateNhanVien", connectDB);
                cmdUpdateNhanVien.CommandType = CommandType.StoredProcedure;
                cmdUpdateNhanVien.Parameters.AddWithValue("@maNV", sMa);
                cmdUpdateNhanVien.Parameters.AddWithValue("@TenNV", sTen);
                cmdUpdateNhanVien.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                cmdUpdateNhanVien.Parameters.AddWithValue("@DiaChi", diachi);
                cmdUpdateNhanVien.Parameters.AddWithValue("@soDienThoai", SoDT);
                cmdUpdateNhanVien.Parameters.AddWithValue("@NgaySinh", NgaySinh);
                int ketqua = 0;
                ketqua = cmdUpdateNhanVien.ExecuteNonQuery();
                if (ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi Sua Du Lieu: " + ex.Message);
                return 0;
            }
        }

        //lấy tất cả Hãng Sản Xuất
        public DataTable getDataFromHangSX()
        {
            DataTable dtHangSX = new DataTable();
            SqlCommand cmdgetHangSX = new SqlCommand("spGetAllHangSX", connectDB);
            cmdgetHangSX.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daSanPham = new SqlDataAdapter(cmdgetHangSX);
            daSanPham.Fill(dtHangSX);
            return dtHangSX;
        }

        //lấy tên hãng sản xuất theo Mã hãng sản xuất
        public string getTenHangSXByMa(string maHSX)
        {
            string TenHangSX = string.Empty;
            SqlCommand cmdgetTenHang = new SqlCommand("spSearchHangSX", connectDB);
            cmdgetTenHang.CommandType = CommandType.StoredProcedure;
            cmdgetTenHang.Parameters.AddWithValue("@keysearch", maHSX);
            SqlDataReader sreader;
            sreader = cmdgetTenHang.ExecuteReader();
            while(sreader.Read())
            {
                TenHangSX = sreader.GetValue(1).ToString();
            }
            sreader.Close();
            return TenHangSX;
        }

        //thêm hãng sản xuất
        public int insertHangSX(string MaHangSX, string TenHangSX, string DiaChi)
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdInsertHangSX = new SqlCommand("spInsertHangSX", connectDB);
                cmdInsertHangSX.CommandType = CommandType.StoredProcedure;
                cmdInsertHangSX.Parameters.AddWithValue("@maHSX", MaHangSX);
                cmdInsertHangSX.Parameters.AddWithValue("@tenHSX", TenHangSX);
                cmdInsertHangSX.Parameters.AddWithValue("@diachi", DiaChi);
                ketqua = cmdInsertHangSX.ExecuteNonQuery();
                if (ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi them du lieu " + ex.Message);
                return 0;
            }
        }

        //xóa hang sản xuất
        public int deleteHangSX(string MaHangSX)
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdDeleteHangSX = new SqlCommand("spDeleteHangSX", connectDB);
                cmdDeleteHangSX.CommandType = CommandType.StoredProcedure;
                cmdDeleteHangSX.Parameters.AddWithValue("@maHang", MaHangSX);
                ketqua = cmdDeleteHangSX.ExecuteNonQuery();
                if(ketqua > 0 )
                {
                    ketqua = 1;
                }
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi xoa du lieu " + ex.Message);
                return 0;
            }
        }
        
        //sửa thông tin hãng sản xuất
        public int updateHangSX(string MaHangSX, string TenHangSX, string DiaChi)
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdUpdateHangSX = new SqlCommand("spUpdateHangSX", connectDB);
                cmdUpdateHangSX.CommandType = CommandType.StoredProcedure;
                cmdUpdateHangSX.Parameters.AddWithValue("@maHSX", MaHangSX);
                cmdUpdateHangSX.Parameters.AddWithValue("@tenHSX", TenHangSX);
                cmdUpdateHangSX.Parameters.AddWithValue("@diachi", DiaChi);
                ketqua = cmdUpdateHangSX.ExecuteNonQuery();
                if(ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi Xoa san pham " + ex.Message);
                return 0;
            }
        }

        //tìm kiếm nhân viên
        public DataTable searchNhanVien(string key)
        {
            DataTable dtSNhanVien = new DataTable();
            SqlCommand cmdSearchNV = new SqlCommand("spSearchNhanVien", connectDB);
            cmdSearchNV.CommandType = CommandType.StoredProcedure;
            cmdSearchNV.Parameters.AddWithValue("@keysearch", key);
            SqlDataAdapter daSNhanVien = new SqlDataAdapter(cmdSearchNV);
            daSNhanVien.Fill(dtSNhanVien);
            return dtSNhanVien;
        }

        //tìm kiếm hãng Sản xuất
        public DataTable searchHangSX(string key)
        {
            DataTable dtSHangSanXuat = new DataTable();
            SqlCommand cmdSearchHangSX = new SqlCommand("spSearchHangSX", connectDB);
            cmdSearchHangSX.CommandType = CommandType.StoredProcedure;
            cmdSearchHangSX.Parameters.AddWithValue("@keysearch", key);
            SqlDataAdapter daSHangSX = new SqlDataAdapter(cmdSearchHangSX);
            daSHangSX.Fill(dtSHangSanXuat);
            return dtSHangSanXuat;
        }

        //tìm kiếm khách hàng
        public DataTable searchKhachHang(string key)
        {
            DataTable dtSKhachHang = new DataTable();
            SqlCommand cmdSearchKhachHang = new SqlCommand("spSearchKhachHang", connectDB);
            cmdSearchKhachHang.CommandType = CommandType.StoredProcedure;
            cmdSearchKhachHang.Parameters.AddWithValue("@keysearch", key);
            SqlDataAdapter daSKhachHang = new SqlDataAdapter(cmdSearchKhachHang);
            daSKhachHang.Fill(dtSKhachHang);
            return dtSKhachHang;
        }
        //tìm kiếm sản phẩm
        public DataTable searchSanPham(string key)
        {
            DataTable dtSSanPham = new DataTable();
            SqlCommand cmdSearchSanPham  = new SqlCommand("spSearchSanPham", connectDB);
            cmdSearchSanPham.CommandType = CommandType.StoredProcedure;
            cmdSearchSanPham.Parameters.AddWithValue("@keysearch", key);
            SqlDataAdapter daSKhachHang = new SqlDataAdapter(cmdSearchSanPham);
            daSKhachHang.Fill(dtSSanPham);
            return dtSSanPham;
        }
        //tim hoa don ban
        public string getMaKHbyMaHDB(string MaHDB)
        {
            string MaKH = string.Empty;
            SqlCommand cmdMaKH = new SqlCommand("spsearchHoaDonBan", connectDB);
            cmdMaKH.CommandType = CommandType.StoredProcedure;
            cmdMaKH.Parameters.AddWithValue("@keysearch", MaHDB);
            SqlDataReader sreader;
            sreader = cmdMaKH.ExecuteReader();
            while(sreader.Read())
            {
                MaKH = sreader.GetValue(3).ToString();
            }
            sreader.Close();
            return MaKH;
        }
        //lấy chi tiết hóa đơn bán
        public DataTable getDataFromChiTietHDB(string MaSP)
        {
            DataTable dtChiTietHDB = new DataTable();
            SqlCommand cmdChiTietHDB = new SqlCommand("spGetChiTietHDB", connectDB);
            cmdChiTietHDB.Parameters.AddWithValue("@maSP", MaSP);
            cmdChiTietHDB.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daChiTietHDB = new SqlDataAdapter(cmdChiTietHDB);
            daChiTietHDB.Fill(dtChiTietHDB);
            return dtChiTietHDB;
        }

        //ham kiem tra khoa chinh
        public bool CheckKeyMHDB(string sMa)
        {
            DataTable table = new DataTable();
            SqlCommand cmdCheckK = new SqlCommand("spCheckKeyHDB", connectDB);
            cmdCheckK.Parameters.AddWithValue("@MaHDB", sMa);
            cmdCheckK.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter Check = new SqlDataAdapter(cmdCheckK);
            Check.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }
        
        //them hoa don ban
        public int insertHoaDonBan(string MaHDB, string MaNV, string NgayBan, string MaKH, int TongTien)
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdInsertHoaDonBan = new SqlCommand("spInsertHoaDonBan", connectDB);
                cmdInsertHoaDonBan.CommandType = CommandType.StoredProcedure;
                cmdInsertHoaDonBan.Parameters.AddWithValue("@maHDB", MaHDB);
                cmdInsertHoaDonBan.Parameters.AddWithValue("@maNV", MaNV);
                cmdInsertHoaDonBan.Parameters.AddWithValue("@ngayBan", NgayBan);
                cmdInsertHoaDonBan.Parameters.AddWithValue("@maKH", MaKH);
                cmdInsertHoaDonBan.Parameters.AddWithValue("@tongtien", TongTien);
                ketqua = cmdInsertHoaDonBan.ExecuteNonQuery();
                if(ketqua > 0)
                {
                    ketqua = 1;
                }
               
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi Them du lieu: " + ex.Message);
                return 0;
            }
        }

        //xoa hoa don ban
        public int deleteHoaDonBan(string sMa)
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdDelHDB = new SqlCommand("spDeleteHoaDonBan", connectDB);
                cmdDelHDB.CommandType = CommandType.StoredProcedure;
                cmdDelHDB.Parameters.AddWithValue("@sMa", sMa);
                ketqua = cmdDelHDB.ExecuteNonQuery();
                if(ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi : " + ex.Message);
                return 0;
            }
        }

        //lay ten va gia san pharm theo ma san pham
        public string getNameSP(string sMa)
        {
            string tenSp = string.Empty;
            SqlCommand cmdName = new SqlCommand("spGetTenAndGiaSP", connectDB);
            cmdName.CommandType = CommandType.StoredProcedure;
            cmdName.Parameters.AddWithValue("@MaSP", sMa);
            SqlDataReader sreader;
            sreader = cmdName.ExecuteReader();
            while(sreader.Read())
            {
                tenSp = sreader.GetValue(0).ToString();
            }
            sreader.Close();
            return tenSp;
        }

        //lay gia san phaam
        public string getPriceSP(string sMa)
        {
            string giaSP = string.Empty;
            SqlCommand cmdPriceSP = new SqlCommand("spGetTenAndGiaSP", connectDB);
            cmdPriceSP.CommandType = CommandType.StoredProcedure;
            cmdPriceSP.Parameters.AddWithValue("@MaSP", sMa);
            SqlDataReader sreader;
            sreader = cmdPriceSP.ExecuteReader();
            while (sreader.Read())
            {
                giaSP = sreader.GetValue(1).ToString();
            }
            sreader.Close();
            return giaSP;
        }

        //lay ten nhan vien theo ma nhan vien
        public string getNameNhanVien(string sMaNV)
        {
            string TenNV = string.Empty;
            SqlCommand cmdNameNV = new SqlCommand("spGetNameNV", connectDB);
            cmdNameNV.CommandType = CommandType.StoredProcedure;
            cmdNameNV.Parameters.AddWithValue("@maNV", sMaNV);
            SqlDataReader sreader;
            sreader = cmdNameNV.ExecuteReader();
            while (sreader.Read())
            {
                TenNV = sreader.GetValue(1).ToString();
            }    
            sreader.Close();
            return TenNV;
        }
        //lay ten khach hang theo MaKH
        public int getSDTHbyMaKH(string sMaKH)
        {
            int SDT = 0;
            SqlCommand cmdGetSDT = new SqlCommand("spSearchKhachHang", connectDB);
            cmdGetSDT.CommandType = CommandType.StoredProcedure;
            cmdGetSDT.Parameters.AddWithValue("@keysearch", sMaKH);
            SqlDataReader sreader;
            sreader = cmdGetSDT.ExecuteReader();
            while(sreader.Read())
            {
                SDT = int.Parse(sreader.GetValue(3).ToString());
            }
            sreader.Close();
            return SDT;
           
        }

        //lay ten khach hang theo SDT
        public string getNameKhachHang(int SoDT)
        {
            string TenKH = string.Empty;
            SqlCommand cmdTenKH = new SqlCommand("spGetInfoKhachHang", connectDB);
            cmdTenKH.CommandType = CommandType.StoredProcedure;
            cmdTenKH.Parameters.AddWithValue("@soDT", SoDT);
            SqlDataReader sreader;
            sreader = cmdTenKH.ExecuteReader();
            while(sreader.Read())
            {
                TenKH = sreader.GetValue(1).ToString();
            }
            sreader.Close();
            return TenKH;
        }

        //lay dia chi khach hang theo ma
        public string getAddressKhachHang(int SoDT)
        {
            string DiaChiKH = string.Empty;
            SqlCommand cmdDiaChiKH = new SqlCommand("spGetInfoKhachHang", connectDB);
            cmdDiaChiKH.CommandType = CommandType.StoredProcedure;
            cmdDiaChiKH.Parameters.AddWithValue("@soDT", SoDT);
            SqlDataReader sreader;
            sreader = cmdDiaChiKH.ExecuteReader();
            while (sreader.Read())
            {
                DiaChiKH = sreader.GetValue(2).ToString();
            }
            sreader.Close();
            return DiaChiKH;
        }
        //lay so dien thoai khach hang theo ma
        public string getPhoneKhachHang(string sMaKH)
        {
            string SoDTKH = string.Empty;
            SqlCommand cmdSoDTKH = new SqlCommand("spSearchKhachHang", connectDB);
            cmdSoDTKH.CommandType = CommandType.StoredProcedure;
            cmdSoDTKH.Parameters.AddWithValue("@keysearch", sMaKH);
            SqlDataReader sreader;
            sreader = cmdSoDTKH.ExecuteReader();
            while (sreader.Read())
            {
                SoDTKH = sreader.GetValue(3).ToString();
            }
            sreader.Close();
            return SoDTKH;
        }

        //lay ma khach hang theo ten khach hang

        public string getMaKhachHang(int SoDT)
        {
            string MaKH = string.Empty;
            SqlCommand cmdMaKH = new SqlCommand("spGetInfoKhachHang", connectDB);
            cmdMaKH.CommandType = CommandType.StoredProcedure;
            cmdMaKH.Parameters.AddWithValue("@soDT", SoDT);
            SqlDataReader sreader;
            sreader = cmdMaKH.ExecuteReader();
            while (sreader.Read())
            {
                MaKH = sreader.GetValue(0).ToString();
            }
            sreader.Close();
            return MaKH;
        }

        //them chi tiet hoa don ban
        public int insertChiTietHDB(string sMaHDB, string sMaSP, int SoLuong, int DonGia, int GiamGia,int ThanhTien )
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdInsertChiTietHDB = new SqlCommand("spInsertChiTietHDB", connectDB);
                cmdInsertChiTietHDB.CommandType = CommandType.StoredProcedure;
                cmdInsertChiTietHDB.Parameters.AddWithValue("@maHDB", sMaHDB);
                cmdInsertChiTietHDB.Parameters.AddWithValue("@maSP", sMaSP);
                cmdInsertChiTietHDB.Parameters.AddWithValue("@soLuong", SoLuong);
                cmdInsertChiTietHDB.Parameters.AddWithValue("@donGia", DonGia);
                cmdInsertChiTietHDB.Parameters.AddWithValue("@giamGia", GiamGia);
                cmdInsertChiTietHDB.Parameters.AddWithValue("@thanhTien", ThanhTien);
                ketqua = cmdInsertChiTietHDB.ExecuteNonQuery();
                if(ketqua > 0 )
                {
                    ketqua = 1;
                }
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi: " + ex.Message);
                return 0;
            }
        }

        //lay tat ca san pham trong bang chitietHoaDon theo MaHDB
        public DataTable getChiTietHD(string sMaHD)
        {
            DataTable dtChiTiet = new DataTable();
            SqlCommand cmdChiTiet = new SqlCommand("spGetCTHDBByMaHDB", connectDB);
            cmdChiTiet.CommandType = CommandType.StoredProcedure;
            cmdChiTiet.Parameters.AddWithValue("@maHDB", sMaHD);
            SqlDataAdapter daChiTiet = new SqlDataAdapter(cmdChiTiet);
            daChiTiet.Fill(dtChiTiet);
            return dtChiTiet;
        }

        //xoa chi tiet hoa don theo maHDB va maSP
        public int deleteChiTietHDB(string MaSP,string MaHDB)
        {
            try
            {
                int ketqua = 0;
                SqlCommand cmdDeleteCTHDB = new SqlCommand("spDeleteSPCTHDB", connectDB);
                cmdDeleteCTHDB.CommandType = CommandType.StoredProcedure;
                cmdDeleteCTHDB.Parameters.AddWithValue("@maSP", MaSP);
                cmdDeleteCTHDB.Parameters.AddWithValue("@maHDB", MaHDB);
                ketqua = cmdDeleteCTHDB.ExecuteNonQuery();
                if(ketqua > 0)
                {
                    ketqua = 1;
                }
                return ketqua;
            }catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Loi: " + ex.Message);
                return 0;
            }
        }

        //lay thong tin Hoa Don
        //lay ma nhan vien theo hoa don ban
        public string getMaHDB(string sMaKH)
        {
            string sMaHDB = string.Empty;
            SqlCommand cmdgetMV = new SqlCommand("spGetMaHDB", connectDB);
            cmdgetMV.CommandType = CommandType.StoredProcedure;
            cmdgetMV.Parameters.AddWithValue("@maKH", sMaKH);
            SqlDataReader sreader;
            sreader = cmdgetMV.ExecuteReader();
            while(sreader.Read())
            {
                sMaHDB = sreader.GetValue(0).ToString();
            }
            sreader.Close();
            return sMaHDB;
        }

        //lay ngay ban
        public string getNgayHDB(string sMaKH)
        {
            string NgayBan = string.Empty;
            SqlCommand cmdGetNgay = new SqlCommand("spGetMaHDB", connectDB);
            cmdGetNgay.CommandType = CommandType.StoredProcedure;
            cmdGetNgay.Parameters.AddWithValue("@maKH", sMaKH);
            SqlDataReader sreader;
            sreader = cmdGetNgay.ExecuteReader();
            while (sreader.Read())
            {
                NgayBan = sreader.GetValue(2).ToString();
            }
            sreader.Close();
            return NgayBan;
        }

        //lay ma hoa don theo thu tu giam dan
        public DataTable getMaHDBDESC()
        {
            DataTable dtMaHDB = new DataTable();
            SqlCommand cmdGetMaHDB = new SqlCommand("spGetKeyHDB", connectDB);
            cmdGetMaHDB.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter daDtMaHDB = new SqlDataAdapter(cmdGetMaHDB);
            daDtMaHDB.Fill(dtMaHDB);
            return dtMaHDB;
        }


    }
}
