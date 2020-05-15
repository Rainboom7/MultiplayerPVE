using System;
using System.Collections.Generic;
using Controllers;
using Objects;
using UnityEngine;

namespace Views
{
	public class GameView : MonoBehaviour, IGameView
	{
		public GameController Controller;
  
		public FollowCamera FollowCamera;
        public Spawner[] GameObjects;
        private void OnEnable()
        {
            Controller.FollowCamera = FollowCamera;
            Controller.OnOpen(this);
        }
      

        private void OnDisable()
        {
            Controller.OnClose(this);
        }

		public void StartGame( )
		{
            foreach (var o in GameObjects)
            {         
                o.gameObject.SetActive(true);
            }

		}

		public void StopGame()
		{
            Controller.Base = null;    
			foreach(var o in GameObjects) 
				o.gameObject.SetActive(false);
		}

    }
}
