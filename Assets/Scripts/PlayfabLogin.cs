using UnityEngine;
using UnityEngine.UI; 
using PlayFab.ClientModels;
using PlayFab;
public class PlayFabLogin : MonoBehaviour
{
    public Text nameRecordText; // �ǉ�: �����L���O�f�[�^��\������Text�ւ̎Q��

    private void OnEnable()
    {
        PlayFabAuthService.OnLoginSuccess += PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError += PlayFabAuthService_OnPlayFabError;
    }

    private void OnDisable()
    {
        PlayFabAuthService.OnLoginSuccess -= PlayFabAuthService_OnLoginSuccess;
        PlayFabAuthService.OnPlayFabError -= PlayFabAuthService_OnPlayFabError;
    }

    private void PlayFabAuthService_OnLoginSuccess(LoginResult success)
    {
        Debug.Log("���O�C������");
        GetLeaderboard();
    }

    private void PlayFabAuthService_OnPlayFabError(PlayFabError error)
    {
        Debug.Log("���O�C�����s: " + error.GenerateErrorReport());
    }

    void Start()
    {
        // �����Ń��O�C�����������s����
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }


    public void GetLeaderboard()
    {
        PlayFabClientAPI.GetLeaderboard(new GetLeaderboardRequest
        {
            StatisticName = "SpeedScore"
        }, result =>
        {
            nameRecordText.text = ""; // �e�L�X�g��������
            foreach (var item in result.Leaderboard)
            {
                string displayName = item.DisplayName ?? "NoName";
                nameRecordText.text += $"{item.Position + 1}��: {displayName} �X�R�A {item.StatValue}\n";
            }
        }, error =>
        {
            Debug.Log(error.GenerateErrorReport());
        });
    }
}