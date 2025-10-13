using System;
using System.Linq;
using LiteDB;
using UnityEngine;
public interface IAdminDataService
{
    // ����� �˻�(�̸���/�̸� �Ϻ�)
    Result<UserSummary[]> SearchUsers(string query);

    // Ư�� ����� ��� ���
    Result<ResultDoc[]> FetchResultsByUser(string userEmail);

    // �ǵ�� ����(����� �ڸ�Ʈ/���� ��)
    Result SubmitFeedback(string resultId, Feedback feedback);
}

public class LocalAdminDataService : IAdminDataService
{
    const string CUsers = "users";    
    const string CResults = "results";
    const string CFeedback = "feedbacks";

    public Result<UserSummary[]> SearchUsers(string query)
    {
        try
        {
            var arr = DBHelper.With(db =>
            {
                var users = db.GetCollection<User>(CUsers);
                users.EnsureIndex(x => x.Email, true);
                var q = (query ?? "").Trim().ToLower();

                return users.Find(u =>
                            (u.Email ?? "").ToLower().Contains(q) ||
                            (u.Name ?? "").ToLower().Contains(q))
                        .Select(u => new UserSummary
                        {
                            Email = u.Email,
                            Name = u.Name,
                            Role = u.Role,
                            IsActive = u.IsActive
                        })
                        .ToArray();
            });
            return Result<UserSummary[]>.Success(arr);
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalAdminData] SearchUsers: {e}");
            return Result<UserSummary[]>.Fail(AuthError.Internal);
        }
    }

    public Result<ResultDoc[]> FetchResultsByUser(string userEmail)
    {
        try
        {
            var arr = DBHelper.With(db =>
                db.GetCollection<ResultDoc>(CResults).Find(r => r.UserId == userEmail).ToArray()
            );
            return Result<ResultDoc[]>.Success(arr);
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalAdminData] FetchResultsByUser: {e}");
            return Result<ResultDoc[]>.Fail(AuthError.Internal);
        }
    }

    public Result SubmitFeedback(string resultId, Feedback feedback)
    {
        try
        {
            feedback.Id ??= ObjectId.NewObjectId().ToString();
            feedback.ResultId = resultId;
            feedback.CreatedAt = DateTime.UtcNow;

            DBHelper.With(db =>
            {
                var col = db.GetCollection<Feedback>(CFeedback);
                col.EnsureIndex(x => x.Id, true);
                col.EnsureIndex(x => x.ResultId);
                col.Insert(feedback);
            });
            return Result.Success();
        }
        catch (Exception e)
        {
            Debug.LogError($"[LocalAdminData] SubmitFeedback: {e}");
            return Result.Fail(AuthError.Internal);
        }
    }
}
