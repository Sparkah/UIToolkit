using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.RobotInstantiationPanel
{
    [RequireComponent(typeof(UIDocument))]
    public class MenuScript : MonoBehaviour
    {
        /*
          public  new class UxmlFactory : UxmlFactory<MenuController, UxmlTraits>{}
          public new class UxmlTraits : VisualElement.UxmlTraits{}
         */
        private VisualElement _root;

        private Button _buttonShowHidePanel;
        private VisualElement _slidePanel;
        private Toggle _positioner;
        private Toggle _painter;

        private bool _showPanel = false;

        //Instantiation UI
        private VisualElement _togglePositionerVisualElement;
        private VisualElement _togglePainterVisualElement;

        //Components UI
        private ComponentsScript _componentsScript;

        [SerializeField] private SceneObjectsInstantiator _sceneObjectsInstantiator;

        void Start()
        {
            //Setup
            _sceneObjectsInstantiator.ConstructMenuScript(this);
            _root = GetComponent<UIDocument>().rootVisualElement;
            _buttonShowHidePanel = _root.Q<Button>("ButtonShowHidePanel");
            _slidePanel = _root.Q<VisualElement>("SlidePanel");
            _positioner = _root.Q<Toggle>("TogglePositioner");
            _painter = _root.Q<Toggle>("TogglePainter");

            //TogglePositionerVisualElement

            _buttonShowHidePanel.text = "Show Panel";

            _buttonShowHidePanel.clicked += ShowHidePanel;
            _painter.RegisterCallback<ClickEvent>(CreatePainter);
            _positioner.RegisterCallback<ClickEvent>(CreatePositioner);

            //Instantiation Runtime UI
            //      m_Toggle.RegisterValueChangedCallback(OnToggleValueChanged); 
            _togglePositionerVisualElement = _root.Q<VisualElement>("TogglePositionerVisualElement");
            _togglePainterVisualElement = _root.Q<VisualElement>("TogglePainterVisualElement");
            _togglePositionerVisualElement.style.display = DisplayStyle.None;
            _togglePainterVisualElement.style.display = DisplayStyle.None;

        }

        private void OnDestroy()
        {
            _buttonShowHidePanel.clickable.clicked -= ShowHidePanel;
            _painter.UnregisterCallback<ClickEvent>(CreatePainter);
            _positioner.UnregisterCallback<ClickEvent>(CreatePositioner);
        }


        private void CreatePainter(ClickEvent evt)
        {
            //Initialize UI Components
            _componentsScript = new ComponentsScript();

            if (_painter.value)
            {
                _togglePainterVisualElement.style.display = DisplayStyle.Flex;
                _sceneObjectsInstantiator.CreateDestroyPainter();
                _componentsScript.CreateSliderComponent(_togglePainterVisualElement,
                    _sceneObjectsInstantiator.SceneObject);
            }
            else
            {
                _togglePainterVisualElement.style.display = DisplayStyle.None;
                _sceneObjectsInstantiator.CreateDestroyPainter();
                _componentsScript.DestroySliderComponent(_togglePainterVisualElement);
            }
        }


        private void CreatePositioner(ClickEvent evt)
        {
            if (_positioner.value)
            {
                _togglePositionerVisualElement.style.display = DisplayStyle.Flex;
                _sceneObjectsInstantiator.CreateDestroyPositioner();
                _componentsScript.CreateSliderComponent(_togglePositionerVisualElement,
                    _sceneObjectsInstantiator.SceneObject);
                _componentsScript.CreatePoseComponent(_togglePositionerVisualElement,
                    _sceneObjectsInstantiator.SceneObject);
            }
            else
            {
                _togglePositionerVisualElement.style.display = DisplayStyle.None;
                _sceneObjectsInstantiator.CreateDestroyPositioner();
                _componentsScript.DestroySliderComponent(_togglePositionerVisualElement);
                _componentsScript.DestroyPoseComponent(_togglePositionerVisualElement);
            }
        }

        private void ShowHidePanel()
        {
            _showPanel = !_showPanel;
            if (_showPanel)
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