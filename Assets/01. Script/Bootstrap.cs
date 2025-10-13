// Bootstrap.cs
using UnityEngine;
public class Bootstrap : MonoBehaviour
{
    [SerializeField] SceneNavigator navigator;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        navigator.IsAuthenticated = false;           // �ʱ⿣ ������
        navigator.GoTo(ScreenId.REGISTER);
    }
}
