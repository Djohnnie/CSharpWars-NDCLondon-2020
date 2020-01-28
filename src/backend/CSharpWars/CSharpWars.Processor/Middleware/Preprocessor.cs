using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpWars.Model;
using CSharpWars.Processor.Middleware.Interfaces;

namespace CSharpWars.Processor.Middleware
{
    public class Preprocessor : IPreprocessor
    {
        public Task<ProcessingContext> Go(Arena arena, IList<Bot> bots)
        {
            throw new NotImplementedException();
        }
    }
}