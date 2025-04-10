using UnityEngine;
using UnityEngine.UI;

public class BCMode : MonoBehaviour
{
    [SerializeField] private Toggle toggle;

    void Start()
    {
        if (toggle == null)
        {
            toggle = GetComponent<Toggle>();
            if (toggle == null) 
            {
                Debug.LogError("Toggle component not found on " + gameObject.name);
                return;
            }
        }        

        // Load the saved state
        bool savedState = PlayerPrefs.GetInt("BCMode", 0) == 1;

        // Set the toggle without triggering the event
        toggle.isOn = savedState;

        // Add the listener
        toggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    public void OnToggleValueChanged(bool isOn)
    {
        PlayerPrefs.SetInt("BCMode", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}