using System;
using System.Threading.Tasks;
using CSharpWars.Processor.Middleware.Interfaces;
using Microsoft.Extensions.Logging;

namespace CSharpWars.Processor.Middleware
{
    public class Processor : IProcessor
    {
        private readonly IBotScriptCompiler _botScriptCompiler;
        private readonly IBotScriptCache _botScriptCache;
        private readonly ILogger<Processor> _logger;

        public Processor(
            IBotScriptCompiler botScriptCompiler,
            IBotScriptCache botScriptCache,
            ILogger<Processor> logger)
        {
            _botScriptCompiler = botScriptCompiler;
            _botScriptCache = botScriptCache;
            _logger = logger;
        }

        public async Task Go(ProcessingContext context)
        {
            throw new NotImplementedException();
        }
    }
}