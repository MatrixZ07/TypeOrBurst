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
        savedText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume) {
        audioMixer.SetFloat("MasterVolume",Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MasterVolume", volume);
        PlayerPrefs.Save();
    }

    public IEnumerator DisplaySaveText() //displayed save text im Settingsmenü
    {
        savedText.gameObject.SetActive(true);
        yield return new WaitForSecondsRealtime(3f);
        savedText.gameObject.SetActive(false);
    }


	public void UseCustomListToggle(bool value)
	{
        WordListManager.useCustomWordList = value;
        if (value)
        {
            PlayerPrefs.SetInt("UseCustomWordList", 1);
        }
        else { 
            PlayerPrefs.SetInt("UseCustomWordList", 0);

        }
        PlayerPrefs.Save();
	}
    public void LoadPlayerPrefs() {
        //MasterVolume laden
        if (PlayerPrefs.HasKey("MasterVolume"))
        {
            masterVolume = PlayerPrefs.GetFloat("MasterVolume");
            slider.value = masterVolume;
            audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterVolume) * 20);
        }
        else {
            Debug.Log("No MasterVolume saved in PlayerPrefs");
        }
        //UseCustomListToggle laden
        if (PlayerPrefs.HasKey("UseCustomWordList"))
        {
            if (PlayerPrefs.GetInt("UseCustomWordList") == 1)
            {
                WordListManager.useCustomWordList = true;
                toggle.SetIsOnWithoutNotify(true);
            }
            else if (PlayerPrefs.GetInt("UseCustomWordList") == 0) {
                WordListManager.useCustomWordList = false;
                toggle.SetIsOnWithoutNotify(false);
            }
        }
        else { 
            Debug.Log("No Boolean 'UseCustomWordList' saved in PlayerPrefs");
        }
        //HasCustomWordList laden
        if (PlayerPrefs.HasKey("hasCustomWordList"))
        {
            if (PlayerPrefs.GetInt("hasCustomWordList") == 1)
            {
                WordListManager.hasCustomWordList = true;
            }
            else if (PlayerPrefs.GetInt("hasCustomWordList") == 0)
            {
                WordListManager.hasCustomWordList = false;
            }
        }
        else
        {
            Debug.Log("No Boolean 'hasCustomWordList' saved in PlayerPrefs");
        }
    }
}