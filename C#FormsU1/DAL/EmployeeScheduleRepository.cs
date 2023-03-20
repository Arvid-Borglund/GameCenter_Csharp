using GameCenter.Interfaces;
using GameCenter.Models;
using GameCenter.DAL.DALErrorHandling;

namespace GameCenter.DAL
{//Written partly with AI assistance
    internal class EmployeeScheduleRepository : ICRUDRepository<EmployeeSchedule>
    {
        private readonly GameCenterDatabaseContext _context;

        public EmployeeScheduleRepository(GameCenterDatabaseContext context)
        {
            _context = context;
        }
        // create CRUD for employee schedule class
        public void Create(EmployeeSchedule employeeSchedule)
        {
            try
            {
                _context.EmployeeSchedules.Add(employeeSchedule);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }           
        }


        EmployeeSchedule ICRUDRepository<EmployeeSchedule>.GetById(string id)
        {
            throw new NotImplementedException();
        }

        public List<EmployeeSchedule> GetAll()
        {
            try
            {
                return _context.EmployeeSchedules.ToList();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }            
        }

        public void Update(EmployeeSchedule employeeSchedule)
        {
            try
            {
                _context.EmployeeSchedules.Update(employeeSchedule);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public void Delete(EmployeeSchedule employeeSchedule)
        {
            try
            {
                _context.EmployeeSchedules.Remove(employeeSchedule);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        // create find by composite id method

        public EmployeeSchedule GetByCompositeId(string id, DateTime dateTime)
        {
            try
            {
                return _context.EmployeeSchedules.Find(id, dateTime);
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }
    }
}
