using Project_Dien_Thoai.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_Dien_Thoai.Controllers
{
    public class LogoutController : Controller
    {
        private Project_Dien_ThoaiDBContexxt db = new Project_Dien_ThoaiDBContexxt();
        // GET: Logout
        public ActionResult Index()
        {
            ViewBag.Error = "";
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection field)
        {
            string strError = "";
            string MaKH = rd_MaKH();
            string userName = field["TenDN"];
            string fullname = field["fullname"];
            string email = field["email"];
            string matkhau = field["MatKhau"];
            string LTaiKhoan = "Khách Hàng";
            string con_matkhau = field["confirmation_pwd"];
            if (kt_TenDN(userName,email)==false)
            {
                if (matkhau == con_matkhau)
                {
                    TaiKhoan Insert_tk = new TaiKhoan();
                    Insert_tk.TenDN = userName;
                    Insert_tk.Email = email;
                    Insert_tk.MatKhau = matkhau;
                    Insert_tk.LoaiTaiKhoan = LTaiKhoan;
                    db.TaiKhoans.Add(Insert_tk);
                    db.SaveChanges();
                    string tendn = Insert_tk.TenDN;
                    KhachHang Insert_kh = new KhachHang();
                    Insert_kh.MaKH = MaKH;
                    Insert_kh.TenDN = tendn;
                    Insert_kh.TenKH = fullname;
                    Insert_kh.DiaChiKH = "";
                    Insert_kh.GioiTinh = "";
                    Insert_kh.EmailKH = email;
                    Insert_kh.NgaySinh = DateTime.Now;
                    Insert_kh.SODTKH = 0;
                    db.KhachHangs.Add(Insert_kh);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Authen");
                }
                else
                    strError = "Mật Khẩu Không Khớp, Vui Lòng Nhập lại Mật Khẩu";
            }
            else
                strError = "Tên Đăng Nhập hoặc Email đã tồn tại, vui lòng kiểm tra lại";
            ViewBag.Error = strError;
            return View();
        }
        public string rd_MaKH()
        {
            Random rd = new Random();
            string R;
            string makh;
            do
            {
                R = rd.Next(1, 9999999).ToString();
                makh = "KH_" + R;
            } while (kt_MaKH(makh) == true);
            return makh;
        }
        public bool kt_MaKH(string makh)
        {

            var kh = from KH in db.KhachHangs where KH.MaKH.Equals(makh) select KH;
            if (kh.Any())
                return true;
            else
                return false; 
        }
        public bool kt_TenDN(string tendn, string email)
        {
            var tk = from TK in db.TaiKhoans where TK.TenDN.Equals(tendn) || TK.Email.Equals(email) select TK;
            if (tk.Any())
                return true;
            else
                return false;
        }
            
    }
}