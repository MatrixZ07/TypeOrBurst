using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public float sliderSpeed;

    float healthTarget = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }
    private void Update()
    {
        if (slider.value > healthTarget) { 
           slider.value -= Time.unscaledDeltaTime * sliderSpeed ;
            fill.color = gradient.Evaluate(slider.normalizedValue);
        }
    }

    public void SetMaxHealth(int maxHealth) {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
        healthTarget = maxHealth;
        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth(int health) {
        healthTarget = health;
        //ReduceHealthTo(health);
        //fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void ReduceHealthTo(int health)
    {
        if(slider.value != health) {
            slider.value = Mathf.Lerp(health, slider.value, 30f*Time.deltaTime);
        }
    }


}
