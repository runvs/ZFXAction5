﻿using System;
using JamUtilities;
using JamUtilities.Particles;
using JamUtilities.ScreenEffects;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    class Game
    {

        #region Fields

        private State _gameState;

        World _myWorld;
        Score _gameStats;

        float _timeTilNextInput = 0.0f;

        private Music _bgm;


        #endregion Fields

        #region Methods

        public Game()
        {
            // Predefine game state to menu
            _gameState = State.Menu;

            //TODO  Default values, replace with correct ones !
            SmartSprite._scaleVector = new Vector2f(2.0f, 2.0f);
            ScreenEffects.Init(new Vector2u(800, 600));
            ParticleManager.SetPositionRect(new FloatRect(-500, 0, 1400, 600));
            ParticleManager.Gravity = new Vector2f();
            Camera.MinPosition = new Vector2f(0, 0);
            Camera.MaxPosition = GameProperties.TileSizeInPixel * GameProperties.LevelSize * new Vector2f(1, 1) - new Vector2f(800, 600);
            //ParticleManager.Gravity = GameProperties.GravitationalAcceleration;
            try
            {
                SmartText._font = new Font("../GFX/font.ttf");

                SmartText._lineLengthInChars = 18;
                SmartText._lineSpread = 1.2f;
                _bgm = new Music("../SFX/ZFX_Action5_OST.ogg");
                _bgm.Loop = true;
                _bgm.Play();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void GetInput()
        {
            if (_timeTilNextInput < 0.0f)
            {
                if (_gameState == State.Menu)
                {
                    GetInputMenu();
                }
                else if (_gameState == State.Game)
                {
                    _myWorld.GetInput();
                }
                else if (_gameState == State.Credits || _gameState == State.Score)
                {
                    GetInputCreditsScore();
                }
            }
        }

        private char level = 'a';

        private void GetInputMenu()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.A))
            {
                level = 'a';
                StartGame();
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.B))
            {
                level = 'b';
                StartGame();
            }

            if (Keyboard.IsKeyPressed(Keyboard.Key.C))
            {
                ChangeGameState(State.Credits);
            }

        }

        private void GetInputCreditsScore()
        {
            if (Keyboard.IsKeyPressed(Keyboard.Key.Escape) || Keyboard.IsKeyPressed(Keyboard.Key.Return) || Keyboard.IsKeyPressed(Keyboard.Key.Space))
            {
                ChangeGameState(State.Menu, 1.0f);
            }
        }

        public void Update(float deltaT)
        {
            if (_timeTilNextInput >= 0.0f)
            {
                _timeTilNextInput -= deltaT;
            }

            CanBeQuit = false;
            if (_gameState == State.Game)
            {
                _myWorld.Update(Timing.Update(deltaT));

                if (_myWorld.Dead)
                {
                    _gameStats = new Score(_myWorld);
                    ChangeGameState(State.Score, 0.5f);
                }
            }
            else if (_gameState == State.Menu && this._timeTilNextInput <= 0.0f)
            {
                CanBeQuit = true;
            }
        }

        public void Draw(RenderWindow rw)
        {
            rw.Clear();
            if (_gameState == State.Menu)
            {
                DrawMenu(rw);
            }
            else if (_gameState == State.Game)
            {
                _myWorld.Draw(rw);
            }
            else if (_gameState == State.Credits)
            {
                DrawCredits(rw);
            }
            else if (_gameState == State.Score)
            {
                _gameStats.Draw(rw);
            }
        }

        private void DrawMenu(RenderWindow rw)
        {
            SmartText.DrawText("Yet Another Tower Defence Game", TextAlignment.MID, new Vector2f(400.0f, 150.0f), 1.5f, rw);

            SmartText.DrawText("Start Level [A] or [B]", TextAlignment.MID, new Vector2f(400.0f, 250.0f), rw);
            SmartText.DrawText("[Arrows] or [WASD] for Camera", TextAlignment.MID, new Vector2f(400.0f, 340.0f), rw);
            SmartText.DrawText("[Mouse] for Building Towers", TextAlignment.MID, new Vector2f(400.0f, 375.0f), rw);
            SmartText.DrawText("[1] [2] [3] for special abilities", TextAlignment.MID, new Vector2f(400.0f, 410.0f), rw);

            SmartText.DrawText("[C]redits", TextAlignment.LEFT, new Vector2f(30.0f, 550.0f), rw);
            ScreenEffects.GetStaticEffect("vignette").Draw(rw);
        }

        private void DrawCredits(RenderWindow rw)
        {

            SmartText.DrawText("Yet Another Tower Defence Game", TextAlignment.MID, new Vector2f(400.0f, 20.0f), 1.5f, rw);

            SmartText.DrawText("A Game by", TextAlignment.MID, new Vector2f(400.0f, 100.0f), 0.75f, rw);
            SmartText.DrawText("Julian Dinges and Simon Weis", TextAlignment.MID, new Vector2f(400.0f, 135.0f), rw);

            SmartText.DrawText("Visual Studio 2012 \t C#", TextAlignment.MID, new Vector2f(400, 170), 0.75f, rw);
            SmartText.DrawText("aseprite \t SFML.NET 2.1", TextAlignment.MID, new Vector2f(400, 200), 0.75f, rw);
            SmartText.DrawText("Cubase 5 \t SFXR", TextAlignment.MID, new Vector2f(400, 230), 0.75f, rw);

            SmartText.DrawText("Thanks to", TextAlignment.MID, new Vector2f(400, 350), 0.75f, rw);
            SmartText.DrawText("Families & Friends for their great support", TextAlignment.MID, new Vector2f(400, 375), 0.75f, rw);

            SmartText.DrawText("Created 2014-10-19 for ZFXAction 5", TextAlignment.MID, new Vector2f(400.0f, 500.0f), 0.75f, rw);
            ScreenEffects.GetStaticEffect("vignette").Draw(rw);
        }

        private void ChangeGameState(State newState, float inputdeadTime = 0.5f)
        {
            this._gameState = newState;
            _timeTilNextInput = inputdeadTime;
        }


        private void StartGame()
        {
            _myWorld = new World(level);
            ChangeGameState(State.Game, 0.1f);
        }


        #endregion Methods

        #region Subclasses/Enums

        private enum State
        {
            Menu,
            Game,
            Score,
            Credits
        }

        #endregion Subclasses/Enums


        public bool CanBeQuit { get; set; }
    }
}
