using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketLauncher : MonoBehaviour
{

    public GameObject rocket;
    public GameObject explosion;
    public GameObject spawnPoint;

    public AudioClip shotSound;
    public AudioClip reloadSound;
    public AudioClip emptyGunSound;
    public AudioClip explosionSound;

    public float rocketForce;
    public float explosionRadius;
    public float explosionDamage;
    public LayerMask explosionLayerMask;

    public Text ammoText;

    public int rocketsAmount;

    int rocketsLeft;
    AudioSource source;

    bool isReloading;
    bool isCharged = true;
    bool isShot;

    int rocketInChamber;
    GameObject crosshair;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        rocketsLeft = rocketsAmount;
    }

    void OnEnable()
    {
        isReloading = false;
        crosshair = GameObject.Find("Crosshair");
        crosshair.SetActive(false);
    }

    void OnDisable()
    {
        crosshair.SetActive(true);
    }

    void Update()
    {
        rocketInChamber = isCharged ? 1 : 0;
        ammoText.text = rocketInChamber + " / " + rocketsLeft;

        if (Input.GetButtonDown("Fire1") && isCharged && !isReloading)
        {
            isCharged = false;
            source.PlayOneShot(shotSound);
            GameObject rocketInstantiated = (GameObject)Instantiate(rocket, spawnPoint.transform.position, Quaternion.identity);
            rocketInstantiated.GetComponent<Rocket>().damage = explosionDamage;
            rocketInstantiated.GetComponent<Rocket>().radius = explosionRadius;
            rocketInstantiated.GetComponent<Rocket>().explosionSound = explosionSound;
            rocketInstantiated.GetComponent<Rocket>().layerMask = explosionLayerMask;
            rocketInstantiated.GetComponent<Rocket>().explosion = explosion;
            Rigidbody rocketRb = rocketInstantiated.GetComponent<Rigidbody>();
            rocketRb.AddForce(Camera.main.transform.forward * rocketForce, ForceMode.Impulse);
            Reload();
        }
        else if (Input.GetButtonDown("Fire1") && !isCharged && !isReloading)
            Reload();
    }

    void Reload()
    {
        if (rocketsLeft <= 0)
        {
            source.PlayOneShot(emptyGunSound);
        }
        else
        {
            StartCoroutine("ReloadWeapon");
            rocketsLeft--;
            isCharged = true;
        }
    }

    IEnumerator ReloadWeapon()
    {
        isReloading = true;
        source.PlayOneShot(reloadSound);
        yield return new WaitForSeconds(2.5f);
        isReloading = false;
    }
}