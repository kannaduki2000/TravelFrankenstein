using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[Serializable]
public class SceneUnit
{
	[SerializeField] private string _name = "";

	private List<string> _pathList = new List<string>();

	/// <summary>
	/// コンストラクタ
	/// </summary>
	public SceneUnit(string name, List<string> pathList)
	{
		_name = name;
		_pathList = new List<string>(pathList);
	}

	/// <summary>
	/// シーンの読み込み
	/// </summary>
	public void Load()
	{
		if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) { return; }

		// 存在チェック
		for (int i = 0; i < _pathList.Count; i++)
		{
			string path = _pathList[i];
			if (AssetDatabase.LoadAssetAtPath<SceneAsset>(path) == null)
			{
				Debug.LogError(path + "が存在しません");
				return;
			}
		}
		for (int i = 0; i < _pathList.Count; i++)
		{
			EditorSceneManager.OpenScene(_pathList[i], i == 0 ? OpenSceneMode.Single : OpenSceneMode.Additive);
		}
	}

	/// <summary>
	/// 名前とパス
	/// </summary>
	public string GetNameAndPath()
	{
		string nameAndPath = "";

		foreach (string path in _pathList)
		{
			if (!string.IsNullOrEmpty(nameAndPath)) { nameAndPath += " + "; }
			nameAndPath += Path.GetFileNameWithoutExtension(path);
		}
		return _name + " : (" + nameAndPath + ")";
	}

}

[Serializable]
public class SceneUnitSet
{
	private static SceneUnitSet _instance;
	public static SceneUnitSet Instance
	{
		get
		{
			if (_instance == null)
			{
				string json = EditorUserSettings.GetConfigValue(SAVE_KEY);

				if (string.IsNullOrEmpty(json))
				{
					_instance = new SceneUnitSet();
				}
				else
				{
					_instance = JsonUtility.FromJson<SceneUnitSet>(json);
				}
			}
			return _instance;
		}
	}

	[SerializeField] public List<SceneUnit> _sceneUnitList = new List<SceneUnit>();

	public int UnitNum { get { return _sceneUnitList.Count; } }

	private const string SAVE_KEY = "SCENE_UNIT_SAVE_KEY";

	private void SaveSceneUnitList()
	{
		EditorUserSettings.SetConfigValue(SAVE_KEY, JsonUtility.ToJson(this));
	}

	/// <summary>
	/// 生成と追加
	/// </summary>
	public void Add(string sceneUnitName, List<string> scenePathList)
	{
		if (string.IsNullOrEmpty(sceneUnitName))
		{
			sceneUnitName = UnitNum.ToString();
		}
		_sceneUnitList.Add(new SceneUnit(sceneUnitName, scenePathList));
		SaveSceneUnitList();
	}

	public SceneUnit GetAtNo(int no)
	{
		return _sceneUnitList[no];
	}

	/// <summary>
	/// SceneUnitを消去
	/// </summary>
	public void Remove(SceneUnit sceneUnit)
	{
		_sceneUnitList.Remove(sceneUnit);
		SaveSceneUnitList();
	}

	public void Move(SceneUnit sceneUnit, bool isUp)
	{
		int beforeNo = _sceneUnitList.IndexOf(sceneUnit);
		int afterNo = beforeNo + (isUp ? -1 : 1);

		_sceneUnitList[beforeNo] = _sceneUnitList[afterNo];
		_sceneUnitList[afterNo] = sceneUnit;
	}

	public void Reset()
	{
		_sceneUnitList = new List<SceneUnit>();
	}
}

/// <summary>
/// シーンをクリックで開くウィンドウ
/// </summary>
public class OpenSceneWindow : EditorWindow
{
	// スクロールの位置
	private Vector2 _scrollPosition = Vector2.zero;

	// プロジェクト内の全シーンのパスと、それが選択されているかどうか
	private Dictionary<String, bool> _scenePathDict = new Dictionary<string, bool>();

	// 選択シーンのList
	private List<string> _selectingScenePathList = new List<string>();

	// シーンの名前
	private string _sceneUnitName = "";

	// エディット上のRabbit Frog -> Scene Window をクリックすると開く
	[MenuItem("Frankenstein/Scene Window")]

	private static void Open()
	{
		OpenSceneWindow.GetWindow<OpenSceneWindow>(typeof(OpenSceneWindow));
	}

	private void Init()
	{
		_scenePathDict = AssetDatabase.FindAssets("t:SceneAsset")
			.Select(guid => AssetDatabase.GUIDToAssetPath(guid))
			.ToDictionary(path => path, flag => false);

		_sceneUnitName = "";
		_selectingScenePathList.Clear();
	}

	private void OnEnable()
	{
		Init();
	}

	private void OnGUI()
	{
		_scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, GUI.skin.scrollView);
		// 処理ここから

		EditorGUILayout.BeginVertical(GUI.skin.box);
		{
			if (SceneUnitSet.Instance.UnitNum != 0)
			{
				OnGUIWithTitle(OnSettingSceneUI, "設定したシーン");
			}
			OnGUIWithTitle(OnAllSceneGUI, "プロジェクト内のシーン");
			if (_selectingScenePathList.Count != 0)
			{
				OnGUIWithTitle(OnSelectingSceneGUI, "選択中のシーン");
			}
		}
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical(GUI.skin.box);
		{
			EditorGUILayout.LabelField("熊倉のデバッグ用");
		}
		EditorGUILayout.BeginHorizontal(GUI.skin.box);
		{
			if (GUILayout.Button("Reset", GUILayout.Width(100)))
			{
				for (int i = 0; i < SceneUnitSet.Instance.UnitNum; i++)
				{
					SceneUnitSet.Instance.Reset();
				}
			}

			if (GUILayout.Button("Debug", GUILayout.Width(100)))
			{
				Debug.Log(SceneUnitSet.Instance.UnitNum);
			}
		}
		EditorGUILayout.EndHorizontal();
		EditorGUILayout.EndVertical();
		EditorGUILayout.EndScrollView();
	}

	private void OnGUIWithTitle(Action onGUIaction, string title)
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);
		{
			EditorGUILayout.LabelField(title);
			GUILayout.Space(10);
			onGUIaction();
		}
		EditorGUILayout.EndVertical();
		GUILayout.Space(10);
	}

	public void OnSettingSceneUI()
	{
		for (int i = 0; i < SceneUnitSet.Instance.UnitNum; i++)
		{
			EditorGUILayout.BeginHorizontal(GUI.skin.box);

			if (GUILayout.Button("x", GUILayout.Width(20)))
			{
				SceneUnitSet.Instance.Remove(SceneUnitSet.Instance.GetAtNo(i));
				return;
			}

			EditorGUILayout.LabelField(SceneUnitSet.Instance.GetAtNo(i).GetNameAndPath());
			if (GUILayout.Button("読み込み", GUILayout.Width(100)))
			{
				SceneUnitSet.Instance.GetAtNo(i).Load();
				return;
			}

			if (i > 0)
			{
				if (GUILayout.Button("?", GUILayout.Width(20)))
				{
					SceneUnitSet.Instance.Move(SceneUnitSet.Instance.GetAtNo(i), isUp: false);
					return;
				}
				else
				{
					GUILayout.Label("", GUILayout.Width(20));
				}

				if (i < SceneUnitSet.Instance.UnitNum - 1)
				{
					if (GUILayout.Button("?", GUILayout.Width(20)))
					{
						SceneUnitSet.Instance.Move(SceneUnitSet.Instance.GetAtNo(i), isUp: false);
						return;
					}
				}
				else
				{
					GUILayout.Label("", GUILayout.Width(20));
				}
				EditorGUILayout.EndHorizontal();
			}

		}
	}

	private void OnAllSceneGUI()
	{
		// 全シーンのパスを表示
		List<string> changedPathList = new List<string>();

		foreach (KeyValuePair<string, bool> pair in _scenePathDict)
		{
			EditorGUILayout.BeginHorizontal(GUI.skin.box);

			bool beforeFlag = pair.Value;
			bool afterFlag = EditorGUILayout.ToggleLeft(Path.GetFileNameWithoutExtension(pair.Key), beforeFlag);

			// チェックボックスの変更があればListに登録
			if (beforeFlag != afterFlag)
			{
				changedPathList.Add(pair.Key);
			}

			// 読み込みボタン表示
			if (GUILayout.Button("読み込み", GUILayout.Width(100)))
			{
				// 現在のシーンに変更があった場合、保存するか確認のウィンドウを出す(キャンセルされたら読み込みをしない)
				if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
				{
					return;
				}

				// シーン存在するかチェック
				if (AssetDatabase.LoadAssetAtPath<SceneAsset>(pair.Key) == null)
				{
					Debug.LogError(pair.Key + "が存在しません！");
					return;
				}

				// シーン読み込み
				EditorSceneManager.OpenScene(pair.Key);
				return;
			}
			EditorGUILayout.EndHorizontal();
		}

		// 変更があったパスのフラグを変更、選択中のシーンのListも更新
		foreach (string changedPath in changedPathList)
		{
			_scenePathDict[changedPath] = !_scenePathDict[changedPath];

			if (_scenePathDict[changedPath])
			{
				_selectingScenePathList.Add(changedPath);
			}
			else
			{
				_selectingScenePathList.Remove(changedPath);
			}
		}
		GUILayout.Space(10);

		// シーン再取得と選択全解除を行うボタン表示
		if (GUILayout.Button("シーン再取得、選択全解除"))
		{
			Init();
		}
	}

	// 選択中のシーンを表示するGUI
	private void OnSelectingSceneGUI()
	{
		EditorGUILayout.BeginVertical(GUI.skin.box);
		{
			for (int i = 0; i < _selectingScenePathList.Count; i++)
			{
				string path = _selectingScenePathList[i];
				EditorGUILayout.LabelField((i + 1).ToString() + " : " + Path.GetFileNameWithoutExtension(path));
			}
		}
		EditorGUILayout.EndVertical();
		GUILayout.Space(10);

		// シーンユニットの名前を入力するGUIを表示
		_sceneUnitName = EditorGUILayout.TextField("シーンユニット名", _sceneUnitName);
		GUILayout.Space(10);

		// シーンユニットの設定を行うボタン表示
		if (GUILayout.Button("設定"))
		{
			SceneUnitSet.Instance.Add(_sceneUnitName, _selectingScenePathList);
			Init();
		}
	}
}
