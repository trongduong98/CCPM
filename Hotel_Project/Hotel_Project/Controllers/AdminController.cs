using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Models.Entities;
using QuanLyKhachSan.Models.ViewModels;
using QuanLyKhachSan.Models.Functions;
using System.Web.Security;
using System.IO;

namespace QuanLyKhachSan.Controllers
{
    public class AdminController : Controller
    {
        private ModelQLKS db = new ModelQLKS();
        private int MaxPhanTuMoiTrang = 8;

        // GET

        [Authorize]
        public ActionResult DSTaiKhoan()
        {
            var list = db.TaiKhoans.ToList();
            int TongPhanTu = list.Count;
            int SoTrang = (TongPhanTu - 1) / MaxPhanTuMoiTrang + 1;
            int Trang = 1;
            try
            {
                string id = RouteData.Values["id"].ToString();
                Trang = Convert.ToInt16(id);
                if (Trang > SoTrang) Trang = SoTrang;
            }
            catch (Exception e) { }
            int PhanTuDau = (Trang - 1) * MaxPhanTuMoiTrang;
            int SoPhanTu = MaxPhanTuMoiTrang;
            if (Trang == SoTrang) SoPhanTu = TongPhanTu - (SoTrang - 1) * MaxPhanTuMoiTrang;
            var listMoiTrang = list.GetRange(PhanTuDau, SoPhanTu);
            ViewBag.STT = PhanTuDau;
            ViewBag.Trang = Trang;
            ViewBag.SoTrang = SoTrang;
            return View(listMoiTrang);
        }

        [Authorize]
        public ActionResult DangXuat()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("TrangChu", "TrangChu");
        }

        [Authorize]
        public ActionResult DSLoaiPhong()
        {
            var list = db.LoaiPhongs.ToList();
            return View(list);
        }

        [Authorize]
        public ActionResult DSPhong()
        {
            var list = db.Phongs.ToList();
            int TongPhanTu = list.Count;
            int SoTrang = (TongPhanTu - 1) / MaxPhanTuMoiTrang + 1;
            int Trang = 1;
            try
            {
                string id = RouteData.Values["id"].ToString();
                Trang = Convert.ToInt16(id);
                if (Trang > SoTrang) Trang = SoTrang;
            }
            catch (Exception e) { }
            int PhanTuDau = (Trang - 1) * MaxPhanTuMoiTrang;
            int SoPhanTu = MaxPhanTuMoiTrang;
            if (Trang == SoTrang) SoPhanTu = TongPhanTu - (SoTrang - 1) * MaxPhanTuMoiTrang;
            var listMoiTrang = list.GetRange(PhanTuDau, SoPhanTu);
            ViewBag.STT = PhanTuDau;
            ViewBag.Trang = Trang;
            ViewBag.SoTrang = SoTrang;
            return View(listMoiTrang);
        }

        [Authorize]
        public ActionResult DSDichVu()
        {
            var list = db.DichVus.ToList();
            int TongPhanTu = list.Count;
            int SoTrang = (TongPhanTu - 1) / MaxPhanTuMoiTrang + 1;
            int Trang = 1;
            try
            {
                string id = RouteData.Values["id"].ToString();
                Trang = Convert.ToInt16(id);
                if (Trang > SoTrang) Trang = SoTrang;
            }
            catch (Exception e) { }
            int PhanTuDau = (Trang - 1) * MaxPhanTuMoiTrang;
            int SoPhanTu = MaxPhanTuMoiTrang;
            if (Trang == SoTrang) SoPhanTu = TongPhanTu - (SoTrang - 1) * MaxPhanTuMoiTrang;
            var listMoiTrang = list.GetRange(PhanTuDau, SoPhanTu);
            ViewBag.STT = PhanTuDau;
            ViewBag.Trang = Trang;
            ViewBag.SoTrang = SoTrang;
            return View(listMoiTrang);
        }

        // Thêm
        [Authorize]
        public ActionResult ThemTaiKhoan()
        {
            return View(new TaiKhoan());
        }

        [Authorize]
        public ActionResult ThemLoaiPhong()
        {
            return View(new LoaiPhong());
        }

        [Authorize]
        public ActionResult ThemPhong()
        {
            ViewBag.listLoaiPhong = db.LoaiPhongs.ToList();
            return View(new Phong());
        }

        [Authorize]
        public ActionResult ThemDichVu()
        {
            return View(new DichVu());
        }

        // Sửa
        [Authorize]
        public ActionResult SuaTaiKhoan()
        {
            string TenTaiKhoan = RouteData.Values["id"].ToString();
            var taiKhoan = db.TaiKhoans.Find(TenTaiKhoan);
            return View(taiKhoan);
        }

        [Authorize]
        public ActionResult SuaLoaiPhong()
        {
            string MaLoai = RouteData.Values["id"].ToString();
            var loaiPhong = db.LoaiPhongs.Find(MaLoai);
            return View(loaiPhong);
        }

        [Authorize]
        public ActionResult SuaPhong()
        {
            string MaPhong = RouteData.Values["id"].ToString();
            ViewBag.listLoaiPhong = db.LoaiPhongs.ToList();
            var phong = db.Phongs.Find(MaPhong);
            return View(phong);
        }

        [Authorize]
        public ActionResult SuaDichVu()
        {
            string MaDichVu = RouteData.Values["id"].ToString();
            var dichVu = db.DichVus.Find(MaDichVu);
            return View(dichVu);
        }

        // Xóa
        [Authorize]
        public ActionResult XoaTaiKhoan()
        {
            string TenTaiKhoan = RouteData.Values["id"].ToString();
            // Trước khi xóa Tài Khoản phải xóa thông tin đặt phòng
            var listDatPhong = db.DatPhongs.Where(m => m.TenTaiKhoan == TenTaiKhoan).ToList();
            var HamDP = new HamDatPhong();
            foreach (DatPhong dp in listDatPhong)
                HamDP.Delete(dp.MaDatPhong);
            // Sau đó mới xóa Tài Khoản
            var HamTK = new HamTaiKhoan();
            HamTK.Delete(TenTaiKhoan);
            return RedirectToAction("DSTaiKhoan", "Admin");
        }

        [Authorize]
        public ActionResult XoaLoaiPhong()
        {
            string MaLoai = RouteData.Values["id"].ToString();
            // Trước khi xóa Loại Phòng phải xóa các Phòng liên quan
            // Nhưng muốn xóa Phòng phải xóa Đặt Phòng trước
            var listPhong = db.Phongs.Where(m => m.MaLoai == MaLoai).ToList();
            var HamP = new HamPhong();
            var HamDP = new HamDatPhong();
            foreach (Phong p in listPhong)
            {
                var listDatPhong = db.DatPhongs.Where(m => m.MaPhong == p.MaPhong).ToList();
                foreach (DatPhong dp in listDatPhong)
                    HamDP.Delete(dp.MaDatPhong);
                HamP.Delete(p.MaPhong);
            }
            // Sau đó mới xóa Loại Phòng
            var HamLP = new HamLoaiPhong();
            HamLP.Delete(MaLoai);
            return RedirectToAction("DSLoaiPhong", "Admin");
        }

        [Authorize]
        public ActionResult XoaPhong()
        {
            string MaPhong = RouteData.Values["id"].ToString();
            // Trước khi xóa Phòng phải xóa thông tin đặt phòng
            var listDatPhong = db.DatPhongs.Where(m => m.MaPhong == MaPhong).ToList();
            var HamDP = new HamDatPhong();
            foreach (DatPhong dp in listDatPhong)
                HamDP.Delete(dp.MaDatPhong);
            // Sau đó mới xóa Phòng
            var HamP = new HamPhong();
            HamP.Delete(MaPhong);
            return RedirectToAction("DSPhong", "Admin");
        }

        [Authorize]
        public ActionResult XoaDichVu()
        {
            string MaDichVu = RouteData.Values["id"].ToString();
            var HamDV = new HamDichVu();
            HamDV.Delete(MaDichVu);
            return RedirectToAction("DSDichVu", "Admin");
        }

        // POST

        [Authorize]
        [HttpPost]
        public ActionResult ThemTaiKhoan(TaiKhoan tk)
        {
            if (ModelState.IsValid)
            {
                var taiKhoan = db.TaiKhoans.Find(tk.TenTaiKhoan);
                if (taiKhoan != null)
                {
                    ModelState.AddModelError("TenTaiKhoan", "Tài Khoản đã tồn tại");
                    return View(tk);
                }
                var hTK = new HamTaiKhoan();
                hTK.Insert(tk);
                return RedirectToAction("DSTaiKhoan", "Admin");
            }
            return View(tk);
        }

        [Authorize]
        

        [HttpPost]
        public ActionResult ThemDichVu(DichVu dv)
        {
            if (ModelState.IsValid)
            {
                var dichVu = db.DichVus.Find(dv.MaDichVu);
                if (dichVu != null)
                {
                    ModelState.AddModelError("MaDichVu", "Mã Dịch Vụ đã tồn tại");
                    return View(dv);
                }
                var hDV = new HamDichVu();
                hDV.Insert(dv);
                return RedirectToAction("DSDichVu", "Admin");
            }
            return View(dv);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SuaTaiKhoan(TaiKhoan tk)
        {
            if (ModelState.IsValid)
            {
                var HamTK = new HamTaiKhoan();
                HamTK.Update(tk);
                return RedirectToAction("DSTaiKhoan", "Admin");
            }
            return View(tk);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SuaLoaiPhong(LoaiPhong lp, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/images/"), fileName);
                    file.SaveAs(path);
                }
                var HamLP = new HamLoaiPhong();
                HamLP.Update(lp);
                return RedirectToAction("DSLoaiPhong", "Admin");
            }
            return View(lp);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SuaPhong(Phong p)
        {
            if (ModelState.IsValid)
            {
                var HamP = new HamPhong();
                HamP.Update(p);
                return RedirectToAction("DSPhong", "Admin");
            }
            ViewBag.listLoaiPhong = db.LoaiPhongs.ToList();
            return View(p);
        }

        [Authorize]
        [HttpPost]
        public ActionResult SuaDichVu(DichVu dv)
        {
            if (ModelState.IsValid)
            {
                var HamDV = new HamDichVu();
                HamDV.Update(dv);
                return RedirectToAction("DSDichVu", "Admin");
            }
            return View(dv);
        }
    }
}
