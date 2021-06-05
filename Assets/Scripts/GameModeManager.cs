using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModeManager : MonoBehaviour
{
    public List<GameObject> Qubes;
    public static GameModeManager instance;
   
    public void ButtonKicks(string name)
    {
        GameObject go = Qubes.Find(x => x.name == name);
        Qubes.Remove(go);

    }
    
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
