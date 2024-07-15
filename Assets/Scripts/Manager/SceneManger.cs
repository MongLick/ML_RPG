using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySceneManager = UnityEngine.SceneManagement.SceneManager;

public class SceneManger : MonoBehaviour
{
    [SerializeField] Image fade;
    [SerializeField] Slider loadingBar;

    private BaseScene curScene;

    public BaseScene GetCurScene()
    {
        if(curScene == null)
        {
			curScene = FindAnyObjectByType<BaseScene>();
		}
        
        return curScene;
    }

    public T GetCurScene<T>() where T : BaseScene
    {
        if(curScene == null)
        {
            curScene= FindAnyObjectByType<BaseScene>();
        }

        return curScene as T;
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingRoutine(sceneName));
	}

    IEnumerator LoadingRoutine(string sceneName)
    {

        float time = 0;
        while(time < 0.5f)
        {
            time += Time.unscaledDeltaTime;
            fade.color = new Color(0, 0, 0, time * 2);

            yield return null;
        }

        Time.timeScale = 0;
        loadingBar.gameObject.SetActive(true);
        AsyncOperation oper = UnitySceneManager.LoadSceneAsync(sceneName);
        //oper.allowSceneActivation = false;
        while(oper.isDone == false)
        {
            loadingBar.value = Mathf.Lerp(0f, 0.5f, oper.progress);
            yield return null;
        }

        BaseScene curScene = GetCurScene();
        yield return curScene.LoadingRoutine();
		Time.timeScale = 1;
        loadingBar.value = 1f;
        loadingBar.gameObject.SetActive(false);

		yield return new WaitForSeconds(0.3f);

        time = 0.5f;
        while(time > 0)
        {
            time -= Time.deltaTime;
			fade.color = new Color(0, 0, 0, time * 2);

			yield return null;
		}
    }

    public void SetLoadingBarValue(float value)
    {
        loadingBar.value = value;
    }
}
