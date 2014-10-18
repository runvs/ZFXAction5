using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace JamTemplate
{
    public enum eDirection
    {
        NORTH,
        SOUTH,
        EAST,
        WEST
    }

    public static class Direction
    {
        public static Vector2i GetDirectionFromEnum(eDirection dir)
        {
            Vector2i ret = new Vector2i(0, 0);
            if (dir == eDirection.EAST)
            {
                ret = new Vector2i(1, 0);
            }
            else if (dir == eDirection.WEST)
            {
                ret = new Vector2i(-1, 0);
            }
            else if (dir == eDirection.NORTH)
            {
                ret = new Vector2i(0, 1);
            }
            else if (dir == eDirection.SOUTH)
            {
                ret = new Vector2i(0, -1);
            }

            return ret;
        }


        public static Vector2f GetDirectionFloatFromEnum(eDirection dir)
        {
            Vector2f ret = new Vector2f(0, 0);
            if (dir == eDirection.EAST)
            {
                ret = new Vector2f(1, 0);
            }
            else if (dir == eDirection.WEST)
            {
                ret = new Vector2f(-1, 0);
            }
            else if (dir == eDirection.NORTH)
            {
                ret = new Vector2f(0, 1);
            }
            else if (dir == eDirection.SOUTH)
            {
                ret = new Vector2f(0, -1);
            }

            return ret;
        }

    }
}
