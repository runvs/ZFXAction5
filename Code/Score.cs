using SFML.Graphics;

namespace JamTemplate
{
    class Score
    {

        #region Fields

        private int Kills;

        #endregion Fields

        #region Methods

        public Score(World world)
        {
            Kills = world.Kills;
        }

        public void Draw(RenderWindow rw)
        {
        }

        #endregion Methods

    }
}
