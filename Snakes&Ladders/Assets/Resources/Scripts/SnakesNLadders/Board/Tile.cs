using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGAC.SnakesLadders.Board_Auxiliary
{
    [DisallowMultipleComponent]
    public class Tile : MonoBehaviour
    {
        public Tile ConnectedTile { get; private set; } = null;
        public int ConnectedTileIndex { get; private set; } = -1;

        public bool isConnected = false;
        [SerializeField]
        private MeshRenderer renderer;

        public void ConnectTo(Tile tile, int index)
        {
            ConnectedTile = tile;
            isConnected = true;
            tile.isConnected = true;
            ConnectedTileIndex = index;
        }
        public void SetTexture(Texture2D texture, Color color) 
        {
            if (renderer == null) return;
            if (renderer.materials.Length == 0) return;
            renderer.materials[0].SetTexture("_MainTex", texture);
            renderer.materials[0].SetColor("_Color", color);
        }
        public void Reset() 
        {
            if (!isConnected) return;
            isConnected = false; 
            ConnectedTile = null;
            ConnectedTileIndex = -1;
        }
    }
}