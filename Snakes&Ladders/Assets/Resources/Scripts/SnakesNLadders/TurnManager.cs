using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGAC.SnakesLadders.Players;
using AGAC.SnakesLadders.Menus;

namespace AGAC.SnakesLadders
{
    public class TurnManager : MonoBehaviour
    {
        #region VARIABLES
        private Board board;
        private PlayersAdmin playersAdmin;
        private int currentPlayerIndex = 0;
        private Player currentPlayer  {
            get { 
                return playersAdmin[currentPlayerIndex]; 
            }
        }
        public int Round { get; private set; } = 1;
        public int Turn { get { return currentPlayerIndex + 1; } }
        public bool TurnEnded { get; private set; } = true;
        #endregion

        #region PUBLIC METHODS
        public void SetVariables(Board board, PlayersAdmin playersAdmin)
        {
            this.board = board;
            this.playersAdmin = playersAdmin;
        }
        public void MovePlayer(int tiles) 
        {
            if (!TurnEnded) return;
            TurnEnded = false;
            StartCoroutine(MoveCurrentPlayer(tiles));
        }
        public bool PlayerReachedLastTile() 
        {
            return currentPlayer.CurrentTile >= (board.Count-1);
        }
        public void Reset()
        {
            currentPlayerIndex = 0;
            Round = 1;
            TurnEnded = true;
            StopAllCoroutines();
        }
        #endregion

        #region PRIVATE METHODS
        #region player movement
        private IEnumerator MoveCurrentPlayer(int tiles)
        {
            int startTile = currentPlayer.CurrentTile;
            int endTile = startTile + tiles;
            if (endTile >= board.Count)
                endTile = board.Count - 1;

            StartCoroutine(MovePlayerThroughtTiles(currentPlayer, startTile, endTile));
            currentPlayer.CurrentTile += tiles;
            float timeToMove = TimeForPlayerToMoveATile(currentPlayer, startTile + 1);
            float totalTurnTime = timeToMove * (endTile - startTile);
            yield return new WaitForSeconds(totalTurnTime);

            PlayerAfterTurnActions(currentPlayer);
        }
        private IEnumerator MovePlayerThroughtTiles(Player player, int start, int end)
        {
            for (int tile = start; tile <= end; ++tile)
            {
                StartCoroutine(MovePlayerToTile(player, board[tile].transform));
                float timeToWait = TimeForPlayerToMoveATile(player, tile);
                yield return new WaitForSeconds(timeToWait);
            }
            yield return null;
        }
        #endregion

        #region after turn actions
        private void PlayerAfterTurnActions(Player player)
        {
            if (PlayerReachedLastTile()) return;
            ActivateTileConnectionForPlayer(player);
            ChangePlayer();
        }
        private void ActivateTileConnectionForPlayer(Player player)
        {
            int tile = player.CurrentTile;
            if (!board[tile].isConnected) return;
            if (board[tile].ConnectedTile == null) return;

            StartCoroutine(MovePlayerToTile(player, board[tile].ConnectedTile.transform));
            player.CurrentTile = board[tile].ConnectedTileIndex;
        }
        private void ChangePlayer()
        {
            currentPlayerIndex++;
            if (currentPlayerIndex >= playersAdmin.Count) { 
                currentPlayerIndex = 0;
                ++Round;
            }
            UpdateUI();
            TurnEnded = true;
        }
        #endregion

        #region auxiliary
        private float TimeForPlayerToMoveATile(Player player, int nextTile)
        {
            float distance = GetDistance(player.transform, board[nextTile].transform);
            float timeToMove = distance / playersAdmin.MovementSpeed;
            return timeToMove;
        }
        private float GetDistance(Transform pointA, Transform pointB)
        {
            return (pointA.position - pointB.position).magnitude;
        }
        private IEnumerator MovePlayerToTile(Player player, Transform tile)
        {
            float distanceToTile = GetDistance(player.transform, tile);
            while (distanceToTile > 0.1f)
            {
                float speed = playersAdmin.MovementSpeed * Time.deltaTime;
                player.MoveTo(tile.transform, speed);
                yield return new WaitForEndOfFrame();
                distanceToTile = GetDistance(player.transform, tile);
            }
        }
        #endregion

        #region ui
        private void UpdateUI()
        {
            UpdateInGameMenuElement(InGameMenuElements.ROUND_COUNTER, "Round ", Round, Color.white);
            UpdateInGameMenuElement(InGameMenuElements.TURN_COUNTER, "Turn ", Turn, currentPlayer.Color);
        }

        private void UpdateInGameMenuElement(InGameMenuElements element, string baseLabel, int value, Color labelColor)
        {
            Menu in_game = MenusController.Instance[(uint)GameMenus.IN_GAME];
            string label = baseLabel + value.ToString();
            in_game.UpdateElement((uint)element, label, labelColor);
        }
        #endregion
        #endregion
    }
}