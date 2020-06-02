using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGAC.SnakesLadders
{
    public enum GamePhase
    { 
        MAIN_MENU, 
        PLAYER_SELECTION, 
        IN_GAME, 
        AFTER_MATCH,
        _counter
    }
    namespace Menus
    {
        public enum GameMenus
        {
            NONE = -1,
            MAIN_MENU,
            PLAYER_SELECTION,
            IN_GAME,
            AFTER_MATCH,
            _counter
        }
        public enum MainMenuElements { _counter }
        public enum PlayerSelectionElements { _counter }
        public enum InGameMenuElements
        {
            TURN_COUNTER,
            ROUND_COUNTER,
            _counter
        }
        public enum AfterMatchElements
        {
            WINNER_DISPLAY,
            _counter
        }
    }
}
