using UnityEngine;
using UnityEngine.UIElements;

namespace UI.RobotInstantiationPanel
{
    public class ComponentsScript
    {
        #region Slider Component

        private GameObject _sliderObject;
        private Slider _sliderSlider;
        private TextField _sliderTextField;
        private Label _sliderLabel;

        public void CreateSliderComponent(VisualElement root, GameObject robot)
        {
            _sliderObject = robot.gameObject;

            _sliderLabel = new Label("Slider Component");
            _sliderLabel.name = "SliderLabel";
            root.Add(_sliderLabel);

            _sliderSlider = new Slider("Size Slider");
            _sliderSlider.RegisterValueChangedCallback(ChangeSliderSizeSlider);
            _sliderSlider.value = 1;
            _sliderSlider.name = "SliderSlider";
            root.Add(_sliderSlider);

            _sliderTextField = new TextField("Size Text Input Field");
            _sliderTextField.RegisterValueChangedCallback(ChangeSliderSizeText);
            _sliderTextField.value = "1";
            _sliderTextField.name = "SliderTextInputField";
            root.Add(_sliderTextField);
        }

        public void DestroySliderComponent(VisualElement root)
        {
            root.Remove(root.Q<Slider>("SliderSlider"));
            root.Remove(root.Q<Label>("SliderLabel"));
            root.Remove(root.Q<TextField>("SliderTextInputField"));
        }

        private void ChangeSliderSizeSlider(ChangeEvent<float> evt)
        {
            _sliderObject.transform.localScale = new Vector3(evt.newValue, evt.newValue, evt.newValue);
            _sliderTextField.value = _sliderSlider.value.ToString();
        }

        private void ChangeSliderSizeText(ChangeEvent<string> evt)
        {
            if (float.Parse(evt.newValue) <= 10 || float.Parse(evt.newValue) >= 0)
            {
                _sliderObject.transform.localScale = new Vector3(float.Parse(evt.newValue), float.Parse(evt.newValue),
                    float.Parse(evt.newValue));
                _sliderSlider.value = float.Parse(evt.newValue);
            }
            else
            {
                _sliderSlider.value = 0;
                _sliderTextField.value = "0";
            }
        }

        #endregion

        #region Pose Component

        private GameObject _poseObject;
        private Slider _poseSliderPosition;
        private Slider _poseSliderRotation;
        private Label _poseLabel;

        public void CreatePoseComponent(VisualElement root, GameObject robot)
        {
            _poseObject = robot.gameObject;

            _poseLabel = new Label("Pose Component");
            _poseLabel.name = "PoseLabel";
            root.Add(_poseLabel);

            _poseSliderPosition = new Slider("Position");
            _poseSliderPosition.RegisterValueChangedCallback(ChangeSliderPositionSlider);
            _poseSliderPosition.value = 1;
            _poseSliderPosition.name = "PoseSliderPosition";
            root.Add(_poseSliderPosition);

            _poseSliderRotation = new Slider("Rotation");
            _poseSliderRotation.RegisterValueChangedCallback(ChangeSliderRotationSlider);
            _poseSliderRotation.value = 1;
            _poseSliderRotation.name = "PoseSliderRotation";
            root.Add(_poseSliderRotation);
        }

        private void ChangeSliderPositionSlider(ChangeEvent<float> evt)
        {
            Debug.Log("Position Changed");
            //_poseObject.Translate
        }

        private void ChangeSliderRotationSlider(ChangeEvent<float> evt)
        {
            Debug.Log("Rotation Changed");
            //_poseObject.Rotate
        }

        public void DestroyPoseComponent(VisualElement root)
        {
            root.Remove(root.Q<Slider>("PoseSliderPosition"));
            root.Remove(root.Q<Slider>("PoseSliderRotation"));
            root.Remove(root.Q<Label>("PoseLabel"));
        }

        #endregion

        #region IP Component



        #endregion
    }
}