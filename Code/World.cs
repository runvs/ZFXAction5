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
        private List<Tower> _towers;
        private List<Projectile> _projectiles;
        private Level _level;

        private int Lives { get; set; }
        public int Kills { get; set; }
        public bool Dead { get; private set; }

        public int CareerPoints { get; private set; }

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

            if (Keyboard.IsKeyPressed(Keyboard.Key.Num1))
            {
                SpecialAbilities.FreezePrisoners();
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num2))
            {
                SpecialAbilities.DamagePrisoners();
            }
            else if (Keyboard.IsKeyPressed(Keyboard.Key.Num3))
            {
                //SpecialAbilities.DamagePrisoners();
            }
            else if (!Mouse.IsButtonPressed(Mouse.Button.Left) && _isMouseDown)
            {
                // Mouse up
                _isMouseDown = false;

                BuildOrUpgradeTower();
            }
        }

        private void BuildOrUpgradeTower()
        {
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
                else if (tile.Type == TileType.Tower)
                {
                    TowerBuilder.ShowUpgradeMenu(tile);
                }
                else
                {
                    // We need to check here if the player clicked inside
                    // the build/upgrade window.
                    if (TowerBuilder.IsBuildMenuShown)
                    {
                        var selectedTower = TowerBuilder.ClickedInsideBuildMenu(mousePos);

                        if (selectedTower != TowerType.None)
                        {
                            var tower = new Tower(selectedTower, TowerBuilder.AffectedTile.Position, this);

                            if (tower.BaseCost <= CareerPoints)
                            {
                                _towers.Add(tower);
                                CareerPoints -= tower.BaseCost;
                                TowerBuilder.AffectedTile.Type = TileType.Tower;
                            }
                            else
                            {
                                Console.WriteLine("Not enough career points!");
                            }
                        }
                    }
                    else if (TowerBuilder.IsUpgradeMenuShown)
                    {
                        var action = TowerBuilder.ClickedInsideUpgradeMenu(mousePos);
                        Tower towerToRemove = null;

                        // Find the tower
                        foreach (var tower in _towers)
                        {
                            if (tower.Intersects(TowerBuilder.AffectedTile.Sprite.Sprite.GetGlobalBounds()))
                            {
                                if (action == "UPGRADE")
                                {
                                    if (tower.CalculateUpgradeCosts() <= CareerPoints)
                                    {
                                        tower.Upgrade();
                                        CareerPoints -= tower.CalculateUpgradeCosts();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Not enough career points!");
                                    }
                                }
                                else if (action == "SELL")
                                {
                                    towerToRemove = tower;
                                    CareerPoints += tower.BaseCost;
                                    TowerBuilder.AffectedTile.Type = TileType.Buildzone;
                                    break;
                                }
                            }
                        }

                        if (towerToRemove != null) _towers.Remove(towerToRemove);
                    }

                    TowerBuilder.HideMenus();
                }
            }
        }

        public void Update(TimeObject timeObject)
        {
            ScreenEffects.Update(timeObject);
            SpriteTrail.Update(timeObject);
            ParticleManager.Update(timeObject);
            Camera.DoCameraMovement(timeObject);

            SpecialAbilities.Update(timeObject);

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
                        CareerPoints += GameProperties.CareerPointsGainForPrisonerKill;
                    }
                }
            }
            _prisonersList = newPrisonerList;

            //Console.WriteLine(_projectiles.Count);
            List<Projectile> newProjectileList = new List<Projectile>();
            foreach (var p in _projectiles)
            {
                p.Update(timeObject);

                if (!p.IsDead())
                {
                    newProjectileList.Add(p);
                }
                else
                {
                    p._target.TakeDamage(p.Damage);
                    if (p._tower.Type == TowerType.Splash)
                    {
                        DoSplashDamage(p._target, p);
                    }
                    else if (p._tower.Type == TowerType.Freeze)
                    {
                        p._target.Freeze(GameProperties.TowerFreezeFreezeTime);
                    }
                }
            }
            _projectiles = newProjectileList;


            foreach (var tower in _towers)
            {
                tower.Update(timeObject);
            }
        }

        private void DoSplashDamage(Prisoner prisoner, Projectile proj)
        {
            if (proj._tower.Type == TowerType.Splash)
            {
                foreach (var p in _prisonersList)
                {
                    if (p != prisoner)
                    {
                        Vector2i distanceInTiles = p.PositionInTiles - prisoner.PositionInTiles;
                        float dist = (float)(Math.Sqrt((distanceInTiles.X * distanceInTiles.X + distanceInTiles.Y * distanceInTiles.Y)));
                        if (dist <= GameProperties.TowerSpashDamageRange)
                        {
                            p.TakeDamage(proj.Damage);
                        }
                    }
                }
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

            foreach (var p in _prisonersList)
            {
                p.DrawHealthShape(rw);
            }
            foreach (var p in _projectiles)
            {
                p.Draw(rw);
            }

            TowerBuilder.Draw(rw, this);
            SpecialAbilities.Draw(rw);

            SmartText.DrawText("Lives: " + Lives, TextAlignment.LEFT, new Vector2f(10, 10), rw);
            SmartText.DrawText("Career Points: " + CareerPoints, TextAlignment.LEFT, new Vector2f(10, 35), rw);


            ParticleManager.Draw(rw);
            ScreenEffects.GetStaticEffect("vignette").Draw(rw);
            ScreenEffects.Draw(rw);
        }

        private void InitGame()
        {
            _prisonersList = new List<Prisoner>();
            _towers = new List<Tower>();
            _projectiles = new List<Projectile>();

            _level = LevelLoader.GetLevel(1, this);
            Lives = GameProperties.PlayerInitialLives;
            Dead = false;

            CareerPoints = GameProperties.InitialCareerPoints;
            TowerBuilder.HideMenus();

            SpecialAbilities.SetWorld(this);

        }

        public void SpawnProjectile(Projectile p)
        {
            //
            _projectiles.Add(p);
        }

        public void Spawn(Prisoner prisoner)
        {
            _prisonersList.Add(prisoner);
        }

        public void FreezePrisoners()
        {
            foreach (var p in _prisonersList)
            {
                p.Freeze(GameProperties.SpecialAbilitiesFreezeTime);
            }
        }

        public void DamageAllPrisoners()
        {
            foreach (var p in _prisonersList)
            {
                p.TakeDamage(p.Health * GameProperties.SpecialAbilitiesDamageFactor);
            }
        }

        public Prisoner GetPrisonerNextTo(Vector2i pos)
        {
            Prisoner retVal = null;

            float mindist = float.MaxValue;

            foreach (var p in _prisonersList)
            {
                Vector2i distance = pos - p.PositionInTiles;
                float dist = distance.X * distance.X + distance.Y * distance.Y;
                if (dist <= mindist)
                {
                    retVal = p;
                    mindist = dist;
                }
            }

            return retVal;
        }

        #endregion Methods


    }
}
