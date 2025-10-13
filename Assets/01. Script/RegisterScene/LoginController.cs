using UnityEngine;
public class LoginController : MonoBehaviour
{
    LoginFormUI view;
    AuthService auth;
    private void Awake()
    {
        view = GetComponent<LoginFormUI>();
        auth = new AuthService();

        view.OnLoginRequested += HandleLogin;
      //  view.OnGoSignupRequested += () => screenm
    }


    void HandleLogin(string email, string password)
    {
        view.SetInteractable(false);

        var user = auth.Login(email, password);
        if(user == null)
        {
            view.Show("�̸��� �Ǵ� ��й�ȣ�� �ùٸ��� �ʽ��ϴ�.");
            view.SetInteractable(true);
            return;
        }
        view.Show("�α��� ����");

       // ScreenManager.GoTo(ScreenID.HOME);
    }
}
