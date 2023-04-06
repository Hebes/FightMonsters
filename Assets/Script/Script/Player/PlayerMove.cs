using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : PlayeBase, IMove
{
    public float playerMoveSpeed = 5f;
    public Vector3 velocityVector;

    public override void OnUpdate()
    {
        base.OnUpdate();

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
        playerComponent.T_Playr_BodyAnimator.Play(isIdle ? "knightIdle" : "knightRun");
        GetComponent<IMove>().SetMove(moveVector);
    }

    public override void OnFixedUpdate()
    {
        base.OnFixedUpdate();
        playerComponent.player_Rigidbody2D.velocity = velocityVector * playerMoveSpeed;//�����˶�
    }

    public void SetMove(Vector3 velocityVector)
    {
        this.velocityVector = velocityVector;
    }
}
