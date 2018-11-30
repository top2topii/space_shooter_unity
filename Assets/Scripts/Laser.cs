using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    //총알이 움직일 속도를 상수로 지정해줍시다.
    private const float moveSpeed = 10.0f;

    public GameObject ParticleFXExplosion;

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
        if (other.tag.Equals("Enemy"))
        { //부딪힌 객체가 적인지 검사합니다.
            Instantiate(ParticleFXExplosion, this.transform.position, Quaternion.identity); //폭발 이펙트를 생성합니다.

            GameManager.instance.AddScore(50);
            Destroy(other.gameObject); //부딪힌 적을 지웁니다.
            Destroy(this.gameObject); //자기 자신을 지웁니다.
        }
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);// 자기 자신을 지웁니다.
    }
}
