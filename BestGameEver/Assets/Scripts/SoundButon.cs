using UnityEngine.UI;
using UnityEngine;

public class SoundButon : MonoBehaviour
{
    public Text text;
    public string s1;
    public string s2;
    private bool b;

    public void ChangeText()
    {
        b = !b;
        if (b)
            text.text = s1;
        else
            text.text = s2;
    }
}
