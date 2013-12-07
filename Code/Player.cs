using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JamTemplate
{
    class Player
    {

        public Player(World world, int number)
        {
            myWorld = world;
            playerNumber = number;

            try
            {
                LoadGraphics();
            }
            catch (SFML.LoadingFailedException e)
            {
                System.Console.Out.WriteLine("Error loading player Graphics.");
                System.Console.Out.WriteLine(e.ToString());
            }
        }

        private void SetPlayerNumberDependendProperties()
        {
            PlayerName = "Player" + playerNumber.ToString();
        }



        public string PlayerName { get; private set; }

        public void GetInput()
        {
            ResetActionMap();
            if (movementTimer <= 0.0f)
            {
                MapInputToActions();
            }
        }

        public void Update(float deltaT)
        {

        }

        public void Draw(SFML.Graphics.RenderWindow rw)
        {
            rw.Draw(this.playerSprite);
        }

        private void ResetActionMap()
        {

        }

       

        private void MapInputToActions()
        {
            //if (SFML.Window.Keyboard.IsKeyPressed(MoveLeftKey()))
            //{
            //    MoveLeftAction();
            //}
            //else if (SFML.Window.Keyboard.IsKeyPressed(MoveRightKey()))
            //{
            //    MoveRightAction();
            //}
            //else if (SFML.Window.Keyboard.IsKeyPressed(MoveDownKey()))
            //{
            //    MoveDownAction();
            //}
            //else if (SFML.Window.Keyboard.IsKeyPressed(MoveUpKey()))
            //{
            //    MoveUpAction();
            //}

            //if (SFML.Window.Keyboard.IsKeyPressed(BombKey()))
            //{
            //    PlaceBombAction();
            //}
        }


        private SFML.Graphics.Texture playerTexture;
        private SFML.Graphics.Sprite playerSprite;

        private void LoadGraphics()
        {
            //SFML.Graphics.Image playerImage = new SFML.Graphics.Image("../gfx/player.png");
        }

        private float movementTimer = 0.0f; // time til two successive Movement commands
        
        private World myWorld;
        public int playerNumber;
    }
}
