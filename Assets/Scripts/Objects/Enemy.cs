using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Controllers;
using Photon.Pun;
using System;
using Photon.Pun.UtilityScripts;

namespace Objects
{
    public class Enemy : Character, IPunObservable
    {
        public Rigidbody Rigidbody;
        [Range(0, 100)]
        public float DamageToPlayer = 20f;
        [Range(0, 100)]
        public float DamageToBase = 10f;
        [HideInInspector]
        public Base Base;
        public PhotonView PhotonView;
        public int ScoreCost = 20;
        public Behaviour Behaviour;
        public float Speed = 10f;

        private Photon.Realtime.Player _killer;
        private float _healthPoints;
     


        private void OnEnable()
        {          
           _healthPoints = Health.Hitpoints;
            Movement.Speed = Speed;
            if (Base != null)
                Movement.MovePosition(Base.transform.position);
            Health.DieEvent += Die;
        }

        private void OnDestroy()
        {
            Health.DieEvent -= Die;
        }
        public void Damage(float value, Photon.Realtime.Player attacker)
        {
            Health.Damage(value);
            PhotonView.RPC("SetKillerRpc", RpcTarget.MasterClient, attacker);
        }
        [PunRPC]
        public void SetKillerRpc(Photon.Realtime.Player attacker)
        {
            _killer = attacker;
        }
        public void Die() {
            ScoreExtensions.AddScore(_killer, ScoreCost);
            PhotonView.RPC("DieRPC", RpcTarget.All);
        }
        [PunRPC]
        public void DieRPC()
        {            
            Destroy(gameObject);
        }
        
        private void Update()
        {
            if (!PhotonNetwork.IsMasterClient)
            {            
                Health.SetHp(_healthPoints);
            }
        }
        public void ChangeTarget(GameObject target)
        {

            if (target != null)
                Movement.MovePosition(target.transform.position);
            else if (Base != null)
            {
                Movement.MovePosition(Base.transform.position);
            }

        }
      

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Player>()!=null)
            {
                var health = collision.gameObject.GetComponent<PlayerController>();
                health?.Damage(DamageToPlayer);
                PhotonView.RPC("DieRPC", RpcTarget.All);
            }
            if (collision.gameObject.GetComponent<Base>() != null)
            {
                var health = collision.gameObject.GetComponent<Health>();
                health?.Damage(DamageToBase);
                PhotonView.RPC("DieRPC", RpcTarget.All);
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
           
                stream.SendNext(Health.Currenthealth);
            }
            else if (stream.IsReading)
            {
            
                _healthPoints = (float)stream.ReceiveNext();
          
            }
        }
    }
}
