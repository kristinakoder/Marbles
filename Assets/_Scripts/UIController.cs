using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Slider shakeSensitivitySlider;
    [SerializeField] private IntVariable shakeSensitivity;

    void Start()
    {
        shakeSensitivitySlider.onValueChanged.AddListener((v) =>
        {
            shakeSensitivity.i = (int) v;
        });
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}