using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    void Start()
    {
        Enemy[] eo = FindObjectsByType<Enemy>(FindObjectsSortMode.InstanceID);
        Enemies.AddRange(eo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
