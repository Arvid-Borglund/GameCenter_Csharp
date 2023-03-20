using GameCenter.DAL.DALErrorHandling;
using GameCenter.Interfaces;
using GameCenter.Models;

namespace GameCenter.DAL
{    //Written partly with AI assistance
    internal class ComputerRepository : ICRUDRepository<Computer>
    {
        private readonly GameCenterDatabaseContext _context;

        public ComputerRepository(GameCenterDatabaseContext context)
        {
            _context = context;


        }

        public void Create(Computer computer)
        {
            try
            {
                _context.Computers.Add(computer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public List<Computer> GetAll()
        {
            try
            {
                return _context.Computers.ToList();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }

        public Computer GetById(String id)
        {
            try
            {
                return _context.Computers.Find(id);
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }

        public void Update(Computer computer)
        {
            try
            {
                _context.Computers.Update(computer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public void Delete(Computer computer)
        {
            try
            {
                _context.Computers.Remove(computer);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        Computer ICRUDRepository<Computer>.GetByCompositeId(string id, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

    }
}
