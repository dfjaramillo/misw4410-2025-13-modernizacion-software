using Common;
using Model.Domain;
using NLog;
using Persistence.Repository;
using System;
using Persistence.DbContextScope.Interfaces;

namespace Service
{
    public interface IStudentPerCourseService
    {
        ResponseHelper Insert(StudentPerCourse model);
    }

    public class StudentPerCourseService : IStudentPerCourseService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly IDbContextScopeFactory _dbContextScopeFactory;
        private readonly IRepository<StudentPerCourse> _studentPerCourseRepository;

        public StudentPerCourseService(
            IDbContextScopeFactory dbContextScopeFactory,
            IRepository<StudentPerCourse> studentPerCourseRepository
        )
        {
            _dbContextScopeFactory = dbContextScopeFactory;
            _studentPerCourseRepository = studentPerCourseRepository;
        }

        public ResponseHelper Insert(StudentPerCourse model)
        {
            var rh = new ResponseHelper();

            try
            {
                using (var ctx = _dbContextScopeFactory.Create())
                {
                    _studentPerCourseRepository.Insert(model);

                    ctx.SaveChanges();
                    rh.SetResponse(true);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }

            return rh;
        }
    }
}
