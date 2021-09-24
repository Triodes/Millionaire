using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System;

namespace Millionaire
{

    public class InputHelper
    {
        KeyboardState keyStateNew, keyStateOld;

        public InputHelper()
        {
        }

        public void Update()
        {
            // update all devices
            keyStateOld = keyStateNew;
            keyStateNew = Keyboard.GetState();
        }

        public bool IsKeyPressed(Keys key) // returns if key is pressed
        {
            return keyStateNew.IsKeyDown(key) && keyStateOld.IsKeyUp(key);
        }

        public bool IsKeyDown(Keys key) // returns if key is down
        {
            return keyStateNew.IsKeyDown(key);
        }

    }
}