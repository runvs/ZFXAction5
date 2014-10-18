using System;

namespace JamTemplate
{
    public static class LevelLoader
    {
        public static Level GetLevel(int levelNumber)
        {
            switch (levelNumber)
            {
                case 1:
                    return GetLevel1();
                case 2:
                    return GetLevel2();
                case 3:
                    return GetLevel3();
                case 4:
                    return GetLevel4();
                case 5:
                    return GetLevel5();

                default:
                    throw new ArgumentException("Please don't do that again...");
            }
        }

        private static Level GetLevel1()
        {
            throw new NotImplementedException();
        }

        private static Level GetLevel2()
        {
            throw new NotImplementedException();
        }

        private static Level GetLevel3()
        {
            throw new NotImplementedException();
        }

        private static Level GetLevel4()
        {
            throw new NotImplementedException();
        }

        private static Level GetLevel5()
        {
            throw new NotImplementedException();
        }
    }
}
