using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class TurretAbility : SpawnedAbility
    {
        private List<Enemy> _enemiesInRange = new List<Enemy>();
        public string BulletPrefabPath;
        public float Damage;

        public override void Use()
        {
            if (_enemiesInRange.Capacity == 0)
                return;
            int randomIndex = Random.Range(0, _enemiesInRange.Count);
            if (randomIndex < 0 || randomIndex > _enemiesInRange.Count)
                return;
            Enemy target = _enemiesInRange[randomIndex];
            if (_enemiesInRange[randomIndex] != null)
            {
                transform.rotation = Quaternion.LookRotation(target.transform.position);
                GameObject prefab = PhotonNetwork.Instantiate(BulletPrefabPath, transform.position, Quaternion.Euler(90, 0, 0));
                Bullet bullet = prefab.gameObject.GetComponent<Bullet>();
                if (bullet != null)
                {
                    bullet.Damage = Damage;
                    bullet.SetTarget(target.transform.position);
                }
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                _enemiesInRange.Add(enemy);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                _enemiesInRange.Remove(enemy);
            }
        }

    }
}
