using KetNoiDB.Data;
using KetNoiDB.Models.Domain;
using KetNoiDB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KetNoiDB.Controllers
{
    public class StudentController : Controller
    {
        private SchoolDbContext dbContext; // Hàm khởi tạo Constructor để tạo phiên kết nối DB mỗi khi Controller được gọi

        public StudentController(SchoolDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult GetAll()
        {
            IEnumerable<Student> allStudent = dbContext.Students;
            return View(allStudent);
        }
        [HttpPost]
        public IActionResult GetStudentById(int id)
        {
            var studentbyId = dbContext.Students.FirstOrDefault(s => s.Id == id);
            if (studentbyId == null)
            {
                return NotFound("Không tìm thấy sinh viên này"); // Trả về 404 nếu không tìm thấy
            }
            return View(studentbyId); // Trả về view với dữ liệu của sinh viên
        }
        public IActionResult EditStudentById(int id)
        {
            var Student = dbContext.Students.FirstOrDefault(p => p.Id == id);
            if (Student != null)
            {
                string GenderVm;
                if (Student.Gender == false) GenderVm = "female";else GenderVm = "male";
                var studentVM = new VMStudent()
                {
                    Name = Student.Name,
                    Birth = Student.Birth,
                    ImgUrl = Student.ImgUrl,
                    Gender = GenderVm,
                    Mssv = Student.Mssv,
                    Description = Student.Description,
                };

                return View(studentVM);
            }
            else
            {
                return View("NotFound");
            }

        }

        [HttpPost]
        public IActionResult EditStudentById([FromRoute] int id, VMStudent student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var StudentById = dbContext.Students.FirstOrDefault(p => p.Id == id);
                    if (StudentById != null)
                    {
                        StudentById.Name = student.Name;
                        StudentById.Birth = student.Birth;
                        if (student.Gender == "male") StudentById.Gender = true; else StudentById.Gender = false;
                        StudentById.ImgUrl = student.ImgUrl;
                        StudentById.Mssv = student.Mssv;
                        StudentById.Description = student.Description;
                        dbContext.SaveChanges();
                        TempData["successMessage"] = "Successful";
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        return View("NotFound");
                    }
                }
                else
                {
                    TempData["errorMessage"] = "data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        public IActionResult DelStudentById(int id)
        {
            var student = dbContext.Students.FirstOrDefault(p => p.Id == id);
            if (student != null)
            {
                dbContext.Students.Remove(student);
                dbContext.SaveChanges();
                TempData["successMessage"] = "Deleted";
                return RedirectToAction("GetAll");
            }
            else
            {
                return View("NotFound");
            }
        }

    }

}
