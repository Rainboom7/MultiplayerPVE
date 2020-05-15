using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Objects
{
    public  class Weapon : MonoBehaviour
    {
        private enum WeaponState
        {
            Firing,
            Reloading,
            None
        }
        public float Damage;
        public Text AmmoText;
        public int Ammo { get; set; }
        public Bullet Bullet;
        public float ReloadTime;
        public float FireTime;
        public int MaxAmmo;

        private WeaponState _weaponState;
        private float _timer;
        private Vector3 _taget;

        private void OnEnable()
        {
            _weaponState = WeaponState.None;
            Ammo = MaxAmmo;
        }
        public void SetAmmo(int ammo) {
            Ammo = ammo;
            ChangeAmmoView();
        }

        private void Update()
        {
            if (_timer > 0f)
                _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                if (_weaponState == WeaponState.Firing)
                {
                    _timer = 0f;
                   _weaponState = WeaponState.None;
                }
                else if (_weaponState == WeaponState.Reloading)
                {
                    _weaponState = WeaponState.None;
                    _timer = 0f;
                    Ammo = MaxAmmo;
                    ChangeAmmoView();
                }
            }
            
        }

        public bool Fire()
        {
            if (_weaponState != WeaponState.None)
                return false;
            if (Ammo < 1)
            {
                Reload();
                return false;
            }
            _weaponState = WeaponState.Firing;
            _timer = FireTime;
            Ammo--;
            ChangeAmmoView();
            return true;
        }

        private void Reload()
        {
            _timer = ReloadTime;
            _weaponState = WeaponState.Reloading;
            ChangeAmmoView();
        }
        private void ChangeAmmoView() {

            if (AmmoText != null)
            {
                if (_weaponState == WeaponState.Reloading)
                    AmmoText.text = "Realoading";
                else
                    AmmoText.text = Ammo + " / " + MaxAmmo;

            }
                

        }

    }
}