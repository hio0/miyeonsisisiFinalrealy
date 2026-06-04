using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Content : MonoBehaviour
{
    [SerializeField] TMP_Text targetT;
    int lastlengh;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(lastlengh != targetT.textInfo.lineCount)
        {
            lastlengh = targetT.textInfo.lineCount;
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(gameObject.GetComponent<RectTransform>().sizeDelta.x, targetT.textInfo.lineCount * 90);
        }
    }
}
