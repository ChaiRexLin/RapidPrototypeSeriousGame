using UnityEngine;

public class SoundManager : MonoBehaviour {
    public static SoundManager Instance { get; private set; }

    private const string PLAYER_PREFS_SOUND_EFFECTS_VOLUME = "SoundEffectsVolume";

    [SerializeField] private AudioClipRefSO audioClipRefsSO;

    private float volume = 10f;

    private void Awake() {
        Instance = this;

        volume = PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, 0.5f);
    }

    //========================================================================================

    public void SoundGameOver(Vector3 position) {
        PlaySound(audioClipRefsSO.gameOver, position);
    }
    public void SoundVictory(Vector3 position) {
        PlaySound(audioClipRefsSO.victory, position);
    }
    public void SoundEating(Vector3 position) {
        PlaySound(audioClipRefsSO.eating, position);
    }
    public void SoundFootsteps(Vector3 position) {
        PlaySound(audioClipRefsSO.footsteps, position);
    }
    public void SoundRustling(Vector3 position) {
        PlaySound(audioClipRefsSO.rustling, position);
    }
    //========================================================================================

    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volumeMultiplier = 1f) {
        PlaySound(audioClipArray[Random.Range(0, audioClipArray.Length)], position, volumeMultiplier * volume);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
        AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier * volume);
    }

    public void ChangeVolume(float newVolume) {
        volume = newVolume;
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_EFFECTS_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume() {
        return volume;
    }
}

