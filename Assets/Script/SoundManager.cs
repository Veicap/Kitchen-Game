using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private const string PLAYER_PERFS_MUSIC_VOLUME = "SoundEffectsVolume";
    public static SoundManager Instance { get; private set; }
    [SerializeField] private AudioClipRefsSO audioClipRefsSO;
    private float volume = 1f;
    private void Awake()
    {
        Instance = this; 
        volume = PlayerPrefs.GetFloat(PLAYER_PERFS_MUSIC_VOLUME, volume);
    }
    private void Start()
    {
        DeliveryManager.Instance.OnrecipeSuccess += DeliveryManager_OnrecipeSuccess;
        DeliveryManager.Instance.OnRecipeFailed += DeliveryManager_OnRecipeFailed;
        CuttingCounter.OnChop += CuttingCounter_OnChop;
        TrashCounter.OnTrashDrop += TrashCounter_OnTrashDrop;
        Player.Instance.OnPickedUp += Instance_OnPickedUp;
        BaseCounter.OnDropOut += BaseCounter_OnDropOut;
    }

    private void BaseCounter_OnDropOut(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter = (BaseCounter)sender;
        PlaySound(audioClipRefsSO.objectDrop, baseCounter.transform.position);
    }

    private void Instance_OnPickedUp(object sender, System.EventArgs e)
    {
        Player player = Player.Instance;
        PlaySound(audioClipRefsSO.objectPickUp, player.transform.position);
    }

    private void TrashCounter_OnTrashDrop(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter = (TrashCounter)sender;
        PlaySound(audioClipRefsSO.trash, trashCounter.transform.position);
    }

    private void CuttingCounter_OnChop(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter = sender as CuttingCounter;
        PlaySound(audioClipRefsSO.chop, cuttingCounter.transform.position);
    }

    private void DeliveryManager_OnRecipeFailed(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliveryFail, deliveryCounter.transform.position, 3f);
    }

    private void DeliveryManager_OnrecipeSuccess(object sender, System.EventArgs e)
    {
        DeliveryCounter deliveryCounter = DeliveryCounter.Instance;
        PlaySound(audioClipRefsSO.deliverySuccess, deliveryCounter.transform.position, 3f);
    }

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volume);
    }
    private void PlaySound(AudioClip audioClip, Vector3 position, float multiplyVolume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, multiplyVolume * volume);
    }

    public void PlayFootStepSound(Vector3 position, float volume)
    {
        PlaySound(audioClipRefsSO.footstep, position, volume);
    }
    public void PlayCountDownSound()
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero, volume);
    }
    public void PlayWarningSound(Vector3 position )
    {
        PlaySound(audioClipRefsSO.warning, Vector3.zero);
    }
    public void ChangeVolume()
    {
        volume += 0.1f;
        if(volume > 1f)
        {
            volume = 0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PERFS_MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }
    public float GetVoulume()
    {
        return volume;
    }
}
