using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    private const string PLAYER_PREFS_SOUND_VOLUME = "SoundVolume";
    private float volume=1f;
    void Awake()
    {
        Instance=this;
        volume=PlayerPrefs.GetFloat(PLAYER_PREFS_SOUND_VOLUME, volume);
    }
    [SerializeField]private AudioClipRefsSO audioClipRedSO;
    void Start()
    {
        DeliverManager.Instance.OnRecipeSuccess+=DeliverManager_OnRecipeSuccess;
        DeliverManager.Instance.OnRecipeFail+=DeliverManager_OnRecipeFail;
        CuttingCounter.OnAnyCut+=CuttingCounter_OnAnyCut;
        PlayerControl.Instance.OnPickedSomething+=PlayerControl_OnPickedSomething;
        BaseCounter.OnAnyObjectPlacedHere+=BaseCounter_OnAnyObjectPlacedHere;
        TrashCounter.OnAnyObjectTrashed+=TrushCounter_OnAnyObjectTrashed;
    }

    private void TrushCounter_OnAnyObjectTrashed(object sender, System.EventArgs e)
    {
        TrashCounter trashCounter=sender as TrashCounter;
        PlaySound(audioClipRedSO.trash,trashCounter.transform.position);
    }

    private void BaseCounter_OnAnyObjectPlacedHere(object sender, System.EventArgs e)
    {
        BaseCounter baseCounter=sender as BaseCounter;
        PlaySound(audioClipRedSO.object_drop,baseCounter.transform.position);
    }

    private void PlayerControl_OnPickedSomething(object sender, System.EventArgs e)
    {
        PlayerControl playerControl=sender as PlayerControl;
        PlaySound(audioClipRedSO.object_pickup,playerControl.transform.position);
    }

    private void CuttingCounter_OnAnyCut(object sender, System.EventArgs e)
    {
        CuttingCounter cuttingCounter=sender as CuttingCounter;
        PlaySound(audioClipRedSO.chop,cuttingCounter.transform.position);
    }

    private void DeliverManager_OnRecipeFail(object sender, System.EventArgs e)
    {
        DeliverManager deliverManager=sender as DeliverManager;
        PlaySound(audioClipRedSO.delivery_fail,deliverManager.transform.position);
    }
    private void DeliverManager_OnRecipeSuccess(object sender, System.EventArgs e)
    {
        DeliverManager deliverManager=sender as DeliverManager;
        PlaySound(audioClipRedSO.delivery_success,deliverManager.transform.position);
    }
    public void PlaySound(AudioClip[] audioClip,Vector3 position,float volume=1f){
        PlaySound(audioClip[Random.Range(0,audioClip.Length)],position,volume);
    }
    public void PlaySound(AudioClip audioClip,Vector3 position,float volumeMultiplier=1f){
        AudioSource.PlayClipAtPoint(audioClip,position,volumeMultiplier*volume);
    }
    public void PlayFootStepSound(Vector3 position,float volume)
    {
        PlaySound(audioClipRedSO.footste,position,volume);
    }
    public void ChangeVolume(){
        volume+=0.1f;
        if (volume>=1.1f)
        {
            volume=0f;
        }
        PlayerPrefs.SetFloat(PLAYER_PREFS_SOUND_VOLUME, volume);
    }
    public float GetVolume(){
        return volume;
    }
    public void PlayCountDownSound()
    {
        PlaySound(audioClipRedSO.warning,Vector3.zero);
    }
    public void PlayStoveWarningSound(Vector3 position){
        PlaySound(audioClipRedSO.warning,position);
    }
}