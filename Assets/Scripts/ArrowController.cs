using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ArrowController : MonoBehaviour
{
    private Vector3 initial_position;
    [SerializeField] private ParticleSystem collisionParticleSystemPrefab;
    [SerializeField] private AudioSource collisionSoundPrefab;
    // Start is called before the first frame update
    void Start()
    {
        initial_position = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.x > 500f || gameObject.transform.position.z > 500f || gameObject.transform.position.y < 10f)
        {
            Destroy(gameObject);
        }
    }

    public void Shoot(Vector3 direction)
    {
        GetComponent<Rigidbody>().AddForce(direction);
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            var score = (int)((1000 - (gameObject.transform.position - initial_position).magnitude) / 10);
            if (score < 0)
            {
                score = 1;
            }
            gameManager.AddScore(score);
            AddedScoreTextController addedScoreTextController = GameObject.Find("AddedScoreText").GetComponent<AddedScoreTextController>();
            addedScoreTextController.ShowAddedScoreText(score);
            var collisionParticleSystem = Instantiate(collisionParticleSystemPrefab, gameObject.transform.position, Quaternion.identity);
            var collisionParticleSystemController = collisionParticleSystem.GetComponent<CollisionParticleSystemController>();
            collisionParticleSystemController.DestroyAfterPlay();
            var collisionSound = Instantiate(collisionSoundPrefab, gameObject.transform.position, Quaternion.identity);
            var collisionAudioSourceController = collisionSound.GetComponent<CollisionAudioSourceController>();
            collisionAudioSourceController.DestroyAfterPlay();
            Destroy(gameObject);
            Destroy(other.gameObject);
            // GameManagerの衝突回数をインクリメント
            gameManager.collisionCount++;
            // 衝突回数が2になったらタイマーを停止
            if (gameManager.collisionCount == 2)
            {
                TimerRank timerRank = GameObject.Find("TimerObject").GetComponent<TimerRank>(); // TimerObjectはタイマーがアタッチされているゲームオブジェクトの名前です
                timerRank.StopTimer();
                gameManager.isGameActive = false; // isGameActive変数をfalseにする
                gameManager.DestroyAllBalls(); // すべてのボールを消去する
                // ScoreResultに移動する
                SceneManager.LoadScene("ScoreResult");
            }
        }
    }
}
