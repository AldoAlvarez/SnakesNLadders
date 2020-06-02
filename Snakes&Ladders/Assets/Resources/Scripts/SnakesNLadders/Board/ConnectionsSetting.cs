using AGAC.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AGAC.SnakesLadders.Board_Auxiliary
{
    [CreateAssetMenu(fileName = "New Connection Setting", menuName = "Snakes N Ladders/Board/Connection")]
    public class ConnectionsSetting : ScriptableObject
    {
        [SerializeField]
        private BoardSettings Board;

        [Tooltip("The tiles range where this type of connections can be created.")]
        public RangedInt CreationTiles = new RangedInt(0, 0);

        [Tooltip("The range of tiles between the entry and the exit points of the connection.")]
        public RangedInt tilesToMove = new RangedInt(-30, 30);

        [Tooltip("The maximum number of this type of connections that can be created on the board.")]
        [Range(1, 10)]
        public int MaxConnections = 5;

        public Texture2D[] AccessTextures = new Texture2D[2];
    }
}