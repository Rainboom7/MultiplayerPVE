using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class RifleBullet : Bullet, IPunObservable
    {
        public float Speed = 5f;

        private Vector3 _normalizeDirection= Vector3.zero;
      

        public override void SetTarget(Vector3 target)
        {
            if (target != null)
                _normalizeDirection = (new Vector3(target.x, transform.position.y, target.z) - transform.position).normalized;
            Rigidbody.velocity = _normalizeDirection * Speed;

        }
       
        
        private void OnTriggerEnter(Collider other)
        {
            var enemy = other.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
               enemy.Damage(Damage, PhotonNetwork.LocalPlayer);
                if(PhotonView.IsMine)
                    PhotonNetwork.Destroy(gameObject);

            }

        }

          
    }
}
