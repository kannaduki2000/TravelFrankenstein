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
	/// �R���X�g���N�^
	/// </summary>
	public SceneUnit(string name, List<string> pathList)
	{
		_name = name;
		_pathList = new List<string>(pathList);
	}

	/// <summary>
	/// �V�[���̓ǂݍ���
	/// </summary>
	public void Load()
	{
		if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) { return; }

		// ���݃`�F�b�N
		for (int i = 0; i < _pathList.Count; i++)
		{
			string path = _pathList[i];
			if (AssetDatabase.LoadAssetAtPath<SceneAsset>(path) == null)
			{
				Debug.LogError(path + "�����݂��܂���");
				return;
			}
		}
		for (int i = 0; i < _pathList.Count; i++)
		{
			EditorSceneManager.OpenScene(_pathList[i], i == 0 ? OpenSceneMode.Single : OpenSceneMode.Additive);
		}
	}

	/// <summary>
	/// ���O�ƃp�X
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
	/// �����ƒǉ�
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
	/// SceneUnit������
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
/// �V�[�����N���b�N�ŊJ���E�B���h�E
/// </summary>
public class OpenSceneWindow : EditorWindow
{
	// �X�N���[���̈ʒu
	private Vector2 _scrollPosition = Vector2.zero;

	// �v���W�F�N�g���̑S�V�[���̃p�X�ƁA���ꂪ�I������Ă��邩�ǂ���
	private Dictionary<String, bool> _scenePathDict = new Dictionary<string, bool>();

	// �I���V�[����List
	private List<string> _selectingScenePathList = new List<string>();

	// �V�[���̖��O
	private string _sceneUnitName = "";

	// �G�f�B�b�g���Rabbit Frog -> Scene Window ���N���b�N����ƊJ��
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
		// ������������

		EditorGUILayout.BeginVertical(GUI.skin.box);
		{
			if (SceneUnitSet.Instance.UnitNum != 0)
			{
				OnGUIWithTitle(OnSettingSceneUI, "�ݒ肵���V�[��");
			}
			OnGUIWithTitle(OnAllSceneGUI, "�v���W�F�N�g���̃V�[��");
			if (_selectingScenePathList.Count != 0)
			{
				OnGUIWithTitle(OnSelectingSceneGUI, "�I�𒆂̃V�[��");
			}
		}
		EditorGUILayout.EndVertical();

		EditorGUILayout.BeginVertical(GUI.skin.box);
		{
			EditorGUILayout.LabelField("�F�q�̃f�o�b�O�p");
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
			if (GUILayout.Button("�ǂݍ���", GUILayout.Width(100)))
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
		// �S�V�[���̃p�X��\��
		List<string> changedPathList = new List<string>();

		foreach (KeyValuePair<string, bool> pair in _scenePathDict)
		{
			EditorGUILayout.BeginHorizontal(GUI.skin.box);

			bool beforeFlag = pair.Value;
			bool afterFlag = EditorGUILayout.ToggleLeft(Path.GetFileNameWithoutExtension(pair.Key), beforeFlag);

			// �`�F�b�N�{�b�N�X�̕ύX�������List�ɓo�^
			if (beforeFlag != afterFlag)
			{
				changedPathList.Add(pair.Key);
			}

			// �ǂݍ��݃{�^���\��
			if (GUILayout.Button("�ǂݍ���", GUILayout.Width(100)))
			{
				// ���݂̃V�[���ɕύX���������ꍇ�A�ۑ����邩�m�F�̃E�B���h�E���o��(�L�����Z�����ꂽ��ǂݍ��݂����Ȃ�)
				if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
				{
					return;
				}

				// �V�[�����݂��邩�`�F�b�N
				if (AssetDatabase.LoadAssetAtPath<SceneAsset>(pair.Key) == null)
				{
					Debug.LogError(pair.Key + "�����݂��܂���I");
					return;
				}

				// �V�[���ǂݍ���
				EditorSceneManager.OpenScene(pair.Key);
				return;
			}
			EditorGUILayout.EndHorizontal();
		}

		// �ύX���������p�X�̃t���O��ύX�A�I�𒆂̃V�[����List���X�V
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

		// �V�[���Ď擾�ƑI��S�������s���{�^���\��
		if (GUILayout.Button("�V�[���Ď擾�A�I��S����"))
		{
			Init();
		}
	}

	// �I�𒆂̃V�[����\������GUI
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

		// �V�[�����j�b�g�̖��O����͂���GUI��\��
		_sceneUnitName = EditorGUILayout.TextField("�V�[�����j�b�g��", _sceneUnitName);
		GUILayout.Space(10);

		// �V�[�����j�b�g�̐ݒ���s���{�^���\��
		if (GUILayout.Button("�ݒ�"))
		{
			SceneUnitSet.Instance.Add(_sceneUnitName, _selectingScenePathList);
			Init();
		}
	}
}
