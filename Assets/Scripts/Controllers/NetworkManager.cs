using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Core;
using Photon.Realtime;
using System;
using Objects;
using Views;
using Photon.Pun.UtilityScripts;

namespace Controllers
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public GameController Controller;
        [HideInInspector]
        public string PlayerPrefabPath;
        public GameObject BasePoint;
        public string BasePrefabPath;
        public GameObject LoadingText;
        public MenuView MainMenu;
        public HudView HudView;
        public LobbyView LobbyView;
        public EndGameView EndGameView;
        private Coroutine _scoreCoroutine;

        public void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
            LobbyView.BeginGameAction += OnGameStart;
            LobbyView.LeaveLobbyEvent += LeaveLobby;
            EndGameView.LeaveEvent += LeaveLobby;
            MainMenu.NetworkManager = this;
            Controller.HudView = HudView;
        }

        public bool IsPlayerSelected()
        {
            return (!PlayerPrefabPath.Equals(""));
        }
        public override void OnCreatedRoom()
        {

        }
        public void LeaveLobby() {
            PhotonNetwork.LeaveRoom();
            MainMenu.Open();
        }


        private void Spawn()
        {
            Debug.Log(PhotonNetwork.CurrentRoom);
            int shiftValue = UnityEngine.Random.Range(1, 3);
            Vector3 shift = new Vector3(shiftValue, 0, shiftValue);
            GameObject player = PhotonNetwork.Instantiate(PlayerPrefabPath, transform.position + shift, Quaternion.identity);
        }
        public override void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
        }
        public override void OnJoinedRoom()
        {
            LobbyView.Open();
            LobbyView.UpdateLobby(PhotonNetwork.PlayerList);
            Debug.Log("OnJoinedRoom");
            base.OnJoinedRoom();

        }

        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            LobbyView.UpdateLobby(PhotonNetwork.PlayerList);

        }
        public override void OnPlayerLeftRoom(Photon.Realtime.Player newPlayer)
        {
            LobbyView.UpdateLobby(PhotonNetwork.PlayerList);
        }
        public void OnGameStart()
        {
            photonView.RPC("OnGameStartRPC", RpcTarget.All);
        }
        [PunRPC]
        public void OnGameStartRPC()
        {
            LobbyView.Close();
            HudView.Open();
            Spawn();
            _scoreCoroutine = StartCoroutine(UpdateScore());
            if (PhotonNetwork.IsMasterClient)
            {
                Controller.Base = PhotonNetwork.Instantiate(BasePrefabPath, BasePoint.transform.position, Quaternion.identity).GetComponent<Base>();
                Controller.Base.Health.DieEvent += OnGameEnd;
                Controller.NewGame();
            }
        }
        public void OnGameEnd()
        {
            photonView.RPC("OnGameEndRPC", RpcTarget.All);
        }
        [PunRPC]
        public void OnGameEndRPC()
        {
            HudView.Close();
            EndGameView.EndGame(new List<Photon.Realtime.Player>(PhotonNetwork.PlayerList));
            ScoreExtensions.SetScore(PhotonNetwork.LocalPlayer, 0);
            if (PhotonNetwork.IsMasterClient)
            {
                Controller.Base.Health.DieEvent -= OnGameEnd;
                Controller.StopGame();
                PhotonNetwork.DestroyAll();  
            }
        }
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            Debug.Log("OnCreateRoomFailed");
            base.OnCreateRoomFailed(returnCode, message);
            MainMenu.Open();

        }
        public override void OnConnectedToMaster()
        {
            LoadingText.SetActive(false);
            Debug.Log("OnConnectedToMaster");
            MainMenu.gameObject.SetActive(true);
     
        }
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            Debug.Log("OnJoinRandomFailed :" + message);
            base.OnJoinRandomFailed(returnCode, message);
            MainMenu.Open();

        }
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            Debug.Log("OnLeftRoom");
        }
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);

        }
        public void CreateGame(string room)
        {
            if (PhotonNetwork.InRoom)
                PhotonNetwork.LeaveRoom();
            PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"] = PlayerPrefs.GetString("PlayerName");
            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("PlayerName");
            string heroName = PlayerPrefs.GetString("Hero");
            heroName = heroName.Substring(heroName.LastIndexOf('/')+1);
            PhotonNetwork.LocalPlayer.CustomProperties["Hero"] = heroName;
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 4;
            PhotonNetwork.CreateRoom(room,options);
        }
        public void ConnectToRandomRoom()
        {
            PhotonNetwork.LocalPlayer.CustomProperties["PlayerName"] = PlayerPrefs.GetString("PlayerName");
            PhotonNetwork.LocalPlayer.NickName = PlayerPrefs.GetString("PlayerName");
            string heroName = PlayerPrefs.GetString("Hero");
            heroName = heroName.Substring(heroName.LastIndexOf('/')+1);
            PhotonNetwork.LocalPlayer.CustomProperties["Hero"] = heroName;
            PhotonNetwork.JoinRandomRoom();
        }
        private IEnumerator UpdateScore()
        {
            while (true)
            {
                var list = new List<Photon.Realtime.Player>(PhotonNetwork.PlayerList);
                list.Sort((player1, player2) => ScoreExtensions.GetScore(player2).CompareTo(ScoreExtensions.GetScore(player1)));
                HudView?.UpdateBoard(list);
                yield return new WaitForSeconds(0.5f);
            }

        }
      
     

     
       



    }
}
