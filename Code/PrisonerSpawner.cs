
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
        private World _world;

        private List<eDirection> _path;



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
            spawnTimer -= timeObject.ElapsedGameTime;
            if (spawnTimer <= 0)
            {
                spawnTimer = spawnTimerMax;
                Spawn();
            }
        }

        private void Spawn()
        {
            Prisoner prisoner = new Prisoner();
            prisoner.PositionInTiles = SpawnPositionInTiles;
            prisoner.SetPath(_path);

            _world.Spawn(prisoner);
        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            // nothing to do here
        }
    }
}
