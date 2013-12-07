using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamTemplate
{
    class Statistic
    {
        public Statistic()
        {
            font = new SFML.Graphics.Font("../GFX/font.ttf");
        }

        private SFML.Graphics.Font font;

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            SFML.Graphics.Text text = new SFML.Graphics.Text();
        }
    }
}
