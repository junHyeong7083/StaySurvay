using System;

public class Model
{}

public enum UserRole {  USER=0, ADMIN = 1, SUPERADMIN = 2}
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
