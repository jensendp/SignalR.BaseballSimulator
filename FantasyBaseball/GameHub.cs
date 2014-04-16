﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using FantasyBaseball.Models;

namespace FantasyBaseball
{
    [HubName("games")]
    public class GameHub : Hub
    {
        private readonly GameWatcher _watcher;

        public GameHub()
            : this(GameWatcher.Instance)
        {

        }

        public GameHub(GameWatcher watcher)
        {
            _watcher = watcher;
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _watcher.GetAllGames();
        }

        public string GetGamesState()
        {
            return _watcher.GamesState.ToString();
        }

        public void RunGames()
        {
            _watcher.StartWatcher();
        }

        public void PauseGames()
        {
            _watcher.PauseWatcher();
        }

        public void Reset()
        {
            _watcher.Reset();
        }
    }
}