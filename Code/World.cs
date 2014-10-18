using System.Collections.Generic;
using SFML.Graphics;
using JamUtilities;
using JamUtilities.Particles;
using JamUtilities.ScreenEffects;
using SFML.Window;

// ReSharper disable once SuggestUseVarKeywordEvident

namespace JamTemplate
{
    public class World
    {

        #region Fields

        private List<Prisoner> _prisonersList;
        private Level _level;

        #endregion Fields

        #region Methods

        public World()
        {
            InitGame();
        }

        public void GetInput()
        {
            if (SFML.Window.Keyboard.IsKeyPressed(SFML.Window.Keyboard.Key.C))
            {
                //ScreenEffects.ScreenFlash(SFML.Graphics.Color.Black, 4.0f);
            }
        }

        public void Update(TimeObject timeObject)
        {
            ScreenEffects.Update(timeObject);
            SpriteTrail.Update(timeObject);
            ParticleManager.Update(timeObject);


            _level.Update(timeObject);

            List<Prisoner> newPrisonerList = new List<Prisoner>();
            foreach (var p in _prisonersList)
            {
                p.Update(timeObject);
                if (!p.IsDead())
                {
                    newPrisonerList.Add((p));
                }
            }
            _prisonersList = newPrisonerList;
        }

        public void Draw(RenderWindow rw)
        {
            rw.Clear(SFML.Graphics.Color.Blue);


            _level.Draw(rw);

            foreach (var p in _prisonersList)
            {
                p.Draw(rw);
            }

            ParticleManager.Draw(rw);
            ScreenEffects.GetStaticEffect("vignette").Draw(rw);
            ScreenEffects.Draw(rw);
        }

        private void InitGame()
        {
            _prisonersList = new List<Prisoner>();
            _level = LevelLoader.GetLevel(1, this);
        }

        #endregion Methods


        public void Spawn(Prisoner prisoner)
        {
            _prisonersList.Add(prisoner);
        }
    }
}
