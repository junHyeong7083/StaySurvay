using UnityEngine;

public class DataService : MonoBehaviour
{
    public static DataService Instance { get; private set; }

    [SerializeField] bool useRemote = false;
    [SerializeField] string baseUrl = "https://api.example.com";

    public IUserDataService User { get; private set; }
    public IAdminDataService Admin { get; private set; }

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // ������ ���� ������ ���
        User = new LocalUserDataService();
        Admin = new LocalAdminDataService();
    }
}
