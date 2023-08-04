using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonSelectorScript : MonoBehaviour
{
    public GameObject buttonSelector;
    public float lerpspeed=1f;
    static bool moveToPos = false;
    static Transform activeTargetpos;

    AudioManager audioManager;
	private void Awake()
	{
		audioManager = GameObject.FindObjectOfType<AudioManager>();
	}
	// Start is called before the first frame update
	void Start()
    {
        buttonSelector.SetActive(false);
    }
    private void Update()
    {
        if (buttonSelector != null) { 
            if (buttonSelector.activeInHierarchy && moveToPos) {
                if (buttonSelector.transform.localPosition.y > activeTargetpos.localPosition.y + 0.01f)
                {
                    buttonSelector.transform.localPosition = Vector3.Lerp(buttonSelector.transform.localPosition, activeTargetpos.localPosition, Time.unscaledDeltaTime * lerpspeed);
                }
                if (buttonSelector.transform.localPosition.y <= activeTargetpos.localPosition.y)
                {
                    moveToPos = false;
                }
            }
        } else
        {

        }
        
        
    }
    public void MoveButtonSelector() {
        PlayUIHover();
        buttonSelector.SetActive(true);
        activeTargetpos = transform;
        Vector3 startPosition = new Vector3(activeTargetpos.localPosition.x, activeTargetpos.localPosition.y + 10f, activeTargetpos.localPosition.z);
        buttonSelector.transform.localPosition = startPosition;
        moveToPos = true;
    }
    public void PlayUIHover() {
        audioManager.Play("UIHover");
    }
    public void RemoveButtonSelector() {
        buttonSelector.SetActive(false);
    }

    public void PlayClickSound() {
        audioManager.Play("UIClick");
        if(buttonSelector != null)
        {
			buttonSelector.SetActive(false);
		}
	}

}
