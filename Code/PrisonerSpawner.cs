
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using JamUtilities;
using SFML.Window;

namespace JamTemplate
{
    public class PrisonerSpawner : IGameObject
    {
        public Vector2i SpawnPositionInTiles { get; set; }


        private float spawnTimer;
        private float spawnTimerMax;
        private float spawnTimerOffset;
        private World _world;

        private int prisonersSpawned = 0;

        private List<eDirection> _path;


        public float Power { get; set; }


        public PrisonerSpawner(World world, Vector2i tilePosition, float timer)
        {
            _world = world;
            SpawnPositionInTiles = tilePosition;
            spawnTimerMax = timer;

        }

        public void SetPath(List<eDirection> path)
        {
            _path = path;
        }

        public void SetOffset(float off)
        {
            spawnTimerOffset = off;
        }

        public bool IsDead()
        {
            // nothing to do here
            return false;
        }

        public void GetInput()
        {
            // nothing to do here
        }

        public void Update(TimeObject timeObject)
        {
            if (spawnTimerOffset < 0)
            {
                spawnTimer -= timeObject.ElapsedGameTime;
                if (spawnTimer <= 0)
                {
                    spawnTimer = spawnTimerMax;
                    Spawn();
                }
            }
            else
            {
                spawnTimerOffset -= timeObject.ElapsedGameTime;

            }
        }

        private void Spawn()
        {

            prisonersSpawned++;
            Prisoner prisoner = new Prisoner(Power);
            prisoner.PositionInTiles = SpawnPositionInTiles;
            prisoner.SetPath(_path);

            _world.Spawn(prisoner);

            Power = (float)(Math.Pow(prisonersSpawned, 1.25f)) / 100.0f + 1.0f;
        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            // nothing to do here
        }
    }
}
