using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    // Start is called before the first frame update

    //1.�v���C���[�̃L�[���͂��󂯎��
    //2.�L�[���͂̕����Ɉړ�����
    //3.�ړ������ɍ��킹�ăA�j���[�V�������Đ�����

    CharacterController controller;
    Vector3 moveDirection = Vector3.zero;
    public float speed = 0f;
    Animator animator;

    //�W�����v�̍��������߂�ϐ�
    public float jumppower = 0f;

    //�d�͂̋��������߂�ϐ�
    public float gravitypower = 0;

    //���C���̐��̍ő�l
    int MaxLine = 2;
    //���C���̐��̍ŏ��l
    int MinLine = -2;
    //���C���Ԃ̋���
    float LineWidth = 1.0f;
    //�ړ���̃��C��
    int targetLine = 0;

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
            //�˂������W�����v���鏈��
            if(Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jumppower;
            }
        }

        //1�t���[�����ɑO�i���鋗���̍X�V
        float movePowerZ = moveDirection.z + (speed * Time.deltaTime);
        //�X�V���������ƌ��ݒn�̍��������̌v�Z
        moveDirection.z = Mathf.Clamp(speed, 0f, speed);

        //x�����͖ڕW�̃|�W�V�����܂ł̍����ő��x���o��
        float ratioX = (targetLine * LineWidth - transform.position.x) / LineWidth;
        moveDirection.x = ratioX * speed;
        //�E���[���؂�ւ�
        if (Input.GetKeyDown("right"))
        {
            if (controller.isGrounded && targetLine < MaxLine)
            {
                targetLine = targetLine + 1;
            }
        }
        //�����[���؂�ւ�
         if (Input.GetKeyDown("left"))
        {
            if (controller.isGrounded && targetLine > MinLine)
            {
                targetLine = targetLine - 1;
            }
        }

        /*
        if(Input.GetAxis("Vertical") > 0.0f)
        {
            //�˂������O�i���鏈��
            moveDirection.z = Input.GetAxis("Vertical") * speed;
        }
        else
        {
            moveDirection.z = 0.0f;
        }

        //Horizontal�i���E���́j������΁A�˂�������]������
        transform.Rotate(0, Input.GetAxis("Horizontal") * 3f, 0);
        */

        //�˂������d�͂ŗ������鏈��
        moveDirection.y = moveDirection.y - gravitypower * Time.deltaTime;
        
        //�ړ��ʂ�Transform�ɕϊ�����
        Vector3 globalDirection = transform.TransformDirection(moveDirection);

        //Controller�Ɉړ��ʂ�n��
        controller.Move(globalDirection * Time.deltaTime);

        //�˂����̃A�j���[�V�������ŐV����
        animator.SetBool("run", moveDirection.z > 0f);
    }
}
