using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setting : UICanvas
{ 
    public void ContinueButton()
    {
        Close(0.5f);
    }
    public void RetryButton()
    {
        LevelManager.Instance.OnRetry();
        Close(0.5f);
    }
}
