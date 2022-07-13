using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Moq;
using TestingApp.Controllers;
using Xunit;
using TestingApp.Controllers;

namespace TestsTestingApp
{
    public class UnitTest1
    {
        [Fact]
        public void IndexView_Model_Student()
        {
            Mock<HomeController.IStudentService> studentService = new Mock<HomeController.IStudentService>();
            studentService.Setup(x => x.GetStudenti()).Returns(new List<HomeController.Student>()
            {
                new HomeController.Student(){ ImePrezime = "Semir Klapuh", BrojIndeksa = 160014, Godina = 1},
                new HomeController.Student(){ ImePrezime = "Denis Music", BrojIndeksa = 150051, Godina = 2 },
                new HomeController.Student(){ ImePrezime = "Jasmin Azemovic", BrojIndeksa = 150052, Godina = 1},
                new HomeController.Student(){ ImePrezime = "Zanin Vejzovic", BrojIndeksa = 150053, Godina = 3}
                
            });
            
            HomeController hc = new HomeController(studentService.Object);
            ViewResult vr = hc.Index(160014) as ViewResult;
            HomeController.Student s = vr.Model as HomeController.Student;
            
            Assert.Equal("Semir Klapuh", s.ImePrezime);
        }
        
        [Fact]
        public void IndexView_Model_List_Students()
        {
            Mock<HomeController.IStudentService> studentService = new Mock<HomeController.IStudentService>();
            studentService.Setup(x => x.GetStudentiByGodina(1)).Returns(
            new List<HomeController.Student>()
            {
                new HomeController.Student() { ImePrezime = "Semir Klapuh", BrojIndeksa = 160014, Godina = 1 },
                new HomeController.Student() { ImePrezime = "Jasmin Azemovic", BrojIndeksa = 150052, Godina = 1 }
            });
        

            List<HomeController.Student> ocekivani = new List<HomeController.Student>()
            {
                new HomeController.Student(){ ImePrezime = "Semir Klapuh", BrojIndeksa = 160014, Godina = 1},
                new HomeController.Student(){ ImePrezime = "Jasmin Azemovic", BrojIndeksa = 150052, Godina = 1}

            };
            
            HomeController hc = new HomeController(studentService.Object);
            ViewResult vr = hc.GetStudentiByGodina(1) as ViewResult;
            List<HomeController.Student> aktualni = vr.Model as List<HomeController.Student>;
            
            Assert.Equal(2, aktualni.Count);

            Assert.Collection(aktualni,
                item => Assert.Equal("Semir Klapuh", item.ImePrezime),
                item => Assert.Equal("Jasmin Azemovic", item.ImePrezime));
            
            Assert.Collection(aktualni,  
                item => Assert.Equal(160014, item.BrojIndeksa),
                item => Assert.Equal(150052, item.BrojIndeksa));


        }
        
        
        [Fact]
        public void IndexView_Model_Student_FakeItEasy()
        {
            var faketStudentService = A.Fake<HomeController.IStudentService>();
            A.CallTo(() => faketStudentService.GetStudenti()).Returns(new List<HomeController.Student>()
            {
                new HomeController.Student() { ImePrezime = "Semir Klapuh", BrojIndeksa = 160014, Godina = 1 },
                new HomeController.Student() { ImePrezime = "Denis Music", BrojIndeksa = 150051, Godina = 2 },
                new HomeController.Student() { ImePrezime = "Jasmin Azemovic", BrojIndeksa = 150052, Godina = 1 },
                new HomeController.Student() { ImePrezime = "Zanin Vejzovic", BrojIndeksa = 150053, Godina = 3 }

            });

            HomeController hc = new HomeController(faketStudentService);
            ViewResult vr = hc.Index(160014) as ViewResult;
            HomeController.Student s = vr.Model as HomeController.Student;
            
            Assert.Equal("Semir Klapuh", s.ImePrezime);
        }
        
    
        [Fact]
        public void IndexView_Model_List_Students_FakeItEasy()
        {
            var fakeStudentService = A.Fake<HomeController.IStudentService>();
            A.CallTo(() => fakeStudentService.GetStudentiByGodina(1)).Returns(
                new List<HomeController.Student>()
                {
                    new HomeController.Student() { ImePrezime = "Semir Klapuh", BrojIndeksa = 160014, Godina = 1 },
                    new HomeController.Student() { ImePrezime = "Jasmin Azemovic", BrojIndeksa = 150052, Godina = 1 }
                });

            List<HomeController.Student> ocekivani = new List<HomeController.Student>()
            {
                new HomeController.Student(){ ImePrezime = "Semir Klapuh", BrojIndeksa = 160014, Godina = 1},
                new HomeController.Student(){ ImePrezime = "Jasmin Azemovic", BrojIndeksa = 150052, Godina = 1}

            };
            
            HomeController hc = new HomeController(fakeStudentService);
            ViewResult vr = hc.GetStudentiByGodina(1) as ViewResult;
            List<HomeController.Student> aktualni = vr.Model as List<HomeController.Student>;
            
            Assert.Equal(2, aktualni.Count);

            Assert.Collection(aktualni,
                item => Assert.Equal("Semir Klapuh", item.ImePrezime),
                item => Assert.Equal("Jasmin Azemovic", item.ImePrezime));
            
            Assert.Collection(aktualni,  
                item => Assert.Equal(160014, item.BrojIndeksa),
                item => Assert.Equal(150052, item.BrojIndeksa));


        }


        [Fact]
        public void GetStudentBrojIndeksa_Students_FakeItEasy()
        {
            var faketStudentService = A.Fake<HomeController.IStudentService>();

            A.CallTo(() => faketStudentService.GetStudentiByBrojIndeksa(160014)).Returns(
                new HomeController.Student()
                {
                    ImePrezime = "Semir Klapuh",
                    BrojIndeksa = 160014,
                    Godina = 1
                });

            HomeController hc = new HomeController(faketStudentService);
            ViewResult vr = hc.getByIndex(160014) as ViewResult;
            
            HomeController.Student student = vr.Model as HomeController.Student;
            
            Assert.Equal("Semir Klapuh", student.ImePrezime);


        }


    }
}