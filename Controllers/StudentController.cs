using KetNoiDB.Data;
using KetNoiDB.Models;
using KetNoiDB.Models.Domain;
using KetNoiDB.Models.Repository;
using KetNoiDB.Models.ViewModels;
using KetNoiDB.Models.Repository;
using KetNoiDB.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace KetNoiDB.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(IStudentRepository studentRepository)
        {
            this._studentRepository = studentRepository;
        }

        // GET: /Student/GetAll
        public IActionResult GetAll(string? searchString, string? type)
        {
            var allStudent = _studentRepository.GetAll(searchString, type);
            return View(allStudent);
        }

        // GET: /Student/GetStudentById/id
        public IActionResult GetStudentById(int id)
        {
            var studentById = _studentRepository.GetStudentsById(id);
            if (studentById != null)
                return View(studentById);
            else
                return View("NotFound");
        }

        // GET: /Student/EditStudentById/id
        [HttpGet]
        public IActionResult EditStudentById(int id)
        {
            var studentVM = _studentRepository.GetStudentsById(id);
            if (studentVM != null)
                return View(studentVM);
            else
                return View("NotFound");
        }

        // POST: /Student/EditStudentById/id
        [HttpPost]
        public IActionResult EditStudentById([FromRoute] int id, VMStudent student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var studentById = _studentRepository.GetStudentsById(id);
                    if (studentById != null)
                    {
                        _studentRepository.UpdateStudentById(id, student);
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
                    TempData["errorMessage"] = "Data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: /Student/AddStudent
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        // POST: /Student/AddStudent
        [HttpPost]
        public IActionResult AddStudent(VMStudent studentData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _studentRepository.AddStudent(studentData);
                    TempData["successMessage"] = "Successful";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    TempData["errorMessage"] = "Data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: /Student/DelStudentById/id
        public IActionResult DelStudentById(int id)
        {
            var studentById = _studentRepository.GetStudentsById(id);
            if (studentById != null)
            {
                _studentRepository.DeleteStudentById(id);
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
