using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public GameObject bg1;
    public GameObject bg2;
    public GameObject bg3;
    public GameObject bg4;

    private float sizeX;
    private float sizeY;
    private bool swapFinished=true;
    // Start is called before the first frame update
    void Start()
    {
        sizeX = bg1.GetComponent<RectTransform>().rect.width;
        sizeY = bg1.GetComponent<BoxCollider2D>().size.y;
    }
    private void Update()
    {
        if (swapFinished && Camera.main.transform.position.x > bg4.transform.position.x && Camera.main.transform.position.y > bg4.transform.position.y)
        {
            swapFinished = false;
            AdjustPosition(bg2, 1);
            AdjustPosition(bg3, 1);
            AdjustPosition(bg1, 2);
            SwapBackgrounds();
        }
        MoveBackground(bg1);
        MoveBackground(bg2);
        MoveBackground(bg3);
        MoveBackground(bg4);

    }

    private void MoveBackground(GameObject obj) { 
        obj.transform.Translate(new Vector2(-10.8f,-19.2f)*Time.unscaledDeltaTime/60f);
    }
    private void AdjustPosition(GameObject obj, int multiplier) {
        if (Camera.main.transform.position.x > obj.transform.position.x && Camera.main.transform.position.y > obj.transform.position.y) {
            obj.transform.position = new Vector3(obj.transform.position.x + sizeX * multiplier, obj.transform.position.y + sizeY * multiplier, obj.transform.position.z);
            //Debug.Log("new Position of " + obj.ToString() + " = " + obj.transform.position.ToString());
        }
    }
    private void SwapBackgrounds() {
        GameObject saveObject = bg1;
        bg1 = bg4;
        bg4 = saveObject;
        //Debug.Log("bg1 and bg4 Objects swapped.");
        swapFinished = true;

    }
}
