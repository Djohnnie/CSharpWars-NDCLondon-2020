using System.Linq;
using CSharpWars.Common.Extensions;
using CSharpWars.Enums;
using CSharpWars.Processor.Middleware.Interfaces;
using CSharpWars.Scripting;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;

namespace CSharpWars.Processor.Middleware
{
    public class BotScriptCompiler : IBotScriptCompiler
    {
        public Script Compile(string script)
        {
            var decodedScript = script.Base64Decode();
            var mscorlib = typeof(object).Assembly;
            var systemCore = typeof(Enumerable).Assembly;
            var csharpScript = typeof(BotProperties).Assembly;
            var enums = typeof(PossibleMoves).Assembly;
            
            var scriptOptions = ScriptOptions.Default.AddReferences(mscorlib, systemCore, csharpScript, enums);
            scriptOptions = scriptOptions.WithImports("System", "System.Linq", "System.Collections", "System.Collections.Generic", "CSharpWars.Enums", "CSharpWars.Scripting");
            var botScript = CSharpScript.Create(decodedScript, scriptOptions, typeof(ScriptGlobals));
            
            botScript.Compile();

            return botScript;
        }
    }
}