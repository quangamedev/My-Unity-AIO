using UnityEditor;

public class SettingsToggleWindow : ScriptingDefineSymbolsToggleWindow<SettingsToggleWindow>
{
    [MenuItem("QuanGameDev/Settings toggle")]
    public static void CreateWindow()
    {
        CreateWindow("Settings toggle");
    }

    protected override void PopulateSymbols()
    {
        AddNewDefineSymbol("ENABLE_REGULAR_LOGS", "Enable regular logs in player, warnings and errors are uneffected");
    }
}
