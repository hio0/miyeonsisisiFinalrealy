using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlamManager : MonoBehaviour
{
    public static AlamManager Alam;

    public RectTransform alam;
    public TMP_Text alamT;

    private void Awake()
    {
        if(Alam == null)
        {
            Alam = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        alam.anchoredPosition = new Vector2(120, alam.anchoredPosition.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StopAlam()
    {
        //Coroutine cor = AlamText;
    }

    public IEnumerator AlamText(string what)
    {
        alamT.text = what;
        float pos = alamT.text.Length * 35;

        StartCoroutine(UIMovement.UIMove.MoveAnimation(alam, new Vector2(-pos, alam.anchoredPosition.y), 5f, null));
        yield return new WaitForSeconds(3f);
        StartCoroutine(UIMovement.UIMove.MoveAnimation(alam, new Vector2(120, alam.anchoredPosition.y), 5f, null));
    }
}
