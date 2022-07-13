using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TestingApp.Models;

namespace TestingApp.Controllers
{
    public class HomeController : Controller
    {
      //  private readonly ILogger<HomeController> _logger;
      IStudentService _studentService;
      public HomeController(IStudentService studentService)
      {
          // _logger = logger;
          _studentService = studentService;

      }
      public interface IStudentService
      {
          List<Student> GetStudenti();
          List<Student> GetStudentiByGodina(int godina);

          Student GetStudentiByBrojIndeksa(int brojIndeksa);
      }
      
      public class StudentService : IStudentService
      {
          private List<Student> _studenti;
          public StudentService()
          {
              _studenti = new List<Student>()
              {
                  new Student(){ ImePrezime = "Semir Klapuh", BrojIndeksa = 160014, Godina = 1},
                  new Student(){ ImePrezime = "Denis Music", BrojIndeksa = 150051, Godina = 2 },
                  new Student(){ ImePrezime = "Jasmin Azemovic", BrojIndeksa = 150052, Godina = 1},
                  new Student(){ ImePrezime = "Zanin Vejzovic", BrojIndeksa = 150053, Godina = 3}
                
              };
          }
          public List<Student> GetStudenti()
          {
              return _studenti.ToList();
          }

          public List<Student> GetStudentiByGodina(int godina)
          {
              return _studenti.Where(x => x.Godina == godina).ToList();
          }
          
          public Student GetStudentiByBrojIndeksa(int brojIndeksa)
          {
              return _studenti.Where(x => x.BrojIndeksa == brojIndeksa).FirstOrDefault();
          }
      }
        public class Student
        {
            public string ImePrezime { get; set; }
            public int BrojIndeksa { get; set; }
            public int Godina { get; set; }
        }

       // private List<Student> _studenti;

       

        public IActionResult GetStudentiByGodina(int godina)
        {
            List<Student> studenti = new List<Student>();

            studenti = _studentService.GetStudentiByGodina(godina);

            return View(studenti);
        }

        public void SporaMetoda()
        {
            Thread.Sleep(2000);
        }

        public IActionResult Index(int indeks)
        {
            Student student = _studentService.GetStudenti().Where(x=> x.BrojIndeksa == indeks).FirstOrDefault();
            return View(student);
        }
        
        public IActionResult getByIndex(int indeks)
        {
            Student student = _studentService.GetStudentiByBrojIndeksa(indeks);
            return View(student);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
