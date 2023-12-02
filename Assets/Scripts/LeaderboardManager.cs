using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI; // UI���g�p���邽�߂ɕK�v

public class LeaderboardManager : MonoBehaviour
{
    public GameObject textPrefab; // �e�L�X�g�v���n�u�ւ̎Q��
    public Transform contentPanel; // �X�N���[���r���[�̃R���e���c�̈�ւ̎Q��

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "SpeedScore",
            StartPosition = 0,
            MaxResultsCount = 10 // �g�b�v10�̃X�R�A���擾
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }

    private void OnLeaderboardGet(GetLeaderboardResult result)
    {
        // �����̃��[�_�[�{�[�h�G���g�����N���A����
        foreach (Transform child in contentPanel)
        {
            Destroy(child.gameObject);
        }

        // �����L���O�f�[�^�̎擾�ƕ\��
        int rank = 1; // �����N�̏����l��ݒ�
        foreach (var item in result.Leaderboard)
        {
            // �f�o�b�O���O
            Debug.Log($"Rank: {rank}, Name: {item.DisplayName}, Value: {item.StatValue}");

            GameObject newText = Instantiate(textPrefab, contentPanel);
            newText.GetComponent<Text>().text = $"Rank: {rank++}, Name: {item.DisplayName}, Value: {item.StatValue}";
        }
    }

    private void OnError(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}
