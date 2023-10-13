using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BFGWeapon : MonoBehaviour
{
    [SerializeField] private GameObject objBFG;

    [SerializeField] private float coolingTime = 25.0f;
    [SerializeField] private GameObject wpContainer;
    [SerializeField] private Button wpBtn;
    bool isDelay;
    Animator anim;

    void Awake()
    {
        wpContainer = GameObject.Find("---GameManager---").transform.GetChild(2).transform.GetChild(2).gameObject;
        wpBtn = wpContainer.transform.Find(GameManager.instance.upgradeController.FindWeaponIndex("Canon").ToString()).GetComponentInChildren<Button>();
        anim = GetComponent<Animator>();

        wpBtn.onClick.AddListener(Fire);
    }
    void Fire()
    {
        if (GameManager.instance.playerHealth.isHurt == false)
        {
            if (isDelay == false)
            {
                isDelay = true;
                anim.SetTrigger("On");

                StartCoroutine(FireBFGCoroutine());

                StartCoroutine(CoolTime());
            }
            else
            {
                Debug.Log("√Ê¿¸¡ﬂ");
            }
        }
        
    }

    IEnumerator CoolTime()
    {
        yield return new WaitForSeconds(coolingTime);
        isDelay = false;
    }

    IEnumerator FireBFGCoroutine()
    {
        yield return new WaitForSeconds(0.6f);
        FireBFG();
        GameManager.instance.soundManager.PlaySFX("Bfg");
        yield return new WaitForSeconds(1.1f);
    }

    void FireBFG()
    {

        GameObject BFGBullet = GameManager.instance.objectManager.MakeObj("Bullet_Player_BFGun");
        BFGBullet.transform.position = GameManager.instance.playerMove.transform.position;

        Rigidbody2D rigid = BFGBullet.GetComponent<Rigidbody2D>();

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
}
