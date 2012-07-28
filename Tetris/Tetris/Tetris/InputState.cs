#region File Description
//-----------------------------------------------------------------------------
// InputState.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace Tetris
{
    /// <summary>
    /// Helper for reading input from mouse, keyboard and gamepad. This class tracks both the current and previous state of both input devices, and implements query
    /// properties for high level input actions such as "move up through the menu" or "pause the game".
    /// </summary>
    public class InputState
    {
        #region Fields
        public const int MaxInputs = 4;

        public MouseState CurrentMouseState;
        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly GamePadState[] CurrentGamePadStates;

        public MouseState LastMouseState;
        public readonly KeyboardState[] LastKeyboardStates;
        public readonly GamePadState[] LastGamePadStates;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructs a new input state.
        /// </summary>
        public InputState()
        {
            CurrentMouseState = new MouseState();
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];

            LastMouseState = new MouseState();
            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];
        }
        #endregion

        #region Methods
        /// <summary>
        /// Reads the latest state of the mouse, keyboard and gamepad.
        /// </summary>
        public void Update()
        {
            LastMouseState = CurrentMouseState;
            CurrentMouseState = Mouse.GetState();

            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);
            }
        }
        /// <summary>
        /// Helper for checking if a key was newly pressed during this update,
        /// by any player.
        /// </summary>
        public bool IsNewKeyPress(Keys key)
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                if (IsNewKeyPress(key, (PlayerIndex)i))
                    return true;
            }

            return false;
        }
        /// <summary>
        /// Helper for checking if a key was newly pressed during this update,
        /// by the specified player.
        /// </summary>
        public bool IsNewKeyPress(Keys key, PlayerIndex playerIndex)
        {
            return (CurrentKeyboardStates[(int)playerIndex].IsKeyDown(key) &&
                    LastKeyboardStates[(int)playerIndex].IsKeyUp(key));
        }
        /// <summary>
        /// Helper for checking if a button was newly pressed during this update, by any player.
        /// </summary>
        public bool IsNewButtonPress(Buttons button)
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                if (IsNewButtonPress(button, (PlayerIndex)i))
                    return true;
            }

            return false;
        }
        /// <summary>
        /// Helper for checking if a button was newly pressed during this update, by the specified player.
        /// </summary>
        public bool IsNewButtonPress(Buttons button, PlayerIndex playerIndex)
        {
            return (CurrentGamePadStates[(int)playerIndex].IsButtonDown(button) && LastGamePadStates[(int)playerIndex].IsButtonUp(button));
        }
        ///<summary> 
        /// Checks for a left mouse button click input from the user and returns true if a left click was performed. 
        /// </summary> 
        /// <returns>Whether the left mouse button was clicked.</returns> 
        public bool IsNewLeftMouseClick()
        {
            return ((CurrentMouseState.LeftButton == ButtonState.Released) && (LastMouseState.LeftButton == ButtonState.Pressed));
        }
        /// <summary> 
        /// Checks for a right mouse button click input form the user and returns true if a right mouse click was performed. 
        /// </summary> 
        /// <returns>Whether a right mouse button was clicked.</returns> 
        public bool IsNewRightMouseClick()
        {
            return ((CurrentMouseState.RightButton == ButtonState.Released) && (LastMouseState.RightButton == ButtonState.Pressed));
        }
        /// <summary> 
        /// Checks for a middle mouse button click input form the user and returns true if a middle mouse click was performed. 
        /// </summary> 
        /// <returns>Whether a middle mouse button was clicked</returns> 
        public bool IsNewMiddleMouseClick()
        {
            return ((CurrentMouseState.MiddleButton == ButtonState.Released) && (LastMouseState.MiddleButton == ButtonState.Pressed));
        }
        ///<summary> 
        /// Checks for a left mouse button press input from the user and returns true if such a left press is performed. 
        /// </summary> 
        /// <returns>Whether the left mouse button was pressed down.</returns> 
        public bool IsNewLeftMousePress()
        {
            return ((CurrentMouseState.LeftButton == ButtonState.Pressed) && (LastMouseState.LeftButton == ButtonState.Pressed));
        }
        /// <summary> 
        /// Checks for a right mouse button press input form the user and returns true if such a right mouse press is performed. 
        /// </summary> 
        /// <returns>Whether a right mouse button was pressed down.</returns> 
        public bool IsNewRightMousePress()
        {
            return ((CurrentMouseState.RightButton == ButtonState.Pressed) && (LastMouseState.RightButton == ButtonState.Pressed));
        }
        /// <summary> 
        /// Checks for a middle mouse button press input form the user and returns true if such a middle mouse press is performed. 
        /// </summary> 
        /// <returns>Whether a middle mouse button was pressed down.</returns> 
        public bool IsNewMiddleMousePress()
        {
            return ((CurrentMouseState.MiddleButton == ButtonState.Pressed) && (LastMouseState.MiddleButton == ButtonState.Pressed));
        }
        ///<summary> 
        /// Checks for a left mouse button release input from the user and returns true if such a left release is performed. 
        /// </summary> 
        /// <returns>Whether the left mouse button was released down.</returns> 
        public bool IsNewLeftMouseReleased()
        {
            return ((CurrentMouseState.LeftButton == ButtonState.Released) && (LastMouseState.LeftButton == ButtonState.Pressed));
        }
        /// <summary> 
        /// Checks for a right mouse button release input form the user and returns true if such a right mouse release is performed. 
        /// </summary> 
        /// <returns>Whether a right mouse button was released down.</returns> 
        public bool IsNewRightMouseReleased()
        {
            return ((CurrentMouseState.RightButton == ButtonState.Released) && (LastMouseState.RightButton == ButtonState.Pressed));
        }
        /// <summary> 
        /// Checks for a middle mouse button release input form the user and returns true if such a middle mouse release is performed. 
        /// </summary> 
        /// <returns>Whether a middle mouse button was released down.</returns> 
        public bool IsNewMiddleMouseReleased()
        {
            return ((CurrentMouseState.MiddleButton == ButtonState.Released) && (LastMouseState.MiddleButton == ButtonState.Pressed));
        }
        ///<summary> 
        ///Checks if the mouse has been scrolled up.
        ///</summary> 
        ///<returns>Whether the the mouse wheel scrolled up.</returns> 
        public bool IsNewMouseScrollUp()
        {
            return CurrentMouseState.ScrollWheelValue > LastMouseState.ScrollWheelValue;
        }
        ///<summary> 
        ///Checks if the mouse has been scrolled down.
        ///</summary> 
        ///<returns>Whether the mouse wheel scrolled down.</returns> 
        public bool IsNewMouseScrollDown()
        {
            return CurrentMouseState.ScrollWheelValue < LastMouseState.ScrollWheelValue;
        }
        /// <summary>
        /// Helper for checking if any key was pressed during this update, by any player.
        /// </summary>
        public bool IsAnyKeyPress()
        {
            for (int i = 0; i < MaxInputs; i++) { if (CurrentKeyboardStates[i].GetPressedKeys().Length != 0) { return true; } }
            return false;
        }
        /// <summary>
        /// Helper for checking if a key was pressed during this update, by any player.
        /// </summary>
        public bool IsKeyDown(Keys key)
        {
            for (int i = 0; i < MaxInputs; i++) { if (CurrentKeyboardStates[i].IsKeyDown(key)) { return true; } }
            return false;
        }
        /// <summary>
        /// Checks for a "menu select" input action from the specified player.
        /// </summary>
        public bool IsMenuSelect(PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Space, playerIndex) || IsNewKeyPress(Keys.Enter, playerIndex) || IsNewButtonPress(Buttons.A, playerIndex) ||
                   IsNewButtonPress(Buttons.Start, playerIndex);
        }
        /// <summary>
        /// Checks for a "menu cancel" input action from the specified player.
        /// </summary>
        public bool IsMenuCancel(PlayerIndex playerIndex)
        {
            return IsNewKeyPress(Keys.Escape, playerIndex) || IsNewButtonPress(Buttons.B, playerIndex) || IsNewButtonPress(Buttons.Back, playerIndex);
        }
        #endregion

        #region Properties


        /// <summary>
        /// Checks for a "menu up" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuUp
        {
            get
            {
                return IsNewKeyPress(Keys.Up) ||
                       IsNewButtonPress(Buttons.DPadUp) ||
                       IsNewButtonPress(Buttons.LeftThumbstickUp);
            }
        }


        /// <summary>
        /// Checks for a "menu down" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuDown
        {
            get
            {
                return IsNewKeyPress(Keys.Down) ||
                       IsNewButtonPress(Buttons.DPadDown) ||
                       IsNewButtonPress(Buttons.LeftThumbstickDown);
            }
        }

        /// <summary>
        /// Checks for a "menu left" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuLeft
        {
            get
            {
                return IsNewKeyPress(Keys.Left) ||
                       IsNewButtonPress(Buttons.DPadLeft) ||
                       IsNewButtonPress(Buttons.LeftThumbstickLeft);
            }
        }

        /// <summary>
        /// Checks for a "menu right" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuRight
        {
            get
            {
                return IsNewKeyPress(Keys.Right) ||
                       IsNewButtonPress(Buttons.DPadRight) ||
                       IsNewButtonPress(Buttons.LeftThumbstickRight);
            }
        }


        /// <summary>
        /// Checks for a "menu select" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuSelect
        {
            get
            {
                return IsNewKeyPress(Keys.Space) ||
                       IsNewKeyPress(Keys.Enter) ||
                       IsNewButtonPress(Buttons.A) ||
                       IsNewButtonPress(Buttons.Start);
            }
        }


        /// <summary>
        /// Checks for a "menu cancel" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MenuCancel
        {
            get
            {
                return IsNewKeyPress(Keys.Escape) ||
                       IsNewButtonPress(Buttons.B) ||
                       IsNewButtonPress(Buttons.Back);
            }
        }


        /// <summary>
        /// Checks for a "pause the game" input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool PauseGame
        {
            get
            {
                return IsNewKeyPress(Keys.Escape) ||
                       IsNewButtonPress(Buttons.Back) ||
                       IsNewButtonPress(Buttons.Start);
            }
        }

        /// <summary>
        /// Checks for a general main select input action, from any player,
        /// on either keyboard or gamepad.
        /// </summary>
        public bool MainSelect
        {
            get
            {
                return IsNewKeyPress(Keys.Z) ||
                       IsNewButtonPress(Buttons.A);
            }
        }
        #endregion
    }
}