using KetNoiDB.Models.Domain;
using KetNoiDB.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace KetNoiDB.Models.Repository
{
    public interface IStudentRepository
    {
        public IEnumerable<Student> GetAll(string? searchString, string? type)
        {
            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                var List = from l in dbContext.Students   // lấy toàn bộ liên kết
                           select l;

                if (type == "Mssv")
                {
                    return List.Where(s => s.Mssv.Contains(searchString));  // lọc theo chuỗi tìm kiếm
                }
                else
                {
                    return List.Where(s => s.Name.Contains(searchString));  // lọc theo chuỗi tìm kiếm
                }
            }
            else
            {
                return dbContext.Students;
            }

        }
        VMStudent GetStudentsById(int id);
        void UpdateStudentById(int id, VMStudent model);
        void AddStudent(VMStudent model);
        void DeleteStudentById(int id);
    }
}
