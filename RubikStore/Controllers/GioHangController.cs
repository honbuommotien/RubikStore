using RubikStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RubikStore.Controllers
{
    public class GiohangController : Controller
    {
        dbRubikStoreDataContext data = new dbRubikStoreDataContext();
        public List<Giohang> LayGioHang()
        {
            List<Giohang> listGiohang = Session["GioHang"] as List<Giohang>;
            if (listGiohang == null)
            {
                listGiohang = new List<Giohang>();
                Session["GioHang"] = listGiohang;
            }
            return listGiohang;
        }
        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<Giohang> listGiohang = LayGioHang();
            Giohang sanpham = listGiohang.Find(n => n.id == id);
            if (sanpham == null)
            {
                sanpham = new Giohang(id);
                listGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.iSoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoluong()
        {
            int tong = 0;
            List<Giohang> listgiohang = Session["GioHang"] as List<Giohang>;
            if (listgiohang != null)
            {
                tong = listgiohang.Sum(n => n.iSoluong);
            }
            return tong;
        }

        private int TongSoLuongSanPham()
        {
            int tong = 0;
            List<Giohang> listgiohang = Session["GioHang"] as List<Giohang>;
            if (listgiohang != null)
            {
                tong = listgiohang.Count;
            }
            return tong;
        }

        private double TongTien()
        {
            double tongtien = 0;
            List<Giohang> listGioHnag = Session["GioHang"] as List<Giohang>;
            if (listGioHnag != null)
            {
                tongtien = listGioHnag.Sum(n => n.dThanhtien);
            }
            return tongtien;
        }

        public ActionResult GioHang()
        {
            List<Giohang> listGiohang = LayGioHang();
            ViewBag.Tongsoluong = TongSoluong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(listGiohang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoluong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }

        public ActionResult XoaGioHang(int id)
        {
            List<Giohang> listGiohang = LayGioHang();
            Giohang sanpham = listGiohang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                listGiohang.RemoveAll(n => n.id == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult CapNhatGioHang(int id, FormCollection collection)
        {
            List<Giohang> listGiohang = LayGioHang();
            Giohang sanpham = listGiohang.SingleOrDefault(n => n.id == id);
            if (sanpham != null)
            {
                sanpham.iSoluong = int.Parse(collection["txtSoluong"].ToString());
            }
            return RedirectToAction("GioHang");
        }

        public ActionResult XoaTatCaGioHang()
        {
            List<Giohang> listGiohang = LayGioHang();
            listGiohang.Clear();
            return RedirectToAction("GioHang");
        }
    }
}