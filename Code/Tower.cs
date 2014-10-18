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

                case TowerType.CloseRange:
                    _sprite = new SmartSprite("../GFX/towerCloseRange.png");
                    break;

                case TowerType.LongRange:
                    _sprite = new SmartSprite("../GFX/towerLongRange.png");
                    break;
            }
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
            throw new NotImplementedException();
        }

        public void Draw(RenderWindow rw)
        {
            throw new NotImplementedException();
        }
    }

    public enum TowerType
    {
        Melee, CloseRange, LongRange
    }
}
