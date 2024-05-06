using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NavMeshを使うときに書く
using UnityEngine.AI;

public class NavWalk : MonoBehaviour
{
    private NavMeshAgent agent;

    //アニメーション用
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    public void Move(Vector3 point)
    {
        // エージェントが現在設定された目標地点に行くように設定します
        agent.destination = point;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Move", agent.velocity.magnitude);
    }
}