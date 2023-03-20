using GameCenter.Forms;

namespace GameCenter.Interfaces
{
    public interface IEntityManager
    {
        void Create(EmployeeForm form);
        void Update(EmployeeForm form);
        void Delete(EmployeeForm form);
        void View(EmployeeForm form);
        void Load(EmployeeForm form);

    }
}
