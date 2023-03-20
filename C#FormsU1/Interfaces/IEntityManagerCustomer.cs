using GameCenter.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCenter.Interfaces
{
    public interface IEntityManagerCustomer
    {
        void Create(CustomerForm form);
        void Update(CustomerForm form);
        void Delete(CustomerForm form);
        void View(CustomerForm form);
        void Load(CustomerForm form);

    }
}
