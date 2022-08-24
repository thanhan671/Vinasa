using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Vinasa.DAL;
using Vinasa.Models;

namespace Vinasa.Services
{
    public class ImportManager
    {
        #region Fields
        private readonly SeminarContext _db;
        #endregion

        #region Contructor
        public ImportManager(
                    SeminarContext db)
        {
            _db = db;
        }
        #endregion

        #region Methods
        public virtual async Task<Tuple<int, int>> ImportSeminarParticipantFromXlsx(int seminarId, HttpPostedFileBase importExcelFile)
        {
            var stream = importExcelFile.InputStream;

            string extension = Path.GetExtension(importExcelFile.FileName);
            if (extension != ".xls" && extension != ".xlsx" && extension != ".csv")
            {
                throw new System.Exception("Chỉ hỗ trợ các tệp có đuôi .xls; .xlsx; .csv!");
            }
            else if (stream.Length > 1024 * 1024 * 100)
            {
                throw new System.Exception("Dung lượng file tải lên không vượt quá 100MB");
            }


            var workbook = new XSSFWorkbook(stream);
            var worksheet = workbook.GetSheetAt(0);
            if (worksheet == null)
                throw new System.Exception("No worksheet found");

            var poz = 0;
            Dictionary<string, int> manager = new Dictionary<string, int>();
            while (true)
            {
                try
                {
                    var cell = worksheet.GetRow(0).Cells[poz];

                    if (cell == null)
                        break;

                    var cellValue = cell?.ToString();
                    if (string.IsNullOrEmpty(cellValue))
                        break;

                    manager[cellValue] = poz;

                    poz += 1;
                }
                catch
                {
                    break;
                }
            }

            if (manager.Count != 14)
                throw new System.Exception("Số cột dữ liệu của file không đúng mẫu, vui lòng tải mẫu Excel và thử lại !");

            var provinces = _db.Provinces.ToList();
            var addedRows = 0;
            var existedRows = 0;

            for (var iRow = 1; iRow < worksheet.PhysicalNumberOfRows; iRow++)
            {
                var participant = new SeminarParticipant()
                {
                    SeminarId = seminarId,
                    CreatedUtc = DateTime.UtcNow
                };
                var _row = worksheet.GetRow(iRow);
                bool isSave = false;
                foreach (var property in manager)
                {
                    var cell = _row.GetCell(property.Value);
                    if (cell == null)
                        continue;
                    var cellValue = cell?.ToString().Trim();
                    isSave |= !string.IsNullOrEmpty(cellValue);
                    switch (property.Key.ToString().Trim())
                    {
                        case "Mã số thuế":
                            participant.TaxNumber = cellValue;
                            break;

                        case "Tên đơn vị":
                            participant.Company = cellValue;
                            break;

                        case "Họ và tên người tham dự":
                            participant.Name = cellValue;
                            break;

                        case "Chức danh":
                            participant.Position = cellValue;
                            break;

                        case "Email":
                            participant.Email = cellValue;
                            break;

                        case "Di động":
                            participant.PhoneNumber = cellValue;
                            break;

                        case "Tỉnh thành":
                            //int.TryParse(cellValue,out int value );
                            var province = provinces.FirstOrDefault(it => it.Title.Trim().ToLower() == cellValue.Trim().ToLower());
                            if (province != null)
                                participant.ProvinceId = province.Id;
                            break;

                        case "Đơn vị chúng tôi là":
                            participant.JobTitle = cellValue;
                            break;

                        case "Lĩnh vực hoạt động":
                            participant.Operation = cellValue;
                            break;

                        case "Đăng ký hội thảo":
                            participant.RegistrySeminar = (cellValue == "có");
                            break;

                        case "Đăng ký Business Matching":
                            participant.RegistryBusinessMatching = (cellValue == "có");
                            break;

                        case "Đăng ký gian hàng triển lãm":
                            participant.RegistryExhibition = (cellValue == "có");
                            break;

                        case "Đăng ký vé tham dự":
                            participant.RegistryTicket = (cellValue == "có");
                            break;
                    }
                }
                if (_db.SeminarParticipants.Any(it => it.TaxNumber == participant.TaxNumber && it.SeminarId == seminarId))
                {
                    isSave = false;
                    existedRows++;
                }

                if (isSave)
                {
                    addedRows++;
                    _db.SeminarParticipants.Add(participant);
                    await _db.SaveChangesAsync();
                }
            }
            return new Tuple<int, int>(addedRows, existedRows);
        }

        public virtual async Task<Tuple<int, int>> ImportNguoiNhanGiaiThuongsFromXlsx(int giaiThuongId, HttpPostedFileBase importExcelFile)
        {
            var stream = importExcelFile.InputStream;

            string extension = Path.GetExtension(importExcelFile.FileName);
            if (extension != ".xls" && extension != ".xlsx" && extension != ".csv")
            {
                throw new System.Exception("Chỉ hỗ trợ các tệp có đuôi .xls; .xlsx; .csv!");
            }
            else if (stream.Length > 1024 * 1024 * 100)
            {
                throw new System.Exception("Dung lượng file tải lên không vượt quá 100MB");
            }

            var workbook = new XSSFWorkbook(stream);
            var worksheet = workbook.GetSheetAt(0);
            if (worksheet == null)
                throw new System.Exception("No worksheet found");
            var poz = 0;
            Dictionary<string, int> manager = new Dictionary<string, int>();
            while (true)
            {
                try
                {
                    var cell = worksheet.GetRow(0).Cells[poz];

                    if (cell == null)
                        break;

                    var cellValue = cell?.ToString();
                    if (string.IsNullOrEmpty(cellValue))
                        break;

                    manager[cellValue] = poz;

                    poz += 1;
                }
                catch
                {
                    break;
                }
            }

            if (manager.Count != 16)
                throw new System.Exception("Số cột dữ liệu của file không đúng mẫu, vui lòng tải mẫu Excel và thử lại !");

            var provinces = _db.Provinces.ToList();
            var addedRows = 0;
            var existedRows = 0;
            for (var iRow = 1; iRow < worksheet.PhysicalNumberOfRows; iRow++)
            {
                var nguoiNhanGiaiThuong = new NGUOINHANGIAITHUONG()
                {
                    GiaiThuongId = giaiThuongId
                };
                var _row = worksheet.GetRow(iRow);
                bool isSave = false;
                foreach (var property in manager)
                {
                    var cell = _row.GetCell(property.Value);
                    if (cell == null)
                        continue;
                    var cellValue = cell?.ToString().Trim();
                    isSave |= !string.IsNullOrEmpty(cellValue);
                    switch (property.Key.ToString().Trim())
                    {
                        case "Mã số thuế":
                            nguoiNhanGiaiThuong.MaSoThue = cellValue;
                            break;

                        case "Tên đơn vị":
                            nguoiNhanGiaiThuong.TenDonVi = cellValue;
                            break;

                        case "Tên người đại diện pháp luật":
                            nguoiNhanGiaiThuong.TenNguoiDaiDienPhapLuat = cellValue;
                            break;

                        case "Chức danh":
                            nguoiNhanGiaiThuong.ChucDanh = cellValue;
                            break;

                        case "Email":
                            nguoiNhanGiaiThuong.Email = cellValue;
                            break;

                        case "Di động":
                            nguoiNhanGiaiThuong.DiDong = cellValue;
                            break;

                        case "Tỉnh thành":
                            var province = provinces.FirstOrDefault(it => it.Title.Trim().ToLower() == cellValue.Trim().ToLower());
                            if (province != null)
                                nguoiNhanGiaiThuong.ProvinceId = province.Id;
                            break;

                        case "Tên người liên hệ với BTC":
                            nguoiNhanGiaiThuong.TenNguoiLienHeVoiBTC = cellValue;
                            break;
                        case "Chức danh người liên hệ":
                            nguoiNhanGiaiThuong.ChucDanhNguoiLienHe = cellValue;
                            break;
                        case "Email người liên hệ":
                            nguoiNhanGiaiThuong.EmailNguoiLienHe = cellValue;
                            break;
                        case "Di động người liên hệ":
                            nguoiNhanGiaiThuong.DiDongNguoiLienHe = cellValue;
                            break;
                        case "Địa chỉ":
                            nguoiNhanGiaiThuong.DiaChi = cellValue;
                            break;
                        case "Phiếu đăng ký (đính kèm file hoặc link URL)":
                            nguoiNhanGiaiThuong.PhieuDangKy = cellValue;
                            break;

                        case "Kinh phí truyền thông":
                            int.TryParse(cellValue, out int iValue);
                            nguoiNhanGiaiThuong.KinhPhiTruyenThong = iValue;
                            break;

                        case "Giải thưởng":
                            nguoiNhanGiaiThuong.GiaiThuong = cellValue;
                            break;
                    }
                }
                if (_db.NGUOINHANGIAITHUONG.Any(it => it.MaSoThue == nguoiNhanGiaiThuong.MaSoThue && it.GiaiThuongId == giaiThuongId))
                {
                    isSave = false;
                    existedRows++;
                }
                if (isSave)
                {
                    addedRows++;
                    _db.NGUOINHANGIAITHUONG.Add(nguoiNhanGiaiThuong);
                    await _db.SaveChangesAsync();
                }
            }
            return new Tuple<int, int>(addedRows, existedRows);
        }

        public virtual async Task<Tuple<int, int>> ImportHoiPhiFromXlsx(Stream stream)
        {
            var workbook = new XSSFWorkbook(stream);
            var worksheet = workbook.GetSheetAt(0);
            if (worksheet == null)
                throw new System.Exception("No worksheet found");
            var poz = 0;
            Dictionary<string, int> manager = new Dictionary<string, int>();
            while (true)
            {
                try
                {
                    var cell = worksheet.GetRow(0).Cells[poz];

                    if (cell == null)
                        break;

                    var cellValue = cell?.ToString();
                    if (string.IsNullOrEmpty(cellValue))
                        break;

                    manager[cellValue] = poz;

                    poz += 1;
                }
                catch
                {
                    break;
                }
            }
            var addedRows = 0;
            var existedRows = 0;
            for (var iRow = 1; iRow < worksheet.PhysicalNumberOfRows; iRow++)
            {
                var hoiPhi = new HoiPhi();
                
                var _row = worksheet.GetRow(iRow);
                bool isSave = true;
                foreach (var property in manager)
                {
                    var cell = _row.GetCell(property.Value);
                    if (cell == null)
                        continue;
                    var cellValue = cell?.ToString().Trim();
                    int.TryParse(cellValue, out int iValue);
                    switch (property.Key.ToString().Trim())
                    {
                                            
                        case "Mã số thuế":
                            hoiPhi.MaSoThue = cellValue;
                            break;

                        case "Tên Công ty":
                            hoiPhi.TenCongTy = cellValue;
                            break;

                        case "Địa chỉ ghi trên Phiếu thu":
                            hoiPhi.DiaChiGhiPhieuThu = cellValue;
                            break;

                        case "Địa chỉ gửi phiếu thu":
                            hoiPhi.DiaChiGuiPhieuThu = cellValue;
                            break;

                        case "Người nhận phiếu thu":
                            hoiPhi.NguoiNhanPhieu = cellValue;
                            break;

                        case "Điện thoại":
                            hoiPhi.DienThoai = iValue;
                            break;

                        case "Hội phí 2020":
                            hoiPhi.HoiPhiNamTruoc = iValue;
                            break;

                        case "Hội phí 2021":
                            hoiPhi.HoiPhiNamSau = iValue;
                            break;

                        case "Tổng thu Hội phí 2021":
                            hoiPhi.TongThu = iValue;
                            break;

                        case "Đã đóng":
                            hoiPhi.DaDong = iValue;
                            break;

                        case "Còn lại":
                            hoiPhi.ConLai = iValue;
                            break;

                        case "Ngày chuyển tiền":
                            if (!string.IsNullOrEmpty(cellValue))
                                hoiPhi.NgayChuyenTien = Convert.ToDateTime(cellValue);
                            break;

                        case "Ngày gửi phiếu thu":
                            if (!string.IsNullOrEmpty(cellValue))
                                hoiPhi.NgayGuiPhieuThu = Convert.ToDateTime(cellValue); 
                            break;

                        case "Ghi chú":
                            hoiPhi.GhiChu = cellValue;
                            break;

                    }
                }
                if (_db.HoiPhi.Any(it => it.MaSoThue == hoiPhi.MaSoThue))
                {
                    existedRows++;
                    isSave = false;
                }
                if (isSave)
                {
                    addedRows++;
                    _db.HoiPhi.Add(hoiPhi);
                    await _db.SaveChangesAsync();
                }
            }
            return new Tuple<int, int>(addedRows, existedRows);

        }
        #endregion
    }
}