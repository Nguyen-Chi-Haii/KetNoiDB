using KetNoiDB.Data;
using KetNoiDB.Models.Domain;
using KetNoiDB.Models.Repository;
using KetNoiDB.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace KetNoiDB.Models.Repository
{
        public class StudentRepository : IStudentRepository
        {
            private SchoolDbContext dbContext;

            public StudentRepository(SchoolDbContext dbContext)
            {
                this.dbContext = dbContext;
            }

            public IEnumerable<Student> GetAll()
            {
                return dbContext.Students;
            }

            public VMStudent? GetStudentsById(int id)
            {
                var student = dbContext.Students.FirstOrDefault(p => p.Id == id);
                if (student != null)
                {
                    var genderVm = student.Gender ? "male" : "female";

                    return new VMStudent
                    {
                        Name = student.Name,
                        Birth = student.Birth,
                        ImgUrl = student.ImgUrl,
                        Gender = genderVm,
                        Mssv = student.Mssv,
                        Description = student.Description
                    };
                }
                return null;
            }

            public void UpdateStudentById(int id, VMStudent model)
            {
                var studentById = dbContext.Students.FirstOrDefault(p => p.Id == id);
                if (studentById != null)
                {
                    studentById.Name = model.Name;
                    studentById.Birth = model.Birth;
                    studentById.Gender = model.Gender == "male";
                    studentById.ImgUrl = model.ImgUrl;
                    studentById.Mssv = model.Mssv;
                    studentById.Description = model.Description;

                    dbContext.Update(studentById);
                    dbContext.SaveChanges();
                }
            }

            public void AddStudent(VMStudent model)
            {
                var genderData = model.Gender == "male";

                var student = new Student
                {
                    Name = model.Name,
                    Birth = model.Birth,
                    Gender = genderData,
                    ImgUrl = model.ImgUrl,
                    Mssv = model.Mssv,
                    Description = model.Description
                };

                dbContext.Students.Add(student);
                dbContext.SaveChanges();
            }

            public void DeleteStudentById(int id)
            {
                var student = dbContext.Students.FirstOrDefault(p => p.Id == id);
                if (student != null)
                {
                    dbContext.Students.Remove(student);
                    dbContext.SaveChanges();
                }
            }
        }
}
