using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    private static Manager instance;
    public static Manager Inst { get { return instance; } }

    [SerializeField] SceneManger sceneManger;
	[SerializeField] DataManager dataManager;
    public static SceneManger Scene{ get { return instance.sceneManger; } }
	public static DataManager Data{ get { return instance.dataManager; } }

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}
}
