using System;
using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public class Tile : IGameObject
    {
        public TileType Type
        {
            get { return type; }
            set
            {
                type = value;
                LoadImage();
            }
        }

        private void LoadImage()
        {
            if (type == TileType.FLOOR)
            {
                _sprite = new SmartSprite("../GFX/floor.png");
            }
            else
            {
                throw new Exception("Tile Type not known");
            }
        }

        private TileType type;
        public Vector2i Position { get; set; }

        private SmartSprite _sprite;

        public bool IsDead() { return false; }

        public void GetInput() { }

        public void Update(TimeObject timeObject) { }

        public Vector2f GetOnScreenPosition()
        {
            return GameProperties.TileSizeInPixel * new Vector2f(Position.X, Position.Y) - Camera.CameraPosition;
        }

        public void Draw(RenderWindow rw)
        {
            _sprite.Position = GetOnScreenPosition();
            _sprite.Draw((rw));
        }
    }

    public enum TileType
    {
        FLOOR, WALL, BUILDZONE
    }
}
