using SFML.Window;

namespace JamTemplate
{
    public class Level
    {
        Tile[,] _level;

        public Level()
        {
            _level = new Tile[GameProperties.LevelSize, GameProperties.LevelSize];
        }

        public Tile GetTileAt(int x, int y)
        {
            return _level[x, y];
        }

        public Tile GetTileAt(Vector2i position)
        {
            return GetTileAt(position.X, position.Y);
        }
    }
}
