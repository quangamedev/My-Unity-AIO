using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class ScriptingDefineSymbolsToggleWindow<T> : EditorWindow where T : ScriptingDefineSymbolsToggleWindow<T>
{
    private static List<KeyValuePair<string, string>> s_descriptionBySymbols = new List<KeyValuePair<string, string>>();

    protected abstract void PopulateSymbols();

    protected void AddNewDefineSymbol(string symbol, string description)
    {
        bool isContain = false;
        for (int i = 0; i < s_descriptionBySymbols.Count; i++)
        {
            if (s_descriptionBySymbols[i].Key == symbol)
            {
                isContain = true;
                break;
            }
        }

        if (!isContain)
        {
            s_descriptionBySymbols.Add(new KeyValuePair<string, string>(symbol,description));
        }
    }

    protected static void CreateWindow(string windowName)
    {
        T window = GetWindow<T>();
        window.titleContent = new GUIContent(windowName);
    }

    protected bool SymbolInUse(string symbol)
    {
        var namedBuildTarget = GetNamedBuildTarget();
        PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget, out var symbols);

        foreach (var enabledSymbols in symbols)
        {
            if (symbol == enabledSymbols)
            {
                return true;
            }
        }

        return false;
    }

    protected virtual void OnPreApplyDefines()
    {

    }

    protected virtual void OnPostApplyDefines()
    {

    }

    private List<Toggle> _toggles = new List<Toggle>();
    public void CreateGUI()
    {
        PopulateSymbols();

        VisualElement container = rootVisualElement;

        CreateDefineSymbolToggles(container);

        Button applyButton = new Button
        {
            text = "Apply"
        };
        applyButton.clicked += ApplyDefineSymbols;

        container.Add(applyButton);

        InitToggles();
    }

    void InitToggles()
    {
        if (_toggles.Count != s_descriptionBySymbols.Count)
        {
            return;
        }
        var namedBuildTarget = GetNamedBuildTarget();
        string[] symbols;
        PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget, out symbols);

        for (int i = 0; i < _toggles.Count; i++)
        {
            foreach (var symbol in symbols)
            {
                if (s_descriptionBySymbols[i].Key == symbol)
                {
                    _toggles[i].value = true;
                    break;
                }
            }
        }

    }

    private static NamedBuildTarget GetNamedBuildTarget()
    {
        BuildTarget buildTarget = EditorUserBuildSettings.activeBuildTarget;
        BuildTargetGroup targetGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);
        NamedBuildTarget namedBuildTarget = NamedBuildTarget.FromBuildTargetGroup(targetGroup);
        return namedBuildTarget;
    }

    void CreateDefineSymbolToggles(VisualElement container)
    {
        foreach (var descriptionBySymbol in s_descriptionBySymbols)
        {
            Toggle toggle = new Toggle(descriptionBySymbol.Value);
            container.Add(toggle);
            _toggles.Add(toggle);
        }
    }

    void ApplyDefineSymbols()
    {
        if (s_descriptionBySymbols.Count != _toggles.Count)
        {
            return;
        }
        var namedBuildTarget = GetNamedBuildTarget();
        string[] symbols;
        PlayerSettings.GetScriptingDefineSymbols(namedBuildTarget, out symbols);

        List<string> applySymbolList = new List<string>(symbols);
        for (int i = 0; i < _toggles.Count; i++)
        {
            string sb = s_descriptionBySymbols[i].Key;
            if (_toggles[i].value)
            {
                if (!symbols.Contains(sb))
                {
                    applySymbolList.Add(sb);
                }
            }
            else
            {
                if (symbols.Contains(sb))
                {
                    applySymbolList.Remove(sb);
                }
            }
        }

        OnPreApplyDefines();
        PlayerSettings.SetScriptingDefineSymbols(namedBuildTarget,applySymbolList.ToArray());
        OnPostApplyDefines();
    }
}