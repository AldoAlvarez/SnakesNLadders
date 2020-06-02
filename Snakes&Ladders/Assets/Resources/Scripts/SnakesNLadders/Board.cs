using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGAC.SnakesLadders.Board_Auxiliary;

namespace AGAC.SnakesLadders
{
    [DisallowMultipleComponent]
    public class Board : MonoBehaviour
    {
        #region UNITY METHODS
        private void Start()
        {
            Initialize();
        }
        #endregion

        #region VARIABLES
        [SerializeField]
        private BoardSettings Settings;
        public Transform StartingPoint;
        [SerializeField]
        private GameObject tilePrefab;
        [SerializeField]
        private Texture2D tileTexture;

        [SerializeField]
        private ConnectionsAdmin connectionsAdmin = new ConnectionsAdmin();

        private Stack<Tile> unusedTiles;
        private List<Tile> tiles;

        public int Count { get { return tiles.Count; } }
        public Tile this[int tile]
        {
            get
            {
                Initialize();
                if (tile >= tiles.Count) return null;
                return tiles[tile];
            }
        }
        #endregion

        #region PUBLIC METHODS
        public void CreateBoard()
        {
            Initialize();
            for (int row= 0; row < Settings.Rows; ++row)
            {
                CreateRow(row);
                if (row + 1 < Settings.Rows)
                    AddRowSeparationTile(row);
            }
            connectionsAdmin.CreateConnections(tiles);
        }

        public void Reset() 
        {
            if (tiles == null) return;
            foreach (Tile tile in tiles) 
            {
                tile.gameObject.SetActive(false);
                tile.Reset();
                unusedTiles.Push(tile);
            }
            tiles.Clear();
        }
        #endregion

        #region PRIVATE METHODS
        private void SetTile(int column, int row)
        {
            Tile tile = GetATile();
            tile.gameObject.SetActive(true);
            tile.transform.position = GetTilePosition(column, row);
            tile.transform.parent = transform;
            tile.SetTexture(tileTexture, Color.white);
            tiles.Add(tile);
        }
        private void AddRowSeparationTile(int y)
        {
            int x = !isRowPair(y) ? 0 : (int)Settings.Columns - 1;
            y = (y * 2) + 1;
            SetTile(x, y);
        }
        private Tile GetATile()
        {
            if (unusedTiles.Count <= 0)
                CreateNewTile();
            return unusedTiles.Pop();
        }

        #region auxiliary methods
        private void CreateRow(int row) 
        {
            int delta = isRowPair(row) ? 0 : (int)Settings.Columns-1;
            for (int x = 0; x < Settings.Columns; ++x) 
            {
                int column = Mathf.Abs(x - delta);
                SetTile(column, 2 * row);
            }
        }
        private bool isRowPair(int row)
        {
            return (row % 2) == 0;
        }
        private void CreateNewTile()
        {
            GameObject newTile = Instantiate(tilePrefab);
            if (newTile.GetComponent<Tile>() == null)
                newTile.AddComponent<Tile>();
            unusedTiles.Push(newTile.GetComponent<Tile>());
        }
        private Vector3 GetTilePosition(int column, int row)
        {
            Vector3 pos = StartingPoint.position;
            pos.x += column * Settings.TilesSeparation.x;
            pos.z += row * Settings.TilesSeparation.y;
            return pos;
        }
        #endregion

        private void Initialize()
        {
            if (unusedTiles == null)
                unusedTiles = new Stack<Tile>();
            if (tiles == null)
                tiles = new List<Tile>();
        }
        #endregion
    }
}