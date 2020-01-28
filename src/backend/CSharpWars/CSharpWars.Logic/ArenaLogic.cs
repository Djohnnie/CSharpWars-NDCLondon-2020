using System;
using CSharpWars.Logic.Interfaces;
using System.Threading.Tasks;
using CSharpWars.Common.Configuration.Interfaces;
using CSharpWars.Model;

namespace CSharpWars.Logic
{
    public class ArenaLogic : IArenaLogic
    {
        private readonly IConfigurationHelper _configurationHelper;

        public ArenaLogic(IConfigurationHelper configurationHelper)
        {
            _configurationHelper = configurationHelper;
        }

        public Task<Arena> GetArena()
        {
            throw new NotImplementedException();
        }
    }
}