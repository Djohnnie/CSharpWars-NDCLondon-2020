using System.Threading.Tasks;
using CSharpWars.Model;

namespace CSharpWars.Logic.Interfaces
{
    public interface IArenaLogic : ILogic
    {
        Task<Arena> GetArena();
    }
}