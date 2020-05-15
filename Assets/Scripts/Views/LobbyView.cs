using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;
namespace Views
{
    public class LobbyView : BaseView
    {
        public Text[] Players;
        public Text[] PlayersHeroes;
        private int _currentPlayer;
        [SerializeField]
        public Button PlayButton;
        public event Action LeaveLobbyEvent;
        public event Action BeginGameAction;
        public void UpdateLobby(Player[] players)
        {
            if (!PhotonNetwork.IsMasterClient)
                PlayButton.gameObject.SetActive(false);
            else {
                PlayButton.gameObject.SetActive(true);

            }
            _currentPlayer = 0;
            foreach (Player player in players)
            {
                Players[_currentPlayer].text = player.NickName;
                PlayersHeroes[_currentPlayer].text = (string)player.CustomProperties["Hero"];
                _currentPlayer++;
            }
            while (_currentPlayer <= 3)
            {
                Players[_currentPlayer].text = "empty";
                PlayersHeroes[_currentPlayer].text = "none";
                _currentPlayer++;
            }

        }
        public void OnLeaveClick() {
            LeaveLobbyEvent?.Invoke();
            gameObject.SetActive(false);

        }
        public void OnStartClick()
        {
            BeginGameAction?.Invoke();
        }

    }
}
