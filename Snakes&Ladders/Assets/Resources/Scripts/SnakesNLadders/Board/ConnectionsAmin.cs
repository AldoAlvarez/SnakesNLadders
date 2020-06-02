using AGAC.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGAC.SnakesLadders.Board_Auxiliary
{
    [System.Serializable]
    public class ConnectionsAdmin
    {

        public ConnectionsSetting[] Settings;

        #region PUBLIC METHODS
        public void CreateConnections(List<Tile> board)
        {
            foreach (ConnectionsSetting setting in Settings) 
                for (int connection = 0; connection < setting.MaxConnections; ++connection) 
                    if (!CreateConnection(board, setting)) 
                        return;
        }
        #endregion

        #region PRIVATE METHODS
        private bool CreateConnection(List<Tile> board, ConnectionsSetting setting) 
        {
            Vector2Int tiles = GetTilesToConnect(board, setting);
            if (tiles.x == -1 || tiles.y == -1)
                return false;
            else
                ConnectTiles(
                    board[tiles.x],
                    board[tiles.y], 
                    tiles.y,
                    setting);
            return true;
        }

        private void ConnectTiles(Tile entry, Tile exit, int exitIndex, ConnectionsSetting setting) 
        {
            entry.ConnectTo(exit, exitIndex);
            Color random = GetRandomColor();
            entry.SetTexture(setting.AccessTextures[0], random);
            exit.SetTexture(setting.AccessTextures[1], random);
        }

        #region auxiliary
        private Vector2Int GetTilesToConnect(List<Tile>board, ConnectionsSetting setting) 
        {
            Vector2Int tiles = new Vector2Int(-1, -1);
            tiles.x=
                GetRandomTileInBoard(
                    board,
                    setting.CreationTiles);

            tiles.y =
                GetRandomTileInBoard(
                    board,
                    setting.tilesToMove,
                    tiles.x);
            return tiles;
        }
        private int GetRandomTileInBoard(List<Tile> tiles, RangedInt range, int startingTile = 0) 
        {
            for (int attempts = 0; attempts < 15; ++attempts)
            {
                int tile = startingTile + range.Value;
                if (tile < 0) continue;
                else if (tile >= tiles.Count) continue;
                else if (!tiles[tile].isConnected)
                    return tile;
            }
            return GetFreeTileInBoard(tiles, range, startingTile);
        }
        private int GetFreeTileInBoard(List<Tile> tiles, RangedInt range, int startingTile)
        {
            for (int tile = 0; tile < tiles.Count; ++tile)
            {
                if (range.isInRange(tile - startingTile))
                {
                    if (!tiles[tile].isConnected)
                        return tile;
                }
            }
            return -1;
        }
        private Color GetRandomColor() 
        {
            Color color = Color.white;
            for (int i = 0; i < 3; ++i)
                color[i] = Random.Range(0f, 1f);
            return color;
        }
        #endregion
        #endregion
    }
}