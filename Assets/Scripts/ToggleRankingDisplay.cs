using UnityEngine;

public class RankingUIController : MonoBehaviour
{
    public GameObject RankingView; // �X�N���[���r���[�ւ̎Q��

    public void ToggleRankingDisplay()
    {
        RankingView.SetActive(!RankingView.activeSelf);
    }
}

