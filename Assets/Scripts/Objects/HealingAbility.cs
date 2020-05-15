using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{
    public class HealingAbility : SpawnedAbility
    {
        public float HealAmount;
        public Animator Animator;
        private List<Player> _playersInRange = new List<Player>();

        public override void Use()
        {
            Animator.Play("ActivateAnimation");
            foreach (Player player in _playersInRange)
            {
                player.Health.Heal(HealAmount);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                _playersInRange.Add(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                _playersInRange.Remove(player);
            }
        }
        
    }
}
