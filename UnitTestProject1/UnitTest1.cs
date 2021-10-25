using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repository;
using RepositoryLayer;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        
       
       
        [TestMethod]

        public void TestMethod1()
        {
             Mock<RepoContext> _context;
             Repo _repo;
        var data = new List<VendorDetailsEntity> {
                 new VendorDetailsEntity
                {
                   Email="vinu@gmail.com",
                   Pswd="vinu123"
                }
                   

            }.AsQueryable();

            var mockSet = new Mock<DbSet<VendorDetailsEntity>>();
            mockSet.As<IQueryable<VendorDetailsEntity>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<VendorDetailsEntity>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<VendorDetailsEntity>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<VendorDetailsEntity>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            _context = new Mock<RepoContext>();

            _context.Setup(c => c.VendorDetails).Returns(mockSet.Object);

            _repo = new Repo();
            var Email = "vinu@gmail.com";
            var Pswd = "vinu123";
            var result = _repo.Validate(Email, Pswd,_context);
            Assert.AreEqual(result,true);
        }
        [TestMethod]
        public void TestValidateMenues()
        {
            Repo rp = new Repo();
            bool actual = true;
            string email = "ramu@gmail.com";

            bool expected = rp.ValidateMenu(email);

            Assert.AreEqual(expected, actual);
        }
        [TestMethod]
        public void TestMenues()
        {
            //Mock<Repo> chk = new Mock<Repo>();
            //chk.Setup(x => x.ValidateMenu()).Returns(true);
            //Repo obje = new Repo();
            //Assert.AreEqual(obje.TestGetLeftRoomData(chk.Object), true);
        }

    }
}
