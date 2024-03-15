using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SceneManagement;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private float speed = 5f;
   
  

    void Update()
    {
        if (GameManager.Instance.IsState(GameState.GamePlay))
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 nextPoint = JoystickControl.direct * speed * Time.deltaTime + TF.position;
                if (CanMove(nextPoint))
                {
                    TF.position = CheckGround(nextPoint);
                }
                if (JoystickControl.direct != Vector3.zero)
                {
                    skin.forward = JoystickControl.direct;
                }
                ChangeAnim(Constans.ANIM_RUN);
            }
            if(Input.GetMouseButtonUp(0)) 
            {
                ChangeAnim(Constans.ANIM_IDLE);
            }
        }
    }
   

   
}