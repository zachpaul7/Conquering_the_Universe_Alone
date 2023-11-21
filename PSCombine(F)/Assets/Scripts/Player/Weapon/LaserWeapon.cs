using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LaserWeapon : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject endLaserSet;
    [SerializeField] private Vector3 laserOffset;
    [SerializeField] private Vector3 endLaserOffset;

    [SerializeField] private float coolingTime = 25.0f;
    [SerializeField] private float shootingTime = 2.0f;

    [SerializeField] private GameObject wpContainer;
    [SerializeField] private Button wpBtn;

    Animator anim;
    bool isDelay;

    void Awake()
    {
        wpContainer = GameObject.Find("---GameManager---").transform.GetChild(2).transform.GetChild(1).gameObject;
        wpBtn = wpContainer.transform.Find(GameManager.instance.upgradeController.FindWeaponIndex("Razer").ToString()).GetComponentInChildren<Button>();

        anim = GetComponent<Animator>();

        wpBtn.onClick.AddListener(Fire);
    }

    void Fire()
    {
        if (isDelay == false)
        {
            transform.GetChild(0).GetComponent<Collider2D>().enabled = true;
            isDelay = true;
            anim.SetTrigger("Fire");

            StartCoroutine(SpawnLaserDelayed(0.5f));
            StartCoroutine(ShootingLaserDelayed());
            StartCoroutine(CoolTime());
        }
        else
        {
            Debug.Log("√Ê¿¸¡ﬂ");
        }
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolingTime);
        isDelay = false;
    }

    IEnumerator SpawnLaserDelayed(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        SpawnLaser(true);
    }

    IEnumerator ShootingLaserDelayed()
    {
        yield return new WaitForSeconds(shootingTime);
        SpawnLaser(false);
        EndLaser(true);
        yield return new WaitForSeconds(0.1f);
        EndLaser(false);
    }

    public void SpawnLaser(bool isActive)
    {
        laserPrefab.SetActive(isActive);
    }

    public void EndLaser(bool isActive)
    {
        endLaserSet.SetActive(isActive);
    }
}