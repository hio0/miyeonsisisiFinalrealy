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
    float malpungtimer;

    public TMP_Text plusT;

    // Start is called before the first frame update
    void Start()
    {
        myname.text = mydiv.me.name;
        icon.sprite = mydiv.me.icon;
    }

    
    void Update()
    {
        hogamdogauge.fillAmount = mydiv.hogamdo / MainManager.main.nextepi[mydiv.episodeCount];

        if (MainManager.main.nextepi.Length > mydiv.episodeCount)
        {
            hogamdocount.text = $"{mydiv.hogamdo}<size=30>/{MainManager.main.nextepi[mydiv.episodeCount]}</size>";
        }    
        else
        {
            myname.text = $"<b><color=#FF407F>{mydiv.me.name}</color></b>";
            hogamdocount.text = $"{mydiv.hogamdo}<size=30>/MAX</size>";
        }

        if (MainManager.main.nowchangediv == mydiv && !plusT.gameObject.activeSelf) // 그냥 OnHogamdoPlused 이벤트 만들어놓고 거기에 구독시켜 놓으면 됨. 굳이 이렇게 mydiv가 nowchangeddiv야!! 라고 감지 할 필요 없이.
        {
            StartCoroutine(ChangeHogamdo(MainManager.main.nowchangedhogamdo));
        }
    }

    public void IHaveNoEyesSoGiveToMeThis(Division div)
    {
        mydiv = div;

        plusT.gameObject.SetActive(false);
    }

    void RssetTimer()
    {

    }

    public IEnumerator ChangeHogamdo(float plusvalue)
    {
        plusT.gameObject.SetActive(true);
        plusT.text = $"+{plusvalue}";

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
    }
}
