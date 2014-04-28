using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using FantasyBaseball.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FantasyBaseball
{
    [Authorize]
    [HubName("games")]
    public class GameHub : Hub
    {
        private readonly GameWatcher _watcher;

        public GameHub()
            : this(GameWatcher.Instance)
        {

        }

        public override System.Threading.Tasks.Task OnConnected()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindById(HttpContext.Current.User.Identity.GetUserId());
            foreach (var team in user.FavoriteTeams)
            {
                Groups.Add(Context.ConnectionId, team.Name);
            }
            return base.OnConnected();
        }

        public override System.Threading.Tasks.Task OnDisconnected()
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            var user = manager.FindById(Context.User.Identity.GetUserId());
            foreach (var team in user.FavoriteTeams)
            {
                Groups.Remove(Context.ConnectionId, team.Name);
            }
            return base.OnDisconnected();
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