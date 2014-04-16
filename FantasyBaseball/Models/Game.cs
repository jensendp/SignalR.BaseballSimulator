using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyBaseball.Models
{
    public class Game
    {
        private List<Batter> _homeBatters;
        private List<Pitcher> _homePitchers;
        private List<Batter> _awayBatters;
        private List<Pitcher> _awayPitchers;

        private string _name;
        private int _homeScore;
        private int _awayScore;
        private Inning _inning;
        private int _outs;
        private int _nextHomeBatter;
        private int _nextAwayBatter;
        private string _identifier;
        
        public Game(string identifier, string name)
        {
            var homeTeam = "Chicago Cubs";
            var awayTeam = "Pittsburg Pirates";
            _homeBatters = GetHomeBatters(homeTeam);
            _awayPitchers = GetAwayPitchers(awayTeam);
            _awayBatters = GetAwayBatters(awayTeam);
            _homePitchers = GetHomePitchers(homeTeam);
            _name = name;
            _homeScore = 0;
            _awayScore = 0;
            _inning = new Inning();
            _nextHomeBatter = 0;
            _nextAwayBatter = 1;
            _identifier = identifier;
            CurrentAtBat = new AtBat(_awayBatters[0], _homePitchers[0]);
        }

        public string Name 
        { 
            get { return _name; } 
        }

        public int HomeScore 
        { 
            get { return _homeScore; } 
        }

        public int AwayScore
        {
            get { return _awayScore; }
        }

        public string Inning
        {
            get { return _inning.ToString(); }
        }

        public int Outs
        {
            get { return _outs; }
        }

        public string Identifier
        {
            get { return _identifier; }
        }

        public AtBat CurrentAtBat { get; set; }

        public void AddHomeScore(int runs)
        {
            _homeScore += runs;
        }

        public void AddAwayScore(int runs)
        {
            _awayScore += runs;
        }

        public void AdvanceInning()
        {
            _inning.Advance();
            if (_inning.HalfInning == HalfInning.Top)
            {
                CurrentAtBat.Pitcher = _homePitchers[0];
            }
            else
            {
                CurrentAtBat.Pitcher = _awayPitchers[0];
            }

            AdvanceBatter();
            _outs = 0;
        }

        public void Hit(string type, string location)
        {
            CurrentAtBat.Hit(type, location);
            AdvanceBatter();
        }

        public void Out(string type, string location)
        {
            CurrentAtBat.Out(type, location);
            if (++_outs == 3)
                AdvanceInning();
            else
                AdvanceBatter();
        }

        public void Strike()
        {
            CurrentAtBat.Strikes++;
            if (CurrentAtBat.Strikes == 3)
                if (++_outs == 3)
                    AdvanceInning();
                else
                    AdvanceBatter();
        }

        public void Ball()
        {
            CurrentAtBat.Balls++;
            if (CurrentAtBat.Balls == 4)
                AdvanceBatter();
        }

        private void AdvanceBatter()
        {
            if(_inning.HalfInning == HalfInning.Top)
            {
                var batter = _nextAwayBatter >= _awayBatters.Count ? 0 : _nextAwayBatter;
                CurrentAtBat.Batter = _awayBatters[batter];
                _nextAwayBatter++;
            }
            else
            {
                var batter = _nextHomeBatter >= _homeBatters.Count ? 0 : _nextHomeBatter;
                CurrentAtBat.Batter = _homeBatters[batter];
                _nextHomeBatter++;
            }
        }

        private List<Batter> GetHomeBatters(string teamName)
        {
            return new List<Batter> {
                new Batter { Name = "Home Player 1", Number = 1, Position = "P", Team = teamName, Bats = "R"},
                new Batter { Name = "Home Player 2", Number = 2, Position = "C", Team = teamName, Bats = "L"},
                new Batter { Name = "Home Player 3", Number = 3, Position = "1B", Team = teamName, Bats = "R"},
                new Batter { Name = "Home Player 4", Number = 4, Position = "2B", Team = teamName, Bats = "L"},
                new Batter { Name = "Home Player 5", Number = 5, Position = "3B", Team = teamName, Bats = "R"},
                new Batter { Name = "Home Player 6", Number = 6, Position = "SS", Team = teamName, Bats = "R"},
                new Batter { Name = "Home Player 7", Number = 7, Position = "LF", Team = teamName, Bats = "L"},
                new Batter { Name = "Home Player 8", Number = 8, Position = "CF", Team = teamName, Bats = "R"},
                new Batter { Name = "Home Player 9", Number = 9, Position = "RF", Team = teamName, Bats = "L"},
            };
        }

        private List<Batter> GetAwayBatters(string teamName)
        {
            return new List<Batter> {
                new Batter { Name = "Away Player 1", Number = 1, Position = "P", Team = teamName, Bats = "R"},
                new Batter { Name = "Away Player 2", Number = 2, Position = "C", Team = teamName, Bats = "L"},
                new Batter { Name = "Away Player 3", Number = 3, Position = "1B", Team = teamName, Bats = "R"},
                new Batter { Name = "Away Player 4", Number = 4, Position = "2B", Team = teamName, Bats = "L"},
                new Batter { Name = "Away Player 5", Number = 5, Position = "3B", Team = teamName, Bats = "R"},
                new Batter { Name = "Away Player 6", Number = 6, Position = "SS", Team = teamName, Bats = "R"},
                new Batter { Name = "Away Player 7", Number = 7, Position = "LF", Team = teamName, Bats = "L"},
                new Batter { Name = "Away Player 8", Number = 8, Position = "CF", Team = teamName, Bats = "R"},
                new Batter { Name = "Away Player 9", Number = 9, Position = "RF", Team = teamName, Bats = "R"},
            };
        }

        private List<Pitcher> GetHomePitchers(string teamName)
        {
            return new List<Pitcher>
            {
                new Pitcher { Name = "Home Pitcher 1", Number = 25, Position = "P", Team = teamName, Throws = "L"}
            };
        }

        private List<Pitcher> GetAwayPitchers(string teamName)
        {
            return new List<Pitcher>
            {
                new Pitcher { Name = "Away Pitcher 1", Number = 13, Position = "P", Team = teamName, Throws = "R"}
            };
        }
    }
}