using System;
using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public static class TowerBuilder
    {
        public static bool IsBuildMenuShown { get; private set; }
        private static SmartSprite _sprite = new SmartSprite("../GFX/buildMenu.png");
        public static Tile OriginTile { get; private set; }

        public static void ShowBuildMenu(Tile tile)
        {
            _sprite.Position = tile.GetOnScreenPosition();
            _sprite.Origin = new Vector2f(32, 32);
            OriginTile = tile;

            IsBuildMenuShown = true;
        }

        public static TowerType ClickedInsideBuildMenu(Vector2f mousePos)
        {
            mousePos *= GameProperties.TileSizeInPixel;

            var bounds = _sprite.Sprite.GetGlobalBounds();
            if (bounds.Contains(mousePos.X, mousePos.Y))
            {
                // The player clicked inside the build menu
                var selectedPosition = (int)Math.Round((mousePos.X - _sprite.Origin.X - bounds.Left) / GameProperties.TileSizeInPixel);

                switch (selectedPosition)
                {
                    case 0:
                        return TowerType.Melee;
                    //case 1:
                    //   return TowerType.CloseRange;
                    //case 2:
                    //   return TowerType.LongRange;

                    default:
                        Console.WriteLine("Something bad happened...");
                        return TowerType.None;
                }
            }

            return TowerType.None;
        }

        public static void HideBuildMenu()
        {
            IsBuildMenuShown = false;
            OriginTile = null;
        }

        public static void Draw(RenderWindow rw)
        {
            if (IsBuildMenuShown)
            {
                _sprite.Draw(rw);
            }
        }
    }
}
