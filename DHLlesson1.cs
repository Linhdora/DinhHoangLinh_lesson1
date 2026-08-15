using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DHLlesson1
{
    internal class SinhVien
    {
        public string MaSinhVien { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
        public string ChuyenNganh { get; set; }
        public double DiemTrungBinh { get; set; }
        public string TrangThaiHocTap { get; set; }

        internal SinhVien(
            string maSinhVien,
            string hoTen,
            DateTime ngaySinh,
            bool gioiTinh,
            string email,
            string soDienThoai,
            string chuyenNganh,
            double diemTrungBinh,
            string trangThaiHocTap)
        {
            MaSinhVien = maSinhVien;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Email = email;
            SoDienThoai = soDienThoai;
            ChuyenNganh = chuyenNganh;
            DiemTrungBinh = diemTrungBinh;
            TrangThaiHocTap = trangThaiHocTap;
        }

        internal void HienThi()
        {
            Console.WriteLine("Ma sinh vien: " + MaSinhVien);
            Console.WriteLine("Ho ten: " + HoTen);
            Console.WriteLine("Ngay sinh: " + NgaySinh.ToString("dd/MM/yyyy"));
            Console.WriteLine("Gioi tinh: " + (GioiTinh ? "Nam" : "Nu"));
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("So dien thoai: " + SoDienThoai);
            Console.WriteLine("Chuyen nganh: " + ChuyenNganh);
            Console.WriteLine("Diem trung binh: " + DiemTrungBinh);
            Console.WriteLine("Trang thai hoc tap: " + TrangThaiHocTap);
        }
    }

    internal class QuanLySinhVien
    {
        private List<SinhVien> DanhSachSinhVien = new List<SinhVien>();

        // Them sinh vien vao danh sach
        internal void ThemSinhVien(
            string maSinhVien,
            string hoTen,
            DateTime ngaySinh,
            bool gioiTinh,
            string email,
            string soDienThoai,
            string chuyenNganh,
            double diemTrungBinh,
            string trangThaiHocTap)
        {
            SinhVien sinhVien = new SinhVien(
                maSinhVien,
                hoTen,
                ngaySinh,
                gioiTinh,
                email,
                soDienThoai,
                chuyenNganh,
                diemTrungBinh,
                trangThaiHocTap
            );
            DanhSachSinhVien.Add(sinhVien);
        }

        // Hien thi danh sach sinh vien
        internal void HienThiDanhSach()
        {
            foreach (SinhVien sinhVien in DanhSachSinhVien)
            {
                sinhVien.HienThi();
                Console.WriteLine("-------------------------");
            }
        }

        // Tim sinh vien theo ma sinh vien
        internal SinhVien TimSinhVienTheoMa(string maSinhVien)
        {
            return DanhSachSinhVien.Find(sv => sv.MaSinhVien == maSinhVien);
        }

        // Tim sinh vien theo ho ten (gan dung)
        internal List<SinhVien> TimHoTenSinhVien(string hoTen)
        {
            return DanhSachSinhVien.FindAll(sv => sv.HoTen.Contains(hoTen));
        }

        // Cap nhat thong tin sinh vien theo ma sinh vien (khong doi ma)
        internal bool CapNhatSinhVien(
            string maSinhVien,
            string hoTen,
            DateTime ngaySinh,
            bool gioiTinh,
            string email,
            string soDienThoai,
            string chuyenNganh,
            double diemTrungBinh,
            string trangThaiHocTap)
        {
            SinhVien sinhVien = TimSinhVienTheoMa(maSinhVien);
            if (sinhVien != null)
            {
                sinhVien.HoTen = hoTen;
                sinhVien.NgaySinh = ngaySinh;
                sinhVien.GioiTinh = gioiTinh;
                sinhVien.Email = email;
                sinhVien.SoDienThoai = soDienThoai;
                sinhVien.ChuyenNganh = chuyenNganh;
                sinhVien.DiemTrungBinh = diemTrungBinh;
                sinhVien.TrangThaiHocTap = trangThaiHocTap;
                return true;
            }
            return false;
        }

        // Xoa sinh vien theo ma sinh vien
        internal bool XoaSinhVien(string maSinhVien)
        {
            SinhVien sinhVien = TimSinhVienTheoMa(maSinhVien);
            if (sinhVien != null)
            {
                DanhSachSinhVien.Remove(sinhVien);
                return true;
            }
            return false;
        }

        // Sap xep sinh vien theo ten
        internal List<SinhVien> SapXepSinhVienTheoTen()
        {
            List<SinhVien> ketQua = new List<SinhVien>(DanhSachSinhVien);
            ketQua.Sort((sv1, sv2) => sv1.HoTen.CompareTo(sv2.HoTen));
            return ketQua;
        }

        // Hien thi sinh vien co diem tu 8 tro len
        internal List<SinhVien> HienThiSinhVienGioi()
        {
            return DanhSachSinhVien.FindAll(sv => sv.DiemTrungBinh >= 8);
        }

        // Hien thi sinh vien co diem cao nhat
        internal List<SinhVien> HienThiSinhVienDiemCaoNhat()
        {
            if (DanhSachSinhVien.Count == 0)
            {
                return new List<SinhVien>();
            }

            double diemCaoNhat = double.MinValue;
            foreach (SinhVien sinhVien in DanhSachSinhVien)
            {
                if (sinhVien.DiemTrungBinh > diemCaoNhat)
                {
                    diemCaoNhat = sinhVien.DiemTrungBinh;
                }
            }
            return DanhSachSinhVien.FindAll(sv => sv.DiemTrungBinh == diemCaoNhat);
        }

        // Diem trung binh cua tat ca sinh vien
        internal double TinhDiemTrungBinhCuaTatCaSinhVien()
        {
            if (DanhSachSinhVien.Count == 0)
            {
                return 0;
            }

            double tongDiem = 0;
            foreach (SinhVien sinhVien in DanhSachSinhVien)
            {
                tongDiem += sinhVien.DiemTrungBinh;
            }
            return tongDiem / DanhSachSinhVien.Count;
        }

        // Thong ke sinh vien theo nganh
        internal Dictionary<string, int> ThongKeSinhVienTheoNganh()
        {
            Dictionary<string, int> ketQua = new Dictionary<string, int>();
            foreach (SinhVien sinhVien in DanhSachSinhVien)
            {
                if (ketQua.ContainsKey(sinhVien.ChuyenNganh))
                {
                    ketQua[sinhVien.ChuyenNganh]++;
                }
                else
                {
                    ketQua[sinhVien.ChuyenNganh] = 1;
                }
            }
            return ketQua;
        }

        // Thong ke sinh vien theo trang thai hoc tap
        internal Dictionary<string, int> ThongKeSinhVienTheoTrangThaiHocTap()
        {
            Dictionary<string, int> ketQua = new Dictionary<string, int>();
            foreach (SinhVien sinhVien in DanhSachSinhVien)
            {
                if (ketQua.ContainsKey(sinhVien.TrangThaiHocTap))
                {
                    ketQua[sinhVien.TrangThaiHocTap]++;
                }
                else
                {
                    ketQua[sinhVien.TrangThaiHocTap] = 1;
                }
            }
            return ketQua;
        }
    }

    internal class Program
    {
        // Doc va kiem tra ngay sinh, tranh crash khi nguoi dung nhap sai dinh dang
        static DateTime NhapNgaySinh()
        {
            DateTime ngaySinh;
            Console.Write("Nhap ngay sinh (dd/MM/yyyy): ");
            while (!DateTime.TryParseExact(
                        Console.ReadLine() ?? "",
                        "dd/MM/yyyy",
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out ngaySinh))
            {
                Console.Write("Ngay sinh khong hop le, vui long nhap lai (dd/MM/yyyy): ");
            }
            return ngaySinh;
        }

        // Doc gioi tinh, bat nguoi dung nhap lai neu sai
        static bool NhapGioiTinh()
        {
            Console.Write("Nhap gioi tinh (Nam/Nu): ");
            while (true)
            {
                string gioiTinhInput = Console.ReadLine() ?? "";
                if (string.Equals(gioiTinhInput, "Nam", StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                if (string.Equals(gioiTinhInput, "Nu", StringComparison.OrdinalIgnoreCase))
                {
                    return false;
                }
                Console.Write("Gioi tinh khong hop le, vui long nhap lai (Nam/Nu): ");
            }
        }

        // Doc email hop le
        static string NhapEmail()
        {
            Console.Write("Nhap email: ");
            string email;
            do
            {
                email = Console.ReadLine() ?? "";
                if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    Console.Write("Email khong hop le, vui long nhap lai: ");
                    continue;
                }
                break;
            } while (true);
            return email;
        }

        // Doc diem trung binh hop le (0-10)
        static double NhapDiemTrungBinh()
        {
            Console.Write("Nhap diem trung binh: ");
            double diemTrungBinh;
            do
            {
                if (!double.TryParse(Console.ReadLine(), out diemTrungBinh) || diemTrungBinh < 0 || diemTrungBinh > 10)
                {
                    Console.Write("Diem trung binh khong hop le, vui long nhap lai (0-10): ");
                    continue;
                }
                break;
            } while (true);
            return diemTrungBinh;
        }

        // Nhap thong tin cho THEM MOI sinh vien - co kiem tra trung ma
        static SinhVien NhapThongTinThemMoi(QuanLySinhVien quanLy)
        {
            string maSinhVien;
            do
            {
                Console.Write("Nhap ma sinh vien: ");
                maSinhVien = Console.ReadLine() ?? "";
                if (string.IsNullOrEmpty(maSinhVien))
                {
                    Console.WriteLine("Ma sinh vien khong duoc de trong, vui long nhap lai");
                    continue;
                }
                if (quanLy.TimSinhVienTheoMa(maSinhVien) != null)
                {
                    Console.WriteLine("Ma sinh vien da ton tai, vui long nhap lai");
                    continue;
                }
                break;
            } while (true);

            string hoTen;
            do
            {
                Console.Write("Nhap ho ten: ");
                hoTen = Console.ReadLine() ?? "";
                if (string.IsNullOrEmpty(hoTen))
                {
                    Console.WriteLine("Ho ten khong duoc de trong, vui long nhap lai");
                    continue;
                }
                break;
            } while (true);

            DateTime ngaySinh = NhapNgaySinh();
            bool gioiTinh = NhapGioiTinh();
            string email = NhapEmail();

            Console.Write("Nhap so dien thoai: ");
            string soDienThoai = Console.ReadLine() ?? "";

            Console.Write("Nhap chuyen nganh: ");
            string chuyenNganh = Console.ReadLine() ?? "";

            double diemTrungBinh = NhapDiemTrungBinh();

            Console.Write("Nhap trang thai hoc tap: ");
            string trangThaiHocTap = Console.ReadLine() ?? "";

            return new SinhVien(
                maSinhVien,
                hoTen,
                ngaySinh,
                gioiTinh,
                email,
                soDienThoai,
                chuyenNganh,
                diemTrungBinh,
                trangThaiHocTap
            );
        }

        // Nhap thong tin CAP NHAT cho sinh vien da ton tai - KHONG hoi/kiem tra lai ma sinh vien
        static SinhVien NhapThongTinCapNhat(string maSinhVien)
        {
            string hoTen;
            do
            {
                Console.Write("Nhap ho ten: ");
                hoTen = Console.ReadLine() ?? "";
                if (string.IsNullOrEmpty(hoTen))
                {
                    Console.WriteLine("Ho ten khong duoc de trong, vui long nhap lai");
                    continue;
                }
                break;
            } while (true);

            DateTime ngaySinh = NhapNgaySinh();
            bool gioiTinh = NhapGioiTinh();
            string email = NhapEmail();

            Console.Write("Nhap so dien thoai: ");
            string soDienThoai = Console.ReadLine() ?? "";

            Console.Write("Nhap chuyen nganh: ");
            string chuyenNganh = Console.ReadLine() ?? "";

            double diemTrungBinh = NhapDiemTrungBinh();

            Console.Write("Nhap trang thai hoc tap: ");
            string trangThaiHocTap = Console.ReadLine() ?? "";

            return new SinhVien(
                maSinhVien,
                hoTen,
                ngaySinh,
                gioiTinh,
                email,
                soDienThoai,
                chuyenNganh,
                diemTrungBinh,
                trangThaiHocTap
            );
        }

        static void Main(string[] args)
        {
            QuanLySinhVien quanLy = new QuanLySinhVien();

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Chuong trinh quan ly sinh vien");
                Console.WriteLine("1. Them sinh vien");
                Console.WriteLine("2. Hien thi danh sach sinh vien");
                Console.WriteLine("3. Tim sinh vien theo ma");
                Console.WriteLine("4. Tim sinh vien theo ho ten");
                Console.WriteLine("5. Cap nhat thong tin sinh vien");
                Console.WriteLine("6. Xoa sinh vien");
                Console.WriteLine("7. Sap xep sinh vien theo ten");
                Console.WriteLine("8. Hien thi sinh vien co diem tu 8 tro len");
                Console.WriteLine("9. Hien thi sinh vien co diem cao nhat");
                Console.WriteLine("10. Tinh diem trung binh cua tat ca sinh vien");
                Console.WriteLine("11. Thong ke sinh vien theo nganh");
                Console.WriteLine("12. Thong ke sinh vien theo trang thai hoc tap");
                Console.WriteLine("0. Thoat chuong trinh");
                Console.Write("Nhap lua chon cua ban: ");
                if (!int.TryParse(Console.ReadLine(), out int luaChon))
                {
                    Console.WriteLine("Lua chon khong hop le");
                    continue;
                }

                switch (luaChon)
                {
                    case 1:
                        {
                            SinhVien sinhVien = NhapThongTinThemMoi(quanLy);
                            quanLy.ThemSinhVien(
                                sinhVien.MaSinhVien,
                                sinhVien.HoTen,
                                sinhVien.NgaySinh,
                                sinhVien.GioiTinh,
                                sinhVien.Email,
                                sinhVien.SoDienThoai,
                                sinhVien.ChuyenNganh,
                                sinhVien.DiemTrungBinh,
                                sinhVien.TrangThaiHocTap
                            );
                            Console.WriteLine("Them sinh vien thanh cong");
                            break;
                        }
                    case 2:
                        Console.WriteLine("Danh sach sinh vien: ");
                        quanLy.HienThiDanhSach();
                        break;
                    case 3:
                        {
                            Console.Write("Nhap ma sinh vien can tim: ");
                            string maSinhVienTim = Console.ReadLine() ?? "";
                            SinhVien sinhVienTim = quanLy.TimSinhVienTheoMa(maSinhVienTim);
                            if (sinhVienTim != null)
                            {
                                sinhVienTim.HienThi();
                            }
                            else
                            {
                                Console.WriteLine("Khong tim thay sinh vien co ma: " + maSinhVienTim);
                            }
                            break;
                        }
                    case 4:
                        {
                            Console.Write("Nhap ho ten sinh vien can tim: ");
                            string hoTenTim = Console.ReadLine() ?? "";
                            List<SinhVien> danhSachTim = quanLy.TimHoTenSinhVien(hoTenTim);
                            if (danhSachTim.Count > 0)
                            {
                                foreach (SinhVien sv in danhSachTim)
                                {
                                    sv.HienThi();
                                    Console.WriteLine("-------------------------");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Khong tim thay sinh vien co ho ten: " + hoTenTim);
                            }
                            break;
                        }
                    case 5:
                        {
                            Console.Write("Nhap ma sinh vien can cap nhat: ");
                            string maSinhVienCapNhat = Console.ReadLine() ?? "";
                            SinhVien sinhVienCapNhat = quanLy.TimSinhVienTheoMa(maSinhVienCapNhat);
                            if (sinhVienCapNhat != null)
                            {
                                Console.WriteLine("Nhap thong tin moi cho sinh vien:");
                                SinhVien sinhVienMoi = NhapThongTinCapNhat(maSinhVienCapNhat);
                                quanLy.CapNhatSinhVien(
                                    maSinhVienCapNhat,
                                    sinhVienMoi.HoTen,
                                    sinhVienMoi.NgaySinh,
                                    sinhVienMoi.GioiTinh,
                                    sinhVienMoi.Email,
                                    sinhVienMoi.SoDienThoai,
                                    sinhVienMoi.ChuyenNganh,
                                    sinhVienMoi.DiemTrungBinh,
                                    sinhVienMoi.TrangThaiHocTap
                                );
                                Console.WriteLine("Cap nhat sinh vien thanh cong");
                            }
                            else
                            {
                                Console.WriteLine("Khong tim thay sinh vien co ma: " + maSinhVienCapNhat);
                            }
                            break;
                        }
                    case 6:
                        {
                            Console.Write("Nhap ma sinh vien can xoa: ");
                            string maSinhVienXoa = Console.ReadLine() ?? "";
                            if (quanLy.XoaSinhVien(maSinhVienXoa))
                            {
                                Console.WriteLine("Xoa sinh vien thanh cong");
                            }
                            else
                            {
                                Console.WriteLine("Khong tim thay sinh vien co ma: " + maSinhVienXoa);
                            }
                            break;
                        }
                    case 7:
                        {
                            List<SinhVien> danhSachSapXep = quanLy.SapXepSinhVienTheoTen();
                            Console.WriteLine("Danh sach sinh vien sau khi sap xep theo ten: ");
                            foreach (SinhVien sv in danhSachSapXep)
                            {
                                sv.HienThi();
                                Console.WriteLine("-------------------------");
                            }
                            break;
                        }
                    case 8:
                        {
                            List<SinhVien> danhSachGioi = quanLy.HienThiSinhVienGioi();
                            Console.WriteLine("Danh sach sinh vien co diem tu 8 tro len: ");
                            foreach (SinhVien sv in danhSachGioi)
                            {
                                sv.HienThi();
                                Console.WriteLine("-------------------------");
                            }
                            break;
                        }
                    case 9:
                        {
                            List<SinhVien> danhSachDiemCaoNhat = quanLy.HienThiSinhVienDiemCaoNhat();
                            Console.WriteLine("Danh sach sinh vien co diem cao nhat: ");
                            foreach (SinhVien sv in danhSachDiemCaoNhat)
                            {
                                sv.HienThi();
                                Console.WriteLine("-------------------------");
                            }
                            break;
                        }
                    case 10:
                        {
                            double diemTrungBinhTatCa = quanLy.TinhDiemTrungBinhCuaTatCaSinhVien();
                            Console.WriteLine("Diem trung binh cua tat ca sinh vien: " + diemTrungBinhTatCa);
                            break;
                        }
                    case 11:
                        {
                            Dictionary<string, int> thongKeTheoNganh = quanLy.ThongKeSinhVienTheoNganh();
                            Console.WriteLine("Thong ke sinh vien theo nganh: ");
                            foreach (var item in thongKeTheoNganh)
                            {
                                Console.WriteLine("Nganh: " + item.Key + ", So luong: " + item.Value);
                            }
                            break;
                        }
                    case 12:
                        {
                            Dictionary<string, int> thongKeTheoTrangThai = quanLy.ThongKeSinhVienTheoTrangThaiHocTap();
                            Console.WriteLine("Thong ke sinh vien theo trang thai hoc tap: ");
                            foreach (var item in thongKeTheoTrangThai)
                            {
                                Console.WriteLine("Trang thai: " + item.Key + ", So luong: " + item.Value);
                            }
                            break;
                        }
                    case 0:
                        Console.WriteLine("Thoat chuong trinh");
                        return;
                    default:
                        Console.WriteLine("Lua chon khong hop le");
                        break;
                }
            }
        }
    }
}