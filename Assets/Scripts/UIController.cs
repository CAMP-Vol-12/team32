using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject RankingView; // �X�N���[���r���[�ւ̎Q��

    public void ToggleRankingDisplay()
    {
        // �X�N���[���r���[�̕\����Ԃ�؂�ւ���
        RankingView.SetActive(!RankingView.activeSelf);
    }
}
