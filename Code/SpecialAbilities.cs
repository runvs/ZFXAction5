using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JamUtilities;
using SFML.Graphics;

namespace JamTemplate
{
    public static class SpecialAbilities
    {
        private static World _world;

        private static float _freezePrizonersLoader;

        private static float _damagePrisonersLoader;


        public static void SetWorld(World world)
        {
            _world = world;
            _freezePrizonersLoader = 0;
            _damagePrisonersLoader = 0;
        }
        public static void Update(TimeObject timeObject)
        {
            if (_world != null)
            {
                float careerPoints = _world.CarreerPoints;

                IncreaseAbilities(careerPoints, timeObject);
                //Console.WriteLine(_freezePrizonersLoader);
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


        private static void IncreaseAbilities(float careerPoints, TimeObject timeObject)
        {
            _freezePrizonersLoader += careerPoints * GameProperties.SpecialAbilitiesCareerPointsScalingFreeze *
                                     timeObject.ElapsedGameTime;
            if (_freezePrizonersLoader >= GameProperties.SpecialAbilitiesMaxValue)
            {
                _freezePrizonersLoader = GameProperties.SpecialAbilitiesMaxValue;
            }

            _damagePrisonersLoader += careerPoints * GameProperties.SpecialAbilitiesCareerPointsScalingDamage *
                         timeObject.ElapsedGameTime;
            if (_damagePrisonersLoader >= GameProperties.SpecialAbilitiesMaxValue)
            {
                _damagePrisonersLoader = GameProperties.SpecialAbilitiesMaxValue;
            }
        }


        public static void Draw(RenderWindow rw)
        {

        }
    }
}
