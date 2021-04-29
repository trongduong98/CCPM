using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyKhachSan.Models.Entities;
using QuanLyKhachSan.Models.ViewModels;
using QuanLyKhachSan.Models.Functions;

namespace QuanLyKhachSan.Controllers
{
    public class TrangChuController : Controller
    {
        private ModelQLKS db = new ModelQLKS();

        // GET

        public ActionResult TrangChu()
        {
            if (Request.IsAuthenticated) return RedirectToAction("DSTaiKhoan", "Admin");
            if (Session["TaiKhoan"] == null)
            {
                if (Request.Cookies["TenTaiKhoan"] != null)
                {
                    HttpCookie TenTaiKhoan = Request.Cookies["TenTaiKhoan"];
                    HttpCookie MatKhau = Request.Cookies["MatKhau"];
                    var listTK = db.TaiKhoans.Where(m => m.TenTaiKhoan == TenTaiKhoan.Value && m.MatKhau == MatKhau.Value).ToList();
                    if (listTK.Count != 0)
                    {
                        TaiKhoan taiKhoan = listTK.First();
                        Session["TaiKhoan"] = taiKhoan;
                    }
                }
            }
            List<LoaiPhong> list = db.LoaiPhongs.ToList();
            return View(list);
        }

        public ActionResult TinTuc()
        {
            return View();
        }

        public ActionResult LienHe()
        {
            return View();
        }


        // POST

    }
}
