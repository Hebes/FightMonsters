using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour, IMove
{
    public float playerMoveSpeed = 1;
    public Vector3 velocityVector;

    #region 组件属性
    public Transform T_Gun_ShootPointTransform { set; get; }
    public Rigidbody2D player_Rigidbody2D { get; private set; }
    public BoxCollider2D player_BoxCollider2D { get; private set; }

    public void SetMove(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }
    #endregion

    private void Awake()
    {
        OnGetComponent();
    }

    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(Config.Key_UP)) moveY = +1f;
        if (Input.GetKey(Config.Key_Down)) moveY = -1f;
        if (Input.GetKey(Config.Key_Left)) moveX = -1f;
        if (Input.GetKey(Config.Key_Right)) moveX = +1f;

        //float moveX = Input.GetAxis("Horizontal");
        //float moveY = Input.GetAxis("Vertical");

        Vector3 moveVector = new Vector3(moveX, moveY).normalized;

        bool isIdle = moveX == 0 && moveY == 0;

        if (isIdle)
        {
            //player_Components.Player_Animator.SetBool("IsMoving", false);
            //player_Components.Player_MiniMap_Animator.SetBool("IsMoving", false);
        }
        else
        {
            //player_Components.Player_Animator.SetBool("IsMoving", true);
            //player_Components.Player_MiniMap_Animator.SetBool("IsMoving", true);
        }

        GetComponent<IMove>().SetMove(moveVector);
    }

    private void OnGetComponent()
    {
        T_Gun_ShootPointTransform = transform.Find("Player_Gun/T_Gun_ShootPoint");
        player_Rigidbody2D = GetComponent<Rigidbody2D>();
        player_BoxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        player_Rigidbody2D.velocity = velocityVector * playerMoveSpeed;//刚体运动
    }
}
