using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FantasyBaseball.Models
{
    public class Inning
    {
        private HalfInning _halfInning;
        private int _inning;

        public Inning()
        {
            _halfInning = HalfInning.Top;
            _inning = 1;
        }

        public HalfInning HalfInning
        {
            get {return _halfInning;}
        }

        public override string ToString()
        {
            var inningString = string.Empty;
            var lastInningDigit = _inning % 10;

            switch (lastInningDigit)
            {
                case 1:
                    if (_inning == 11)
                        inningString = _inning + "th";
                    else
                        inningString = _inning + "st";
                    break;
                case 2:
                    inningString = _inning + "nd";
                    break;
                case 3:
                    inningString = _inning + "rd";
                    break;
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 0:
                    inningString = _inning + "th";
                    break;
            }

            return string.Format("{0} of the {1}", _halfInning.ToString(), inningString);
        }

        public void Advance()
        {
            if (_halfInning == HalfInning.Top)
                _halfInning = HalfInning.Bottom;
            else
            {
                _halfInning = HalfInning.Top;
                _inning++;
            }
        }
    }

    public enum HalfInning
    {
        Top,
        Bottom
    }
}