using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public abstract class Bullet : MonoBehaviour, IPunObservable
    {
        public float Damage;
        public PhotonView PhotonView;
        public Rigidbody Rigidbody;

        protected float _timer = 5f;
        private Vector3 _correctVelocity = Vector3.zero;
        public abstract void SetTarget(Vector3 target);
        private void Update()
        {
            if (!PhotonView.IsMine)
            {
                Rigidbody.velocity = Vector3.Lerp(Rigidbody.velocity, _correctVelocity, Time.deltaTime);
            }

            _timer -= Time.deltaTime;
            if (_timer <= 0 && PhotonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject);
            }
        }
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(Rigidbody.velocity);
            }
            else
            {
                _correctVelocity = (Vector3)stream.ReceiveNext();

            }
        }
    }
}
