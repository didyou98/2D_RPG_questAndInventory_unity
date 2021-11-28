using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    GameObject scanObject;
    Vector3 dirVec; // for Ray
    public GameManager manager;

    float h, v;
    public int playerSpeed, playerRunSpeed;
    public bool isRun;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 대화 중이라면 움직일 수 없다.
        h = manager.isAction ? 0 : Input.GetAxisRaw("Horizontal");
        v = manager.isAction ? 0 :  Input.GetAxisRaw("Vertical");
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRun = true;
        }
        else
        {
            isRun = false;
        }
        if (anim.GetInteger("vertical") != v)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("vertical", (int)v);
        }
        else if (anim.GetInteger("horizontal") != h)
        {
            anim.SetBool("isChange", true);
            anim.SetInteger("horizontal", (int)h);
        }
        else
        {
            anim.SetBool("isChange", false);
        }
        if (v == 1)
        {
            dirVec = Vector2.up;
        }
        else if (v == -1)
        {
            dirVec = Vector2.down;
        } else if (h == 1)
        {
            dirVec = Vector2.right;
        } else if (h == -1)
        {
            dirVec = Vector2.left;
        }
        // Scan Object
        if (Input.GetButtonDown("Jump") && scanObject != null)
        {
            manager.Action(scanObject);
        }
    }
    void FixedUpdate()
    {
        // Player Move
        Vector2 playerMove = isRun ? new Vector2(h, v) * playerSpeed * playerRunSpeed  : new Vector2(h, v) * playerSpeed  ;
        rigid.velocity = playerMove;
        Debug.DrawRay(rigid.position, dirVec * 0.7f, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, dirVec, 0.7f, LayerMask.GetMask("object"));
        if (rayHit.collider != null)
        {
            scanObject = rayHit.collider.gameObject;
        } else
        {
            scanObject = null;
        }
    }
}
