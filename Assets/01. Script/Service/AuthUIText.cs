using UnityEngine;

[CreateAssetMenu(fileName = "AuthUIText", menuName = "Auth/UI Text", order = 0)]
public class AuthUIText : ScriptableObject
{
    [Header("Common")]
    public string required = "�ʼ� �׸��� �Է��ϼ���.";

    [Header("Signup")]
    public string emailFormatError = "�̸��� ���� ����";
    public string emailDuplicate = "�̹� ��� ���� �̸����Դϴ�.";
    public string emailAvailable = "��� ������ �̸����Դϴ�.";
    public string nameEmpty = "�̸��� �Է��ϼ���.";
    public string pwWeak = "�ּ� 8��, ����+���� ����";
    public string pwStrong = "������ ��й�ȣ";
    public string pwConfirmMismatch = "��й�ȣ Ȯ���� ��ġ���� �ʽ��ϴ�.";
    public string signupFail = "���� ����. �ٽ� �õ��ϼ���.";
    public string signupDone = "���� �Ϸ�";

    [Header("Login")]
    public string loginInProgress = "�α��� ��...";
    public string loginFail = "�̸��� �Ǵ� ��й�ȣ�� �ùٸ��� �ʽ��ϴ�.";
    public string loginDone = "�α��� ����";
}
