using JamUtilities;
using JamUtilities.Particles;
using JamUtilities.ScreenEffects;
using SFML.Graphics;
using SFML.Window;
using System.Collections.Generic;
using Mouse = SFML.Window.Mouse;

// ReSharper disable once SuggestUseVarKeywordEvident

namespace JamTemplate
{
    public class World
    {

        #region Fields

        private List<Prisoner> _prisonersList;
        private List<Tower> _towers;
        private Level _level;

        private int Lives { get; set; }
        public int Kills { get; set; }
        public bool Dead { get; private set; }

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
                Camera.ShouldBePosition = Camera.ShouldBePosition + new Vector2f(-5.0f, 0);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Right))
            {
                Camera.ShouldBePosition = Camera.ShouldBePosition + new Vector2f(5.0f, 0);
            }
            if (Keyboard.IsKeyPressed(Keyboard.Key.Up))
            {
                Camera.ShouldBePosition = Camera.ShouldBePosition + new Vector2f(0, -5.0f);
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Down))
            {
                Camera.ShouldBePosition = Camera.ShouldBePosition + new Vector2f(0, 5.0f);
            }

            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                _isMouseDown = true;
            }
            else if (!Mouse.IsButtonPressed(Mouse.Button.Left) && _isMouseDown)
            {
                // Mouse up
                _isMouseDown = false;

                var mousePos = new Vector2f(
                    JamUtilities.Mouse.MousePositionInWindow.X,
                    JamUtilities.Mouse.MousePositionInWindow.Y
                );

                if (mousePos.X >= 0 && mousePos.Y >= 0)
                {
                    mousePos += Camera.CameraPosition;
                    mousePos /= GameProperties.TileSizeInPixel;

                    var tile = _level.GetTileAt(mousePos);

                    if (tile.Type == TileType.Buildzone)
                    {
                        TowerBuilder.ShowBuildMenu(tile);
                    }
                    else
                    {
                        // We need to check here if the player clicked inside
                        // the build window.
                        var selectedTower = TowerBuilder.ClickedInsideBuildMenu(mousePos);

                        if (selectedTower != TowerType.None)
                        {
                            _towers.Add(new Tower(selectedTower, tile.Position));
                        }

                        TowerBuilder.HideBuildMenu();
                    }
                }
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
                else
                {
                    if (p.finished)
                    {
                        Lives--;
                        CheckPlayerDead();
                    }
                    else
                    {
                        Kills += 1;
                    }
                }
            }
            _prisonersList = newPrisonerList;

            foreach (var tower in _towers)
            {
                tower.Update(timeObject);
            }
        }

        private void CheckPlayerDead()
        {
            if (Lives <= 0)
            {
                Dead = true;
            }
        }

        public void Draw(RenderWindow rw)
        {
            rw.Clear(Color.Blue);


            _level.Draw(rw);

            foreach (var p in _prisonersList)
            {
                p.Draw(rw);
            }

            foreach (var tower in _towers)
            {
                tower.Draw(rw);
            }

            TowerBuilder.Draw(rw);

            SmartText.DrawText("Lives: " + Lives, TextAlignment.LEFT, new Vector2f(10, 10), rw);


            ParticleManager.Draw(rw);
            ScreenEffects.GetStaticEffect("vignette").Draw(rw);
            ScreenEffects.Draw(rw);
        }

        private void InitGame()
        {
            _prisonersList = new List<Prisoner>();
            _towers = new List<Tower>();

            _level = LevelLoader.GetLevel(1, this);
            Lives = GameProperties.PlayerInitialLives;
            Dead = false;
        }

        #endregion Methods

        public void Spawn(Prisoner prisoner)
        {
            _prisonersList.Add(prisoner);
        }
    }
}
