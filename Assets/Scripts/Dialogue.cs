using UnityEngine;
[System.Serializable]
public class Dialogue
{
    public string name;
    [TextArea(3,20)]
    public string sentence;
    public Sprite profile;
}
