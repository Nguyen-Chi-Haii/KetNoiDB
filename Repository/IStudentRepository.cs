using KetNoiDB.Data;
using KetNoiDB.Models.Domain;
using KetNoiDB.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KetNoiDB.Models.Repository
{ 
    public interface IStudentRepository
    {

        public IEnumerable<Student> GetAll(string? searchString, string? type);
        VMStudent GetStudentsById(int id);
        void UpdateStudentById(int id, VMStudent model);
        void AddStudent(VMStudent model);
        void DeleteStudentById(int id);
    }
}
