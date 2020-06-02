using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGAC.SnakesLadders.Board_Auxiliary
{
    [System.Serializable]
    [CreateAssetMenu(fileName ="New Board", menuName = "Snakes N Ladders/Board/Board Setting")]
    public class BoardSettings : ScriptableObject
    {
        public uint TotalTiles
        { 
            get 
            {
                return (Columns * Rows) + (Rows - 1);
            }
        }

        [Range(5, 10)]
        public uint Columns = 6;
        [Range(3, 8)]
        public uint Rows = 5;
        public Vector2 TilesSeparation = Vector2.one;
    }
}