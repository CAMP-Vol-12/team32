using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using System;

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
                // �X�R�A��-1�{���Č��ɖ߂��A100�Ŋ���
                float correctedScore = (float)item.StatValue * -1 / 100;

                // �X�R�A�����A���A�b�ɕ���
                int hours = (int)correctedScore / 60;
                int minutes = (int)correctedScore % 60;
                float seconds = correctedScore % 1f;

                // �~���b���v�Z�i�b�̏���������100�{���A�����ɕϊ��j
                int milliseconds = Mathf.RoundToInt(seconds * 100);

                string hoursText = hours > 0 ? $"{hours}��" : "";
                nameRecordText.text += $"{item.Position + 1}��: {displayName} �X�R�A {hoursText}{minutes:D2}�b{milliseconds:D2}\n";
            }
        }, error =>
        {
            Debug.LogError(error.GenerateErrorReport());
        });
    }
}