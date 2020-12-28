using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField]
    private WeaponHandler[] weapons;

    private int currentWeaponIndex;


    // Start is called before the first frame update
    void Start()
    {
        currentWeaponIndex = 0;
        weapons[currentWeaponIndex].gameObject.SetActive(true);
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) {
            activeSelectedWeapon(0);
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2)) {
            activeSelectedWeapon(1);
        }

        if(Input.GetKeyDown(KeyCode.Alpha3)) {
            activeSelectedWeapon(2);
        }

        if(Input.GetKeyDown(KeyCode.Alpha4)) {
            activeSelectedWeapon(3);
        }

        if(Input.GetKeyDown(KeyCode.Alpha5)) {
            activeSelectedWeapon(4);
        }

        if(Input.GetKeyDown(KeyCode.Alpha6)) {
            activeSelectedWeapon(5);
        }
    }

    void activeSelectedWeapon(int weaponIndex) {
        //don't repeat the draw animation if the same key is clicked again
        if(currentWeaponIndex == weaponIndex) {
            return;
        }

        weapons[currentWeaponIndex].gameObject.SetActive(false); //turn off current/previous weapon
        weapons[weaponIndex].gameObject.SetActive(true); //turn on the new/selected weapon
        currentWeaponIndex = weaponIndex; //save the new weapon index
    }


    //get info to know what weapon it is (Shotgun = bullets, Axe = none, etc.)
    public WeaponHandler getSelectedWeapon() {
        return weapons[currentWeaponIndex];
    }
}
