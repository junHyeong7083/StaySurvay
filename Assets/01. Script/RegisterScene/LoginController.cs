using UnityEngine;

[RequireComponent(typeof(LoginFormUI))]
public class LoginController : MonoBehaviour
{
    [Header("Navigation")]
    [SerializeField] SceneNavigator navigator;              // Bootstrap ���� �׺������
    [Header("Tabs")]
    [SerializeField] RegisterTabsController tabs;           // RegisterScene�� �� ������
    [Header("Texts (Optional)")]
    [SerializeField] AuthUIText texts;

    private LoginFormUI view;
    private IAuthService auth;

    void Awake()
    {
        view = GetComponent<LoginFormUI>();
        auth = new AuthService(); // ���߿� RemoteAuthService�� ��ü ����

        // �� ��ȯ: ȸ������ ������
        view.OnGoSignupRequested += HandleGoSignup;

        // �α��� �õ� (��ư�� �׻� ����)
        view.OnLoginRequested += HandleLogin;

        view.Show(texts ? texts.required : "�ʼ� �׸��� �Է��ϼ���.");
    }

    void OnDestroy()
    {
        if (!view) return;
        view.OnLoginRequested -= HandleLogin;
        view.OnGoSignupRequested -= HandleGoSignup;
    }

    void HandleGoSignup() => tabs?.ShowSignup();

    void HandleLogin(string email, string pw)
    {
        var res = auth.Login(email, pw);

        if (!res.Ok || res.Value == null)
        {
            view.Show(texts ? texts.loginFail : "�̸��� �Ǵ� ��й�ȣ�� �ùٸ��� �ʽ��ϴ�.");
            return;
        }

        if (SessionManager.Instance != null)
            SessionManager.Instance.SignIn(res.Value);

        // Ȩ���� ��ȯ (����� SceneNavigator�� SessionManager�� ��ȸ)
        navigator?.GoTo(ScreenId.HOME);
    }
}
