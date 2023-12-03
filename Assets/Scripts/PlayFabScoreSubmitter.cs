using UnityEngine;
using UnityEngine.UI;
using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
public class PlayFabScoreSubmitter : MonoBehaviour
{
    public InputField nameInputField; // ���[�U�[�����͗p��InputField
    public Text errorMessageText;     // �G���[���b�Z�[�W�\���p��Text
    public SceneChanger sceneChanger; // SceneChanger�ւ̎Q��


    void Start()
    {
        // ���������ɃG���[���b�Z�[�W���\���ɐݒ�
        errorMessageText.text = "";
        errorMessageText.gameObject.SetActive(false); // �K�v�ɉ�����
    }
    public void SubmitScoreWithName()
    {
        if (!GlobalLoginState.IsLoggedIn)
        {
            Debug.LogError("���[�U�[�̓��O�C�����Ă��܂���B");
            return;
        }

        if (nameInputField.text.Length > 6)
        {
            errorMessageText.text = "���[�U�[����6�����܂łȂ̂�";
            errorMessageText.gameObject.SetActive(true); // �G���[���b�Z�[�W��\��
            return;
        }

        // �G���[���Ȃ��ꍇ�̓G���[���b�Z�[�W���\����
        errorMessageText.gameObject.SetActive(false);

        ResultScreen resultScreen = FindObjectOfType<ResultScreen>();
        float score = resultScreen.GetScore();
        Debug.Log(score);

        int scaledScore = Mathf.RoundToInt(score * 100) * -1;

        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "SpeedScore",
                    Value = scaledScore
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, result =>
        {
            Debug.Log("�X�R�A�o�^����");
            SetDisplayName();
        }, OnPlayFabError);
    }

    private void SetDisplayName()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInputField.text
        };

        PlayFabClientAPI.UpdateUserTitleDisplayName(request, result =>
        {
            Debug.Log("�\�������X�V���܂���");
            sceneChanger.ChangeSceneToRanking(); // �����ŃV�[���J�ڂ��g���K�[
        }, OnPlayFabError);
    }

    private void OnPlayFabError(PlayFabError error)
    {
        Debug.LogError("PlayFab�G���[: " + error.GenerateErrorReport());
    }
}
