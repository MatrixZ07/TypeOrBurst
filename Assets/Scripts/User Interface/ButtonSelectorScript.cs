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
    // Start is called before the first frame update
    void Start()
    {
        buttonSelector.SetActive(false);
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }
    private void Update()
    {
        if (buttonSelector != null) { 
            if (buttonSelector.activeInHierarchy && moveToPos) {
                if (buttonSelector.transform.localPosition.y > activeTargetpos.localPosition.y)
                {
                    buttonSelector.transform.localPosition = Vector3.Lerp(buttonSelector.transform.localPosition, activeTargetpos.localPosition, Time.unscaledDeltaTime * lerpspeed);
                }
                if (buttonSelector.transform.localPosition.y <= activeTargetpos.localPosition.y)
                {
                    moveToPos = false;
                }
            }
        }
        
        
    }
    public void MoveButtonSelector() {
        PlayUIHover();
        buttonSelector.SetActive(true);
        activeTargetpos = transform;
        //Debug.Log(activeTargetpos.localPosition.y.ToString() + " of gameobject" + transform.name);
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
        buttonSelector.SetActive(false);
    }

}
