using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class Statt : MonoBehaviour
{
    public Division mydiv;

    public TMP_Text myname;
    public TMP_Text hogamdocount;
    public Image hogamdogauge;
    public Image icon;
    public GameObject malpungsun;
    public TMP_Text malpungT;
    public float malpungtimer;
    bool ismalpungon;

    public TMP_Text plusT;

    // Start is called before the first frame update
    void Start()
    {
        myname.text = mydiv.me.name;
        icon.sprite = mydiv.me.icon;

        plusT.gameObject.SetActive(false);
        malpungsun.SetActive(false);

        RssetTimer();
    }

    
    void Update()
    {
        if (MainManager.main.nextepi.Length > mydiv.episodeCount)
        {
            hogamdogauge.fillAmount = mydiv.hogamdo / MainManager.main.nextepi[mydiv.episodeCount];
            hogamdocount.text = $"{mydiv.hogamdo}<size=30>/{MainManager.main.nextepi[mydiv.episodeCount]}</size>";
        }    
        else
        {
            hogamdogauge.fillAmount = 1;
            myname.text = $"<b><color=#FF407F>{mydiv.me.name}</color></b>";
            hogamdocount.text = $"{mydiv.hogamdo}<size=30>/MAX</size>";
        }

        if (MainManager.main.nowchangediv == mydiv && !plusT.gameObject.activeSelf) // 그냥 OnHogamdoPlused 이벤트 만들어놓고 거기에 구독시켜 놓으면 됨. 굳이 이렇게 mydiv가 nowchangeddiv야!! 라고 감지 할 필요 없이.
        {
            StartCoroutine(ChangeHogamdo(MainManager.main.nowchangedhogamdo, MainManager.main.nowhogamdoplused));
        }

        malpungtimer -= Time.deltaTime;
        if (malpungtimer < 0 && !ismalpungon)
        {
            StartCoroutine(SmallTalk());
            malpungtimer = 0;
        }
    }

    void RssetTimer()
    {
        malpungtimer = Random.Range(15f, 50f);
        ismalpungon = false;
    }

    IEnumerator SmallTalk()
    {
        malpungsun.SetActive(true);
        ismalpungon = true;

        int a = Random.Range(0, 11);
        string t = null;

        if(a >= 6)
        {
            t = MainManager.main.malpungsuntalks[Random.Range(0, MainManager.main.malpungsuntalks.Count - 1)];
        }
        else
        {
            t = mydiv.smalltalks[Random.Range(0, mydiv.smalltalks.Length - 1)];
        }

        malpungT.text = null;
        foreach(char text in t.ToCharArray())
        {
            malpungT.text += text;
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(3f);

        malpungsun.SetActive(false);
        RssetTimer();
    }

    public IEnumerator ChangeHogamdo(float cvalue, bool isplus)
    {
        plusT.gameObject.SetActive(true);

        if(isplus)
        {
            plusT.text = $"+{cvalue}";
        }
        else
        {
            plusT.text = $"-{cvalue}";
        }

        yield return new WaitForSeconds(0.5f);

        UnityEngine.Color color = plusT.color;
        float startAlpha = color.a;
        float time = 0;
        float muchtime = 1.5f;

        while (time < muchtime)
        {
            plusT.gameObject.transform.Translate(Vector2.up * 1 * Time.deltaTime);

            time += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, 0f, time / muchtime);
            plusT.color = new UnityEngine.Color(color.r, color.g, color.b, alpha);

            yield return null;
        }
        plusT.GetComponent<RectTransform>().localPosition = new Vector2(-6.6f, 103.9f);
        plusT.color = new UnityEngine.Color(color.r, color.g, color.b, 255);
        plusT.gameObject.SetActive(false);
        MainManager.main.nowchangediv = null;
    }
}
