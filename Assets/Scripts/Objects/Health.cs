using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public class Health : MonoBehaviour
    {
        public RectTransform Line;
        public event Action DieEvent;
        private Vector3 _healthSize;
        [Range(0, 200)]
        public float Hitpoints;
        public PhotonView PhotonView;
        public float Currenthealth { get; set; }
        private void OnEnable()
        {
            Currenthealth = Hitpoints;
            _healthSize = Line.localScale;
            ChangeView();

        }
        public void Damage(float damage)
        {
            if (Currenthealth-damage <= 0)
                DieEvent?.Invoke();
            else
                PhotonView?.RPC("DamageRPC", RpcTarget.All, damage);
        }
        public void Heal(float value)
        {
            PhotonView?.RPC("HealRPC", RpcTarget.All, value);
        }

        [PunRPC]
        public void HealRPC(float value) {
            Currenthealth += value;
            Currenthealth = Math.Min(Currenthealth, Hitpoints);
            ChangeView();
        }

        [PunRPC]
        public void DamageRPC(float damage)
        {
            Currenthealth -= damage;
            ChangeView();
        }
        public void SetHp(float points)
        {
            Currenthealth = points;
            if (Currenthealth <= 0)
                DieEvent?.Invoke();
            ChangeView();
        }
        private void ChangeView()
        {
            Line.localScale =_healthSize* (Currenthealth / Hitpoints);      
        }


    }

}
