using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Objects
{

    public class Player : Character
    {
        public Weapon Weapon;
        public float AbilityCooldown;
        public string AbilityPrefabPath;
        public bool Fire(Vector3 target)
        {
            if (Weapon == null)
                return false;
            transform.rotation = Quaternion.LookRotation(target);
            return Weapon.Fire();
        }
    }
}
