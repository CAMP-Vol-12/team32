// �N���b�N�����I�u�W�F�N�g�����������i�������̏����j
using UnityEngine;
using TMPro; // TextMeshPro���g�p���邽�߂ɕK�v

public class ShooterGameController : MonoBehaviour
{
    public Camera playerCamera;
    public Texture2D crosshairTexture;
    private Vector2 crosshairHotspot;
    public int score = 0;
    public TextMeshProUGUI scoreText; // ���̍s��ύX

    void Start()
    {
        // �J�[�\���̐ݒ�Ȃ�
        UpdateScoreUI(); // �����X�R�A��UI�ɕ\��

        // �J�[�\�����J�X�^���̏Ə��ɕύX
        Cursor.SetCursor(crosshairTexture, crosshairHotspot, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Target target = hit.transform.GetComponent<Target>();
                if (target != null)
                {
                    score += target.scoreValue;
                }
                else
                {
                    score++; // �^�[�Q�b�g�R���|�[�l���g���Ȃ��ꍇ��1�_�����Z
                }
                UpdateScoreUI(); // �X�R�AUI���X�V
                Destroy(hit.transform.gameObject);
            }
        }
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString(); // �X�R�A���e�L�X�g�ŕ\��
    }
}
