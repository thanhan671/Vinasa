using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Vinasa.DAL;
using Vinasa.Models;
using Vinasa.Services;
using Vinasa.Session_Attribute;

namespace Vinasa.Controllers
{
    [SessionAttributes]
    public class StatisticController : Controller
    {
        private readonly SeminarContext _db = new SeminarContext();
        SEP25Team16Entities2 db = new SEP25Team16Entities2();
        private readonly ImportManager _importManager;

        public StatisticController()
        {
            _importManager = new ImportManager(_db);
        }

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
        public ActionResult BusinessByArea()
        {
            var business = db.HOIVIENs.ToList();
            var viewModels = new List<BusinessByAreaViewModel>();

            viewModels.Add(new BusinessByAreaViewModel()
            {
                Ten = "Bắc",
                SoLuong = business.Count(it => it.KhuVuc == 1)
            });

            viewModels.Add(new BusinessByAreaViewModel()
            {
                Ten = "Trung",
                SoLuong = business.Count(it => it.KhuVuc == 2)
            });

            viewModels.Add(new BusinessByAreaViewModel()
            {
                Ten = "Nam",
                SoLuong = business.Count(it => it.KhuVuc == 3)
            });

            return View(viewModels);
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
        public ActionResult FieldOfActivity()
        {
            var business = db.HOIVIENs.ToList();
            var viewModels = new List<FieldOfActivityViewModel>();

            viewModels.Add(new FieldOfActivityViewModel()
            {
                Ten = "Công Nghệ Thông Tin",
                SoLuong = business.Count(it => it.LinhVucHoatDong.Contains("Công Nghệ Thông Tin"))
            });

            viewModels.Add(new FieldOfActivityViewModel()
            {
                Ten = "Đơn Vị Ứng Dụng CNTT",
                SoLuong = business.Count(it => it.LinhVucHoatDong.Contains("Đơn Vị Ứng Dụng CNTT"))
            });

            viewModels.Add(new FieldOfActivityViewModel()
            {
                Ten = "Cơ Quan Quản Lý Nhà Nước",
                SoLuong = business.Count(it => it.LinhVucHoatDong.Contains("Cơ Quan Quản Lý Nhà Nước"))
            });

            viewModels.Add(new FieldOfActivityViewModel()
            {
                Ten = "Đại Sứ Quán/Thương Vụ",
                SoLuong = business.Count(it => it.LinhVucHoatDong.Contains("Đại Sứ Quán/Thương Vụ"))
            });

            viewModels.Add(new FieldOfActivityViewModel()
            {
                Ten = "Nhà Tài Trợ",
                SoLuong = business.Count(it => it.LinhVucHoatDong.Contains("Nhà Tài Trợ"))
            });

            return View(viewModels);
        }
        public ActionResult TimeOfEstablishment(int? id = -1)
        {
            var business = db.HOIVIENs.ToList();
            switch (id)
            {
                case 1:
                    business = GetHoiVienByTime(5);
                    break;
                case 2:
                    business = GetHoiVienByTime(10);
                    break;
                case 3:
                    business = GetHoiVienByTime(15);
                    break;
                case 4:
                    business = GetHoiVienByTime(20);
                    break;
                case 5:
                    business = GetHoiVienByTime(25);
                    break;
            }
            ViewBag.filterId = id;
            return View(business);
        }
        public ActionResult BirthDayInMonth(int? id = -1)
        {
            var business = db.HOIVIENs.ToList();
            if (id > 0)
                business = db.HOIVIENs.Where(it => it.NgayThanhLap.Value.Month == id).ToList();
            ViewBag.filterId = id;
            return View(business);
        }

        public ActionResult MemberFee()
        {
            var hoiPhi = _db.HoiPhi.ToList();
            return View(hoiPhi);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            HoiPhi hoiPhi = _db.HoiPhi.Find(id);

            _db.HoiPhi.Remove(hoiPhi);
            _db.SaveChanges();

            return RedirectToAction("MemberFee");
        }

        public ActionResult DeleteSelected(int id)
        {
            var model = _db.HoiPhi.Where(m => m.Id == id).FirstOrDefault();
            return PartialView("_DeleteSelected", model);
        }

        [HttpPost, ActionName("ImportExcel")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ImportExcel(HttpPostedFileBase importexcelfile)
        {
            try
            {
                if (importexcelfile != null && importexcelfile.ContentLength > 0)
                {
                    var tuple = await _importManager.ImportHoiPhiFromXlsx(importexcelfile.InputStream);
                    Session["ViewBag.Success"] = tuple.Item1;
                    Session["ViewBag.Exist"] = tuple.Item2;
                }

                return RedirectToAction(nameof(MemberFee));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(MemberFee), new { err = ex.Message });
            }
        }
        public FileResult Download()
        {
            string path = Server.MapPath("~/Content/Files");
            string filename = Path.GetFileName("MauHoiPhi.xlsx");

            string fullPath = Path.Combine(path, filename);
            return File(fullPath, "download/xlsx", "MauHoiPhi.xlsx");
        }

        #region Helper
        private List<HOIVIEN> GetHoiVienByTime(int time)
        {
            var tmp = db.HOIVIENs.ToList();
            var result = new List<HOIVIEN>();

            foreach (var item in tmp)
            {
                if ((DateTime.Now - item.NgayThanhLap).Value.TotalDays / 365 <= time)
                    result.Add(item);
            }
            return result;
        }
        #endregion
    }
}