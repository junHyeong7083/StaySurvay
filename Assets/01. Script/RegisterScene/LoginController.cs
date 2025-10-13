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
        auth = new AuthService();

        // �� ��ȯ: ȸ������ ������
        view.OnGoSignupRequested += () => tabs?.ShowSignup();

        // �α��� �õ� (��ư�� �׻� ����)
        view.OnLoginRequested += HandleLogin;
        view.Show(texts ? texts.required : "�ʼ� �׸��� �Է��ϼ���.");
    }

    void OnDestroy()
    {
        if (!view) return;
        view.OnLoginRequested -= HandleLogin;
        view.OnGoSignupRequested -= () => tabs?.ShowSignup();
    }

    void HandleLogin(string email, string pw)
    {
        // ���� ���е� messageText�θ� ǥ�� (��ư ��Ȱ��ȭ X)
        var res = auth.Login(email, pw);
        if (!res.Ok || res.Value == null)
        {
            // ���� ���ο� ���� �޽���(���ϸ� switch�� �� ����ȭ)
            view.Show(texts ? texts.loginFail : "�̸��� �Ǵ� ��й�ȣ�� �ùٸ��� �ʽ��ϴ�.");
            return;
        }

        // ����
        view.Show(texts ? texts.loginDone : "�α��� ����");
        if (navigator) navigator.IsAuthenticated = true;
        navigator?.GoTo(ScreenId.HOME);
    }
}
