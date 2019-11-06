using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;

public class EnemyWindow : EditorWindow {

    // 編輯器介面材質
    private Texture2D headerSectionTexture;
    private Texture2D mageSectionTexture;
    private Texture2D warriorSectionTexture;
    private Texture2D rogueSectionTexture;

    // 編輯器介面顏色
    private Color headerSectionColor = new Color(13f / 255f, 32f / 255f, 44f / 255f, 1f);

    // 編輯器物件
    private Rect headerSection = new Rect();
    private Rect mageSection = new Rect();
    private Rect warriorSection = new Rect();
    private Rect rogueSection = new Rect();

    public static MageData MageInfo { get; private set; }
    public static WarriorData WarriorInfo { get; private set; }
    public static RogueData RogueInfo { get; private set; }

    // 編輯視窗
    [MenuItem("Window/Enemy Designer")]
    static void OpenWindow()
    {
        EnemyWindow window = (EnemyWindow)GetWindow(typeof(EnemyWindow));
        window.minSize = new Vector2(600, 300);
        window.Show();
    }

    /// <summary>
    /// A couple ways to define texture2Ds drawing texture and 
    /// defining areas OnEnable() & OnGUI() Rect Manipulation.
    /// 定義紋理繪圖紋理和定義區域 OnEnable() 和 OnGUI() 矩形操作的兩種方法。
    /// </summary>

    /// <summary>
    /// Similar to Start() or Awake().
    /// 類似於 Start() 或 Awake()。
    /// </summary>
    private void OnEnable()
    {
        InitTexture();
        InitData();
    }

    /// <summary>
    /// Create instance data
    /// 創建實體資料
    /// </summary>
    public static void InitData()
    {
        MageInfo = (MageData)CreateInstance(typeof(MageData));
        WarriorInfo = (WarriorData)CreateInstance(typeof(WarriorData));
        RogueInfo = (RogueData)CreateInstance(typeof(RogueData));
    }

    /// <summary>
    /// Initialize Texture2D values.
    /// 初始化紋理值。
    /// </summary>
    private void InitTexture()
    {
        headerSectionTexture = new Texture2D(1, 1);
        headerSectionTexture.SetPixel(0,0,headerSectionColor);
        headerSectionTexture.Apply();

        mageSectionTexture = Resources.Load<Texture2D>("Icon/00FFFF");
        warriorSectionTexture = Resources.Load<Texture2D>("Icon/66FF66");
        rogueSectionTexture = Resources.Load<Texture2D>("Icon/FF6633");
    }

    /// <summary>
    /// Similar to any Updata function.
    /// 類似於任何Updata函數。
    /// Not called once per frame. Called 1 or more times per interaction.
    /// 每幀不調用一次。 每次互動被調用1次或多次。
    /// </summary>
    private void OnGUI()
    {
        DrawLayouts();
        DrawHeader();
        DrawMageSettings();
        DrawWarriorSettings();
        DrawRogueSettings();
    }

    /// <summary>
    /// Defines Rect values and points textures based on Rects.
    /// 定義Rect值並基於Rect指向紋理。
    /// </summary>
    private void DrawLayouts()
    {
        headerSection.x = 0;
        headerSection.y = 0;
        headerSection.width = Screen.width;
        headerSection.height = 50;

        mageSection.x = 0;
        mageSection.y = 50;
        mageSection.width = Screen.width / 3f;
        mageSection.height = Screen.height - 50;

        warriorSection.x = Screen.width / 3f;
        warriorSection.y = 50;
        warriorSection.width = Screen.width / 3f;
        warriorSection.height = Screen.height - 50;

        rogueSection.x = (Screen.width / 3f) * 2;
        rogueSection.y = 50;
        rogueSection.width = Screen.width / 3f;
        rogueSection.height = Screen.height - 50;

        GUI.DrawTexture(headerSection, headerSectionTexture);
        GUI.DrawTexture(mageSection, mageSectionTexture);
        GUI.DrawTexture(warriorSection, warriorSectionTexture);
        GUI.DrawTexture(rogueSection, rogueSectionTexture);
    }

    /// <summary>
    /// Draw contents of header.
    /// 繪製標題的內容。
    /// </summary>
    private void DrawHeader()
    {
        GUILayout.BeginArea(headerSection);

        GUILayout.Label("Enemy Designer"); // 顯示文字

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draw contents of mage region.
    /// 繪製法師區域的內容。
    /// </summary>
    private void DrawMageSettings()
    {
        GUILayout.BeginArea(mageSection);

        // 水平排版▼
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Damage"); // 顯示文字
        MageInfo.dmgType = (MageDmgType)EditorGUILayout.EnumPopup(MageInfo.dmgType); // 下拉選單
        EditorGUILayout.EndHorizontal();
        // 水平排版▲

        // 水平排版▼
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon"); // 顯示文字
        MageInfo.wpnType = (MageWpnType)EditorGUILayout.EnumPopup(MageInfo.wpnType); // 下拉選單
        EditorGUILayout.EndHorizontal();
        // 水平排版▲

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.MAGE);
        }

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draw contents of warrior region.
    /// 繪製戰士區域的內容。
    /// </summary>
    private void DrawWarriorSettings()
    {
        GUILayout.BeginArea(warriorSection);

        // 水平排版▼
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Class"); // 顯示文字
        WarriorInfo.classType = (WarriorClassType)EditorGUILayout.EnumPopup(WarriorInfo.classType); // 下拉選單
        EditorGUILayout.EndHorizontal();
        // 水平排版▲

        // 水平排版▼
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon"); // 顯示文字
        WarriorInfo.wpnType = (WarriorWpnType)EditorGUILayout.EnumPopup(WarriorInfo.wpnType); // 下拉選單
        EditorGUILayout.EndHorizontal();
        // 水平排版▲

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.WARRIOR);
        }

        GUILayout.EndArea();
    }

    /// <summary>
    /// Draw contents of rogue region.
    /// 繪製流氓區域的內容。
    /// </summary>
    private void DrawRogueSettings()
    {
        GUILayout.BeginArea(rogueSection);

        // 水平排版▼
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Strategy"); // 顯示文字
        RogueInfo.strategyType = (RogueStrategyType)EditorGUILayout.EnumPopup(RogueInfo.strategyType); // 下拉選單
        EditorGUILayout.EndHorizontal();
        // 水平排版▲

        // 水平排版▼
        EditorGUILayout.BeginHorizontal();
        GUILayout.Label("Weapon"); // 顯示文字
        RogueInfo.wpnType = (RogueWpnType)EditorGUILayout.EnumPopup(RogueInfo.wpnType); // 下拉選單
        EditorGUILayout.EndHorizontal();
        // 水平排版▲

        if (GUILayout.Button("Create!", GUILayout.Height(40)))
        {
            GeneralSettings.OpenWindow(GeneralSettings.SettingsType.ROGUE);
        }

        GUILayout.EndArea();
    }

    // 另開視窗
    public class GeneralSettings : EditorWindow
    {
        // 視窗類型
        public enum SettingsType
        {
            MAGE,
            WARRIOR,
            ROGUE
        }
        public static SettingsType dataSetting;
        private static GeneralSettings window;

        // 另開視窗
        public static void OpenWindow(SettingsType setting)
        {
            dataSetting = setting;
            // 視窗設置
            window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
            window.minSize = new Vector2(250, 200);
            window.Show();
        }
    }
}
