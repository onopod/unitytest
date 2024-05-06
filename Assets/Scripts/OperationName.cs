using UnityEngine;
using UnityEngine.UI;
 
public class OperationName : MonoBehaviour
{

	private GameObject namePlate;   //　名前を表示しているプレート
    public TextMesh nameText;   //　名前を表示するテキスト

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