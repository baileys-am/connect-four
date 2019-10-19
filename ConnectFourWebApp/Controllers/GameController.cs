using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ConnectFour;

namespace ConnectFourWebApp.Controllers
{
    [Route("api/[controller]")]
    public class GameController : Controller
    {
        public struct GameStatus
        {
            public string Board { get; set; }
        }

        private readonly ConnectFourGame _game = new ConnectFourGame();

        [HttpGet("[action]")]
        public GameStatus Status()
        {
            return new GameStatus()
            {
                Board = this._game.DrawBoard()
            };
        }
    }
}