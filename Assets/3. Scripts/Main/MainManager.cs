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

    public GameObject statt;
    public Transform stattworld;
    public Division nowchangediv;
    public float nowchangedhogamdo;

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

        if (allhogamdo <= 0)
        {
            allhogamdo = 1;
        }

        MoneyManager.Money.SetHogamdo(allhogamdo);
    }

    public void SetDivision()
    {
        if (alldivisioncount == divisons.Length)
        {
            return;
        }

        for (int i = 0; i < alldivisioncount; i++)
        {
            if (!divisons[i].GetComponent<DivisionControl>().enabled)
            {
                seoultime = false;
                divisons[i].GetComponent<DivisionControl>().enabled = true;
                StartCoroutine(AlamManager.Alam.AlamText("새로운 구 해금"));

                GameObject s = Instantiate(statt, stattworld);
                s.transform.Find("name").GetComponent<TMP_Text>().text = divisons[i].GetComponent<DivisionControl>().my.me.name;
            }

            if (i < alldivisioncount && seoultime)
            {
                seoul.enabled = true;
            }
        }
    }

    public IEnumerator PlusHogamdo(Division div, float plusvalue, float framespeed)
    {
        nowchangediv = div;
        nowchangedhogamdo = plusvalue;
        float muchplus = 0;

        while (muchplus < plusvalue)
        {
            div.hogamdo += plusvalue / framespeed;
            muchplus++;
            yield return null;
        }
        nowchangediv = null;
    }

    public void EndGame()
    {
        SaveManager.save.SaveData();


#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
