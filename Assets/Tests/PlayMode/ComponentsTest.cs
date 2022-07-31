using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UI.RobotInstantiationPanel;
using UnityEngine;
using UnityEngine.TestTools;

public class ComponentsTest
{
    [UnityTest]
    public IEnumerator ComponentsTestWithEnumeratorPasses()
    {
        var components = new ComponentsScript();
        var menu = new GameObject();
        menu.AddComponent<MenuScript>();
        var sceneObj = new GameObject();
        sceneObj.AddComponent<SceneObjectsInstantiator>();
        
        //components.CreatePoseComponent();
        yield return null;
    }
}
