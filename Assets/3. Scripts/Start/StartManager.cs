using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    [SerializeField] float fadeintime;

    [SerializeField] CanvasGroup fadeP;

    bool isclick;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isclick)
        {
            Action action = () => SceneManager.LoadScene("Main");
            StartCoroutine(UIMovement.UIMove.FadeIn(fadeP, 1.5f, action));
            isclick = true;
        }
    }
}
