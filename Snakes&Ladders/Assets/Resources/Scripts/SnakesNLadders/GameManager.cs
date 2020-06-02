using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGAC.SnakesLadders.Menus;

namespace AGAC.SnakesLadders
{
    [RequireComponent(typeof(TurnManager))]
    public class GameManager : MonoBehaviour
    {
        private void Start()
        {
            turnManager = GetComponent<TurnManager>();
            turnManager.SetVariables(Board, playersAdmin);
            Reset();
            ShowMenu(GameMenus.MAIN_MENU);
        }
        private void Update()
        {
            switch (phase) {
                case GamePhase.IN_GAME:
                    if (turnManager.PlayerReachedLastTile())
                        GameOver();
                    return;
                default:return; 
            }
        }

        #region VARIABLES
        private GamePhase phase = GamePhase.MAIN_MENU;

        [SerializeField]
        private Dice dice;
        [SerializeField]
        private Board Board;
        [SerializeField]
        private PlayersAdmin playersAdmin;
        private TurnManager turnManager;
        #endregion

        #region PUBLIC METHODS
        public void StartGame() 
        {
            Reset();
            playersAdmin.CreatePlayers(2, Board.StartingPoint);
            Board.CreateBoard();
            phase = GamePhase.IN_GAME;
            ShowMenu(GameMenus.IN_GAME);
            ResetInGameUI();
        }
        public void Reset() 
        {
            playersAdmin.Reset();
            Board.Reset();
            turnManager.Reset();
        }
        public void Quit()
        {
            General.GeneralMethods.CloseApp();
        }
        public void RollDice() 
        {
            if (turnManager.TurnEnded)
                StartCoroutine(RollAndMove());
        }
        #endregion

        #region PRIVATE METHODS
        private IEnumerator RollAndMove() 
        {
            dice.Roll();
            while (dice.GetValue() <= 0)
                yield return new WaitForFixedUpdate();
            turnManager.MovePlayer(dice.GetValue());
            yield return null;
        }
        private void GameOver() 
        {
            phase = GamePhase.AFTER_MATCH;
            ShowMenu(GameMenus.AFTER_MATCH);
            UpdateMenuElement(
                GameMenus.AFTER_MATCH,
                (uint)AfterMatchElements.WINNER_DISPLAY,
                "Player ",
                turnManager.Turn,
                playersAdmin[turnManager.Turn-1].Color);
        }
        private void ShowMenu(GameMenus menu) 
        {
            MenusController.Instance.Show(menu);
        }
        private void ResetInGameUI() 
        {
            GameMenus menu = GameMenus.IN_GAME;
            uint element = (uint)InGameMenuElements.ROUND_COUNTER;
            UpdateMenuElement(menu, element, "Round ", 1, Color.white);
            element = (uint)InGameMenuElements.TURN_COUNTER;
            UpdateMenuElement(menu, element, "Turn ", 1, playersAdmin[0].Color);
        }
        private void UpdateMenuElement(GameMenus menu, uint element, string baseLabel, int value, Color labelColor) 
        {
            Menu _menu = MenusController.Instance[(uint)menu];
            string label = baseLabel + value.ToString();
            _menu.UpdateElement(element, label, labelColor);
        }
        #endregion
    }
}