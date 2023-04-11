using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerComponent : PlayeBase
{
    public Rigidbody2D player_Rigidbody2D { get; private set; }
    public BoxCollider2D player_BoxCollider2D { get; private set; }

    public Animator T_Playr_BodyAnimator { get; set; }

    public Transform T_Playr_BodyTransform { get; set; }
    public Transform T_GunTransform { get; set; }
    public Transform T_Gun_ShootPointTransform { get; set; }
    public Transform T_GunSpriteTransform { get; set; }
    public IDamage idamage { get; set; }

    public void Awake()
    {
        this.Log("进入了");

        T_Playr_BodyAnimator = transform.Find("T_Playr_Body").GetComponent<Animator>();

        player_Rigidbody2D = GetComponent<Rigidbody2D>();
        player_BoxCollider2D = GetComponent<BoxCollider2D>();

        T_Playr_BodyTransform = transform.Find("T_Playr_Body").GetComponent<Transform>();
        T_GunTransform = transform.Find("T_Gun").GetComponent<Transform>();
        T_Gun_ShootPointTransform = transform.Find("T_Gun/T_Gun_ShootPoint").GetComponent<Transform>();
        T_GunSpriteTransform = transform.Find("T_Gun/T_GunSprite").GetComponent<Transform>();
    }

    private void OnEnable()
    {
        //玩家受到伤害
        idamage = new PlayerDamage();
        //moduleManager.eventModule.AddEventListener<GameObject>(EventType.playerTakeDamage, idamage.TakeDamage);
    }

    private void OnDisable()
    {
        //moduleManager.eventModule.RemoveEventListener<GameObject>(EventType.playerTakeDamage, idamage.TakeDamage);
    }
}
