using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.RobotInstantiationPanel
{
    [RequireComponent(typeof(UIDocument))]
    public class MenuScript : MonoBehaviour
    {
        private VisualElement _root;
        [SerializeField] private SceneObjectsInstantiator _sceneObjectsInstantiator;
        private ComponentsScript _componentsScript;

        private Button _buttonShowHidePanel;
        private VisualElement _slidePanel;
        private ScrollView _scrollView;
        private bool _showSlidePanel = false;
        
        private List <VisualElement> _visualElementList = new List<VisualElement>();
        private List <Toggle> _toggleList = new List<Toggle>();
        private List<GameObject> _robotsList = new List<GameObject>();

        void Start()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _componentsScript = new ComponentsScript();
            _buttonShowHidePanel = _root.Q<Button>("ButtonShowHidePanel");
            _slidePanel = _root.Q<VisualElement>("SlidePanel");
            _scrollView = _root.Q<ScrollView>("ScrollView");
            _buttonShowHidePanel.text = "Show Panel";
            _buttonShowHidePanel.clicked += ShowHidePanel;
            
            foreach (var robot in _sceneObjectsInstantiator.Robots)
            {
                SetUpRobot(robot);
            }
        }

        private void SetUpRobot(GameObject robot)
        {

                var toggle = new Toggle("Toggle" + robot.name);
                toggle.label = "Create " + robot.name;
                
                var vis = new VisualElement
                {
                    name = "VisualElement" + robot.name,
                    style =
                    {
                        display = DisplayStyle.None
                    }
                };

                _toggleList.Add(toggle);
                _visualElementList.Add(vis);
                _robotsList.Add(robot);
                _scrollView.Add(toggle);
                _scrollView.Add(vis);
                
                toggle.RegisterCallback<ClickEvent, Toggle> (FindRobot, toggle);
            }

        private void FindRobot(ClickEvent evt, Toggle toggleClicked)
        {
            int robotIndex;
            for (int toggleIndex = 0; toggleIndex < _toggleList.Count; toggleIndex++)
            {
                if (toggleClicked == _toggleList[toggleIndex])
                {
                    robotIndex = toggleIndex;
                    CreateRobot(robotIndex, toggleClicked);
                }
            }
        }

        private void CreateRobot(int robotIndex, Toggle toggleClicked)
        {
            if (toggleClicked.value)
            {
                bool create = true;
                _visualElementList[robotIndex].style.display = DisplayStyle.Flex;
                _sceneObjectsInstantiator.CreateOrDestroyObject(_robotsList[robotIndex]);
                AddRobotComponents(_robotsList[robotIndex], robotIndex, create);
            }
            else
            {
                bool create = false;
                _visualElementList[robotIndex].style.display = DisplayStyle.None;
                _sceneObjectsInstantiator.CreateOrDestroyObject(_robotsList[robotIndex]);
                AddRobotComponents(_robotsList[robotIndex], robotIndex, create);
            }
        }

        private void AddRobotComponents(GameObject robot, int robotIndex, bool create)
        {
            if (robot.GetComponent<SliderComponent>() != null)
            {
                if (create)
                {
                    _componentsScript.CreateSliderComponent(_visualElementList[robotIndex],
                        _sceneObjectsInstantiator.SceneObject);
                }
                else
                {
                    _componentsScript.DestroySliderComponent(_visualElementList[robotIndex]);
                }
            }
            if (robot.GetComponent<PoseComponent>() != null)
            {
                if (create)
                {
                    _componentsScript.CreatePoseComponent(_visualElementList[robotIndex],
                        _sceneObjectsInstantiator.SceneObject);
                }
                else
                {
                    _componentsScript.DestroyPoseComponent(_visualElementList[robotIndex]);
                }
            }
            if (robot.GetComponent<IPComponent>() != null)
            {
                if (create)
                {
                    _componentsScript.CreateIPComponent(_visualElementList[robotIndex],
                        _sceneObjectsInstantiator.SceneObject);
                }
                else
                {
                    _componentsScript.DestroyIPComponent(_visualElementList[robotIndex]);
                }
            }
        }

        private void ShowHidePanel()
        {
            _showSlidePanel = !_showSlidePanel;
            if (_showSlidePanel)
            {
                _buttonShowHidePanel.text = "Hide Panel";
                _slidePanel.transform.position = Vector3.left * _slidePanel.localBound.width;
            }
            else
            {
                _buttonShowHidePanel.text = "Show Panel";
                _slidePanel.transform.position = -Vector3.left * _slidePanel.localBound.width;
            }
        }
    }
}