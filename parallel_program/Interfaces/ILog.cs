using parallel_program.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace parallel_program.Interfaces
{
    internal interface ILog
    {
        void RegisterAccess(User user);
    }
}
