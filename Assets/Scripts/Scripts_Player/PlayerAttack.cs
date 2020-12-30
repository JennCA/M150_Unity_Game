using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;

    public float fireRate = 15f; //how many times are we allowed to shoot
    public float damage = 12f;
    private float timeToFireNext;

    private bool isAiming;

    void Awake() {
        weaponManager = GetComponent<WeaponManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootWeapon();
    }

    //shooting with LMB
    void shootWeapon() {
        //assault riffle
        if(weaponManager.getSelectedWeapon().shootType == WeaponShootingType.MULTIPLE) {
            //if we press'n'hold LMB and if time is greater than timetofirenext
            if(Input.GetMouseButton(0) && Time.time > timeToFireNext) {

                timeToFireNext = Time.time + 1f / fireRate;
                weaponManager.getSelectedWeapon().shootAnimation();

                /*shoot function*/
            }

        //other weapon than assault riffle
        } else {
            if(Input.GetMouseButtonDown(0)) {
                //the Axe
                if(weaponManager.getSelectedWeapon().tag == "Axe") {
                    weaponManager.getSelectedWeapon().shootAnimation();
                }

                //the Revolver and Shotgun
                if(weaponManager.getSelectedWeapon().ammoType == WeaponAmmoType.BULLET) {
                    weaponManager.getSelectedWeapon().shootAnimation();

                    /*shoot function*/

                //if we have spear or arrow
                } else {
                    if(isAiming) {
                        weaponManager.getSelectedWeapon().shootAnimation();

                        if(weaponManager.getSelectedWeapon().ammoType == WeaponAmmoType.SPEAR) {
                            /*throw spear function*/

                        } else if(weaponManager.getSelectedWeapon().ammoType == WeaponAmmoType.ARROW) {
                            /*shoot arrow function*/
                        }
                    }

                }
            }

        }
    }
}
