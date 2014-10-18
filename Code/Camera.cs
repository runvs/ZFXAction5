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

            Vector2f newCamPos = CameraPosition + CameraVelocity * deltaT.ElapsedRealTime;
            if (newCamPos.X <= MinPosition.X)
            {
                newCamPos.X = MinPosition.X;
            }
            if (newCamPos.Y <= MinPosition.Y)
            {
                newCamPos.Y = MinPosition.Y;
            }

            if (newCamPos.X >= MaxPosition.X)
            {
                newCamPos.X = MaxPosition.X;
            }

            if (newCamPos.Y >= MaxPosition.Y)
            {
                newCamPos.Y = MaxPosition.Y;
            }

            CameraPosition = newCamPos;
        }
    }
}
