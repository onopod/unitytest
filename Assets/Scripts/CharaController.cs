using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
 
public class CharaController : MonoBehaviour
{

	private GameObject namePlate;   //　名前を表示しているプレート
	private Animator animator;
	private NavMeshAgent agent;
    public TextMesh nameText;   //　名前を表示するテキスト

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