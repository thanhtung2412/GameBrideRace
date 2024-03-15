using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorObject : GameUnit
{
    public ColorType colorType;
    [SerializeField] private Renderer renderer;

    [SerializeField] private ColorData colorData;
      
    public void ChangeColor(ColorType colortype)
    {
        this.colorType = colortype;
        renderer.material = colorData.GetColorMat(colorType);
    }
    public override void OnIntit()
    {
        
    }
    public override void OnDespawn()
    {
    }


}
