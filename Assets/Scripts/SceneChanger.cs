using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneToRanking()
    {
        SceneManager.LoadScene("RankingScene"); // ここに遷移したいシーンの名前を入力
    }
}
