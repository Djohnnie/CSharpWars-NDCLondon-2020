using System;
using System.Threading.Tasks;
using CSharpWars.Processor.Middleware.Interfaces;

namespace CSharpWars.Processor.Middleware
{
    public class Postprocessor : IPostprocessor
    {
        public Task Go(ProcessingContext context)
        {
            throw new NotImplementedException();
        }
    }
}