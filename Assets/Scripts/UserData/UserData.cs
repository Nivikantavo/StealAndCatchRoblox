using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserData
{
    public string UserName;
    public League CurrentLeague;

    public UserData()
    {
        CurrentLeague = League.Bronze1;
    }
}
