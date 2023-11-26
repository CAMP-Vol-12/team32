// �N���b�N�����I�u�W�F�N�g�����������i�������̏����j
using UnityEngine;

public class ShooterGameController : MonoBehaviour
{
    public Camera playerCamera;
    public Texture2D crosshairTexture; // �Ə��̃e�N�X�`��
    private Vector2 crosshairHotspot; // �Ə��̒��S�_
    public int score = 0;

    void Start()
    {
        // �Ə��̒��S���v�Z
        crosshairHotspot = new Vector2(crosshairTexture.width / 2, crosshairTexture.height / 2);

        // �J�[�\�����J�X�^���̏Ə��ɕύX
        Cursor.SetCursor(crosshairTexture, crosshairHotspot, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // �}�E�X���N���b�N�����o
        {
            RaycastHit hit;
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                // ������hit.transform.gameObject���N���b�N���ꂽ�I�u�W�F�N�g
                Destroy(hit.transform.gameObject); // �I�u�W�F�N�g��j��
                score++; // ���_�����Z
            }
        }
    }
}
