using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTag : MonoBehaviour
{
    [SerializeField]
    private string[] tags = new string[1] { "Default" };

    private void Awake()
    {
        if (tags.Length == 0)
        {
            tags = new string[1] { "Default" };
        }
    }

    public bool HasTag(string tag)
    {
        for (int i = 0; i < tag.Length; i++)
        {
            if (tags[i] == tag) return true;
        }
        return false;
    }
}
