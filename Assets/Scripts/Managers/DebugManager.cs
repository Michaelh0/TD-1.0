using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugManager : PersistentManager<DebugManager>
{
    public void TestLevel1()
    {
        SceneManager.LoadScene("Level 1");
    }
    public override void PostStart()
    {
        TestLevel1();
    }
}
