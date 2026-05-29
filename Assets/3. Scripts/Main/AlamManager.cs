using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AlamManager : MonoBehaviour
{
    public static AlamManager Alam;

    public GameObject alam;
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
        Debug.Log(alam.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator AlamText(string what)
    {
        alamT.text = what;

        StartCoroutine(UIMovement.UIMove.MoveAnimation(alam, new Vector3(-12, 3, alam.transform.position.z), 0.1f, null));
        yield return new WaitForSeconds(3f);
        StartCoroutine(UIMovement.UIMove.MoveAnimation(alam, new Vector3(-20, 3, alam.transform.position.z), 0.1f, null));
    }
}
