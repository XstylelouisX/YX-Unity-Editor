using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Types;
using System.IO;

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

    /// <summary>
    /// Other editor window creation.
    /// 另開編輯視窗。
    /// </summary>
    public class GeneralSettings : EditorWindow
    {
        /// <summary>
        /// Editor window type.
        /// 編輯視窗類型。
        /// </summary>
        public enum SettingsType
        {
            MAGE,
            WARRIOR,
            ROGUE
        }
        public static SettingsType dataSetting;
        private static GeneralSettings window;

        /// <summary>
        /// Editor window display.
        /// 編輯視窗顯示
        /// </summary>
        /// <param name="setting">Editor window type.</param>
        public static void OpenWindow(SettingsType setting)
        {
            dataSetting = setting;
            // 視窗設置
            window = (GeneralSettings)GetWindow(typeof(GeneralSettings));
            window.minSize = new Vector2(250, 200);
            window.Show();
        }

        /// <summary>
        /// Draw editor window.
        /// 繪製編輯視窗。
        /// </summary>
        private void OnGUI()
        {
            switch (dataSetting)
            {
                case SettingsType.MAGE:
                    DrawSettings(MageInfo);
                    break;
                case SettingsType.WARRIOR:
                    DrawSettings(WarriorInfo);
                    break;
                case SettingsType.ROGUE:
                    DrawSettings(RogueInfo);
                    break;
            }
        }

        /// <summary>
        /// Editor window setting
        /// </summary>
        /// <param name="charData">Editor window content</param>
        private void DrawSettings(CharacterData charData)
        {
            // 水平排版▼
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Prefab");
            charData.prefab = (GameObject)EditorGUILayout.ObjectField(charData.prefab, typeof(GameObject), false); // 設定資料數值
            EditorGUILayout.EndHorizontal();
            // 水平排版▲

            // 水平排版▼
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Max Health");
            charData.maxHealth = EditorGUILayout.FloatField(charData.maxHealth); // 設定資料數值
            EditorGUILayout.EndHorizontal();
            // 水平排版▲

            // 水平排版▼
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Max Energy");
            charData.maxEnergy = EditorGUILayout.FloatField(charData.maxEnergy); // 設定資料數值
            EditorGUILayout.EndHorizontal();
            // 水平排版▲

            // 水平排版▼
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Power");
            charData.power = EditorGUILayout.Slider(charData.power, 0, 100); // 設定資料數值(拉條)
            EditorGUILayout.EndHorizontal();
            // 水平排版▲

            // 水平排版▼
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("% Crit Chance");
            charData.critChance = EditorGUILayout.Slider(charData.critChance, 0, charData.power); // 設定資料數值(拉條)，最大值依照charData.power調整數值。
            EditorGUILayout.EndHorizontal();
            // 水平排版▲

            // 水平排版▼
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Name");
            charData.playName = EditorGUILayout.TextField(charData.playName); // 設定資料數值(拉條)，最大值依照charData.power調整數值。
            EditorGUILayout.EndHorizontal();
            // 水平排版▲

            // 創建物件的必要條件
            if (charData.prefab == null)
            {
                // 需要選擇預製物件的提示訊息
                EditorGUILayout.HelpBox("This enemy needs a [Prefab] before it can be created", MessageType.Warning);
            }
            else if (charData.playName == null || charData.playName.Length < 1)
            {
                // 需要選擇預製物件的提示訊息
                EditorGUILayout.HelpBox("This enemy needs a [Name] before it can be created", MessageType.Warning);
            }
            else if (GUILayout.Button("Finish and Save", GUILayout.Height(30)))
            {
                SaveCharacterData();
                window.Close();
            }
        }

        /// <summary>
        /// Create data
        /// 創建資料
        /// </summary>
        private void SaveCharacterData()
        {
            // 新增預製物件
            string newPrefabPath = "Assets/Prefabs/Characters/";
            // 新增物件資源
            string dataPath = "Assets/YX-Unity-Editor/Resources/CharacterData/Data/";
            // 物件資源路徑
            string prefabPath;
            // 資料夾名稱
            string folderName = dataSetting.ToString();

            newPrefabPath = newPrefabPath + folderName.ToLower() + "/";
            dataPath = dataPath + folderName.ToLower() + "/";

            // 如果路徑不存在
            if (!Directory.Exists(newPrefabPath))
            {
                Directory.CreateDirectory(newPrefabPath);
            }
            if (!Directory.Exists(dataPath))
            {
                Directory.CreateDirectory(dataPath);
            }

            switch (dataSetting)
            {
                case SettingsType.MAGE:

                    // 建立物件資源
                    dataPath += MageInfo.playName + ".asset";
                    AssetDatabase.CreateAsset(MageInfo, dataPath);

                    // 建立預製物件
                    newPrefabPath += MageInfo.playName + ".prefab";
                    prefabPath = AssetDatabase.GetAssetPath(MageInfo.prefab);

                    // 建立資源
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject magePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                    if (!magePrefab.GetComponent<Mage>())
                    {
                        magePrefab.AddComponent(typeof(Mage));
                    }
                    magePrefab.GetComponent<Mage>().mageData = MageInfo;

                    break;
                case SettingsType.WARRIOR:

                    // 建立物件資源
                    dataPath += WarriorInfo.playName + ".asset";
                    AssetDatabase.CreateAsset(WarriorInfo, dataPath);

                    // 建立預製物件
                    newPrefabPath += WarriorInfo.playName + ".prefab";
                    prefabPath = AssetDatabase.GetAssetPath(WarriorInfo.prefab);

                    // 建立資源
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject warriorPrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                    if (!warriorPrefab.GetComponent<Warrior>())
                    {
                        warriorPrefab.AddComponent(typeof(Warrior));
                    }
                    warriorPrefab.GetComponent<Warrior>().warriorData = WarriorInfo;

                    break;
                case SettingsType.ROGUE:

                    // 建立物件資源
                    dataPath += RogueInfo.playName + ".asset";
                    AssetDatabase.CreateAsset(RogueInfo, dataPath);

                    // 建立預製物件
                    newPrefabPath += RogueInfo.playName + ".prefab";
                    prefabPath = AssetDatabase.GetAssetPath(RogueInfo.prefab);

                    // 建立資源
                    AssetDatabase.CopyAsset(prefabPath, newPrefabPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    GameObject roguePrefab = (GameObject)AssetDatabase.LoadAssetAtPath(newPrefabPath, typeof(GameObject));
                    if (!roguePrefab.GetComponent<Rogue>())
                    {
                        roguePrefab.AddComponent(typeof(Rogue));
                    }
                    roguePrefab.GetComponent<Rogue>().rogueData = RogueInfo;

                    break;
            }
        }
    }
}
