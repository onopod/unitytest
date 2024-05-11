using UnityEngine;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine.AI;
using UnityEngine.UI;

public class TestMessageScript : MonoBehaviour
{
    private float timeOut;
    private float timeElapsed;

    public void Update()
    {
        timeOut = 5;
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
        Debug.Log("Command is");
        Debug.Log(json_text);
        if(json_text != "")
        {
            
            Command command = Command.CreateFromJSON(json_text);

            // ユーザー情報を取得
            HttpResponseMessage user_response = await client.GetAsync("http://localhost:8000/api/users/" + command.user + "/");
            string user_json_text = await user_response.Content.ReadAsStringAsync();
            command.userdata = User.CreateFromJSON(user_json_text);
            Debug.Log("userdata is");
            Debug.Log(command.userdata.name);
            command.Execute();
        }
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

    public User userdata;

    private GameObject CharacterObj;


    private Vector3[] places =
    {
        new Vector3(0, 3, 0),
        new Vector3(1, 3, 0),
        new Vector3(0, 3, 1)
    };

    private string[] prefab_names = {
        "MaleCharacterPBR",
        "FemaleCharacterPBR",
        "Colobus"
    };

    public GameObject GetRandomPreFab()
    {
        string prefab_name = this.prefab_names[Random.Range(0, this.prefab_names.Length)];
        return GameObject.Find("Characters/" + prefab_name);

    }
    public Vector3 GetRandomSpawnPlace()
    {
        return new Vector3(Random.Range(515.0f, 525.0f), 10.0f, Random.Range(365.0f, 375.0f));
    }
    public GameObject GetCharacterObject()
    {
        GameObject obj = GameObject.Find("Characters/" + this.user);
        if (obj == null)
        {
            obj = Object.Instantiate(
                this.GetRandomPreFab(),
                this.GetRandomSpawnPlace(), 
                Quaternion.identity
            );
            obj.name = this.user;
            obj.GetComponent<CharaController>().SetName(this.user_name);
        }
        return obj;
    }
    
    public void Look(GameObject obj, int angle = 0)
    {
        Camera.main.GetComponent<CameraController>().target = obj.transform;

        // キャラクターがカメラ方向に回転
        float y = Camera.main.transform.eulerAngles.y;
        obj.transform.eulerAngles = new Vector3(
            obj.transform.eulerAngles.x,
            Camera.main.transform.eulerAngles.y + 180 + angle,
            obj.transform.eulerAngles.z
        );
    }
    public void Animate(GameObject obj, string emote_name)
    {
        Debug.Log("Animate is ");
        Debug.Log(emote_name);
        obj.GetComponent<Animator>().CrossFade(emote_name, 0.3f);
    }

    public static Command CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Command>(jsonString);
    }

    public void Spawn()
    {
        Debug.Log("Spawn");
        //ユーザーデータをAPIから取得
        Debug.Log(this.userdata.name);
        GameObject canvas = GameObject.Find("Canvases").transform.Find("StatusCanvas").gameObject;
        canvas.SetActive(true);
        GameObject.Find("Canvases/StatusCanvas/LeftPanel/user_text").gameObject.GetComponent<Text>().text = "名前：\n" + userdata.name + "\n------------------------------\n学習内容：\n" +
userdata.subject + "\n場所\n" + userdata.place;
        // キャラクターを出現させる
        GameObject obj = this.GetCharacterObject();
        // カメラのターゲットを変更
        this.Look(obj);
        // 出現アニメーションを実行
        this.Animate(obj, "Spawn");
        //canvas.SetActive(false);


    }
    public void Charge()
    {
        Debug.Log("Charge");
        // キャラクターを取得
        GameObject obj = this.GetCharacterObject();
        // カメラのターゲットを変更
        this.Look(obj);
        // チャージアニメーションを実行
        this.Animate(obj, "Charge");

    }
    public void Despawn()
    {
        Debug.Log("Despawn");
        // キャラクターを取得
        GameObject obj = this.GetCharacterObject();
        // カメラのターゲットを変更
        this.Look(obj);
        //消滅アニメーション
        this.Animate(obj, "Despawn");

        //インスタンスを削除
        Object.Destroy(obj, 2.0f);
    }
    public void Emote()
    {
        Debug.Log("Emote");
        GameObject obj = this.GetCharacterObject();
        this.Animate(obj, this.arg1);
    }

    public void Subject()
    {
        Debug.Log("Subject");
    }
    public void Place()
    {
        Debug.Log("Place");
        // キャラクターを取得
        GameObject obj = this.GetCharacterObject();
        // カメラのターゲットを変更
        this.Look(obj, 180);

        // 場所まで移動させる
        Vector3 pos = GameObject.Find("Places/" + this.arg1).transform.position;
        NavMeshAgent agent = obj.GetComponent<NavMeshAgent>();
        agent.stoppingDistance = 5f;
        agent.SetDestination(pos);
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
}
public class User
{
    public string id;
    public string name;
    public string emote;
    public string chara;
    public string place;
    public string subject;
    public string comment;
    public static User CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<User>(jsonString);
    }
}