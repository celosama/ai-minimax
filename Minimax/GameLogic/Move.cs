using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Minimax.GameLogic
{
    public class Move
    {
        Point position;

        public Move(Point position)
        {
            this.position = position;
        }

        public Point GetPosition()
        {
            return position;
        }
    }
}
