﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	//움직이는 속도를 정의해 줍니다.
    public const float moveSpeed = 5.0f;

    public GameObject explosionPrefab;
    public GameObject laserPrefab; //발사할 레이저를 저장합니다.
    public bool canShoot = true; //레이저를 쏠 수 있는 상태인지 검사합니다.
    const float shootDelay = 0.5f; //레이저를 쏘는 주기를 정해줍니다.
    float shootTimer = 0; //시간을 잴 타이머를 만들어줍니다.

    void Start () {

    }

    void Update () {
    	//캐릭터를 움직이는 함수를 프레임마다 호출 합니다.
        moveControl();
        ShootControl();
    }

    void moveControl()
    {
    	//아까 지정한 Axes를 통해 키의 방향을 판단하고 속도와 프레임 판정을 곱해 이동량을 정해줍니다.
        float distanceX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;

        //아까 지정한 Axes를 통해 키의 방향을 판단하고 속도와 프레임 판정을 곱해 이동량을 정해줍니다.
        float distanceY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        
        //이동량만큼 실제로 이동을 반영합니다.
        this.gameObject.transform.Translate(distanceX, distanceY, 0);
    }

    //rigidBody가 무언가와 충돌할때 호출되는 함수 입니다.
    //Collider2D other로 부딪힌 객체를 받아옵니다.
    void OnTriggerEnter2D(Collider2D other)
    {
        //GameObject explosionPrefab = GameObject.Find("Explosion");
        //부딪힌 객체의 태그를 비교해서 적인지 판단합니다.

        Debug.Log("OnTriggerEnter2D called!");

        //GameObject explosionPrefab;
        if (other.gameObject.tag.Equals("Enemy"))
        {
            Instantiate(explosionPrefab,
            // Instantiate는 객체를 하나 생성(복제)합니다 첫번째 인자로는 생성할 객체의 원본을 넣어주고
                this.transform.position,
                //두번째 인자로는 생성될 위치를 넣어줍니다. this.transform.position은 자기자신의 위치를 나타냅니다.
                Quaternion.identity);
                //세번째 인자로는 객체의 회전값을 넣어주는데요, Quaternion.identity는 회전이 적용되지 않은 값을 나타냅니다.
            
            Destroy(other.gameObject);  //적을 파괴합니다.
            Destroy(this.gameObject);   //자신을 파괴합니다.

        }
    }

    void ShootControl() // 발사를 관리하는 함수 입니다.
    {
        if (canShoot) // 쏠 수 있는 상태인지 검사합니다.
        {
            if (shootTimer > shootDelay && Input.GetKey(KeyCode.Space)) //쿨타임이 지났는지와, 공격키인 스페이스가 눌려있는지 검사합니다.
            {
                Instantiate(laserPrefab, transform.position, Quaternion.identity); //레이저를 생성해줍니다.

                shootTimer = 0; //쿨타임을 다시 카운트 합니다.
            }
            shootTimer += Time.deltaTime; //쿨타임을 카운트 합니다.
        }
    }

}
