using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.IBll
{
    public interface ILoginServices
    {
        LoginResult Loginres(Users entity);
    }
}
