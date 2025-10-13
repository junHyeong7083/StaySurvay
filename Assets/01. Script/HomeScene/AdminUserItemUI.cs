using TMPro;
using UnityEngine;

public class AdminUserItemUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text emailText;
    public TMP_Text roleText;
    public TMP_Text activeText;

    public void Bind(UserSummary u)
    {
        if (nameText) nameText.text = u.Name ?? "-";
        if (emailText) emailText.text = u.Email ?? "-";
        if (roleText) roleText.text = u.Role.ToString();


        var currentEmail = SessionManager.Instance?.CurrentUser?.Email;
        bool isCurrent = !string.IsNullOrEmpty(currentEmail) && currentEmail == u.Email;

        // isCurrent: ���� �α����� �����
        // u.IsActive: ���� ��� ���� ����(����/��Ȱ�� �ƴ�)
        string status = isCurrent
            ? "Ȱ��(���� ����)"
            : (u.IsActive ? "��������" : "����");

        if (activeText) activeText.text = status;
    }
}
