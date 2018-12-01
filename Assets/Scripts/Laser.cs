using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    //총알이 움직일 속도를 상수로 지정해줍시다.
    private const float moveSpeed = 10.0f;
    private const int damage = 100;

    void Start()
    {
        soundManager.instance.PlayShootSound();
    }
    void Update()
    {
        //이동할 거리를 지정해 줍시다.
        float moveX = moveSpeed * Time.deltaTime;

        //이동을 반영해줍시다.
        transform.Translate(moveX, 0, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //부딪힌 객체가 적인지 검사합니다.
        if (other.tag.Equals("Enemy"))
        {

            other.gameObject.GetComponent<Enemy>().Hit(damage);

            Destroy(this.gameObject); //자기 자신을 지웁니다.
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);// 자기 자신을 지웁니다.
    }
}
