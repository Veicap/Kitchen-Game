using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }
    [SerializeField] private Button soundEffectButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button close;
    [SerializeField] private Button moveDown;
    [SerializeField] private Button moveUp;
    [SerializeField] private Button moveLeft;
    [SerializeField] private Button moveRight;
    [SerializeField] private Button interact;
    [SerializeField] private Button interactAlt;
    [SerializeField] private Button pause;
    [SerializeField] private Button gamePad_Interact;
    [SerializeField] private Button gamePab_InteractAlt;
    [SerializeField] private Button gamePab_Pause;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;
    [SerializeField] private TextMeshProUGUI moveDownText;
    [SerializeField] private TextMeshProUGUI moveUpText;
    [SerializeField] private TextMeshProUGUI moveLeftText;
    [SerializeField] private TextMeshProUGUI moveRightText;
    [SerializeField] private TextMeshProUGUI interactText;
    [SerializeField] private TextMeshProUGUI interactAltText;
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private TextMeshProUGUI gamePad_InteractText;
    [SerializeField] private TextMeshProUGUI gamePab_InteractAltText;
    [SerializeField] private TextMeshProUGUI gamePab_PauseText;
    [SerializeField] private Transform pressToRebindKeyTransform;

    private Action onMainMenu;
  
    private void Awake()
    {
        Instance = this;
        soundEffectButton.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        musicButton.onClick.AddListener(() =>
        {
            MusicManager.Instance.ChangeVolume();
            UpdateVisual();
        });
        close.onClick.AddListener(() =>
        {
            Hide();
            onMainMenu();

        });
        moveUp.onClick.AddListener(() => {Rebinding(GameInput.Binding.Move_Up);});
        moveDown.onClick.AddListener(() => { Rebinding(GameInput.Binding.Move_Down); });
        moveLeft.onClick.AddListener(() => { Rebinding(GameInput.Binding.Move_Left); });
        moveRight.onClick.AddListener(() => { Rebinding(GameInput.Binding.Move_Right); });
        interact.onClick.AddListener(() => { Rebinding(GameInput.Binding.Interact); });
        interactAlt.onClick.AddListener(() => { Rebinding(GameInput.Binding.IneractAlternate); });
        pause.onClick.AddListener(() => { Rebinding(GameInput.Binding.Pause); });
        gamePad_Interact.onClick.AddListener(() => { Rebinding(GameInput.Binding.Gamepad_Interact); });
        gamePab_InteractAlt.onClick.AddListener(() => { Rebinding(GameInput.Binding.Gamepad_InteractAlternate); });
        gamePab_Pause.onClick.AddListener(() => { Rebinding(GameInput.Binding.Gamepad_Pause);});
    }
    private void Start()
    {
        KitchenGameManager.Instance.OnGameResume += KitchenGameManager_OnGameResume;
        HideToReBindKey();
        UpdateVisual();
        Hide();
    }

    private void KitchenGameManager_OnGameResume(object sender, System.EventArgs e)
    {
        Hide();
    }

    private void UpdateVisual()
    {
        soundEffectText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetVoulume() * 10).ToString();
        musicText.text = "Music: " + Mathf.Round(MusicManager.Instance.GetVoulume() * 10).ToString();
        moveUpText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Up);
        moveDownText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Down);
        moveLeftText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Left);
        moveRightText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Move_Right);
        interactText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Interact);
        interactAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.IneractAlternate);
        pauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Pause);
        gamePad_InteractText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Interact);
        gamePab_InteractAltText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_InteractAlternate);
        gamePab_PauseText.text = GameInput.Instance.GetBindingText(GameInput.Binding.Gamepad_Pause);
    }
    public void Show(Action onMainMenu)
    {
        this.onMainMenu = onMainMenu;
        gameObject.SetActive(true);
        soundEffectButton.Select();
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    private void ShowToReBindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(true);   
    }
    private void HideToReBindKey()
    {
        pressToRebindKeyTransform.gameObject.SetActive(false);
    }
    private void Rebinding(GameInput.Binding binding)
    {
        ShowToReBindKey();
        GameInput.Instance.RebindBinding(binding,() => {
            HideToReBindKey();
            UpdateVisual();
        });
    }

}
