using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TMP_Text _durabilityText;
    [SerializeField] private Slider _durabilitySlider;
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _weaponNameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private GameManager _manager;

    public void UpdateDurability(float playerDurability)
    {
        _durabilityText.text = ((int)playerDurability).ToString();
        _durabilitySlider.value = playerDurability;
        if (playerDurability <= 0f)
            _manager.GameOver();
    }

    public void AddScore(int addScore)
    {
        var isNextLevel = _manager.AddScore(addScore);
        if (isNextLevel)
            UpdateLevel();
        UpdateScore();
    }

    public void UpdateWeapon(string name)
    {
        _weaponNameText.text = name;
    }

    public void UpdateLevel()
    {
        Debug.Log(_manager.GetCurrentLevel());
        _levelText.text = _manager.GetCurrentLevel().ToString();
    }

    private void UpdateScore()
    {
        _scoreText.text = $"{_manager.GetCurrentScore()}/{_manager.GetTotalNeedScore()}";

    }

    private void Start()
    {
        UpdateLevel();
        UpdateScore();
    }
}
