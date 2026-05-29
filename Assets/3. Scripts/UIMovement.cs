using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMovement : MonoBehaviour
{
    public static UIMovement UIMove;

    private void Awake()
    {
        if (UIMove == null)
        {
            UIMove = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator FadeIn(CanvasGroup what, float fadeTime, Action action)
    {
        float time = 0f;
        what.gameObject.SetActive(true);
        what.alpha = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            what.alpha = Mathf.Lerp(0f, 1f, time / fadeTime);
            yield return null;
        }

        what.alpha = 1f;

        yield return new WaitForSeconds(0.5f);
        action?.Invoke();
    }

    public IEnumerator FadeOut(CanvasGroup what, float fadeTime, Action action)
    {
        float time = 0f;
        what.gameObject.SetActive(true);
        what.alpha = 1f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            what.alpha = Mathf.Lerp(1f, 0f, time / fadeTime);
            yield return null;
        }

        what.alpha = 0f;
        what.gameObject.SetActive(false);

        yield return new WaitForSeconds(0.5f);
        action?.Invoke();
    }

    public IEnumerator MoveAnimation(GameObject what, Vector3 target, float speed, Action action)
    {
        while (what.transform.position != target)
        {
            what.transform.position = Vector3.MoveTowards(what.transform.position, target, speed);
            yield return null;
        }

        action?.Invoke();
    }
}