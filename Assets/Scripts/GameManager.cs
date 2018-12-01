using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //어디서나 접근할 수 있도록 static(정적)으로 자기 자신을 저장할 변수를 만듭니다.
    public Text scoreText; //점수를 표시하는 Text객체를 에디터에서 받아옵니다.
    public Text readyText;
    public Text gameOverText;

    private int score = 0; //점수를 관리합니다.
    void Awake()
    {
        if (!instance) //정적으로 자신을 체크합니다.
            instance = this; //정적으로 자신을 저장합니다.
    }

    public void AddScore(int num) //점수를 추가해주는 함수를 만들어 줍니다.
    {
        score += num; //점수를 더해줍니다.
        scoreText.text = "Score: " + score.ToString(); //텍스트에 반영합니다.
    }

    void Start()
    {
        gameOverText.enabled = false;
        StartCoroutine(Ready());
    }

    void Update()
    {

    }

    IEnumerator Ready()
    {
        spawnManager.instance.SpawnOff();

        int i = 0;
        while (i < 3)
        {
            readyText.enabled = false;
            yield return new WaitForSeconds(0.5f);
            readyText.enabled = true;
            yield return new WaitForSeconds(0.5f);
            i++;
        }

        OffReadyText();
        spawnManager.instance.SpawnOn();

    }

    public void OffReadyText()
    {
        readyText.enabled = false;
    }

    public void OnGameOverText()
    {
        gameOverText.enabled = true;
    }

    public void GameEnd()
    {
        OnGameOverText();
        spawnManager.instance.SpawnOff();
    }
}
