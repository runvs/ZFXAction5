using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public class Tile : IGameObject
    {
        public TileType Type { get; set; }
        public Vector2f Position { get; set; }

        public bool IsDead() { return false; }

        public void GetInput() { }

        public void Update(TimeObject timeObject) { }

        public void Draw(RenderWindow rw)
        {
            throw new System.NotImplementedException();
        }
    }

    public enum TileType
    {
        FLOOR, WALL, BUILDZONE
    }
}
