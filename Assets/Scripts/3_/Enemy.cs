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
    [SerializeField] public float HP { get; protected set; }
    [SerializeField] public float MaxHP { get; protected set; }
    [SerializeField] public int Level { get; protected set; }
    [SerializeField] public bool isboss { get; protected internal set; }

bool havesetbossval = false;
    void Start()
    {
        isboss = false;
        if (Level <= 0) Level = UnityEngine.Random.Range(1,11);
        if (MaxHP <= 0) MaxHP = 5*Level;
        if (isboss) MaxHP *= 3;
        HP = System.MathF.Round(UnityEngine.Random.Range(1,MaxHP),1);
        string name1 = names[UnityEngine.Random.Range(0, names.Count)];
        Name = name1;
        gameObject.name = name1;
    }
    string oldtext = "";
    void FixedUpdate()
    {
        if (isboss&&!havesetbossval) { 
            havesetbossval=true;   
            MaxHP *= 3; 
            HP *= 3;
        }
        string newtext = $"\"{Name}\"\n{HP}/{MaxHP}\nLVL {Level}";
        if (newtext != oldtext)
        {
            oldtext = newtext;
            text.text = newtext;
        }
    }
}
