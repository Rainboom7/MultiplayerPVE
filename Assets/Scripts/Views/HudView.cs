using Controllers;
using Objects;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using Core;

namespace Views
{
    public interface IHudView : IView
    {
          void UpdateBoard(List<Photon.Realtime.Player> newPlayerList);
    }
    public class HudView : BaseView, IHudView
	{
        public List<PlayerScoreView> PlayerInfos;
        public  void UpdateBoard(List<Photon.Realtime.Player> newPlayerList)
        {
            int i;
            for (i=0; i < PlayerInfos.Capacity && i < newPlayerList.Capacity; i++)
            {
                PlayerInfos[i].Name = newPlayerList[i].NickName;
                PlayerInfos[i].Score = ScoreExtensions.GetScore(newPlayerList[i]);
                PlayerInfos[i].UpdateView();
            }
            while (i <= 3)
            {
                PlayerInfos[i].Name = "";
                PlayerInfos[i].Score = -1;
                PlayerInfos[i].UpdateView();
                i++;
            }
        }
      
        
	}
}
