

using CoffeeMachine.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.EntityManagers.Interfaces
{
    public interface ICoffeeMachineManager
    {
        IQueryable<Dose> GetDoses();
        Task<Dose> CreateDose(Dose dose);

        IQueryable<Dose> GetLastDose(bool isBadge, string userName);
    }
}
