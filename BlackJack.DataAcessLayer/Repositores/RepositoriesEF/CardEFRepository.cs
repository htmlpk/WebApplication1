﻿using BlackJack.DataAccessLayer.Context;
using BlackJack.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlackJack.DataAccessLayer.Repository
{
    public class CardEFRepository : BaseEFRepository<GameRound>, ICardRepository
    {
        public CardEFRepository(ApplicationDbContext database)
            :base(database)
        {
        }

        public override async Task<GameRound> FindById(string id)
        {
            return _database.GameRounds.Where(item=>item.Id == Guid.Parse(id)).First();
        }
        public async Task<IEnumerable<GameRound>> FindByGameId(Guid id)
        {
            return _database.GameRounds.Where(item=>item.GameId == id);
        }

        public async Task<IEnumerable<GameRound>> FindByUserId(Guid id)
        {
            return _database.GameRounds.Where(item => item.UserInGameId == id);
        }
    }
}