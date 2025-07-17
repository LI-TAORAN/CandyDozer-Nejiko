using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    // Start is called before the first frame update

    //1.プレイヤーのキー入力を受け取る
    //2.キー入力の方向に移動する
    //3.移動方向に合わせてアニメーションを再生する

    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    public float speed = 0f;
    Animator animator;

    //ジャンプの高さを決める変数
    public float jumppower = 0f;

    //重力の強さを決める変数
    public float gravitypower = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller.isGrounded)
        {
            //ねじこがジャンプする処理
            if(Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jumppower;
            }
        }

        if(Input.GetAxis("Vertical") > 0.0f)
        {
            //ねじこが前進する処理
            moveDirection.z = Input.GetAxis("Vertical") * speed;
        }
        else
        {
            moveDirection.z = 0.0f;
        }
        //Horizontal（左右入力）があれば、ねじこを回転させる
        transform.Rotate(0, Input.GetAxis("Horizontal") * 3f, 0);

        //ねじこが重力で落下する処理
        moveDirection.y = moveDirection.y - gravitypower * Time.deltaTime;
        
        //移動量をTransformに変換する
        Vector3 globalDirection = transform.TransformDirection(moveDirection);

        //Controllerに移動量を渡す
        controller.Move(globalDirection * Time.deltaTime);

        //ねじこのアニメーションを最新する
        animator.SetBool("run", moveDirection.z > 0f);
    }
}
