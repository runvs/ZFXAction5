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

            var path = new List<eDirection>();
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.NORTH);
            path.Add(eDirection.EAST);

            var spawner = new PrisonerSpawner(world, new Vector2i(0, 0), 10f);
            spawner.SetPath(path);

            level.Spawner.Add(spawner);

            return level;
        }

        private static Level GetLevel2(World world)
        {
            throw new NotImplementedException();
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
