using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManger : MonoBehaviour
{
    public void Restart()
    {
        Tools.ReloadScene();
    }    

    public void Load(string name)
    {
        Tools.LoadScene(name);
    }

    public void Next()
    {
        Tools.LoadNextScene();
    }
}
