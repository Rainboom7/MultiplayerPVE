using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class Spawner : MonoBehaviour,IPunObservable
    {
        [Range(0.5f, 10)]
        public float SpawnTime;
        public string EnemyPrefabPath;
        public Controllers.GameController GameController;

        private Coroutine _spawnRoutine;

        private void OnEnable()
        {
            if (PhotonNetwork.IsMasterClient)
                _spawnRoutine = StartCoroutine(SpawnRoutine);
        }
    
        private void OnDisable()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            
        }

        private IEnumerator SpawnRoutine
        {
            get
            {
                yield return new WaitForSeconds(SpawnTime);
                while(true)
                {
                    var obj = PhotonNetwork.InstantiateSceneObject(EnemyPrefabPath, transform.position, transform.rotation);
                    if (obj != null)
                    {
                        Enemy enemy = obj.GetComponent<Enemy>();
                        enemy.Base = GameController.Base;
                    }
                    SpawnTime *= 0.95f;
                    yield return new WaitForSeconds(SpawnTime);      
                }


            }


        }

    }
}
