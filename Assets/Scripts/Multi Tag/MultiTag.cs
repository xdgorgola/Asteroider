using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiTag : MonoBehaviour
{
    /// <summary> GameObject tag </summary>
    [SerializeField]
    private string[] tags;

    private void Awake()
    {
        if (tags.Length == 0)
        {
            tags = new string[1] { "Default" };
        }
    }

    /// <summary> Looks if the object has certain tag </summary>
    /// <param name="tag"> Tag to search for </param>
    /// <returns> If the object has the tag </returns>
    public bool HasTag(string tag)
    {
        for (int i = 0; i < tags.Length; i++)
        {
            if (tags[i] == tag) return true;
        }
        return false;
    }
}
