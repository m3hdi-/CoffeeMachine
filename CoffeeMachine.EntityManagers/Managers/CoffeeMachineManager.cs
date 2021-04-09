using CoffeeMachine.EntityManagers.Interfaces;
using CoffeeMachine.Models;
using CoffeeMachine.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeMachine.EntityManagers.Managers
{
    public class CoffeeMachineManager : ICoffeeMachineManager
    {
        private readonly machineContext _dbContext;

        public CoffeeMachineManager(machineContext dbContext)
        {
            _dbContext = dbContext;
        } 
        public async Task<Dose> CreateDose(Dose dose)
        {

            _dbContext.Doses.Add(dose);
            await _dbContext.SaveChangesAsync();

            return _dbContext.Doses.Where(u => u.Id == dose.Id).FirstOrDefault();
        }

        public IQueryable<Dose> GetDoses() => _dbContext.Doses;

        public IQueryable<Dose> GetLastDose(bool isBadge, string userName)
        {
            return _dbContext.Doses.Where(x => x.User == userName).OrderByDescending(c => c.CreatedAt);
        }
       
    }
}
        