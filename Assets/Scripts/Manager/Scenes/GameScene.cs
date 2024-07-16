using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{
	[SerializeField] Transform player;
	[SerializeField] CharacterController playerController;
	[SerializeField] Monster monsterPrefab;
	[SerializeField] Transform spawnPoint;
	[SerializeField] int count;

	public override IEnumerator LoadingRoutine()
	{
		Debug.Log("게임 로드");
		yield return new WaitForSecondsRealtime(0.5f);
		Manager.Scene.SetLoadingBarValue(0.6f);
		Debug.Log("플레이어 스폰");
		yield return new WaitForSecondsRealtime(0.5f);
		Manager.Scene.SetLoadingBarValue(0.7f);
		Debug.Log("오브젝트 풀 준비");
		yield return new WaitForSecondsRealtime(0.5f);
		Manager.Scene.SetLoadingBarValue(0.8f);
		Debug.Log("몬스터 스폰");
		for (int i = 0; i < count; i++)
		{
			Vector2 randomOffset = Random.insideUnitCircle * 3;
			Vector3 spawnPos = spawnPoint.position + new Vector3(randomOffset.x, 0, randomOffset.y);
			Monster monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

			Debug.Log("몬스터 스폰");

			yield return new WaitForSecondsRealtime(0.2f);
		}
		Manager.Scene.SetLoadingBarValue(0.9f);
		yield return new WaitForSecondsRealtime(0.5f);
		Debug.Log("게임씬 로딩 끝");
	}

	public void ToTitleScene()
	{
		Manager.Scene.LoadScene("TitleScene");
	}

	public override void SceneSave()
	{
		Manager.Data.gameData.sceneSaved[Manager.Scene.GetCurScenenIndex()] = true;
		Manager.Data.gameData.gameSceneData.playerPos = player.position;
		Manager.Data.SaveData();
	}

	public override void SceneLoad()
	{
		if (Manager.Data.gameData.sceneSaved[Manager.Scene.GetCurScenenIndex()] == false)
		{
			return;
		}

		Manager.Data.LoadData();
		playerController.enabled = false;
		player.position = Manager.Data.gameData.gameSceneData.playerPos;
		playerController.enabled = true;
	}
}
