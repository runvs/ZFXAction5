using System;
using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public static class TowerBuilder
    {
        public static bool IsBuildMenuShown { get; private set; }
        public static bool IsUpgradeMenuShown { get; private set; }
        private static SmartSprite _buildMenuSprite = new SmartSprite("../GFX/buildMenu.png");
        private static SmartSprite _upgradeMenuSprite = new SmartSprite("../GFX/upgradeMenu.png");
        private static RectangleShape _shape = new RectangleShape(new Vector2f(GameProperties.TileSizeInPixel, GameProperties.TileSizeInPixel));
        public static Tile AffectedTile { get; private set; }
        public static Tower AffectedTower { get; private set; }

        static TowerBuilder()
        {
            _shape.FillColor = new Color(32, 32, 32, 200);
        }

        public static void ShowBuildMenu(Tile tile)
        {
            if (IsUpgradeMenuShown)
            {
                HideMenus();
            }

            _buildMenuSprite.Position = tile.GetOnScreenPosition();
            _buildMenuSprite.Origin = new Vector2f(48, 32);
            AffectedTile = tile;

            IsBuildMenuShown = true;
        }

        public static void ShowUpgradeMenu(Tile tile)
        {
            if (IsBuildMenuShown)
            {
                HideMenus();
            }

            _upgradeMenuSprite.Position = tile.GetOnScreenPosition();
            _upgradeMenuSprite.Origin = new Vector2f(16, 32);
            AffectedTile = tile;

            IsUpgradeMenuShown = true;
        }

        public static TowerType ClickedInsideBuildMenu(Vector2f mousePos)
        {
            mousePos *= GameProperties.TileSizeInPixel;

            var bounds = _buildMenuSprite.Sprite.GetGlobalBounds();
            if (bounds.Contains(mousePos.X - Camera.CameraPosition.X, mousePos.Y - Camera.CameraPosition.Y))
            {
                // The player clicked inside the build menu
                var selectedPosition = (int)Math.Round(
                    (mousePos.X - _buildMenuSprite.Origin.X - bounds.Left - Camera.CameraPosition.X) / GameProperties.TileSizeInPixel
                );

                switch (selectedPosition)
                {
                    case 0:
                        return TowerType.Melee;
                    case 1:
                        return TowerType.Normal;
                    case 2:
                        return TowerType.Splash;
                    case 3:
                        return TowerType.Freeze;

                    default:
                        Console.WriteLine("Something bad happened...");
                        return TowerType.None;
                }
            }

            return TowerType.None;
        }

        public static string ClickedInsideUpgradeMenu(Vector2f mousePos)
        {
            mousePos *= GameProperties.TileSizeInPixel;

            var bounds = _upgradeMenuSprite.Sprite.GetGlobalBounds();
            if (bounds.Contains(mousePos.X - Camera.CameraPosition.X, mousePos.Y - Camera.CameraPosition.Y))
            {
                // The player clicked inside the upgrade menu
                var selectedPosition = (int)Math.Round(
                    (mousePos.X - _upgradeMenuSprite.Origin.X - bounds.Left - Camera.CameraPosition.X) /
                    GameProperties.TileSizeInPixel
                );

                return selectedPosition == 0 ? "UPGRADE" : "SELL";
            }

            return null;
        }

        public static void HideMenus()
        {
            IsUpgradeMenuShown = false;
            IsBuildMenuShown = false;
            AffectedTile = null;
        }

        public static void Draw(RenderWindow rw, World world)
        {
            if (IsBuildMenuShown)
            {
                _buildMenuSprite.Position = AffectedTile.GetOnScreenPosition();
                _buildMenuSprite.Draw(rw);

                var bounds = _buildMenuSprite.Sprite.GetGlobalBounds();

                _shape.Position = new Vector2f(bounds.Left + GameProperties.TileSizeInPixel * 0, bounds.Top);
                if (world.CareerPoints < GameProperties.TowerMeleeBaseCost)
                {
                    rw.Draw(_shape);
                }

                _shape.Position = new Vector2f(bounds.Left + GameProperties.TileSizeInPixel * 1, bounds.Top);
                if (world.CareerPoints < GameProperties.TowerNormalBaseCost)
                {
                    rw.Draw(_shape);
                }

                _shape.Position = new Vector2f(bounds.Left + GameProperties.TileSizeInPixel * 2, bounds.Top);
                if (world.CareerPoints < GameProperties.TowerSplashBaseCost)
                {
                    rw.Draw(_shape);
                }

                _shape.Position = new Vector2f(bounds.Left + GameProperties.TileSizeInPixel * 3, bounds.Top);
                if (world.CareerPoints < GameProperties.TowerFreezeBaseCost)
                {
                    rw.Draw(_shape);
                }
            }

            if (IsUpgradeMenuShown)
            {
                _upgradeMenuSprite.Position = AffectedTile.GetOnScreenPosition();
                _upgradeMenuSprite.Draw(rw);
            }
        }
    }
}
