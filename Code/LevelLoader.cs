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
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);
            path.Add(eDirection.EAST);

            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);
            path.Add(eDirection.SOUTH);

            var spawner = new PrisonerSpawner(world, new Vector2i(3, 5), 5f);
            spawner.SetPath(path);
            level.Spawner.Add(spawner);



            var path2 = new List<eDirection>();

            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);
            path2.Add(eDirection.SOUTH);

            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);
            path2.Add(eDirection.EAST);



            var spawner2 = new PrisonerSpawner(world, new Vector2i(3, 5), 5f);
            spawner2.SetPath(path2);
            spawner2.SetOffset(2.5f);
            level.Spawner.Add(spawner2);


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
