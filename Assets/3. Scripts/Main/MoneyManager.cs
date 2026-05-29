using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Money;

    public float money;
    public float hogamdo;
    [SerializeField] float moneytimer;

    public TMP_Text moneyT;
    public TMP_Text hogamdoT;

    private void Awake()
    {
        if (Money == null)
        {
            Money = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        moneytimer += Time.deltaTime;
        if (moneytimer >= 1f)
        {
            PlusMoney();
            moneytimer = 0f;
        }

        moneyT.text = $"돈 : <b>{money.ToString("F1")}</b>";
        hogamdoT.text = $"호감도: <b>{hogamdo.ToString("F1")}/s</b>";

        if (money < 0)
        {
            money = 0;
        }
        else if(hogamdo < 0)
        {
            hogamdo = 0;
        }

        if (money >= 100000)
        {
            money = 100000;
        }
        else if (hogamdo >= 50000)
        {
            hogamdo = 50000;
        }
    }

    void PlusMoney()
    {
        money += hogamdo;
    }

    public void SetHogamdo(float much)
    {
        hogamdo = much;
    }
}
