using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthPoints;
    public Gradient healthGradient;
    public Image healthFill;
    public Image deathIcon;

    public void UpdateCurrentHealth(int prev, int current, int maxHealth)
    {
        healthFill.DOKill();
        float amount = ((float)current) / maxHealth;
        healthFill.DOFillAmount(amount, .5f).SetEase(Ease.Linear);
        healthPoints.text = $"{current}";
        healthFill.color = healthGradient.Evaluate(((float)current) / maxHealth);
        if (current <= 0)
        {
            deathIcon.enabled = true;
            healthPoints.text = "";
        }
        else
        {
            deathIcon.enabled = false;
        }
    }
}
