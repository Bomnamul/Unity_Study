using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobManager : Singleton<MobManager>
{
    public string myGlobalVar = "whatever";
    public int MobCount()
    {
        return 100;
    }
}
