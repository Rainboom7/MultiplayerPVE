using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Objects
{
    public class ShotgunBullet : Bullet
    {
        public List<Bullet> Bullets;
        public override void SetTarget(Vector3 target)
        {
            foreach (Bullet bullet in Bullets)
            {
                if (bullet != null)
                {
                    bullet.Damage = Damage;
                    bullet.SetTarget(target);
                }
            }
        }
    }
}
