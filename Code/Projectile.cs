﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JamUtilities;
using JamUtilities.Particles;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public class Projectile : IGameObject
    {
        public Prisoner _target;

        public Vector2f Position { get; private set; }
        private Vector2f _velocity;

        private SmartSprite _sprite;

        public Tower _tower;



        public Projectile(Tower tower, Prisoner target)
        {
            _target = target;
            _tower = tower;


            Position = new Vector2f(_tower.Position.X, _tower.Position.Y) * GameProperties.TileSizeInPixel;
            if (tower.Type == TowerType.Normal)
            {
                _sprite = new SmartSprite("../GFX/projectileNormal.png");
                Damage = GameProperties.TowerNormalAttackDamage * (float)(Math.Sqrt(tower.Level));   // TODO multiply Tower's Level
            }
            else if (tower.Type == TowerType.Freeze)
            {
                _sprite = new SmartSprite("../GFX/projectileFreeze.png");
                Damage = GameProperties.TowerFreezeAttackDamage * (float)(Math.Sqrt(tower.Level)); // TODO multiply Tower's Level
            }
            else if (tower.Type == TowerType.Splash)
            {
                _sprite = new SmartSprite("../GFX/projectileSplash.png");
                Damage = GameProperties.TowerSplashAttackDamage * (float)(Math.Sqrt(tower.Level)); // TODO multiply Tower's Level
            }
            else
            {
                throw new Exception("Cannot create projectile for this tower type");
            }
        }

        public Vector2f GetOnScreenPosition()
        {
            Vector2f pos = new Vector2f(Position.X, Position.Y) - Camera.CameraPosition;
            return pos;
        }

        private float GetDistanceToTarget()
        {
            Vector2f distance = Position - GameProperties.TileSizeInPixel * new Vector2f(_target.PositionInTiles.X, _target.PositionInTiles.Y);
            float dist = distance.X * distance.X + distance.Y * distance.Y;

            return (float)Math.Sqrt(dist);
        }

        public bool IsDead()
        {
            //if( (GetDistanceToTarget() <= GameProperties.ProjectileDistanceToTargetMin))
            //{
            //    return true;
            //}
            FloatRect r1 = _target._sprite.Sprite.GetGlobalBounds();
            FloatRect r2 = _sprite.Sprite.GetGlobalBounds();
            if (r1.Intersects(r2))
            {
                //Console.WriteLine("intersect");
                return true;
            }
            return false;
        }

        public void GetInput()
        {
            // nothing to do
        }

        public void Update(TimeObject timeObject)
        {
            // normalize direction
            Vector2f distance = GameProperties.TileSizeInPixel *
                                new Vector2f(_target.PositionInTiles.X, _target.PositionInTiles.Y) + _target.GetSubTileOffset() - Position;
            float dist = (float)Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
            Vector2f direction = distance / dist;

            if (_tower.Type == TowerType.Freeze)
            {
                ParticleManager.SpawnSmokeCloud(Position, 20, 5, Color.White, 0.5f);
            }

            if (_tower.Type == TowerType.Normal || _tower.Type == TowerType.Splash || _tower.Type == TowerType.Freeze)
            {
                _sprite.Rotation = _sprite.Rotation + 125.5f * timeObject.ElapsedGameTime;
            }

            _velocity = direction * GameProperties.ProjectileSpeed;

            Position = Position + _velocity;
        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            _sprite.Position = GetOnScreenPosition();

            if (_tower.Type != TowerType.Freeze)
                _sprite.Draw(rw);
        }

        public float Damage { get; private set; }
    }
}
