﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public enum ScreenId { REGISTER, HOME, PROBLEM, RESULT }

public class SceneNavigator : MonoBehaviour
{
    [Header("Scene Names")]
    [SerializeField] string registerScene = "RegisterScene";
    [SerializeField] string homeScene = "HomeScene";
    [SerializeField] string problemScene = "ProblemScene";
    [SerializeField] string resultScene = "ResultScene";

    [Header("Optional Fade")]
    [SerializeField] CanvasGroup fade;   // 전환용 페이드 (없으면 비워도 됨)
    [SerializeField] float fadeSpeed = 7f;

    readonly Stack<ScreenId> history = new();
    ScreenId current;

    public void GoTo(ScreenId id) => StartCoroutine(CoGoTo(id));

    public void GoBack()
    {
        if (history.Count == 0) return;
        GoTo(history.Pop());
    }

    IEnumerator CoGoTo(ScreenId id)
    {
        if (!IsAllowed(id))
            id = ScreenId.REGISTER;

        yield return Fade(1f);

        var name = SceneNameOf(id);
        var op = SceneManager.LoadSceneAsync(name, LoadSceneMode.Single);
        while (!op.isDone) yield return null;

        history.Push(current);
        current = id;

        yield return Fade(0f);
    }

    bool IsAllowed(ScreenId target)
    {
        bool needAuth = (target == ScreenId.HOME || target == ScreenId.PROBLEM || target == ScreenId.RESULT);
        return !needAuth || (SessionManager.Instance != null && SessionManager.Instance.IsSignedIn);
    }

    string SceneNameOf(ScreenId id) => id switch
    {
        ScreenId.REGISTER => registerScene,
        ScreenId.HOME => homeScene,
        ScreenId.PROBLEM => problemScene,
        ScreenId.RESULT => resultScene,
        _ => registerScene
    };

    IEnumerator Fade(float target)
    {
        if (!fade) yield break;
        fade.gameObject.SetActive(true);
        while (!Mathf.Approximately(fade.alpha, target))
        {
            fade.alpha = Mathf.MoveTowards(fade.alpha, target, Time.unscaledDeltaTime * fadeSpeed);
            yield return null;
        }
        fade.blocksRaycasts = target > 0.01f;   // 전환 중 클릭 방지
        if (target == 0f) fade.gameObject.SetActive(false);
    }
}
