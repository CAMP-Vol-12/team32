using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner2 : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    private GameManager gameManager; // GameManagerへの参照を追加
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        StartCoroutine(SpawnBall(5));
    }

    private IEnumerator SpawnBall(int numberOfBalls)
    {
        while (gameManager.hitBallCount < 42)
        {
            yield return new WaitForSeconds(1f);

            int sign_x = Random.Range(0, 2) * 2 - 1;
            float random_x = sign_x * Random.Range(50f, 150f);
            float y = Random.Range(30f, 90f);
            Instantiate(ballPrefab, new Vector3(random_x, y, -100), Quaternion.identity);
        }
    }
}
