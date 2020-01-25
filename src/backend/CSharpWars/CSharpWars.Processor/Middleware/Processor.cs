using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpWars.Enums;
using CSharpWars.Model;
using CSharpWars.Processor.Middleware.Interfaces;
using CSharpWars.Scripting;
using Microsoft.CodeAnalysis.Scripting;
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
            var botProcessing = context.Bots.Select(bot => Process(bot, context));
            await Task.WhenAll(botProcessing);
        }

        public async Task Process(Bot bot, ProcessingContext context)
        {
            var botProperties = context.GetBotProperties(bot.Id);

            try
            {
                var botScript = GetCompiledBotScript(bot);
                var scriptGlobals = new ScriptGlobals(botProperties);
                await botScript.RunAsync(scriptGlobals);
            }
            catch
            {
                botProperties.ChangeCurrentMove(PossibleMoves.ScriptError);
            }
        }

        private Script GetCompiledBotScript(Bot bot)
        {
            if (_botScriptCache.ScriptStored(bot.Id))
            {
                return _botScriptCache.LoadScript(bot.Id);
            }

            try
            {
                var botScript = _botScriptCompiler.Compile(bot.Script);
                _botScriptCache.StoreScript(bot.Id, botScript);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex}");
            }

            return _botScriptCache.LoadScript(bot.Id);
        }
    }
}