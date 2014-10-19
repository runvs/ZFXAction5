﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using JamTemplate;
using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{


    public class Prisoner : IGameObject
    {
        public Vector2i PositionInTiles { get; set; }
        public Vector2i TargetPositionInTiles { get; set; }

        public float Health { get; private set; }
        public float HealthMax { get; private set; }

        private Vector2f AbsolutePositionInPixel { get; set; }

        private bool dead;

        public bool finished { get; private set; }

        private SmartSprite _sprite;

        private List<eDirection> _path;

        public float movementTimer { get; private set; }

        private eDirection currentMovementDirection;

        private SFML.Graphics.RectangleShape _healthShape;

        public float FreezeinTimeRemaining { get; private set; }

        public Prisoner()
        {
            _sprite = new SmartSprite("../GFX/prisoner.png");
            Health = HealthMax = GameProperties.PrisonerHealtDefault;
            dead = false;
            finished = false;
            movementTimer = GameProperties.PrisonerMovementTimer;

            _healthShape = new RectangleShape(new Vector2f(60, 10));

            _healthShape.FillColor = Color.Red;
            _healthShape.Origin = new Vector2f(-2, 10);

        }

        public void SetPath(List<eDirection> path)
        {
            _path = new List<eDirection>();
            foreach (var dir in path)
            {
                _path.Add(dir);
            }

            if (_path.Count > 0)
            {
                currentMovementDirection = _path[0];
            }
        }


        public void TakeDamage(float damagevalue)
        {
            Health -= damagevalue;
            CheckIsDead();

            _sprite.Flash(Color.Red, 0.5f);
            _sprite.Shake(0.5f, 0.05f, 2.0f);
        }

        private void CheckIsDead()
        {
            if (Health <= 0)
            {
                dead = true;
            }
        }

        public bool IsDead()
        {
            return dead;
        }

        public void GetInput()
        {
            //throw new NotImplementedException();
            // do not receive input
        }

        public void Update(TimeObject timeObject)
        {
            _healthShape.Scale = new Vector2f(Health / HealthMax, 1);
            _healthShape.Position = GetOnScreenPosition() + new Vector2f(0, -2);
            // movement
            DoMovement(timeObject);
            _sprite.Update(timeObject);
        }

        private void DoMovement(TimeObject timeObject)
        {
            if (FreezeinTimeRemaining >= 0)
            {
                FreezeinTimeRemaining -= timeObject.ElapsedGameTime;
            }
            else
            {
                //Console.WriteLine(_path.Count);
                movementTimer -= timeObject.ElapsedGameTime;
                if (movementTimer <= 0)
                {
                    movementTimer = GameProperties.PrisonerMovementTimer;
                    MoveForward();
                }
            }
        }

        private void MoveForward()
        {
            PositionInTiles += Direction.GetDirectionFromEnum(currentMovementDirection);
            if (_path.Count > 0)
            {
                _path.RemoveAt(0);

            }
            if (_path.Count > 0)
            {
                currentMovementDirection = _path[0];
            }
            else
            {
                finished = true;
                Console.WriteLine("Finish");
                dead = true;
            }
        }

        public Vector2f GetOnScreenPosition()
        {
            AbsolutePositionInPixel = GameProperties.TileSizeInPixel * new Vector2f(PositionInTiles.X, PositionInTiles.Y);
            return AbsolutePositionInPixel - Camera.CameraPosition;
        }

        public void Draw(RenderWindow rw)
        {

            _sprite.Position = GetOnScreenPosition();
            _sprite.Draw((rw));

        }

        public void DrawHealthShape(RenderWindow rw)
        {
            rw.Draw(_healthShape);
        }

        internal void Freeze()
        {
            FreezeinTimeRemaining = GameProperties.SpecialAbilitiesFreezeTime;
            _sprite.Flash(Color.Blue, GameProperties.SpecialAbilitiesFreezeTime);
        }
    }
}
