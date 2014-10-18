using SFML.Window;

namespace JamTemplate
{
    public class Tile
    {
        public TileType Type { get; set; }
        public Vector2f Position { get; set; }
    }

    public enum TileType
    {
        FLOOR, PATH, WALL
    }
}
