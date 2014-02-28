using SFML.Graphics;
using System;
using JamUtilities;
using JamUtilities.Particles;

namespace JamTemplate
{
    class World
    {

        #region Fields

        #endregion Fields

        #region Methods

        public World()
        {
            InitGame();
        }

        public void GetInput()
        {

        }

        public void Update(float deltaT)
        {
            ScreenEffects.Update(deltaT);
            SpriteTrail.Update(deltaT);
            ParticleManager.Update(deltaT);
        }

        public void Draw(RenderWindow rw)
        {
            ParticleManager.Draw(rw);

            ScreenEffects.DrawFadeRadial(rw);
            ScreenEffects.Draw(rw);
        }

        private void InitGame()
        {
        }

        #endregion Methods

    }
}
