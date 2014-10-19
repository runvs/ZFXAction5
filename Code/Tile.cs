using System;
using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public class Tile : IGameObject
    {
        private TileType _type;
        public Vector2i Position { get; set; }

        public SmartSprite Sprite { get; private set; }


        public TileType Type
        {
            get { return _type; }
            set
            {
                _type = value;
                LoadImage();
            }
        }

        private void LoadImage()
        {
            if (_type == TileType.Floor)
            {
                Sprite = new SmartSprite("../GFX/floor.png");
            }
            else if (_type == TileType.Buildzone || _type == TileType.Tower)
            {
                Sprite = new SmartSprite("../GFX/buildzone.png");
            }
            else if (_type == TileType.Wall)
            {
                Sprite = new SmartSprite("../GFX/wall.png");
            }
            else
            {
                throw new Exception("Tile Type not known");
            }
        }

        public bool IsDead() { return false; }

        public void GetInput() { }

        public void Update(TimeObject timeObject) { }

        public Vector2f GetOnScreenPosition()
        {
            return GameProperties.TileSizeInPixel * new Vector2f(Position.X, Position.Y) - Camera.CameraPosition;
        }

        public void Draw(RenderWindow rw)
        {
            Sprite.Position = GetOnScreenPosition();
            Sprite.Draw(rw);
        }
    }

    public enum TileType
    {
        Floor, Wall, Buildzone, Tower
    }
}
