using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour {

    public static int Health = 100;
    public static Slider slider;
    public static bool IsPoisoned = false;
    public static bool IsHealed = false;

    public static int PoisonDuration = 4;
    public static int tickCounter;
    public static float PoisonTimer = 1.0f;
    public static float Timer = 0.0f;

    public GameObject poisonEffect;
    public GameObject healEffect;
    
    void Awake()
    {
        slider = GameObject.Find("Slider").GetComponent<Slider>();
    }

    void Update()
    {
        if(IsPoisoned)
        {
            Timer += Time.deltaTime;

            if(Timer >= PoisonTimer)
                PoisonTick();
        }

        if(IsHealed)
            HealEffect();
    }

    private void HealEffect()
    {
        GameObject.Instantiate(healEffect);
        IsHealed = false;
    }

    private void PoisonTick()
    {
        DamageCharacter(10);
        tickCounter++;

        GameObject.Instantiate(poisonEffect);

        if (tickCounter >= PoisonDuration)
            IsPoisoned = false;

        Timer = 0.0f;
    }

    public static void PoisonCharacter()
    {
        tickCounter = 0;
        Character.IsPoisoned = true;
    }

    public static void HealCharacter(int healValue)
    {
        if(Health < 100)
            Health += healValue;
        IsHealed = true;
        UpdateVisual();
    }

    public static void DamageCharacter(int dmgValue)
    {
        Health -= dmgValue;
        UpdateVisual();
    }

    static void UpdateVisual()
    {
        slider.value = Health * 0.01f;
    }
}
