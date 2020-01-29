# CSharpWars

![CSharp Wars Logo](https://www.djohnnie.be/csharpwars/logo.png "CSharp Wars Logo")

[Return to README](https://github.com/Djohnnie/CSharpWars-NDCLondon-2020)

[Return to step 8](https://github.com/Djohnnie/CSharpWars-NDCLondon-2020/blob/master/workshop/step08/step.md)

## Step 9

In order for the robot to actually do something, we need to work on our middleware processing project. Go to the backend solution in Visual Studio 2019 and select the *CSharpWars.Processor* project. If you just run it like it is, you'll see it throw a *NotImplementedException* every two seconds.

Find the *Middleware.cs" file and add the following code to the *Process* method:

```c#
public async Task Process()
{
    var arena = await _arenaLogic.GetArena();
    var bots = await _botLogic.GetBots();

    var context = await _preprocessor.Go(arena, bots);
    await _processor.Go(context);
    await _postprocessor.Go(context);

    await _botLogic.UpdateBots(context.Bots);
}
```

This piece of code will get the arena dimensions and the list of robots from the database. Next, it will run the data through three stages:

* Preprocessing: getting the data ready for processing.
* Processing: compiling and running the C#-bot-scripts using the Microsoft Compiler Platform (Roslyn)
* Postprocessing: perform the bot-movements, based on the result after processing.

In the end, it will update the state for all bots in the database.

Next: find the *Preprocessor.cs* file and add the following code to the *Go* method:

```c#
public Task<ProcessingContext> Go(Arena arena, IList<Bot> bots)
{
    var processingContext = new ProcessingContext(arena, bots);

    foreach (var bot in bots)
    {
        var botProperties = new BotProperties(arena, bot, bots);
        processingContext.AddBotProperties(bot.Id, botProperties);
    }

    return Task.FromResult(processingContext);
}
```

This piece of code will convert the arena and list of bots to *BotProperties* objects for further processing.

Next: find the *Processor.cs* file and add the following code to it:

```c#
public async Task Go(ProcessingContext context)
{
    var botProcessing = context.Bots.Select(bot => Process(bot, context));
    await Task.WhenAll(botProcessing);
}

public async Task Process(Bot bot, ProcessingContext context)
{
    throw new NotImplementedException();
}
```

This piece of code will call the async *Process* method for each bot and will await all the *Task* results in parallel.

Add the following code to the *Process* method:

```c#
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
```

This piece of code gets the *BotProperties* for the current *Bot*, compiles the C# script and runs the script using the *ScriptGlobals* as the running context. If something goes wrong, the current move for the robot will be set to *ScriptError* and it will look confused in the frontend.

The method *GetCompiledBotScript* does not yet exist, so add it below the *Process* method:

```c#
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
```

This piece of code compiles the script if it wasn't compiled before. Compiling a script takes some time, and we don't need to compile it multiple times. Therefore, we can cache the compiled result.

To see the script compilation happening, find the *BotScriptCompiler.cs" file and add the following code to the *Compile* method:

```c#
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
```

This piece of code uses the Microsoft Compiler Platform (Roslyn) to turn the string-value containing C# code into a compiled and runnable script. For this to work, we need to add references and imports to it, just like we could do in our projects in Visual Studio.

Next, find the *Postprocessor.cs* file and add the following code to the *Go* method:

```c#
public Task Go(ProcessingContext context)
{
    foreach (var bot in context.Bots)
    {
        var botProperties = context.GetBotProperties(bot.Id);

        var botResult = Move.Build(botProperties).Go();
        bot.Orientation = botResult.Orientation;
        bot.X = botResult.X;
        bot.Y = botResult.Y;
        bot.CurrentStamina = botResult.CurrentStamina < 0 ? 0 : botResult.CurrentStamina;
        bot.Move = botResult.Move;
        bot.Memory = botResult.Memory.Serialize();
    }

    return Task.CompletedTask;
}
```

This piece of code builds a *Move* object from the *BotProperties*. After processing, the *BotProperties* will contain a specific move and the *Move* object is responsible to actually perform this move on the *BotProperties* object. The result from this action is a *BotResult* object that can help to update the actual *Bot* object that will be updated in the database.


[Continue to step 10](https://github.com/Djohnnie/CSharpWars-NDCLondon-2020/blob/master/workshop/step10/step.md)