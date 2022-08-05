using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.DAL;
using Vinasa.Models;

namespace Vinasa.Controllers
{
    public class StatisticController : Controller
    {
        private readonly SeminarContext _db = new SeminarContext();
        SEP25Team16Entities2 db = new SEP25Team16Entities2();

        // GET: Statistic
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ActivityJoined()
        {
            var donVi = _db.SeminarParticipants.Select(it => it.Company).ToList();
            donVi.AddRange(_db.NGUOINHANGIAITHUONG.Select(it => it.TenDonVi).ToList());
            var result = new List<DonVi>();
            var tmp = donVi;
            donVi = donVi.Distinct().ToList();

            foreach (var item in donVi)
            {
                var soLuong = tmp.Where(it => it == item).Count();
                result.Add(new DonVi()
                {
                    TenDonVi = item,
                    SoLuong = soLuong
                });
            }
            ViewBag.DonVi = result;
            return View();
        }
        public ActionResult DetailActivitysJoined(string q)
        {
            var activity = new List<string>();

            var seminarParticipants = _db.SeminarParticipants.Where(it => it.Company == q).ToList();
            foreach (var seminarParticipant in seminarParticipants)
            {
                var seminar = _db.Seminars.Find(seminarParticipant.SeminarId);
                if (seminar != null)
                    activity.Add(seminar.Title);
            }

            var nGUOINHANGIAITHUONGs = _db.NGUOINHANGIAITHUONG.Where(it => it.TenDonVi == q).ToList();
            foreach (var nGUOINHANGIAITHUONG in nGUOINHANGIAITHUONGs)
            {
                var gIAITHUONG = _db.GIAITHUONG.Find(nGUOINHANGIAITHUONG.GiaiThuongId);
                if (gIAITHUONG != null)
                    activity.Add(gIAITHUONG.Title);
            }
            ViewBag.Activity = activity;
            ViewBag.TenDonVi = q;
            return View();
        }
        public ActionResult BusinessByArea(int? id = -1)
        {
            var business = db.HOIVIENs.Where(it => it.KhuVuc == id || id == -1).ToList();
            ViewBag.filterId = id;
            return View(business);
        }
       
        public ActionResult SizeOfPersonnel(int? id = -1)
        {
            var business = db.HOIVIENs.ToList();
            switch (id)
            {
                case 1:
                    business = db.HOIVIENs.Where(it => it.SoLuongNhanVien <= 500).ToList();
                    break;
                case 2:
                    business = db.HOIVIENs.Where(it => it.SoLuongNhanVien <= 1000).ToList();
                    break;
                case 3:
                    business = db.HOIVIENs.Where(it => it.SoLuongNhanVien >= 1500).ToList();
                    break;

            }
            ViewBag.filterId = id;
            return View(business);
        }
        public ActionResult FieldOfActivity(int? id = -1)
        {
            var business = db.HOIVIENs.ToList();
            switch (id)
            {
                case 1:
                    business = db.HOIVIENs.Where(it => it.LinhVucHoatDong.Contains("Công Nghệ Thông Tin")).ToList();
                    break;
                case 2:
                    business = db.HOIVIENs.Where(it => it.LinhVucHoatDong.Contains("Đơn Vị Ứng Dụng CNTT")).ToList();
                    break;
                case 3:
                    business = db.HOIVIENs.Where(it => it.LinhVucHoatDong.Contains("Cơ Quan Quản Lý Nhà Nước")).ToList();
                    break;
                case 4:
                    business = db.HOIVIENs.Where(it => it.LinhVucHoatDong.Contains("Đại Sứ Quán/Thương Vụ")).ToList();
                    break;
                case 5:
                    business = db.HOIVIENs.Where(it => it.LinhVucHoatDong.Contains("Nhà Tài Trợ")).ToList();
                    break;
            }
            ViewBag.filterId = id;
            return View(business);
        }
        public ActionResult TimeOfEstablishment(int? id = -1)
        {
            var business = db.HOIVIENs.ToList();
            switch (id)
            {
                case 1:
                    business = db.HOIVIENs.Where(it => (DateTime.Now - it.NgayThanhLap).Value.TotalDays / 365 <= 5).ToList();
                    break;
                case 2:
                    business = db.HOIVIENs.Where(it => (DateTime.Now - it.NgayThanhLap).Value.TotalDays / 365 <= 10).ToList();
                    break;
                case 3:
                    business = db.HOIVIENs.Where(it => (DateTime.Now - it.NgayThanhLap).Value.TotalDays / 365 <= 15).ToList();
                    break;
                case 4:
                    business = db.HOIVIENs.Where(it => (DateTime.Now - it.NgayThanhLap).Value.TotalDays / 365 <= 20).ToList();
                    break;
                case 5:
                    business = db.HOIVIENs.Where(it => (DateTime.Now - it.NgayThanhLap).Value.TotalDays / 365 <= 25).ToList();
                    break;
            }
            ViewBag.filterId = id;
            return View(business);
        }
        public ActionResult BirthDayInMonth()
        {
            //var business = db.HOIVIENs.ToList();
            //switch (id)
            //{
            //    case 1:
            //        business = db.HOIVIENs.Where(it => it. == "5").ToList();
            //        break;
            //    case 2:
            //        business = db.HOIVIENs.Where(it => it.ThoiGianGiaNhap == "10").ToList();
            //        break;
            //    case 3:
            //        business = db.HOIVIENs.Where(it => it.ThoiGianGiaNhap == "15").ToList();
            //        break;
            //    case 4:
            //        business = db.HOIVIENs.Where(it => it.ThoiGianGiaNhap == "20").ToList();
            //        break;
            //    case 5:
            //        business = db.HOIVIENs.Where(it => it.ThoiGianGiaNhap == "25").ToList();
            //        break;
            //}
            //ViewBag.filterId = id;
            //return View(business);
            return View();
        }
        public ActionResult MemberFee()
        {
            return View();
        }
    }
}