using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
 
public class CharaController : MonoBehaviour
{

	private GameObject namePlate;   //�@���O��\�����Ă���v���[�g
	private Animator animator;
	private NavMeshAgent agent;
    public TextMesh nameText;   //�@���O��\������e�L�X�g

	public void Start()
	{
		namePlate = nameText.transform.parent.gameObject;
		animator = GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
	}
    public void Update()
    {
		animator.SetFloat("Move", agent.velocity.magnitude);

	}
	public void SetName(string name)
	{
		nameText.text = name;
	}
}