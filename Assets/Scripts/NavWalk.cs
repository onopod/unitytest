using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NavMesh���g���Ƃ��ɏ���
using UnityEngine.AI;

public class NavWalk : MonoBehaviour
{
    private NavMeshAgent agent;

    //�A�j���[�V�����p
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 point)
    {
        // �G�[�W�F���g�����ݐݒ肳�ꂽ�ڕW�n�_�ɍs���悤�ɐݒ肵�܂�
        Debug.Log("test");
        //agent.Move(new Vector3(3, 7, 60));
        Boolean b = agent.SetDestination(point);
        Debug.Log(b);
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(agent.velocity.magnitude);
        animator.SetFloat("Move", agent.velocity.magnitude);
    }
}