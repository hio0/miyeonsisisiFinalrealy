using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Event : ScriptableObject
{
    public Sprite bg;

    public string eventname;

    [TextArea]
    public string eventlog;

    [TextArea]
    public string correctlog;

    [TextArea]
    public string faillog;

    public float succsecs;

    public int usemoney;

    public bool ismust;

    public float plushogamdo;
}
