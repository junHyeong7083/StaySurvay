using System;
using UnityEngine;

/// <summary>
/// �� ������ �α��� ����/���� ������ �����ϴ� ���� �Ŵ���.
/// - ��Ÿ�� �α��� ����(IsSignedIn), ���� �����(CurrentUser)
/// - �α���/�α׾ƿ� API
/// - (�ɼ�) ���� ����/���� (PlayerPrefs �̿�: ����/�����)
/// </summary>
public class SessionManager : MonoBehaviour
{
    public static SessionManager Instance { get; private set; }

    [Serializable]
    public class UserSnapshot   // ����ȭ�� �ּ� ������ (�ΰ����� ����)
    {
        public string Name;
        public string Email;
        public int Role;      // enum ����� int
        public bool IsActive;
    }

    public bool IsSignedIn => _currentUser != null;
    public User CurrentUser => _currentUser;
    public string SessionId { get; private set; }   // �ʿ� �� ���(���� ����/Ʈ��ŷ)

    public event Action OnChanged;

    User _currentUser;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>�α��� ���� �� ���� ���� ���</summary>
    public void SignIn(User user, string sessionId = null)
    {
        _currentUser = user;
        SessionId = sessionId ?? System.Guid.NewGuid().ToString("N");
        Save();             // �ڵ� ���� ��ġ ������ �ּ� ó��
        OnChanged?.Invoke();
        Debug.Log($"[Session] Signed in: {_currentUser?.Email}");
    }

    /// <summary>����� �α׾ƿ�</summary>
    public void SignOut()
    {
        _currentUser = null;
        SessionId = null;
        Clear();
        OnChanged?.Invoke();
        Debug.Log("[Session] Signed out");
    }

    // ������������ (�ɼ�) ���� ����/���� ���� ������������
    const string KeyUser = "session.user";    // PlayerPrefs Ű (����/���߿�)
    const string KeySess = "session.id";

    /// <summary>��ũ���� ������ ����(������ true)</summary>
    public bool TryRestore()
    {
        if (!PlayerPrefs.HasKey(KeyUser)) return false;
        try
        {
            var json = PlayerPrefs.GetString(KeyUser);
            var snap = JsonUtility.FromJson<UserSnapshot>(json);
            if (snap == null) return false;

            _currentUser = new User
            {
                Name = snap.Name,
                Email = snap.Email,
                Role = (UserRole)snap.Role,
                IsActive = snap.IsActive
            };
            SessionId = PlayerPrefs.GetString(KeySess, null);

            OnChanged?.Invoke();
            Debug.Log($"[Session] Restored: {_currentUser.Email}");
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"[Session] Restore failed: {e}");
            Clear();
            return false;
        }
    }

    /// <summary>���� ������ ��ũ�� ����</summary>
    public void Save()
    {
        if (_currentUser == null) { Clear(); return; }
        var snap = new UserSnapshot
        {
            Name = _currentUser.Name,
            Email = _currentUser.Email,
            Role = (int)_currentUser.Role,
            IsActive = _currentUser.IsActive
        };
        PlayerPrefs.SetString(KeyUser, JsonUtility.ToJson(snap));
        PlayerPrefs.SetString(KeySess, SessionId ?? "");
        PlayerPrefs.Save();
    }

    /// <summary>��ũ ���尪 ����</summary>
    public void Clear()
    {
        PlayerPrefs.DeleteKey(KeyUser);
        PlayerPrefs.DeleteKey(KeySess);
        PlayerPrefs.Save();
    }
}
