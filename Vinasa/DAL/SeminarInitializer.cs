using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Vinasa.Models;

namespace Vinasa.DAL
{
    public class SeminarInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SeminarContext>
    {
        protected override void Seed(SeminarContext context)
        {
            SeminarInitialize(context);
            SeminarParticipantInitialize(context);
            ProviceInitialize(context);
        }

        private static void SeminarInitialize(SeminarContext context)
        {

            // Look for any movies.
            if (context.Seminars.Any())
            {
                return;   // DB has been seeded
            }

            context.Seminars.AddOrUpdate(
                new Seminar
                {
                    Title = "Hội nghị kết nối đầu tư tổ chức tại TP. Hồ Chí Minh: chủ đề THÚC ĐẨY ĐẦU TƯ VÀ PHÁT TRIỂN HỆ SINH THÁI TÀI CHÍNH SỐ",
                    OpenDate = DateTime.Now,
                    Address = "Sihub, 273 Điện Biên Phủ, Phường Võ Thị Sáu, Quận 3, TP. HCM",
                    CreatedUtc = DateTime.UtcNow
                }
            );
            context.SaveChanges();
        }
        private static void SeminarParticipantInitialize(SeminarContext context)
        {
            if (context.SeminarParticipants.Any())
            {
                return;   // DB has been seeded
            }

            context.SeminarParticipants.AddOrUpdate(
                new SeminarParticipant
                {
                    TaxNumber = "0315159393",
                    SeminarId = 1,
                    Company = "Công ty Cổ phần FPT",
                    Name = "Trương Gia Bình",
                    Position = "Chủ tịch HĐQT",
                    Email = "manhnt@vinasa.org.vn",
                    PhoneNumber = "0937688958",
                    ProvinceId = 1,
                    JobTitle = "Doanh nghiệp CNTT",
                    Operation = "Công nghệ thông tin",
                    RegistrySeminar = true,
                    RegistryBusinessMatching = true,
                    RegistryExhibition = true,
                    RegistryTicket = true,
                    CreatedUtc = DateTime.UtcNow
                }
            );
            context.SaveChanges();
        }

        private static void ProviceInitialize(SeminarContext context)
        {
            if (context.SeminarParticipants.Any())
            {
                return;   // DB has been seeded
            }

            context.Provinces.AddOrUpdate(
                new Province
                {
                    Title = "HCM"
                }
            );
            context.Provinces.AddOrUpdate(
                new Province
                {
                    Title = "HN"
                }
            );
            context.Provinces.AddOrUpdate(
                new Province
                {
                    Title = "DN"
                }
            );
            context.SaveChanges();
        }
    }
}