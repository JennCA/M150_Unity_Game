using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private WeaponManager weaponManager;
    private Animator cameraZoomAnimator;
    private Camera mainCamera;
    private GameObject crosshair;

    [SerializeField]
    private GameObject prefabArrow, prefabSpear;

    [SerializeField]
    private Transform startPositionArrowAndSpear;

    public float fireRate = 15f; //how many times are we allowed to shoot
    public float damage = 12f;
    private float timeToFireNext;

    private bool isAiming;
    private bool zoomed;

    void Awake() {
        weaponManager = GetComponent<WeaponManager>();
        cameraZoomAnimator = transform.Find("View_Root").transform.Find("Person_Camera").GetComponent<Animator>();
        crosshair = GameObject.FindWithTag("Crosshair");
        mainCamera = Camera.main;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shootWeapon();
        zoom();
    }

    //shooting with LMB
    void shootWeapon() {
        //assault riffle
        if(weaponManager.getSelectedWeapon().shootType == WeaponShootingType.MULTIPLE) {
            //if we press'n'hold LMB and if time is greater than timetofirenext
            if(Input.GetMouseButton(0) && Time.time > timeToFireNext) {

                timeToFireNext = Time.time + 1f / fireRate;
                weaponManager.getSelectedWeapon().shootAnimation();

                shootBullets();
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

                    shootBullets();

                //if we have spear or arrow
                } else {
                    if(isAiming) {
                        weaponManager.getSelectedWeapon().shootAnimation();

                        //throw spear
                        if(weaponManager.getSelectedWeapon().ammoType == WeaponAmmoType.SPEAR) {
                            shootArrowOrSpear(false);

                        //shoot arrow
                        } else if(weaponManager.getSelectedWeapon().ammoType == WeaponAmmoType.ARROW) {
                            shootArrowOrSpear(true);
                        }
                    }

                }
            }

        }
    }

    //zooming in and out
    void zoom() {
        //aiming with camera on weapon (value=AIM)
        if(weaponManager.getSelectedWeapon().weaponAim == WeaponAim.AIM) {
            //if we press'n'hold RMB
            if(Input.GetMouseButtonDown(1)) {
                cameraZoomAnimator.Play("ZoomIn");
                crosshair.SetActive(false);
            }

            //if we release RMB
            if(Input.GetMouseButtonUp(1)) {
                cameraZoomAnimator.Play("ZoomOut");
                crosshair.SetActive(true);
            }
        }

        //aiming with camera on weapon (value=SELF_AIM)
        if(weaponManager.getSelectedWeapon().weaponAim == WeaponAim.SELF_AIM) {
            //if we press'n'hold RMB
            if(Input.GetMouseButtonDown(1)) {
                weaponManager.getSelectedWeapon().aim(true);
                isAiming = true;
            }

            //if we release RMB
            if(Input.GetMouseButtonUp(1)) {
                weaponManager.getSelectedWeapon().aim(false);
                isAiming = false;
            }
        }
    }

    //shoot bullets
    void shootBullets() {
        RaycastHit hit;

        //raycast from mainCamera position(where we are looking) towards the crosshair
        if(Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit)) {
            if(hit.transform.tag == "Enemy") {
                hit.transform.GetComponent<HealthScript>().applyDamage(damage);
            }
        }
    }

    //shoot/throw arrow or spear
    void shootArrowOrSpear(bool shootArrow) {
        if(shootArrow) {
            GameObject arrow = Instantiate(prefabArrow); //create copy of prefab
            arrow.transform.position = startPositionArrowAndSpear.position;
            arrow.GetComponent<Weapon_Arrow_Spear>().launch(mainCamera);
        } else {
            GameObject spear = Instantiate(prefabSpear); //create copy of prefab
            spear.transform.position = startPositionArrowAndSpear.position;
            spear.GetComponent<Weapon_Arrow_Spear>().launch(mainCamera);
        }
    }
}
