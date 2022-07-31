using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UI.RobotInstantiationPanel;
using UnityEngine;
using UnityEngine.TestTools;

public class SceneObjectsTest
{
    [UnityTest]
    public IEnumerator SceneObjectsTestWithEnumeratorPasses()
    {
        var sceneObjs = new GameObject();
        sceneObjs.AddComponent<SceneObjectsInstantiator>();

        sceneObjs.GetComponent<SceneObjectsInstantiator>().CreateOrDestroyObject(sceneObjs);

        Assert.AreEqual(sceneObjs,sceneObjs);
        yield return null;
        
    }
}
