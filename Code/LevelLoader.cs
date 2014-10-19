using System;
using System.Collections.Generic;
using SFML.Window;

namespace JamTemplate
{
    public static class LevelLoader
    {
        public static Level GetLevel(int levelNumber, World world)
        {
            switch (levelNumber)
            {
                case 1:
                    return GetLevel1(world);
                case 2:
                    return GetLevel2(world);
                case 3:
                    return GetLevel3(world);
                case 4:
                    return GetLevel4(world);
                case 5:
                    return GetLevel5(world);

                default:
                    throw new ArgumentException("Please don't do that again...");
            }
        }

        private static Level GetLevel1(World world)
        {
            var level = new Level();
            // start room
            for (int i = 1; i != 4; ++i)
            {
                for (int j = 1; j != 4; ++j)
                {
                    level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
                }
            }

            for (int i = 0; i != 15; ++i)
            {
                int j = 0;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Wall };
            }

            for (int i = 4; i != 13; ++i)
            {
                int j = 2;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Floor };
            }

            for (int i = 2; i != 17; ++i)
            {
                int j = 13;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Floor };
            }

            for (int i = 13; i != 17; ++i)
            {
                int j = 17;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Floor };
            }

            for (int i = 14; i != 17; ++i)
            {
                for (int j = 14; j != 17; ++j)
                {
                    level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                }
            }

            // end room
            for (int i = 16; i != 19; ++i)
            {
                for (int j = 16; j != 19; ++j)
                {
                    level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
                }
            }



            for (int i = 4; i != 13; ++i)
            {
                int j = 1;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                j = 3;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                j = 12;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                j = 14;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
            }

            for (int i = 4; i != 13; ++i)
            {
                int j = 12;
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Wall };
                j = 14;
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Wall };
                j = 1;
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Wall };
                j = 3;
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Wall };

            }

            for (int i = 1; i != 4; ++i)
            {
                int j = 14;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Wall };

            }

            for (int i = 4; i != 13; ++i)
            {
                for (int j = 4; j != 13; ++j)
                {
                    level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                }
            }

            level.LevelTiles[13, 1] = new Tile { Position = new Vector2i(13, 1), Type = TileType.Buildzone };
            level.LevelTiles[1, 13] = new Tile { Position = new Vector2i(1, 13), Type = TileType.Buildzone };

            level.LevelTiles[12, 3] = new Tile { Position = new Vector2i(12, 3), Type = TileType.Buildzone };
            level.LevelTiles[3, 12] = new Tile { Position = new Vector2i(3, 12), Type = TileType.Buildzone };

            level.LevelTiles[12, 12] = new Tile { Position = new Vector2i(12, 12), Type = TileType.Buildzone };
            level.LevelTiles[14, 14] = new Tile { Position = new Vector2i(14, 14), Type = TileType.Buildzone };

            var path = new List<eDirection>
            {
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,  
            };
            var spawner = new PrisonerSpawner(world, new Vector2i(2, 2), 4f);
            spawner.SetPath(path);
            level.Spawner.Add(spawner);

            var path2 = new List<eDirection>
            {
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,

                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,  
            };
            var spawner2 = new PrisonerSpawner(world, new Vector2i(2, 2), 4f);
            spawner2.SetPath(path2);
            spawner2.SetOffset(2.5f);
            level.Spawner.Add(spawner2);


            return level;
        }

        private static Level GetLevel2(World world)
        {
            var level = new Level();

            // starting pos Enemies

            // walls
            for (int i = 5; i != 10; ++i)
            {
                int j = 1;
                level.LevelTiles[j, i] = new Tile { Position = new Vector2i(j, i), Type = TileType.Wall };
            }
            for (int i = 0; i != 6; ++i)
            {
                int j = 5;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                j = 9;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
            }

            level.LevelTiles[5, 4] = new Tile { Position = new Vector2i(5, 4), Type = TileType.Wall };
            level.LevelTiles[5, 5] = new Tile { Position = new Vector2i(5, 5), Type = TileType.Wall };
            level.LevelTiles[5, 6] = new Tile { Position = new Vector2i(5, 6), Type = TileType.Wall };
            level.LevelTiles[5, 8] = new Tile { Position = new Vector2i(5, 8), Type = TileType.Wall };
            level.LevelTiles[5, 9] = new Tile { Position = new Vector2i(5, 9), Type = TileType.Wall };
            level.LevelTiles[5, 10] = new Tile { Position = new Vector2i(5, 10), Type = TileType.Wall };

            // wall in the middle
            for (int i = 7; i != 15; ++i)
            {
                for (int j = 5; j != 10; ++j)
                {
                    level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                }
            }

            // start room
            for (int i = 2; i != 5; ++i)
            {
                for (int j = 6; j != 9; ++j)
                {
                    level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
                }
            }


            // escape out
            for (int i = 5; i != 6; ++i)
            {
                int j = 7;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
            }


            for (int j = 4; j != 11; ++j)
            {
                int i = 6;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
            }

            // upper and lower walks
            for (int i = 7; i != 15; ++i)
            {
                int j = 4;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
                j = 10;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
            }
            for (int i = 5; i != 17; ++i)
            {
                int j = 3;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                j = 11;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
            }



            level.LevelTiles[16, 4] = new Tile { Position = new Vector2i(16, 4), Type = TileType.Wall };
            level.LevelTiles[16, 5] = new Tile { Position = new Vector2i(16, 5), Type = TileType.Wall };
            level.LevelTiles[16, 6] = new Tile { Position = new Vector2i(16, 6), Type = TileType.Wall };
            level.LevelTiles[16, 8] = new Tile { Position = new Vector2i(16, 8), Type = TileType.Wall };
            level.LevelTiles[16, 9] = new Tile { Position = new Vector2i(16, 9), Type = TileType.Wall };
            level.LevelTiles[16, 10] = new Tile { Position = new Vector2i(16, 10), Type = TileType.Wall };

            for (int j = 4; j != 11; ++j)
            {
                int i = 15;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
            }

            for (int i = 16; i != 17; ++i)
            {
                int j = 7;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
            }
            // escape room
            for (int i = 17; i != 20; ++i)
            {
                for (int j = 6; j != 9; ++j)
                {
                    level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Floor };
                }
            }
            for (int i = 17; i != 20; ++i)
            {
                int j = 5;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
                j = 9;
                level.LevelTiles[i, j] = new Tile { Position = new Vector2i(i, j), Type = TileType.Wall };
            }

            level.LevelTiles[7, 7] = new Tile { Position = new Vector2i(7, 7), Type = TileType.Buildzone };
            level.LevelTiles[14, 7] = new Tile { Position = new Vector2i(14, 7), Type = TileType.Buildzone };

            level.LevelTiles[7, 5] = new Tile { Position = new Vector2i(7, 5), Type = TileType.Buildzone };
            level.LevelTiles[14, 5] = new Tile { Position = new Vector2i(14, 5), Type = TileType.Buildzone };

            level.LevelTiles[7, 9] = new Tile { Position = new Vector2i(7, 9), Type = TileType.Buildzone };
            level.LevelTiles[14, 9] = new Tile { Position = new Vector2i(14, 9), Type = TileType.Buildzone };

            var path = new List<eDirection>
            {
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.NORTH,
                eDirection.NORTH,
                eDirection.NORTH,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST
            };

            var spawner = new PrisonerSpawner(world, new Vector2i(3, 7), 5f);
            spawner.SetPath(path);
            level.Spawner.Add(spawner);


            var path2 = new List<eDirection>
            {
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.SOUTH,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.NORTH,
                eDirection.NORTH,
                eDirection.NORTH,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST,
                eDirection.EAST
            };


            var spawner2 = new PrisonerSpawner(world, new Vector2i(3, 7), 5f);
            spawner2.SetPath(path2);
            spawner2.SetOffset(2.5f);
            level.Spawner.Add(spawner2);


            return level;
        }

        private static Level GetLevel3(World world)
        {
            throw new NotImplementedException();
        }

        private static Level GetLevel4(World world)
        {
            throw new NotImplementedException();
        }

        private static Level GetLevel5(World world)
        {
            throw new NotImplementedException();
        }
    }
}
