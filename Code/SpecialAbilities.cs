using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public static class SpecialAbilities
    {
        private static World _world;

        private static float _freezePrizonersLoader;
        private static RectangleShape _freezeShape;

        private static float _damagePrisonersLoader;
        private static RectangleShape _damageShape;

        private static float _xxxPrisonersLoader;
        private static RectangleShape _xxxShape;

        private static SmartSprite _sprite = new SmartSprite("../GFX/SpecialAbilities.png");



        public static void SetWorld(World world)
        {
            _world = world;

            _freezePrizonersLoader = 0;
            _damagePrisonersLoader = 0;
            _xxxPrisonersLoader = 0;

            _sprite.Origin = new Vector2f(96, 32);
            _sprite.Position = new Vector2f(800, 600);



            _freezeShape = new RectangleShape(new Vector2f(16, 64));
            _freezeShape.FillColor = Color.Red;
            _freezeShape.Origin = new Vector2f(0, 64);
            _freezeShape.Position = new Vector2f(656, 600);


            _damageShape = new RectangleShape(new Vector2f(16, 64));
            _damageShape.FillColor = Color.Red;
            _damageShape.Origin = new Vector2f(0, 64);
            _damageShape.Position = new Vector2f(720, 600);

            _xxxShape = new RectangleShape(new Vector2f(16, 64));
            _xxxShape.FillColor = Color.Red;
            _xxxShape.Origin = new Vector2f(0, 64);
            _xxxShape.Position = new Vector2f(784, 600);


        }
        public static void Update(TimeObject timeObject)
        {
            if (_world != null)
            {
                float careerPoints = _world.CareerPoints;

                IncreaseAbilities(careerPoints, timeObject);
                //Console.WriteLine(_freezePrizonersLoader);

                _freezeShape.Scale = new Vector2f(1, _freezePrizonersLoader / GameProperties.SpecialAbilitiesMaxValue);
                _damageShape.Scale = new Vector2f(1, _damagePrisonersLoader / GameProperties.SpecialAbilitiesMaxValue);
                _xxxShape.Scale = new Vector2f(1, _xxxPrisonersLoader / GameProperties.SpecialAbilitiesMaxValue);

            }
        }

        public static void FreezePrisoners()
        {
            if (_freezePrizonersLoader >= GameProperties.SpecialAbilitiesMaxValue)
            {
                _freezePrizonersLoader = 0.0f;
                _world.FreezePrisoners();
            }
        }

        public static void DamagePrisoners()
        {
            if (_damagePrisonersLoader >= GameProperties.SpecialAbilitiesMaxValue)
            {
                _damagePrisonersLoader = 0.0f;
                _world.DamageAllPrisoners();
            }
        }

        internal static void MoneyAbility()
        {
            if (_xxxPrisonersLoader >= GameProperties.SpecialAbilitiesMaxValue)
            {
                _xxxPrisonersLoader = 0;
                _world.MoneyAbility();
            }
        }


        private static void IncreaseAbilities(float careerPoints, TimeObject timeObject)
        {
            float increase = careerPoints * timeObject.ElapsedGameTime;
            if (increase > 0.07f)
            {
                increase = 0.07f;
            }
            if (increase <= 0)
            {
                increase = 0;
            }
            //Console.WriteLine(increase);
            _freezePrizonersLoader += increase * GameProperties.SpecialAbilitiesCareerPointsScalingFreeze;
            if (_freezePrizonersLoader >= GameProperties.SpecialAbilitiesMaxValue)
            {
                _freezePrizonersLoader = GameProperties.SpecialAbilitiesMaxValue;
            }

            _damagePrisonersLoader += increase * GameProperties.SpecialAbilitiesCareerPointsScalingDamage;
            if (_damagePrisonersLoader >= GameProperties.SpecialAbilitiesMaxValue)
            {
                _damagePrisonersLoader = GameProperties.SpecialAbilitiesMaxValue;
            }

            _xxxPrisonersLoader += increase * GameProperties.SpecialAbilitiesCareerPointsScalingXXX;
            if (_xxxPrisonersLoader >= GameProperties.SpecialAbilitiesMaxValue)
            {
                _xxxPrisonersLoader = GameProperties.SpecialAbilitiesMaxValue;
            }
        }





        public static void Draw(RenderWindow rw)
        {
            _sprite.Draw(rw);

            DrawBars(rw);
        }

        private static void DrawBars(RenderWindow rw)
        {
            rw.Draw(_freezeShape);
            rw.Draw(_damageShape);
            rw.Draw(_xxxShape);
        }


    }
}
