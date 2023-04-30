using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private SceneData sceneData;

    private Slider loadingBar;
    private TMP_Text progressLabel;
    private bool canLoad = true;
    private float timerMax = 0.01f;
    private float timer;

    private void Awake()
    {
        loadingBar = transform.Find("LoadSlider").GetComponent<Slider>();
        progressLabel = transform.Find("ProgressLabel").GetComponent<TMP_Text>();
        
        loadingBar.value = 0;
    }

    /* Adds 0.01 to the slider every 0.01 seconds. */
    private void Update()
    {
        if (loadingBar.value >= 1)
        {
            SceneManager.LoadScene(sceneData.SceneToLoad);
        }
        else if (canLoad)
        {
            loadingBar.value += 1f / 100;
            progressLabel.text = string.Format("{0:0}", loadingBar.value * 100) + "%";
            timer = timerMax;
            canLoad = false;
        }
        else if (!canLoad)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                canLoad = true;
            }
        }
    }
}
