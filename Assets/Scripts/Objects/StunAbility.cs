using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class StunAbility : SpawnedAbility
    {
        private List<Enemy> _enemiesInRange = new List<Enemy>();
        public Animator Animator;
        public override void Use()
        {
            Animator.Play("ActivateAnimation");
            foreach (Enemy enemy in _enemiesInRange)
            {
                enemy.Stun();
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
