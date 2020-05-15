﻿using System;
 using Controllers;
using Core;
using Objects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public interface IMenuView : IView
    {
        event Action PlayEvent;
    }
        public class MenuView : BaseView, IMenuView
	 {	
		public event Action PlayEvent;
        [HideInInspector]
        public NetworkManager NetworkManager;
        public TMP_InputField RoomName;
        public TMP_InputField PlayerName;

        private void OnEnable()
        {
            PlayerName.text = "Player#" + UnityEngine.Random.Range(0, 100);
        }
        public void ActionPlay()
        {
            PlayEvent?.Invoke();
        }
        public void Choose(string playerPrefabPath)
        {
            if (NetworkManager != null)
                NetworkManager.PlayerPrefabPath = playerPrefabPath;
            PlayerPrefs.SetString("Hero",playerPrefabPath) ;
        }
        public void CreateRoom()
        {
            if (NetworkManager != null)
            {
                if (NetworkManager.IsPlayerSelected())
                {
                    PlayerPrefs.SetString("PlayerName", PlayerName.text);
                    NetworkManager.CreateGame(RoomName.text);
                    gameObject.SetActive(false);
                }
            }
           
        }
        public void RandomConnect() {
            if (NetworkManager.IsPlayerSelected())
            {   
                PlayerPrefs.SetString("PlayerName", PlayerName.text);
                NetworkManager.ConnectToRandomRoom();
                gameObject.SetActive(false);
            }
          
        }
	 }
}