using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Refresh : MonoBehaviour
{

    public GameObject rootObj;

    public void CallRefreshOnImplementers()
    {
        var implementers = rootObj.GetComponentsInChildren<IRefreshable>();
        foreach (var IMPLEMENTER in implementers)
        {
            IMPLEMENTER.Refresh();
        }
    }
}

public interface IRefreshable
{

    void Refresh();

}
