using AutoMapper;
using BlackJack.DataAccessLayer.Entities;
using BlackJack.ViewModels;

namespace BlackJack.BusinessLogicLayer
{
    public class MappersProfile: Profile
    {
        public MappersProfile()
        {
            CreateMap<GameViewModel, Game>();
            CreateMap<UserInGameViewModel, UserInGame>();
            CreateMap<RoundViewModel, GameRound>();
            CreateMap<Game, GameViewModel>();
            CreateMap<UserInGame, UserInGameViewModel>();
            CreateMap<GameRound, RoundViewModel>();
        }
    }
}
