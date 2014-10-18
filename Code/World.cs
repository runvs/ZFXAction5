using System;
using System.Collections.Generic;
using JamUtilities;
using JamUtilities.Particles;
using JamUtilities.ScreenEffects;
using SFML.Graphics;
using SFML.Window;
using Mouse = SFML.Window.Mouse;

// ReSharper disable once SuggestUseVarKeywordEvident

namespace JamTemplate
{
    public class World
    {

        #region Fields

        private List<Prisoner> _prisonersList;
        private Level _level;

        private bool _isMouseDown;

        #endregion Fields

        #region Methods

        public World()
        {
            InitGame();
        }

        public void GetInput()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.C))
            {
                //ScreenEffects.ScreenFlash(SFML.Graphics.Color.Black, 4.0f);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Left))
            {
                Camera.ShouldBePosition = Camera.ShouldBePosition + new Vector2f(-10.0f, 0);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                Camera.ShouldBePosition = Camera.ShouldBePosition + new Vector2f(10.0f, 0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                Camera.ShouldBePosition = Camera.ShouldBePosition + new Vector2f(0, -10.0f);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                Camera.ShouldBePosition = Camera.ShouldBePosition + new Vector2f(0, 10.0f);
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                _isMouseDown = true;
            }
            else if (!Mouse.IsButtonPressed(Mouse.Button.Left) && _isMouseDown)
            {
                // Mouse up
                _isMouseDown = false;

                Console.WriteLine("Mouse up");
            }
        }

        public void Update(TimeObject timeObject)
        {
            ScreenEffects.Update(timeObject);
            SpriteTrail.Update(timeObject);
            ParticleManager.Update(timeObject);
            Camera.DoCameraMovement(timeObject);

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
            rw.Clear(Color.Blue);


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
