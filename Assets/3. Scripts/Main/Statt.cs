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

    public TMP_Text plusT;

    // Start is called before the first frame update
    void Start()
    {
        myname.text = mydiv.me.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainManager.main.nextepi.Length > mydiv.episodeCount)
        {
            hogamdocount.text = $"{mydiv.hogamdo}<size=30>/{MainManager.main.nextepi[mydiv.episodeCount]}</size>";
        }    
        else
        {
            myname.text = $"<b><color=#FF407F>{mydiv.me.name}</color></b>";
            hogamdocount.text = $"{mydiv.hogamdo}<size=30>/MAX</size>";
        }

        if(MainManager.main.nowchangediv == mydiv && !plusT.gameObject.activeSelf) // 그냥 OnHogamdoPlused 이벤트 만들어놓고 거기에 구독시켜 놓으면 됨. 굳이 이렇게 mydiv가 nowchangeddiv야!! 라고 감지 할 필요 없이.
        {
            StartCoroutine(ChangeHogamdo(MainManager.main.nowchangedhogamdo));
        }
    }

    void IHaveNoEyesSoGiveToMeThis(Division div,TMP_Text name, TMP_Text hogamT, Image hogamG, TMP_Text plT)
    {
        mydiv = div;

        myname = name;
        hogamdocount = hogamT;
        hogamdogauge = hogamG;
        plusT = plT;
    }

    public IEnumerator ChangeHogamdo(float plusvalue)
    {
        Instantiate(plusT.gameObject, new Vector2(gameObject.transform.position.x - 25, gameObject.transform.position.y), gameObject.transform.rotation);
        plusT.text = $"+{plusvalue}";

        plusT.gameObject.transform.Translate(Vector2.up * 3 * Time.deltaTime);

        yield return new WaitForSeconds(0.5f);

        UnityEngine.Color color = plusT.color;
        float startAlpha = color.a;
        float time = 0;

        while (time > 10)
        {
            time += Time.deltaTime;

            float alpha = Mathf.Lerp(startAlpha, 0f, time / 10);
            plusT.color = new UnityEngine.Color(color.r, color.g, color.b, alpha);

            yield return null;
        }
        Destroy(plusT.gameObject);
    }
}
