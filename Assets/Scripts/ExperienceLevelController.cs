using UnityEngine;

public class ExperienceLevelController : MonoBehaviour
{
    public static ExperienceLevelController instance;

    public int currentLevel = 1;
    public int currentExperience;
    public int expToNextLevel = 20;

    public GameObject levelUpPanel;

    public float bonusDamage = 0f;

    public void Awake()
    {
        instance = this;
    }

    public void GetExp(int amountToGet)
    {
        currentExperience += amountToGet;
        if (currentExperience >= expToNextLevel) LevelUp();
    }

    void LevelUp()
    {
        currentLevel++;
        currentExperience -= expToNextLevel;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.2f);

        Time.timeScale = 0f;
        if (levelUpPanel != null) levelUpPanel.SetActive(true);
    }

    public void UpgradeHealth()
    {
        PlayerHealth.instance.maxHealth += 20;
        PlayerHealth.instance.currentHealth += 20;
        ResumeGame();
    }

    public void UpgradeRegen()
    {
        PlayerHealth.instance.healthRegenAmount += 5f;
        ResumeGame();
    }
    public void UpgradeDamage()
    {
        bonusDamage += 1f;
        ResumeGame();
    }

    public void ResumeGame()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
    }
}