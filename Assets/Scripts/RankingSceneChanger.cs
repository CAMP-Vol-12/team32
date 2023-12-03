using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneToRanking()
    {
        StartCoroutine(ChangeSceneAfterDelay("RankingScene", 2f)); // 2�b��ɃV�[���J�ڂ���R���[�`�����J�n
    }

    IEnumerator ChangeSceneAfterDelay(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay); // 2�b�ԑҋ@
        SceneManager.LoadScene(sceneName); // �V�[�������[�h
    }
}
