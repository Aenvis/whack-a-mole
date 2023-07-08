using UnityEngine;
using System.Collections.Generic;

    [CreateAssetMenu(fileName = "NewAudioPlaylist", menuName = "AudioData/New Playlist", order = 0)]
    public class AudioData : ScriptableObject
    {
        public List<AudioClip> gameSoundtrackPlaylist;

        public AudioClip GetRandom()
        {
            var id = Random.Range(0, gameSoundtrackPlaylist.Count);
            return gameSoundtrackPlaylist[id];
        }
    }
