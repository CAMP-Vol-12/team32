using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;

public class PlayFabLeaderboardViewer : MonoBehaviour
{
    public Text rankingText; // �����L���O�\���p�̃e�L�X�g
    public Text scoreText;   // �X�R�A�\���p�̃e�L�X�g

    void Start()
    {
        GetLeaderboard();
    }

    public void GetLeaderboard()
    {
        if (!GlobalLoginState.IsLoggedIn)
        {
            Debug.LogError("���[�U�[�̓��O�C�����Ă��܂���B");
            return;
        }

        Debug.Log("���[�U�[�̓��O�C�����Ă��܂��B���[�_�[�{�[�h���擾���܂��B");

        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "SpeedScore"
        }, result =>
        {
            rankingText.text = "";
            scoreText.text = "";

            foreach (var item in result.Leaderboard)
            {
                string displayName = item.DisplayName ?? "NoName";

                string line = $"{item.Position + 1}��:{displayName}";
                line = line.PadRight(20); // 20�͓K�X����

                float correctedScore = (float)item.StatValue * -1 / 100;
                int hours = (int)correctedScore / 3600;
                int minutes = ((int)correctedScore % 3600) / 60;
                int seconds = (int)correctedScore % 60;
                int milliseconds = Mathf.RoundToInt((correctedScore - (int)correctedScore) * 100);

                string hoursText = hours > 0 ? $"{hours}����" : "";
                string scoreLine = $"{hoursText}{minutes:D2}��{seconds:D2}�b{milliseconds:D2}";

                rankingText.text += $"{line}\n";
                scoreText.text += $"�X�R�A�F{scoreLine}\n";
            }
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}
