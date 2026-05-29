using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public static MainManager main;

    public float[] nextepi;
    public GameObject[] divisons;
    public int alldivisioncount;

    public float allhogamdo;
    bool seoultime;
    public Button seoul;

    public CanvasGroup fadeP;

    private void Awake()
    {
        if (main == null)
        {
            main = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GetAllHogamdo();
        SetDivision();
        StartCoroutine(UIMovement.UIMove.FadeOut(fadeP, 1.5f, null));
    }

    // Update is called once per frame
    void Update()
    {
         
    }

    public void GetAllHogamdo()
    {
        allhogamdo = 0;

        foreach (GameObject ob in divisons)
        {
            Division div = ob.GetComponent<DivisionControl>().my;
            allhogamdo += div.hogamdo;
        }

        if(allhogamdo <= 0)
        {
            allhogamdo = 1;
        }

        MoneyManager.Money.SetHogamdo(allhogamdo);
    }

    public void SetDivision()
    {
        if(alldivisioncount == divisons.Length)
        {
            return;
        }

        for(int i = 0;i < alldivisioncount; i++)
        {
            if(!divisons[i].GetComponent<DivisionControl>().enabled)
            {
                seoultime = false;
                divisons[i].GetComponent<DivisionControl>().enabled = true;
                StartCoroutine(AlamManager.Alam.AlamText("새로운 구 해금"));
            }

            if(i < alldivisioncount && seoultime)
            {
                seoul.enabled = true;
            }
        }
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
