using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager Event;

    public Event[] events;
    int nowevent;
    Event selectevent;
    public DivisionControl divi;

    public Image bg;
    public RectTransform content;
    Division div;
    public GameObject eventpopup;
    public TMP_Text eventT;
    public TMP_Text eventnameT;
    public TMP_Text eventimer;

    public GameObject buttons;

    private void Awake()
    {
        if(Event == null)
        {
            Event = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNewEvent(RectTransform gb)
    {
        if (gb.gameObject.GetComponent<DivisionControl>().myevent == null)
        {
            int r = Random.Range(0, 2);
            float p = 0;
            if (r == 0)
            {
                p = Random.Range(250f, 250f);
            }
            else
            {
                p = Random.Range(-250f, -200f);
            }

            float x = Mathf.Clamp(gb.anchoredPosition.x + p, -250f, 250f);
            float y = Mathf.Clamp(gb.anchoredPosition.y + p, -150f, 150f);
            gb.gameObject.GetComponent<DivisionControl>().uipos = new Vector2(x, y);


            bool isok = false;
            while (!isok)
            {
                nowevent = Random.Range(0, events.Length);
                isok = true;
            }
            

            div = gb.gameObject.GetComponent<DivisionControl>().my;
            gb.gameObject.GetComponent<DivisionControl>().myevent = events[nowevent];
        }
        else
        {
            return;
        }
    }

    public void EventAlim(DivisionControl divi)
    {
        StopAllCoroutines();
        this.divi = divi;
        eventpopup.SetActive(true);
        eventpopup.transform.parent.gameObject.SetActive(true);
        buttons.SetActive(true);
        eventpopup.GetComponent<RectTransform>().localPosition = divi.uipos;

        selectevent = divi.myevent;
        eventnameT.text = selectevent.eventname;
        bg.sprite = selectevent.bg;

        string log = null;
        if (selectevent.usemoney >= MoneyManager.Money.hogamdo * 2)
        {
            log += "\n거금을 지불해야 될 것 같은 느낌이 든다...";
        }
        if (selectevent.succsecs <= 60)
        {
            log += "\n이 부탁은 성공 난이도가 높을 것 같다...";
        }
        if (selectevent.plushogamdo > div.hogamdo)
        {
            log += "\n이 부탁은 호감도에 영향이 많이 갈 것 같다...";
        }
        if (selectevent.ismust)
        {
            log += "\n이 부탁은 중요한 부탁인 것 같다...";
        }

        eventT.text = selectevent.eventlog + "\n" + log;
    }

    public void Accpet()
    {
        if(MoneyManager.Money.money < selectevent.usemoney)
        {
            eventT.text += "\n돈이 부족한 것 같다...";
        }
        else
        {
            MoneyManager.Money.money -= selectevent.usemoney;

            int a = Random.Range(1, 101);

            if (a < selectevent.succsecs)
            {
                eventT.text += "\n" + selectevent.correctlog + $"<b><color=#FF407F>\n호감도가 {selectevent.plushogamdo.ToString("F1")}만큼 올랐다!</color></b>";
                div.hogamdo += selectevent.plushogamdo;
            }
            else
            {
                string log = null;
                float minus = 0;
                if (selectevent.ismust)
                {
                    minus = selectevent.plushogamdo / 2;

                    log = $"<b><color=#FF407F>\n호감도가 {minus.ToString("F1")}만큼 내려간 것 같다...</color></b>";
                    div.hogamdo -= minus;
                }

                eventT.text += "\n" + selectevent.faillog + log;
            }

            MainManager.main.GetAllHogamdo();
            StartCoroutine(Delete());
        }
    }

    public void Return()
    {
        eventT.text += "\n아무것도 하지 않기로 했다.";
        if (selectevent.ismust)
        {
            float minus = selectevent.plushogamdo / 4;

            eventT.text += $"<b><color=#FF407F>\n호감도가 {minus.ToString("F1")}만큼 내려간 것 같다...</color></b>";
            div.hogamdo -= minus;
            MainManager.main.GetAllHogamdo();
        }
        StartCoroutine(Delete());
    }

    IEnumerator Delete()
    {
        buttons.SetActive(false);
        divi.Clear();
        yield return new WaitForSeconds(3f);

        Hide();
        buttons.SetActive(true);
    }

    public void Hide()
    {
        eventpopup.SetActive(false);
        eventpopup.transform.parent.gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
