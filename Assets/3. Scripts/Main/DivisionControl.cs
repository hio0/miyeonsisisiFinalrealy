using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DivisionControl : MonoBehaviour
{
    public Division my;
    public Event myevent;

    public Image hogadogauge;
    public GameObject eventalim;
    [SerializeField] float eventimer;
    [SerializeField] float eventzuttotimer;
    [SerializeField] bool timersetting;
    bool zuttotimersetting;

    public TMP_Text hogamdoT;
    bool cannextepi;
    bool isalim;

    private void OnEnable()
    {
        Clear();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!EpisodeManager.Episode.talking)
        {
            if (!timersetting)
            {
                eventimer = Random.Range(MainManager.main.alldivisioncount * 3 + 5, MainManager.main.alldivisioncount * 20);
                timersetting = true;
            }
            else
            {
                eventimer -= Time.deltaTime;

                if (eventimer < 0)
                {
                    eventimer = 0;
                    EventManager.Event.SetNewEvent(gameObject.GetComponent<RectTransform>());
                    eventalim.SetActive(true);
                }
            }

            if (eventalim.activeSelf)
            {
                if (!zuttotimersetting)
                {
                    eventzuttotimer = Random.Range(MainManager.main.alldivisioncount * 5 + 20, MainManager.main.alldivisioncount * 8 + 20);
                    zuttotimersetting = true;
                }
                else
                {
                    eventzuttotimer -= Time.deltaTime;
                    if (EventManager.Event.divi == this)
                    {
                        EventManager.Event.eventimer.text = eventzuttotimer.ToString("F0");
                    }

                    if (eventzuttotimer < 0)
                    {
                        EventManager.Event.Return();

                        Clear();
                    }
                }
            }

            if (MainManager.main.nextepi.Length > my.episodeCount)
            {
                if (my.hogamdo >= MainManager.main.nextepi[my.episodeCount])
                {
                    cannextepi = true;
                    hogadogauge.fillAmount = 1f;

                    if (!isalim)
                    {
                        StartCoroutine(AlamManager.Alam.AlamText("새로운 에피소드 해금 가능"));
                        isalim = true;
                    }
                }
                else
                {
                    cannextepi = false;
                    hogadogauge.fillAmount = my.hogamdo / MainManager.main.nextepi[my.episodeCount];
                }
            }
            else
            {
                cannextepi = false;
                hogadogauge.fillAmount = 1f;
                hogamdoT.text = $"<b><color=#FF407F>{my.me.name}</color></b>";
            }

        }
        else
        {
            eventalim.SetActive(false);
            isalim = false;
            Clear();
        }

        if (my.hogamdo >= 3000)
        {
            my.hogamdo = 3000;
        }
        else if(my.hogamdo <= 0)
        {
            my.hogamdo = 1;
        }
    }

    public void GoToEpisode()
    {
        if (cannextepi)
        {
            EpisodeManager.Episode.StartTalk(my);
        }
        else
        {
            return;
        }
    }


    public void Clear()
    {
        eventzuttotimer = 0;
        myevent = null;
        eventalim.SetActive(false);

        timersetting = false;
        zuttotimersetting = false;
        hogamdoT.text = my.me.name;
        hogadogauge.fillAmount = 0f;
        cannextepi = false;
    }
}
