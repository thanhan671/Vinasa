using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vinasa.Controllers;
using Vinasa.Models;

namespace VinasaTests.Controllers
{
    [TestClass]
    public class SeminarParticipantsControllerTest
    {
        [TestMethod]
        public void IndexTest()
        {

            var con = new SeminarParticipantsController();

            var result = con.Index() as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as List<SeminarParticipant>;
            Assert.IsNotNull(result);

            var db = new SEP25Team16Entities2();
            Assert.AreEqual(db.SeminarParticipants.Count(), model.Count);
        }
        [TestMethod]
        public void DetailTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new SeminarParticipantsController();

            var cource = db.SeminarParticipants.First();
            var result = con.Details(cource.Id) as ViewResult;

            Assert.IsNotNull(result);
        }
        [TestMethod]
        public void EditTest()
        {
            var con = new SeminarParticipantsController();
            var result0 = con.Edit(0) as HttpNotFoundResult;
            Assert.IsNotNull(result0);

            var db = new SEP25Team16Entities2();
            var seminarPar = db.SeminarParticipants.First();
            var result = con.Edit(seminarPar.Id) as ViewResult;

            Assert.IsNotNull(result);

            var model = result.Model as SeminarParticipant;
            Assert.IsNotNull(model);
            Assert.AreEqual(seminarPar.Name, model.Name);
            Assert.AreEqual(seminarPar.TaxNumber, model.TaxNumber);
            Assert.AreEqual(seminarPar.Company, model.Company);
            Assert.AreEqual(seminarPar.Position, model.Position);
            Assert.AreEqual(seminarPar.Email, model.Email);
            Assert.AreEqual(seminarPar.PhoneNumber, model.PhoneNumber);
            Assert.AreEqual(seminarPar.ProvinceId, model.ProvinceId);
            Assert.AreEqual(seminarPar.JobTitle, model.JobTitle);
            Assert.AreEqual(seminarPar.Operation, model.Operation);
            Assert.AreEqual(seminarPar.RegistrySeminar, model.RegistrySeminar);
            Assert.AreEqual(seminarPar.RegistryBusinessMatching, model.RegistryBusinessMatching);
            Assert.AreEqual(seminarPar.RegistryExhibition, model.RegistryExhibition);
            Assert.AreEqual(seminarPar.RegistryTicket, model.RegistryTicket);
        }
        [TestMethod]
        public void DeleteTest()
        {
            var db = new SEP25Team16Entities2();
            var con = new SeminarParticipantsController();

            var semi = db.SeminarParticipants.First();
            var result = con.DeleteSelected(semi.Id) as PartialViewResult;

            Assert.IsNotNull(result);
        }
    }
}
