using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BFGWeapon : MonoBehaviour
{
    [SerializeField] private GameObject objBFG;

    [SerializeField] private float coolingTime = 25.0f;

    bool isDelay;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isDelay == false)
            {
                isDelay = true;

                objBFG.SetActive(true);

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

        yield return new WaitForSeconds(1.1f);
        objBFG.SetActive(false);
    }

    void FireBFG()
    {
        GameObject BFGBullet = GameManager.instance.objectManager.MakeObj("Bullet_Player_BFGun");
        BFGBullet.transform.position = GameManager.instance.playerMove.transform.position;

        Rigidbody2D rigid = BFGBullet.GetComponent<Rigidbody2D>();

        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
    }
}
