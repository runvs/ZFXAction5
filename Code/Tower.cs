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

        private float _attackTimerRemaining;
        private float _attackTimerMax;
        private World _world;

        private float _towerRange;


        public Tower(TowerType type, Vector2i tilePosition, World world)
        {
            _world = world;
            Type = type;
            Position = tilePosition;

            switch (type)
            {
                case TowerType.Melee:
                    _sprite = new SmartSprite("../GFX/towerMelee.png");
                    _attackTimerRemaining = _attackTimerMax = GameProperties.TowerMeleeAttackTime;
                    _towerRange = GameProperties.TowerMeleeRange;
                    break;

                case TowerType.Normal:
                    _sprite = new SmartSprite("../GFX/towerNormal.png");
                    _attackTimerRemaining = _attackTimerMax = GameProperties.TowerNormalAttackTime;
                    _towerRange = GameProperties.TowerNormalRange;
                    break;

                case TowerType.Splash:
                    _sprite = new SmartSprite("../GFX/towerSplash.png");
                    _attackTimerRemaining = _attackTimerMax = GameProperties.TowerSpashAttackTime;
                    _towerRange = GameProperties.TowerSpashRange;
                    break;

                case TowerType.Freeze:
                    _sprite = new SmartSprite("../GFX/towerFreeze.png");
                    _attackTimerRemaining = _attackTimerMax = GameProperties.TowerFreezeAttackTime;
                    _towerRange = GameProperties.TowerFreezeRange;
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
            if (_attackTimerRemaining > 0)
            {
                _attackTimerRemaining -= timeObject.ElapsedGameTime;
            }
            else
            {
                Prisoner target = _world.GetPrisonerNextTo(this.Position);
                if (target != null)
                {
                    if (CheckRangeCondition(target))
                    {
                        Shoot(target);
                    }
                }
            }
        }

        private void Shoot(Prisoner target)
        {
            _attackTimerRemaining = _attackTimerMax;
            if (Type == TowerType.Melee)
            {
                target.TakeDamage(GameProperties.TowerMeleeAttackDamage);
            }
            else if (Type == TowerType.Normal)
            {

            }
            else if (Type == TowerType.Splash)
            {

            }
            else if (Type == TowerType.Freeze)
            {

            }
            else
            {
                throw new Exception("Tower Type NONE!!!");
            }
        }


        private bool CheckRangeCondition(Prisoner target)
        {
            Vector2i distance = this.Position - target.PositionInTiles;
            float dist = distance.X * distance.X + distance.Y * distance.Y;

            if (dist <= _towerRange * _towerRange)
            {
                return true;
            }
            return false;
        }


        public Vector2f GetOnScreenPosition()
        {
            return GameProperties.TileSizeInPixel * new Vector2f(Position.X, Position.Y) - Camera.CameraPosition;
        }

        public void Draw(RenderWindow rw)
        {
            _sprite.Position = GetOnScreenPosition();
            _sprite.Draw(rw);
        }
    }

    public enum TowerType
    {
        Melee, Normal, Splash, Freeze, None
    }
}
