using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyChanger : MonoBehaviour
{
    [SerializeField] private List<Enemy> Enemies = new();
    [SerializeField] private TMP_InputField HPMinInput;
    [SerializeField] private TMP_InputField LevelMinInput;
    [SerializeField] private TMP_InputField BossInput;
    [SerializeField] private Button Applyfiltersbutton;
    [SerializeField] private Button Resetfiltersbutton;
    [SerializeField] private Button SetBossButton;
    void FilterEnemies()
    {
        if (Enemies.Count > 0)
        {
            ResetFilters(); // resets filters so old hidden enemies would be shown
            if (float.TryParse(HPMinInput.text, out float minhpset) == true) // checks if minhp is a float
            {
                foreach (Enemy enemy in Enemies)
                {
                    if (enemy.gameObject != null && enemy.gameObject.activeInHierarchy && enemy.HP < minhpset) // loops throught enemies and checks if less than minhp
                    {
                        enemy.gameObject.SetActive(false);
                    }
                }
            }
            if (int.TryParse(LevelMinInput.text,out int levelmin) == true) // same here but with levelmin and int
            {
                foreach (Enemy enemy in Enemies)
                {
                    if (enemy.gameObject != null && enemy.gameObject.activeInHierarchy && enemy.Level < levelmin)
                    {
                        enemy.gameObject.SetActive(false);
                    }
                }
            }
        }
    }
    void ResetFilters()
    {
        foreach (Enemy enemy in Enemies)
        {
            if (enemy.gameObject != null && enemy.gameObject.activeInHierarchy == false)
            {
                enemy.gameObject.SetActive(true);
            }
        }
    }
    void SetBoss()
    {
        string enemyname = BossInput.text;
        int lenght = enemyname.Length;
        foreach (Enemy enemy in Enemies)
        {
            if (enemy.name.Length >= lenght && enemy.name.ToLower().Substring(0, lenght) == enemyname.ToLower())
            {
                enemy.isboss = true;
            }
        }
    }
    void Start()
    {
        Enemy[] eo = FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID);
        Enemies.AddRange(eo);
        Applyfiltersbutton.onClick.AddListener(FilterEnemies);
        Resetfiltersbutton.onClick.AddListener(ResetFilters);
        SetBossButton.onClick.AddListener(SetBoss);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
