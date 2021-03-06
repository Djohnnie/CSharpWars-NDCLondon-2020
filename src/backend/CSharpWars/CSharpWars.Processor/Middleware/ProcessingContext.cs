﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using CSharpWars.Model;
using CSharpWars.Scripting;

namespace CSharpWars.Processor.Middleware
{
    public class ProcessingContext
    {
        private readonly ConcurrentDictionary<Guid, BotProperties> _botProperties = new ConcurrentDictionary<Guid, BotProperties>();

        public Arena Arena { get; }
        public IList<Bot> Bots { get; }


        public ProcessingContext(Arena arena, IList<Bot> bots)
        {
            Arena = arena;
            Bots = bots;
        }


        public void AddBotProperties(Guid botId, BotProperties botProperties)
        {
            _botProperties.TryAdd(botId, botProperties);
        }

        public BotProperties GetBotProperties(Guid botId)
        {
            return _botProperties[botId];
        }

        //public void UpdateBotProperties(BotDto bot)
        //{
        //    foreach (var botProperties in _botProperties.Values)
        //    {
        //        var botToUpdate = botProperties.Bots.Single(x => x.Id == bot.Id);
        //        botToUpdate.Update(bot);
        //    }

        //    var botPropertiesToUpdate = _botProperties.Values.Single(x => x.BotId == bot.Id);
        //    botPropertiesToUpdate.Update(bot);
        //}
    }
}