using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootStepSound : MonoBehaviour
{
    private float timer;
    private float timerMax = .1f;
    private Player player;
    private void Awake()
    {
        player = GetComponent<Player>();
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer < timerMax )
        {
            timer = timerMax;
            float volume = 0.5f;
            if(player.IsWalking())
            {
                SoundManager.Instance.PlayFootStepSound(player.transform.position, volume);
            }
        }
    }
}
