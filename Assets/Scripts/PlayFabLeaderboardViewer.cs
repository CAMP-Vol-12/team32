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
                // �X�R�A�����A���A�b�ɕ���
                int hours = item.StatValue / 3600;
                int minutes = (item.StatValue % 3600) / 60;
                int seconds = item.StatValue % 60;
                // ���A���A�b���������ĕ\��
                nameRecordText.text += $"{item.Position + 1}��: {displayName} �X�R�A {hours:D2}:{minutes:D2}:{seconds:D2}\n";
            }
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}
