using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponAim {
    NONE,
    SELF_AIM,
    AIM
}

public enum WeaponAmmoType {
    NONE,
    BULLET,
    ARROW,
    SPEAR
}

public enum WeaponShootingType {
    SINGLE,
    MULTIPLE
}

public class WeaponHandler : MonoBehaviour
{
    private Animator animator;

    public WeaponAim weaponAim;
    public WeaponShootingType shootType;
    public WeaponAmmoType ammoType;
    public GameObject attackPoint;

    [SerializeField]
    private GameObject muzzleFlash;

    [SerializeField]
    private AudioSource soundShooting, soundReloading;

    void Awake() {
        animator = GetComponent<Animator>();
    }

    public void shootAnimation() {
        animator.SetTrigger("Attack");
    }

    public void aim(bool canAim) {
        animator.SetBool("Aim", canAim);
    }

    void turnOnMuzzleFlash() {
        muzzleFlash.SetActive(true);
    }

    void turnOffMuzzleFlash() {
        muzzleFlash.SetActive(false);
    }

    void turnOnAttackPoint() {
        attackPoint.SetActive(true);
    }

    void turnOffAttackPoint() {
        if(attackPoint.activeInHierarchy) {
            attackPoint.SetActive(false);
        }
    }

    void playShootingSound() {
        soundShooting.Play();
    }

    void playReloadingSound() {
        soundReloading.Play();
    }
}
