using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;


public class EpisodeManager : MonoBehaviour
{
    public static EpisodeManager Episode;
    public bool talking;

    Division targetdivision;
    Episode episode;
    public enum feels
    {
        normal,
        happy,
        sad,
        angry,
        surprised,
        shy,
        tired,
        menhera
    }
    Dictionary<feels, Sprite> feeling = new Dictionary<feels, Sprite>();

    int talktime;
    int oneselecttime;
    int selecttime;
    bool isnormaltalk;
    string log;
    Select sb;

    [SerializeField] GameObject talkP;
    [SerializeField] Image charimage;
    [SerializeField] TMP_Text nameT;
    [SerializeField] TMP_Text mainT;

    [SerializeField] Transform selects;
    [SerializeField] GameObject select;

    [SerializeField] TMP_FontAsset normalF;
    [SerializeField] TMP_FontAsset menheraF;

    float plushogamdo;

    private void Awake()
    {
        if (Episode == null)
        {
            Episode = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        talkP.SetActive(false);
        talking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            if (talkP.activeSelf)
            {
                SetNextTalk();
            }
        }
    }

    public void StartTalk(Division div)
    {
        talking = true;

        targetdivision = div;
        episode = targetdivision.episodes[targetdivision.episodeCount];

        talktime = 0;
        selecttime = 0;
        isnormaltalk = true;
        sb = null;
        plushogamdo = 0;
        log = null;

        if (targetdivision.me.charactorImages.Length != 0)
        {
            foreach (Sprite sp in targetdivision.me.charactorImages)
            {
                int start = sp.name.IndexOf('_');
                string a = sp.name.Substring(start + 1);

                feels feel;
                if (Enum.TryParse(a, out feel))
                {
                    feeling[feel] = sp;
                }
            }
        }

        talkP.SetActive(true);
        selects.gameObject.SetActive(true);
        SetFeels(feels.normal);
        SetNextTalk();

        talkP.GetComponent<RectTransform>().anchoredPosition = new Vector3(2000, 0, 0);
        StartCoroutine(UIMovement.UIMove.MoveAnimation(talkP, new Vector3(0, 0, 0), 1.5f, null));
    }

    void SetNextTalk()
    {
        StopAllCoroutines();
        log = null;

        if (talktime >= episode.texts.Length)
        {
            EpisodeEnd();
        }
        else
        {
            Action action = null;
            
            SetFonts(normalF);
            charimage.gameObject.SetActive(true);

            if (isnormaltalk)
            {
                log = episode.texts[talktime];

                if (log.Contains("*"))
                {
                    action = SetSelect;

                    log = log.Replace("*", "");
                }
                else
                {
                    action = OneTalkEnd;
                }

                Talking(action);
            }
            else
            {
                if (sb != null)
                {
                    if (oneselecttime >= sb.selectedlog.Length)
                    {
                        OneTalkEnd();
                        SetNextTalk();
                    }
                    else
                    {
                        if (oneselecttime == 0)
                        {
                            if (sb.iscorrect)
                            {
                                Correct();
                            }
                            else
                            {
                                Miss();
                            }
                        }

                        log = sb.selectedlog[oneselecttime];

                        action = () => oneselecttime++;
                        Talking(action);
                    }
                }
            }
        }
    }

    void SetFeels(feels what)
    {
        charimage.sprite = feeling[what];
    }

    void SetFonts(TMP_FontAsset fontA)
    {
        mainT.font = fontA;
    }

    void SetSelect()
    {
        List<Select> nowselect = new List<Select>();

        for (int i = 0; i < episode.selects.Length; i++)
        {
            if (episode.selects[i].th == selecttime)
            {
                nowselect.Add(episode.selects[i]);
            }
        }

        for (int i = 0; i < nowselect.Count; i++)
        {
            GameObject pre = Instantiate(select, selects);

            pre.transform.SetSiblingIndex(0);
            pre.GetComponent<SelectButton>().myselect = nowselect[i];
            pre.transform.GetComponentInChildren<TMP_Text>().text = nowselect[i].selectname;
            pre.GetComponent<Button>().onClick.AddListener(() => SetSelectTalk(pre));
        }

        isnormaltalk = false;
        selecttime++;
        oneselecttime = 0;
    }

    void SetSelectTalk(GameObject you)
    {
        sb = you.GetComponent<SelectButton>().myselect;
        you.transform.parent.gameObject.SetActive(false);
        SetNextTalk();
    }

    void OneTalkEnd()
    {
        isnormaltalk = true;
        talktime++;
    }

    void Talking(Action action)
    {
        bool isme = false;

        if (log.Contains("#"))
        {
            isme = true;
            log = log.Replace("#", "");
        }

        if (log.Contains("[") && log.Contains("]"))
        {
            int start = log.IndexOf('[');
            int end = log.IndexOf(']');

            string result = log.Substring(start + 1, end - start - 1);

            SetFeels((feels)Enum.Parse(typeof(feels), result));

            string deleteresult = log.Substring(start, end - start + 1);
            log = log.Replace(deleteresult, "");
        }
        else
        {
            if (log.Contains("`"))
            {
                charimage.gameObject.SetActive(false);
                log = log.Replace("`", "");
            }
        }

        if (log.Contains("%"))
        {
            SetFonts(menheraF);

            log = log.Replace("%", "");
        }

        if (isme)
        {
            charimage.color = new Color32(146, 146, 146, 255);
            nameT.text = "나";
        }
        else
        {
            charimage.color = new Color32(255, 255, 255, 255);
            nameT.text = targetdivision.me.name;
        }

        action?.Invoke();
        StartCoroutine(TypeText(log));
    }

    public IEnumerator TypeText(string text)
    {
        mainT.text = ""; // 텍스트 초기화

        foreach (char letter in text.ToCharArray())
        {
            mainT.text += letter; // 한 글자씩 추가
            yield return new WaitForSeconds(0.05f); // 글자 사이에 딜레이
        }
    }

    void Correct()
    {
        plushogamdo += targetdivision.hogamdo / 3;
    }

    void Miss()
    {
        plushogamdo += targetdivision.hogamdo / 5;
    }

    void EpisodeEnd()
    {
        float plus = plushogamdo * 2;
        targetdivision.hogamdo += plus;
        MainManager.main.GetAllHogamdo();

        if(targetdivision.episodeCount < targetdivision.episodes.Length)
        {
            targetdivision.episodeCount++;
        }
        StartCoroutine(UIMovement.UIMove.MoveAnimation(talkP, new Vector3(-25, 0, 0), 0.5f, EndTalk));
    }

    public void EndTalk()
    {
        talkP.SetActive(false);
        talking = true;

        if (targetdivision.episodeCount == 2)
        {
            MainManager.main.alldivisioncount++;
            MainManager.main.SetDivision();
        }
    }
}
