using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public static class TowerBuilder
    {
        public static bool IsBuildMenuShown { get; private set; }
        private static SmartSprite _sprite = new SmartSprite("../GFX/buildMenu.png");

        public static void ShowBuildMenu(Tile tile)
        {
            _sprite.Position = tile.GetOnScreenPosition();
            _sprite.Origin = new Vector2f(32, 32);

            IsBuildMenuShown = true;
        }

        public static void HideBuildMenu()
        {
            IsBuildMenuShown = false;
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
