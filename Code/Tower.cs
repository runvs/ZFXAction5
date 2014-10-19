using System;
using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public class Tower : IGameObject
    {
        public Vector2i Position { get; private set; }
        public TowerType Type { get; private set; }

        private SmartSprite _sprite;

        public Tower(TowerType type, Vector2i tilePosition)
        {
            Type = type;
            Position = tilePosition;

            switch (type)
            {
                case TowerType.Melee:
                    _sprite = new SmartSprite("../GFX/towerMelee.png");
                    break;

                case TowerType.Normal:
                    _sprite = new SmartSprite("../GFX/towerNormal.png");
                    break;

                case TowerType.Splash:
                    _sprite = new SmartSprite("../GFX/towerSplash.png");
                    break;

                case TowerType.Freeze:
                    _sprite = new SmartSprite("../GFX/towerFreeze.png");
                    break;
            }

            _sprite.Position = new Vector2f(Position.X * GameProperties.TileSizeInPixel, Position.Y * GameProperties.TileSizeInPixel);
        }

        public bool IsDead()
        {
            throw new NotImplementedException();
        }

        public void GetInput()
        {
            throw new NotImplementedException();
        }

        public void Update(TimeObject timeObject)
        {
        }

        public void Draw(RenderWindow rw)
        {
            _sprite.Draw(rw);
        }
    }

    public enum TowerType
    {
        Melee, Normal, Splash, Freeze, None
    }
}
