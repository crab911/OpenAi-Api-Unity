﻿using UnityEditor;

using static OpenAi.Api.V1.SOAuthArgsV1;

namespace OpenAi.Api.V1
{
    [CustomEditor(typeof(SOAuthArgsV1))]
    public class OpenAiApiAuthArgsEditor : Editor
    {
        SerializedProperty AuthType;
        SerializedProperty PrivateApiKey;
        SerializedProperty Organization;


        void OnEnable()
        {
            AuthType = serializedObject.FindProperty("AuthType");
            PrivateApiKey = serializedObject.FindProperty("PrivateApiKey");
            Organization = serializedObject.FindProperty("Organization");
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(AuthType);

            switch((EAuthProvisionMethod)AuthType.enumValueIndex)
            {
                case EAuthProvisionMethod.LocalFile:
                    EditorGUILayout.HelpBox("This auth method will attempt to find the private key at `~/.openai/auth.json` (Linux/Mac) or `%USERPROFILE%/.openai/auth.json` (Windows). If this file does not exist or the key is not present, api calls will fail", MessageType.Warning);
                    break;
                case EAuthProvisionMethod.String:
                    EditorGUILayout.PropertyField(PrivateApiKey);
                    EditorGUILayout.PropertyField(Organization);
                    break;
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}