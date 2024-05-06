using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

// �N���X����ʖ��ł܂Ƃ߂ĊǗ�����
using CUSTOMTYPE = TestMessageScript;

// �g������N���X���w�肷��
[CustomEditor(typeof(CUSTOMTYPE))]
public class TestMessageScriptEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // ���̃C���X�y�N�^�[������\��
        base.OnInspectorGUI();

        // target��ϊ����đΏۃX�N���v�g�̎Q�Ƃ��擾����
        CUSTOMTYPE targetScript = target as CUSTOMTYPE;

        // Editor���s���̂ݗL������UI��ݒ肷��
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