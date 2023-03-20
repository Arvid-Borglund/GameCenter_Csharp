using GameCenter.Interfaces;
using GameCenter.Models;
using GameCenter.DAL.DALErrorHandling;

namespace GameCenter.DAL
{//Written partly with AI assistance
    internal class LoginRepository : ICRUDRepository<Login>
    {
        private readonly GameCenterDatabaseContext _context;

        public LoginRepository(GameCenterDatabaseContext context)
        {
            _context = context;
        }

        // Create CRUD for login class

        public void Create(Login login)
        {
            try
            {
                _context.Logins.Add(login);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public Login GetById(string id)
        {
            try
            {
                return _context.Logins.Find(id);
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }


        public List<Login> GetAll()
        {
            try
            {
                return _context.Logins.ToList();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
                return null;
            }
        }

        public void Update(Login login)
        {
            try
            {
                _context.Logins.Update(login);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }

        public void Delete(Login login)
        {
            try
            {
                _context.Logins.Remove(login);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                DAL_ExceptionHandler.HandleSqlException(ex);
            }
        }


        Login ICRUDRepository<Login>.GetByCompositeId(string id, DateTime dateTime)
        {
            throw new NotImplementedException();
        }

    }
}
