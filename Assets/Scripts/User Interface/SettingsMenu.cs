using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour
{
    public TextMeshProUGUI savedText;
    public float masterVolume;
    public Slider slider;
    public Toggle toggle;

    public AudioMixer audioMixer;

	// Start is called before the first frame update
	void Start()
    {
        if(savedText!=null)
            savedText.gameObject.SetActive(false);
        LoadPlayerPrefs();

	}

    public void SetVolume(float volume) {
        audioMixer.SetFloat("MasterVolume",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public IEnumerator DisplaySaveText() //displayed save text im Settingsmenue
    {
        savedText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        savedText.gameObject.SetActive(false);
    }


	public void UseCustomListToggle(bool value)
	{
        WordListManager.useCustomWordList = value;
        PlayerPrefs.SetInt("UseCustomWordList", value ? 1 : 0);
        PlayerPrefs.Save();
	}
    
    public void LoadPlayerPrefs() {
        //MasterVolume laden
        if (!PlayerPrefs.HasKey("MasterVolume"))
        {
            Debug.Log("No MasterVolume saved in PlayerPrefs");
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(myDefaultVolume) * 20);
            slider.value = myDefaultVolume;
        }
        else
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            if(audioMixer!=null)
                audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
            if (slider != null)
                slider.value = masterVolume;
        }

        if(!PlayerPrefs.HasKey("hasCustomWordList"))
            return;

        WordListManager.hasCustomWordList = PlayerPrefs.GetInt("hasCustomWordList") switch
        {
            0 => false,
            1 => true,
            _ => WordListManager.hasCustomWordList
        };

        if (!PlayerPrefs.HasKey("UseCustomWordList"))
            return;

        WordListManager.useCustomWordList = PlayerPrefs.GetInt("UseCustomWordList") switch
        {
            0 => false,
            1 => true,
            _ => WordListManager.hasCustomWordList
        };

        if (toggle == null) return;
        if(PlayerPrefs.GetInt("UseCustomWordList")==0)
            toggle.SetIsOnWithoutNotify(false);
        if(PlayerPrefs.GetInt("UseCustomWordList")==1)
            toggle.SetIsOnWithoutNotify(true);
    }

    private float myDefaultVolume = 0.5f;
}