using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;

public class PlayFabLeaderboardViewer : MonoBehaviour
{
    public Text nameRecordText; // �����̃t�B�[���h

    void Start()
    {
        // �V�[�������[�h���ꂽ�Ƃ��Ƀ����L���O���擾����
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
            nameRecordText.text = ""; // �e�L�X�g��������
            foreach (var item in result.Leaderboard)
            {
                string displayName = item.DisplayName ?? "NoName";
                // �X�R�A��-1�{���Č��ɖ߂�
                int correctedScore = item.StatValue * -1;

                // �X�R�A�����A���A�b�ɕ���
                int hours = correctedScore / 3600;
                int minutes = (correctedScore % 3600) / 60;
                int seconds = correctedScore % 60;

                string hoursText = hours > 0 ? $"{hours}��" : "";
                nameRecordText.text += $"{item.Position + 1}��: {displayName} �X�R�A {hoursText}{minutes:D2}�b{seconds:D2}\n";
            }
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}