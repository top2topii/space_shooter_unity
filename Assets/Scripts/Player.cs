using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	//움직이는 속도를 정의해 줍니다.
    public const float moveSpeed = 5.0f;

    public GameObject ParticleFXExplosion;
    public GameObject laserPrefab; //발사할 레이저를 저장합니다.
    public bool canShoot = true; //레이저를 쏠 수 있는 상태인지 검사합니다.
    const float shootDelay = 0.5f; //레이저를 쏘는 주기를 정해줍니다.
    float shootTimer = 0; //시간을 잴 타이머를 만들어줍니다.

    public int health=100;
    const int collision_damage = 1000;

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
        transform.Translate(distanceX, distanceY, 0);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position); //캐릭터의 월드 좌표를 뷰포트 좌표계로 변환해준다.
        viewPos.x = Mathf.Clamp01(viewPos.x); //x값을 0이상, 1이하로 제한한다.
        viewPos.y = Mathf.Clamp01(viewPos.y); //y값을 0이상, 1이하로 제한한다.
        Vector3 worldPos = Camera.main.ViewportToWorldPoint(viewPos); //다시 월드 좌표로 변환한다.
        transform.position = worldPos; //좌표를 적용한다.
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
            Hit(collision_damage);
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

    public void Hit(int damage=0)
    {
        health -= damage;
        if (health < 0) health = 0;

        if (health == 0)
        {
            Explosion();
            GameEnd();
        }
    }

    void GameEnd()
    {
        Destroy(this.gameObject);   //자신을 파괴합니다.

        GameManager.instance.GameEnd();
    }

    void Explosion()
    {
        Instantiate(ParticleFXExplosion, this.transform.position, Quaternion.identity); //폭발 이펙트를 생성합니다.
    }
}
