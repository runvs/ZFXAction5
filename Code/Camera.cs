using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Window;

namespace JamTemplate
{
    public static class Camera
    {
        public static SFML.Window.Vector2f CameraPosition { get; set; }
        public static SFML.Window.Vector2f CameraVelocity { get; private set; }
        public static float CameraMaxVelocity = 200.0f;

        public static Vector2f MinPosition;
        public static Vector2f MaxPosition;

        public static Vector2f ShouldBePosition { get; set; }

        public static void DoCameraMovement(JamUtilities.TimeObject deltaT)
        {
            Vector2f playerPosInPixels = ShouldBePosition;
            float distanceXSquared = (float)(Math.Sign(CameraPosition.X - playerPosInPixels.X)) * (CameraPosition.X - playerPosInPixels.X) * (CameraPosition.X - playerPosInPixels.X);
            float distanceYSquared = (float)(Math.Sign(CameraPosition.Y - playerPosInPixels.Y)) * (CameraPosition.Y - playerPosInPixels.Y) * (CameraPosition.Y - playerPosInPixels.Y);

            Vector2f newCamVelocity = 0.125f * new Vector2f(-distanceXSquared, -distanceYSquared);
            float camSpeed = (float)Math.Sqrt(CameraVelocity.X * CameraVelocity.X + CameraVelocity.Y * CameraVelocity.Y);
            if (camSpeed <= 10)
            {
                newCamVelocity *= 0.1f;
            }
            if (camSpeed <= 1)
            {
                newCamVelocity *= 0.1f;
            }
            if (newCamVelocity.X >= CameraMaxVelocity)
            {
                newCamVelocity.X = CameraMaxVelocity;
            }
            else if (newCamVelocity.X <= -CameraMaxVelocity)
            {
                newCamVelocity.X = -CameraMaxVelocity;
            }
            if (newCamVelocity.Y >= CameraMaxVelocity)
            {
                newCamVelocity.Y = CameraMaxVelocity;
            }
            else if (newCamVelocity.Y <= -CameraMaxVelocity)
            {
                newCamVelocity.Y = -CameraMaxVelocity;
            }

            CameraVelocity = newCamVelocity;
            camSpeed = (float)Math.Sqrt(CameraVelocity.X * CameraVelocity.X + CameraVelocity.Y * CameraVelocity.Y);
            Console.WriteLine(camSpeed);

            Vector2f newCamPos = CameraPosition + CameraVelocity * deltaT.ElapsedRealTime;
            if (newCamPos.X <= MinPosition.X)
            {
                newCamPos.X = MinPosition.X;
                ShouldBePosition = new Vector2f(MinPosition.X, ShouldBePosition.Y);
                CameraVelocity = new Vector2f(0, CameraVelocity.Y);
            }
            if (newCamPos.Y <= MinPosition.Y)
            {
                newCamPos.Y = MinPosition.Y;
                ShouldBePosition = new Vector2f(ShouldBePosition.X, MinPosition.Y);
                CameraVelocity = new Vector2f(CameraVelocity.X, 0);
            }

            if (newCamPos.X >= MaxPosition.X)
            {
                newCamPos.X = MaxPosition.X;
                ShouldBePosition = new Vector2f(MaxPosition.X, ShouldBePosition.Y);
                CameraVelocity = new Vector2f(0, CameraVelocity.Y);
            }

            if (newCamPos.Y >= MaxPosition.Y)
            {
                ShouldBePosition = new Vector2f(ShouldBePosition.X, MaxPosition.Y);
                newCamPos.Y = MaxPosition.Y;

            }

            CameraPosition = newCamPos;
        }
    }
}
