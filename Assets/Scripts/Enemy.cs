using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    public const float moveSpeed = 1.3f;
    //상수로 움직일 속도를 지정해 줍니다.
    void Start () {

    }
    void Update () {
        moveControl();    
        //프레임이 변화할때 마다 움직임을 관리해주는 함수를 호출해줍시다.
    }
    void moveControl()
    {
        
        //float distanceX = moveSpeed * Time.deltaTime;
        float distanceX = moveSpeed * Time.deltaTime;
        float distanceY = 0;
        //움직일 거리를 계산해줍니다.
        this.gameObject.transform.Translate(-1 * distanceX, -1 * distanceY, 0);
        //움직임을 반영합니다.
    }

    void OnBecameInvisible()
    {
        Destroy(this.gameObject);// 자기 자신을 지웁니다.
    }
}
