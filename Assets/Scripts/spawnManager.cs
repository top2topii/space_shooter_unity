using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public static spawnManager instance; //어디서나 접근할 수 있도록 static(정적)으로 자기 자신을 저장할 변수를 만듭니다.

    public bool enableSpawn = false;
    public GameObject Enemy; //Prefab을 받을 public 변수 입니다.

    void Awake()
    {
        if (!instance) //정적으로 자신을 체크합니다.
            instance = this; //정적으로 자신을 저장합니다.
    }

    void SpawnEnemy()
    {

        //GameManager.instance.OffReadyText();

        Debug.Log("SpawnEnemy!");

        float randomY = Random.Range(-4.5f, 4.5f); //적이 나타날 X좌표를 랜덤으로 생성해 줍니다.

        if (enableSpawn)
        {
            GameObject a = (GameObject) Instantiate(Enemy, new Vector3(10.0f, randomY, 0f), Quaternion.identity); //랜덤한 위치와, 화면 제일 위에서 Enemy를 하나 생성해줍니다.
            a.GetComponent<Enemy>().SetScore(100);
        }
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, 1); //3초후 부터, SpawnEnemy함수를 1초마다 반복해서 실행 시킵니다.
    }

    void Update()
    {

    }

    public void SpawnOn()
    {
        enableSpawn = true;
    }
    public void SpawnOff()
    {
        enableSpawn = false;
    }
}
