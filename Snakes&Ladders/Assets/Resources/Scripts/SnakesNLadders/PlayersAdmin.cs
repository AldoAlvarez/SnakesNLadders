using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AGAC.SnakesLadders.Players;

namespace AGAC.SnakesLadders {
    public class PlayersAdmin : MonoBehaviour
    {
        public PlayersAdmin()
        {
            Initialize();
        }

        #region VARiABLES
        public Player this[int player]
        {
            get
            {
                if (player < 0 || player >= Players.Count)
                    return null;
                return Players[player];
            }
        }
        public int Count { get { return Players.Count; } }

        [SerializeField]
        [Range(1, 4)]
        private int MaximumPlayers = 2;
        [SerializeField]
        [Range(1, 10)]
        public float MovementSpeed = 1f;
        [SerializeField]
        private GameObject PlayerPrefab;
        [SerializeField]
        private Color[] PlayersColors;

        private List<Player> Players;
        private Stack<Player> unusedPlayers;
        #endregion

        #region PUBLIC METHODS
        public void CreatePlayers(int total, Transform point)
        {
            for (int player = 0; player < total; ++player)
                CreatePlayer(player, point);
        }
        public void SetPlayersOn(Transform point) 
        {
            foreach (Player player in Players)
                player.gameObject.transform.position = point.position;
        }

        public void Reset()
        {
            RecyclePlayers();
            Players.Clear();
        }
        #endregion

        #region PRIVATE METHODS
        #region player creation
        private void CreatePlayer(int index, Transform point)
        {
            Player player = unusedPlayers.Count > 0 ? 
                GetUnusedPlayer() : CreateNewPlayer();

            Vector3 pos = point.position;
            pos.z -= index;
            player.transform.position = pos;
            player.gameObject.SetActive(true);

            player.ChangeColor(PlayersColors[index]);
            player.CurrentTile = 0;
            Players.Add(player);
        }

        private Player GetUnusedPlayer() 
        {
            return unusedPlayers.Pop();
        }
        private Player CreateNewPlayer() 
        {
            GameObject newPlayer = Instantiate(PlayerPrefab);
            Player plyr = newPlayer.GetComponent<Player>();
            if (plyr == null)
                plyr = newPlayer.AddComponent<Player>();
            return plyr;
        }
        #endregion

        private void RecyclePlayers()
        {
            foreach (Player player in Players)
            {
                player.gameObject.SetActive(false);
                unusedPlayers.Push(player);
            }
        }

        private void Initialize()
        {
            unusedPlayers = new Stack<Player>();
            Players = new List<Player>();
            PlayersColors = new Color[MaximumPlayers];
            for (int color = 0; color < MaximumPlayers; ++color)
                PlayersColors[color] = Color.white;
        }
        #endregion
    }
}