using System;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;


namespace DG.DOTweenEditor
{
    internal static class FixDoTweenPaths
    {
        [InitializeOnLoadMethod]
        private static void FixPaths()
        {
            if (string.IsNullOrEmpty(EditorUtils.dotweenDir)) // It's important to trigger default calculation
            {
                return;
            }

            string pluginPath = CompilationPipeline.GetAssemblyDefinitionFilePathFromAssemblyName("Modules.DOTween");
            pluginPath = $"{Application.dataPath}{pluginPath.Substring(6)}"; // Trim double Assets
            pluginPath = Path.GetDirectoryName(pluginPath.Replace("\\", "/"));
            
            SetPrivateField(typeof(EditorUtils), "_dotweenDir", $"{pluginPath}/DOTween/");
            SetPrivateField(typeof(EditorUtils), "_dotweenProDir", $"{pluginPath}/DOTweenPro/");
            SetPrivateField(typeof(EditorUtils), "_demigiantDir", pluginPath);
            SetPrivateField(typeof(EditorUtils), "_dotweenProEditorDir", $"{pluginPath}/Editor/DOTweenPro/");
            SetPrivateField(typeof(EditorUtils), "_dotweenModulesDir", $"{pluginPath}/DOTween/Modules/");
            SetPrivateField(typeof(EditorUtils), "_editorADBDir", $"{pluginPath.Substring(Application.dataPath.Length + 1)}/Editor/DOTween/");


            void SetPrivateField(Type type, string fieldName, string fieldValue)
            {
                FieldInfo fieldInfo = type.GetField(fieldName,BindingFlags.Static | BindingFlags.NonPublic);
                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(null, fieldValue);
                }
                else
                {
                    Debug.LogWarning($"Can't file field {fieldName} in type {type}!");
                }
            }
        }
    }
}
