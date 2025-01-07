using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class AudioClipRefsSO : ScriptableObject
{
    public AudioClip[] chop;//切菜的声音
    public AudioClip[] delivery_fail;//送菜失败的声音
    public AudioClip[] delivery_success;//送菜成功的声音
    public AudioClip[] footste;//脚步声
    public AudioClip[] object_drop;//桌子上物体掉落的声音
    public AudioClip[] object_pickup;//捡起物体的声音
    public AudioClip pickup_food;//煎食物的声音
    public AudioClip[] trash;//丢垃圾的声音
    public AudioClip[] warning;//警告的声音
}
