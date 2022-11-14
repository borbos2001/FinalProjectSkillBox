using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;
    public void LoadGame()
    {
       StartCoroutine(LoadAsync());     
    }

    IEnumerator LoadAsync ()
    {
        AsyncOperation _asyncOperation = SceneManager.LoadSceneAsync(1);

        _loadingScreen.SetActive(true);

        while(!_asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(_asyncOperation.progress / 0.9f);

            _slider.value = progress;
            _text.text = progress * 100f + "%";


            yield return null;
        }
    }


}
