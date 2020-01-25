using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpWars.Model;

namespace CSharpWars.Logic.Interfaces
{
    public interface IBotLogic : ILogic
    {
        Task<IList<Bot>> GetBots();

        Task<IList<BotInfo>> GetBotInfo();

        Task<BotInfo> CreateBot(BotToCreate botToCreate);

        Task UpdateBots(IList<Bot> bots);
    }
}