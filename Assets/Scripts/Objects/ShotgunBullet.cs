using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class ShotgunBullet : Bullet
    {
        public List<GameObject> BulletPoints;
        public string BulletPrefab;
     
        public override void SetTarget(Vector3 target)
        {
            foreach (GameObject point in BulletPoints)
            {
                GameObject prefab = PhotonNetwork.Instantiate(BulletPrefab, point.transform.position, Quaternion.Euler(90, 0, 0));
                Bullet bullet = prefab.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.Damage = Damage;
                    bullet.SetTarget(target);
                }
            }
        }
    }
}
