using System;
using JamUtilities;
using SFML.Audio;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public class Tower : IGameObject
    {
        public Vector2i Position { get; private set; }
        public TowerType Type { get; private set; }
        public float Level { get; private set; }
        public int BaseCost { get; private set; }

        private SmartSprite _sprite;

        private float _attackTimerRemaining;
        private float _attackTimerMax;
        private World _world;

        private float _towerRange;

        private SoundBuffer _MeleeHitSoundBuffer;
        private Sound _MeleeHitSound;

        private SoundBuffer _FreezeHitSoundBuffer;
        private Sound _FreezeHitSound;

        private SoundBuffer _ShootHitSoundBuffer;
        private Sound _ShootHitSound;


        public Tower(TowerType type, Vector2i tilePosition, World world)
        {
            _world = world;
            Type = type;
            Position = tilePosition;
            Level = 1;

            _MeleeHitSoundBuffer = new SoundBuffer("../SFX/hit2.wav");
            _MeleeHitSound = new Sound(_MeleeHitSoundBuffer);

            _FreezeHitSoundBuffer = new SoundBuffer("../SFX/slow.wav");
            _FreezeHitSound = new Sound(_FreezeHitSoundBuffer);

            _ShootHitSoundBuffer = new SoundBuffer("../SFX/shoot.wav");
            _ShootHitSound = new Sound(_FreezeHitSoundBuffer);

            switch (type)
            {
                case TowerType.Melee:
                    _sprite = new SmartSprite("../GFX/towerMelee.png");
                    _attackTimerRemaining = _attackTimerMax = GameProperties.TowerMeleeAttackTime;
                    _towerRange = GameProperties.TowerMeleeRange;
                    BaseCost = GameProperties.TowerMeleeBaseCost;
                    break;

                case TowerType.Normal:
                    _sprite = new SmartSprite("../GFX/towerNormal.png");
                    _attackTimerRemaining = _attackTimerMax = GameProperties.TowerNormalAttackTime;
                    _towerRange = GameProperties.TowerNormalRange;
                    BaseCost = GameProperties.TowerNormalBaseCost;
                    break;

                case TowerType.Splash:
                    _sprite = new SmartSprite("../GFX/towerSplash.png");
                    _attackTimerRemaining = _attackTimerMax = GameProperties.TowerSpashAttackTime;
                    _towerRange = GameProperties.TowerSpashRange;
                    BaseCost = GameProperties.TowerSplashBaseCost;
                    break;

                case TowerType.Freeze:
                    _sprite = new SmartSprite("../GFX/towerFreeze.png");
                    _attackTimerRemaining = _attackTimerMax = GameProperties.TowerFreezeAttackTime;
                    _towerRange = GameProperties.TowerFreezeRange;
                    BaseCost = GameProperties.TowerFreezeBaseCost;
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
                target.TakeDamage(GameProperties.TowerMeleeAttackDamage * (float)(Math.Sqrt(Level)));
                _MeleeHitSound.Volume = 1.5f;
                _MeleeHitSound.Play();
            }
            else if (Type == TowerType.Normal || Type == TowerType.Splash || Type == TowerType.Freeze)
            {
                if (Type == TowerType.Freeze)
                {
                    _FreezeHitSound.Play();
                }
                else
                {
                    _ShootHitSound.Play();
                }
                _world.SpawnProjectile(new Projectile(this, target));
            }
            else
            {
                throw new Exception("Tower Type NONE or not known!!!");
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

        public bool Intersects(FloatRect bounds)
        {
            return _sprite.Sprite.GetGlobalBounds().Intersects(bounds);
        }

        public void Upgrade()
        {
            if (Level < 3)
            {
                Level++;
            }
        }

        public int CalculateUpgradeCosts()
        {
            return (int)Math.Round((Level + 1) * BaseCost);
        }
    }

    public enum TowerType
    {
        Melee, Normal, Splash, Freeze, None
    }
}
