using UnityEngine;
using UnityEngine.UI;
 
public class OperationName : MonoBehaviour
{

	private GameObject namePlate;   //�@���O��\�����Ă���v���[�g
    public TextMesh nameText;   //�@���O��\������e�L�X�g

    void Start()
	{
		namePlate = nameText.transform.parent.gameObject;
	}

	void LateUpdate()
	{
		//namePlate.transform.rotation = Camera.main.transform.rotation;
	}

	public void SetName(string name)
	{
		nameText.text = name;
	}
}