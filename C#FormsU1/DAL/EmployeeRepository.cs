using GameCenter.Interfaces;
using GameCenter.Models;
using GameCenter.DAL.DALErrorHandling;

namespace GameCenter.DAL
{//Written partly with AI assistance
    internal class EmployeeRepository : ICRUDRepository<Employee>
    {
        private readonly GameCenterDatabaseContext _context;

        public EmployeeRepository(GameCenterDatabaseContext context)
        {
            _context = context;

        }


        public void Create(Employee employee)
        {
            try
            {
                _context.Employees.Add(employee);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public Employee GetById(String id)
        {
            try
            {
                return _context.Employees.Find(id);
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }

        public List<Employee> GetAll()
        {
            try
            {
                return _context.Employees.ToList();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }

        public void Update(Employee employee)
        {
            try
            {
                _context.Employees.Update(employee);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public void Delete(Employee employee)
        {
            try
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }


        Employee ICRUDRepository<Employee>.GetByCompositeId(string id, DateTime dateTime)
        {
            throw new NotImplementedException();
        }



    }
}
