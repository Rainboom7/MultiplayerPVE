using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public abstract class SpawnedAbility : MonoBehaviour, IPunObservable
    {
        public abstract void Use();
        public float TimeBeforeAct;
        public float LifeTime;
        private float _lifeTimer;
        protected float _timer;
        public PhotonView PhotonView;
        private void OnEnable()
        {
            _timer = TimeBeforeAct;
            _lifeTimer = LifeTime;
        }
        private void Update()
        {
            if (PhotonView.IsMine)
            {
                if (_lifeTimer > 0)
                    _lifeTimer -= Time.deltaTime;
                else
                {
                    PhotonNetwork.Destroy(gameObject);
                }

                if (_timer > 0)
                    _timer -= Time.deltaTime;
                else
                {
                    _timer = TimeBeforeAct;
                    Use();
                }
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(_timer);
                stream.SendNext(_lifeTimer);
            }
            else
            {
                _timer = (float)stream.ReceiveNext();
                _lifeTimer = (float)stream.ReceiveNext();

            }
        }
    }
}
