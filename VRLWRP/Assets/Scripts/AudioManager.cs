using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    AudioSource audioSource;

    public AudioClip[] goodAnvilHits;
    public AudioClip[] badAnvilHits;

    public AudioClip lobbyMusic;

    public float lobbyMusicVolume;

    public float goodAnvilHitVolume;
    public float badAnvilHitVolume;

    AudioClip previousGoodAnvilHit;
    AudioClip previousBadAnvilHit;

    // Start is called before the first frame update
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)

            //if not, set instance to this
            instance = this;

        //If instance already exists and it's not this:
        else if (instance != this)

            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);

        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGoodAnvilHit()
    {
        AudioClip hitSound;
        do
        {
            hitSound = goodAnvilHits[Random.Range(0, goodAnvilHits.Length - 1)];
        } while (hitSound == previousGoodAnvilHit);

        previousGoodAnvilHit = hitSound;
        audioSource.volume = goodAnvilHitVolume;
        PlaySound(hitSound);
    }

    public void PlayBadAnvilHit()
    {
        AudioClip hitSound;
        do
        {
            hitSound = badAnvilHits[Random.Range(0, badAnvilHits.Length - 1)];
        } while (hitSound == previousBadAnvilHit);

        previousBadAnvilHit = hitSound;
        audioSource.volume = badAnvilHitVolume;
        PlaySound(hitSound);
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(clip);
    }

    public void StopLobbyMusic()
    {
        audioSource.Stop();
    }

    public void PlayLobbyMusic()
    {
        audioSource.volume = lobbyMusicVolume;
        PlaySound(lobbyMusic);
    }
}
