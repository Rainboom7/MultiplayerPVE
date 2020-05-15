using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class EnemyAgressiveBehaviour : Behaviour
    {
        public Enemy Enemy;
        private GameObject _target;
        private float _timer = 0.25f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Character>() != null && other.gameObject.GetComponent<Enemy>() == null)
            {
                if (Enemy != null)
                {
                    _target = other.gameObject;
                    Enemy.ChangeTarget(other.gameObject);
                }

            }

        }
        private void Update()
        {
            if (_timer > 0)
                _timer -= Time.deltaTime;
            else
            {
                _timer = 0.25f;
                Enemy.ChangeTarget(_target);
            }
        }
    }
}

