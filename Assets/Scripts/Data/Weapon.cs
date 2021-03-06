﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BouncyBlasters/Weapon")]
public class Weapon : ScriptableObject
{
    //enum used to determine fire rate
    public enum FireType
    {
        SemiAutomatic,
        Automatic
    }

    //enum used to track which keys are used to fire.
    public enum FireKey
    {
        None, //Does not fire, used for debug purposes or disabling an ability
        PrimaryFire, //Fires using standard button, left click or the like
        SecondaryFire, //Fires using the secondary key, rigth click or the like
        GrenadeFire //Fires using the grenade key
    }

    //The name of the weapon, used in UI displays and the like
    public string name;
    //The amount of ammo in the gun
    public int ammoCount;
    //button used to swap between fire modes, optional
    public FireKey modeSwapKey;
    //List of fire modes
    public List<FireMode> fireModes;

    [System.Serializable]
    public class FireMode
    {
        //The key used to fire this specific mode
        public FireKey key;
        //Fire rate type of the weapon, determines what happens when clicking
        public FireType fireType;
        //The number of shots fired at once
        public int shotsFiredAtOnce;
        //The amount of ammo used each shot
        public int ammoUsedEachShot;
        //The damage of each bullet
        public int bulletDamage;
        //The speed the projectile is fired
        public int fireSpeed;
        //The cooldown before a shot can be fired again, typically used for automatic weapons
        public float fireRate;
        //Max bounces
        public int maxBounces;
        //Bullet prefab
        public GameObject bulletPrefab;
        //Audio clip
        public AudioClip firingSound;
    }
}
