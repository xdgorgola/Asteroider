using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "I_", menuName = "Inv_Object")]
public class Item : ScriptableObject
{
    public string iName = "Hay algo";
    public string iDescription;

    public Sprite iImage = null;
}
