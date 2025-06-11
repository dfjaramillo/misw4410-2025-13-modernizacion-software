using System.Collections.Generic;
using Common;
using Model.Custom;
using Model.Domain;

namespace Service
{
    public interface IStudentService
    {
        IEnumerable<StudentForGridView> GetAll();
        Student Get(int id);
        ResponseHelper InsertOrUpdate(Student model);
        ResponseHelper Delete(int id);
    }
}