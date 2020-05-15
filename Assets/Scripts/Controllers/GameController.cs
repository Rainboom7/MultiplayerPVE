using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using Core;
using Views;

namespace Controllers
{
    public interface IGameView
	{   
        void StartGame( );
		void StopGame();
	
	}

    [CreateAssetMenu(menuName = "Game Controller")]
    public class GameController : ScriptableObject, IGame
    {
        [HideInInspector]
        public FollowCamera FollowCamera;
        public HudView HudView;
        public Base Base { get; set; }

        private IGameView _view;

        public void NewGame()
        {
            _view?.StartGame();
        }
        public void StopGame()
        {
            if (Base != null)
                Destroy(Base.gameObject);
            _view?.StopGame();

        }
        public void UpdateCooldown(int seconds) {
            HudView?.ChangeCooldown(seconds);
        }
        
		public void OnOpen(IGameView view)
		{
            _view = view;
		}

		public void OnClose(IGameView view)
		{
             _view = null;
		}

    }
}
