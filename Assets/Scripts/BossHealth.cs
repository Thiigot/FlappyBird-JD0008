using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class BossHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private int maxHP = 25;

    public int currentHP;

    private void Awake()
    {
        currentHP = maxHP;
    }
    

    public void TakeDamage(int damage)
    {
        if (currentHP <= 0 || damage <= 0)
            return;

        currentHP -= damage;

        currentHP = Mathf.Max(currentHP, 0);

        if (currentHP <= 0)
        {
            FindAnyObjectByType<GameManager>().GameWin();
        }
    }
}
