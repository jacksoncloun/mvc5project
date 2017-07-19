using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IBll
{
    public interface IUserServices
    {
        List<Users> getAlluser();
        void UpdateUser(Users entity);
        Users GetUserById(int id);
    }
}
