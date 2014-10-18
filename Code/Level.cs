using System.Collections.Generic;
using JamUtilities;
using SFML.Graphics;
using SFML.Window;

namespace JamTemplate
{
    public class Level : IGameObject
    {
        public Tile[,] LevelTiles { get; set; }
        public List<PrisonerSpawner> Spawner { get; set; }

        public Level()
        {
            LevelTiles = new Tile[GameProperties.LevelSize, GameProperties.LevelSize];
            Spawner = new List<PrisonerSpawner>();
        }

        public Tile GetTileAt(int x, int y)
        {
            return LevelTiles[x, y];
        }

        public Tile GetTileAt(Vector2i position)
        {
            return GetTileAt(position.X, position.Y);
        }

        public bool IsDead()
        {
            throw new System.NotImplementedException();
        }

        public void GetInput()
        {
            throw new System.NotImplementedException();
        }

        public void Update(TimeObject timeObject)
        {
            foreach (var spawner in Spawner)
            {
                spawner.Update(timeObject);
            }
        }

        public void Draw(RenderWindow rw)
        {
            foreach (var levelTile in LevelTiles)
            {


            }
        }
    }
}
