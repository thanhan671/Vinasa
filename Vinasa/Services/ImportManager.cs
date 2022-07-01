using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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
        public virtual async Task ImportSeminarParticipantFromXlsx(int seminarId, Stream stream)
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
                            int.TryParse(cellValue,out int value );
                            participant.ProvinceId = value;
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
                if(isSave)
                {
                    _db.SeminarParticipants.Add(participant);
                    await _db.SaveChangesAsync();
                }
            }

        }
        #endregion
    }
}
