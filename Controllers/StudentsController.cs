using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentsController : Controller
    {
        public ActionResult Index()
        {
            var db = new AppDbContext();
            var students = db.GetStudents();
            return View(students);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                var db = new AppDbContext();
                db.InsertStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult Edit(int id)
        {
            var db = new AppDbContext();
            var student = db.GetStudentById(id);
            return View(student);
        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                var db = new AppDbContext();
                db.UpdateStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        public ActionResult Delete(int id)
        {
            var db = new AppDbContext();
            var student = db.GetStudentById(id);
            return View(student);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var db = new AppDbContext();
            db.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}