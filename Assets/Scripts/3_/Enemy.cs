using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
     public List<string> names;
    [SerializeField] private string Name;
    [SerializeField] private float HP;
    [SerializeField] private float MaxHP;
    [SerializeField] private int Level;
    [SerializeField] private bool isboss = false;

    bool havesetbossval = false;
    void Start()
    {
        if (Level <= 0) Level = UnityEngine.Random.Range(1,11);
        if (MaxHP <= 0) MaxHP = 5*Level;
        if (isboss) MaxHP *= 3;
        HP = MaxHP;
        string name1 = names[UnityEngine.Random.Range(0, names.Count + 1)];
        Name = name1;
        gameObject.name = name1;
    }
    string oldtext = "";
    void FixedUpdate()
    {
        if (isboss&&!havesetbossval) { 
            havesetbossval=true;   
            MaxHP *= 3; 
        }
        string newtext = $"\"{Name}\"\n{HP}/{MaxHP}\nLVL {Level}";
        if (newtext != oldtext)
        {
            oldtext = newtext;
            text.text = newtext;
        }
    }
}
