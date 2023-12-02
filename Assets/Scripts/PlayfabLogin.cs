using UnityEngine;
using PlayFab.ClientModels;
using PlayFab;

public class PlayfabLogin : MonoBehaviour
{
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
    }
    private void PlayFabAuthService_OnPlayFabError(PlayFabError error)
    {
        Debug.Log("���O�C�����s");
        Debug.Log(error.ToString());    // �����Ȃ�
    }

    void Start()
    {
        PlayFabAuthService.Instance.Authenticate(Authtypes.Silent);
    }
}