using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpWars.Model;

namespace CSharpWars.Processor.Middleware.Interfaces
{
    public interface IPreprocessor
    {
        Task<ProcessingContext> Go(Arena arena, IList<Bot> bots);
    }
}