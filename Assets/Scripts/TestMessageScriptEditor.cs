using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

// クラス名を別名でまとめて管理する
using CUSTOMTYPE = TestMessageScript;

// 拡張するクラスを指定する
[CustomEditor(typeof(CUSTOMTYPE))]
public class TestMessageScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 元のインスペクター部分を表示
        base.OnInspectorGUI();

        // targetを変換して対象スクリプトの参照を取得する
        CUSTOMTYPE targetScript = target as CUSTOMTYPE;

        // Editor実行中のみ有効化なUIを設定する
        if (EditorApplication.isPlaying)
        {
            if (GUILayout.Button("GetMessage"))
            {
                _ = targetScript.GetMessageAsync();
            }
        }
    }
}
#endif