using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = " Colordata", menuName ="ScriptableObject/ColorData", order=1)]
public class ColorData : ScriptableObject
{
    [SerializeField] Material[] colorMat;
    public Material GetColorMat(ColorType colortype)
    {
       
        return colorMat[(int)colortype];
    }
}
