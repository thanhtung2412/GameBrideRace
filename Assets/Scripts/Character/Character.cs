using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : ColorObject
{
    [SerializeField] private LayerMask stairLayer;
    [SerializeField] private LayerMask groundLayer;
    public List<PlayerBrick> playerBricks = new List<PlayerBrick>();
    [SerializeField] private PlayerBrick playerBrickPrefab;
    [SerializeField] Transform brickHolder;
    [SerializeField] protected Transform skin;
    public Animator anim;
    private string currentAnim = "idle";
    [HideInInspector] public Stage stage;
    public int BrickCount => playerBricks.Count;
    public virtual void OnInit()
    {
        ClearBrick();
        skin.rotation = Quaternion.identity;
    }
    public Vector3 CheckGround(Vector3 nextPoint)
    {
        RaycastHit hit;
        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, groundLayer))
        {
            return hit.point + Vector3.up * 1.5f;
        }
        return TF.position;
    }
    public bool CanMove(Vector3 nextPoint)
    {
        bool isCanMove = true;
        RaycastHit hit;

        if (Physics.Raycast(nextPoint, Vector3.down, out hit, 2f, stairLayer))
        {
            Stair stair = Cache.GetStair(hit.collider);
            Debug.DrawRay(nextPoint, Vector3.down, Color.red, 2f);
            if (stair.colorType != colorType && playerBricks.Count > 0)
            {
                stair.ChangeColor(colorType);
                RemoveBrick();               
                stage.NewBrick(colorType);

            }
            if (stair.colorType != colorType && playerBricks.Count == 0 && skin.forward.z > 0)
            {
                isCanMove = false;
            }
        }
        return isCanMove;
    }

    public void AddBrick()
    {
        PlayerBrick playerBrick = Instantiate(playerBrickPrefab, brickHolder);
        playerBrick.ChangeColor(colorType);
        playerBrick.TF.localPosition = Vector3.up * 0.25f * playerBricks.Count;
        playerBricks.Add(playerBrick);
    }
    public void RemoveBrick()
    {
        if (playerBricks.Count > 0)
        {
            PlayerBrick playerBrick = playerBricks[playerBricks.Count - 1];
            playerBricks.RemoveAt(playerBricks.Count - 1);
            Destroy(playerBrick.gameObject);
        }
    }
    public void ClearBrick()
    {
        for (int i = 0; i < playerBricks.Count; i++)
        {            
            Destroy(playerBricks[i].gameObject);
        }
        playerBricks.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Constans.TAG_BRICK))
        {
            Brick brick = Cache.GetBrick(other);
            if (brick.colorType == colorType)
            {
                brick.OnDespawn();
                //Destroy(brick.gameObject);
                SimplePool.Despawn(brick);
                AddBrick();
            }
        }
    }
    public void ChangeAnim(string animName)
    {
        if(currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
}
