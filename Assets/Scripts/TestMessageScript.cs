using UnityEngine;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

public class TestMessageScript : MonoBehaviour
{
    private float timeOut;
    private float timeElapsed;

    public void Update()
    {
        timeOut = 2;
        timeElapsed += Time.deltaTime;

        if (timeElapsed >= timeOut)
        {
             _ = GetMessageAsync();
            timeElapsed = 0.0f;
        }
    }

    public async Task GetMessageAsync()
    {
        using var client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync(@"http://localhost:8000/api/commands/unexecute_unity/");
        string json_text = await response.Content.ReadAsStringAsync();
        Debug.Log(json_text);
        if(json_text != "")
        {
            Command command = Command.CreateFromJSON(json_text);
            command.Execute();
        }
    }
    public void Move(string id, Vector3 target)
    {
        GameObject obj = GameObject.Find(id);
        NavWalk nav = obj.GetComponent<NavWalk>();
        nav.Move(target);
    }

}

[System.Serializable]
public class Command
{
    public string user;
    public string user_name;
    public string command_name;
    public string arg1;
    public string arg2;
    public string arg3;
    public string arg4;

    private Vector3[] places =
    {
        new Vector3(0, 3, 0),
        new Vector3(1, 3, 0),
        new Vector3(0, 3, 1)
    };

    private string[] prefab_names = {
        "MaleCharacterPBR",
        "FemaleCharacterPBR",
    };

    public GameObject GetRandomPreFab()
    {
        string prefab_name = this.prefab_names[Random.Range(0, this.prefab_names.Length)];
        return GameObject.Find(prefab_name);

    }
    public Vector3 GetRandomSpawnPlace()
    {
        return new Vector3(Random.Range(-8.0f, 0.0f), 3, Random.Range(-4.0f, 4.0f));
    }

    public static Command CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Command>(jsonString);
    }
    public void Execute()
    {
        switch (command_name)
        {
            case "in":
                this.Spawn();
                break;
            case "charge":
                this.Charge();
                break;
            case "out":
                this.Despawn();
                break;
            case "emote":
                this.Emote();
                break;
            case "subject":
                this.Subject();
                break;
            case "place":
                this.Place();
                break;
            case "comment":
                this.Comment();
                break;
            case "chara":
                this.Chara();
                break;
            default:
                break;
        }
    }
    public void Spawn()
    {
        Debug.Log("Spawn");
        // キャラクターを出現させる位置を取得
        Vector3 pos = this.GetRandomSpawnPlace();

        // 指定のプレハブからキャラクターをインスタンス化する
        GameObject prefab = this.GetRandomPreFab();
        GameObject obj = Object.Instantiate(prefab, pos, Quaternion.identity);
        obj.name = this.user;

        // キャラクターの名前を表示
        obj.GetComponent<OperationName>().SetName(this.user_name);

        // カメラのターゲットを変更
        GameObject camera = GameObject.Find("Main Camera");
        camera.GetComponent<CameraController>().target = obj.transform;

        // キャラクターがカメラ方向に回転
        float y = camera.transform.eulerAngles.y;
        obj.transform.eulerAngles = new Vector3(obj.transform.eulerAngles.x, camera.transform.eulerAngles.y+180, obj.transform.eulerAngles.z);
        
        // アニメーションを実行
        obj.GetComponent<Animator>().CrossFade("Victory_Battle_SwordAndShield", 0.3f);

    }
    public void Charge()
    {
        Debug.Log("Charge");
        GameObject camera = GameObject.Find("Main Camera");
        GameObject obj = GameObject.Find(this.user);
        obj.transform.LookAt(camera.transform);
        camera.transform.LookAt(obj.transform);

    }
    public void Despawn()
    {
        Debug.Log("Despawn");
        GameObject obj = GameObject.Find(this.user);
        if (obj)
        {
            Object.Destroy(obj);
        }
    }
    public void Emote()
    {
        Debug.Log("Emote");
        GameObject obj = GameObject.Find(this.user);
        obj.GetComponent<Animator>().CrossFade(this.arg1, 0.3f);

    }
    public void Subject()
    {
        Debug.Log("Subject");
    }
    public void Place()
    {
        // 場所の特定
        Debug.Log("Place");
        Dictionary<string, Vector3> dict = new Dictionary<string, Vector3>();
        dict.Add("A12", new Vector3(3, 3, 2));
        dict.Add("A13", new Vector3(3, 3, 3));

        // 場所まで移動させる
        GameObject obj = GameObject.Find(this.user);
        // obj.transform.position = dict[this.arg1];
        obj.GetComponent<NavWalk>().Move(dict[this.arg1]);

    }
    public void Comment()
    {
        Debug.Log("Comment");
    }
    public void Chara()
    {
        Debug.Log("Chara");
        GameObject obj = GameObject.Find(this.user);
        Vector3 pos = obj.transform.position;
        Object.Destroy(obj);

        GameObject newprefab = GameObject.Find(this.arg1);
        GameObject newobj = Object.Instantiate(newprefab, pos, Quaternion.identity);
        newobj.name = this.user;
    }

}
