using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vinasa.DAL;
using Vinasa.Models;

namespace Vinasa.Validation
{

    public class StringRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return (value != null && !string.IsNullOrEmpty(value.ToString()));
        }
    }

    public class DateTimeRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            DateTime dateTime = Convert.ToDateTime(value);
            return dateTime >= DateTime.Now;
        }
    }

    public class EndDateTimeRequired : ValidationAttribute
    {
        private string startDate;
        private const string _errorMessage = "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu";

        public EndDateTimeRequired(string dateToCompare) : base(_errorMessage)
        {
            startDate = dateToCompare;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(_errorMessage, name, startDate);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dateToCompare = validationContext.ObjectType.GetProperty(startDate);
            var dateToCompareValue = dateToCompare.GetValue(validationContext.ObjectInstance, null);
            if (dateToCompareValue != null && value != null && (DateTime)value <= (DateTime)dateToCompareValue)
            {
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }

    public class DropDownListRequired : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            int.TryParse(value.ToString(), out int iValue);
            return iValue > 0;
        }
    }

    //public override bool IsValid(object value)
    //{
    //    if (value == null)
    //        return false;

    //    var seminar = _db.Seminars.FirstOrDefault(it => it.Title == value.ToString());
    //    return !(seminar != null && seminar.Id > 0);
    //}

    public class GiaiThuongRequired : ValidationAttribute
    {
        private readonly SeminarContext _db = new SeminarContext();

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var giaiThuong = _db.GIAITHUONG.FirstOrDefault(it => it.Title == value.ToString());
            return !(giaiThuong != null && giaiThuong.Id > 0);
        }
    }

    public class CourseRequired : ValidationAttribute
    {
        private readonly SEP25Team16Entities2 _db = new SEP25Team16Entities2();

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;

            var course = _db.KHOAHOCs.FirstOrDefault(it => it.TenKhoaDaoTao == value.ToString());

            //if (course.isEdit)
            //{
            //    return true;
            //}

            return !(course != null && course.Id > 0);
        }
    }
}