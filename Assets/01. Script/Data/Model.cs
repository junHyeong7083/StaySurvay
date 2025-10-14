using System;

public class Model
{ }

public enum UserRole { USER = 0, ADMIN = 1, SUPERADMIN = 2 }
public class User
{
    // ���� �ĺ���
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }

    // �α��ν� ���Ǵ� ���̵�
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public UserRole Role { get; set; } = UserRole.USER;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // admin �˻��� �����ʵ�
    public string LowerName { get; set; }
    public string NameChosung { get; set; }
}


public class ResultDoc
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string UserId { get; set; }     // User(class).Id ����
    public int Stage { get; set; }         // 1..10 (���� �ܰ� ��ȣ)
    public int Score { get; set; }          // ����
    public decimal? CorrectRate { get; set; } // �������
    public int? DurationSec { get; set; } // ����Ǯ�̿� �ɸ� �ð�
    public string MetaJson { get; set; }   // ���׺� �α� JSON
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public class Problem
{
    public string Id { get; set; }
    public string OwnerEmail { get; set; }
    public string Theme { get; set; }     // Gardener/Director ��
    public string Title { get; set; }
    public string Content { get; set; }
    public System.DateTime CreatedAt { get; set; }
}
public class SessionRecord
{
    public string Id { get; set; }
    public string UserEmail { get; set; }
    public string Theme { get; set; }
    public string CurrentStep { get; set; }     // enum ����ȭ or string
    public System.DateTime CreatedAt { get; set; }
}
public class Attempt
{
    public string Id { get; set; }
    public string SessionId { get; set; }
    public string UserEmail { get; set; }
    public string Content { get; set; }         // �ؽ�Ʈ/���� ��� ��
    public System.DateTime CreatedAt { get; set; }
}
public class Feedback
{
    public string Id { get; set; }
    public string ResultId { get; set; }
    public string AdminEmail { get; set; }
    public string Comment { get; set; }
    public float? Score { get; set; }
    public System.DateTime CreatedAt { get; set; }
}
public class UserProgress
{
    public string UserEmail { get; set; }
    public int TotalSessions { get; set; }
    public int TotalSolved { get; set; }
    public System.DateTime? LastSessionAt { get; set; }
}

public class UserSummary
{
    public string Email { get; set; }
    public string Name { get; set; }
    public UserRole Role { get; set; }
    public bool IsActive { get; set; }
}