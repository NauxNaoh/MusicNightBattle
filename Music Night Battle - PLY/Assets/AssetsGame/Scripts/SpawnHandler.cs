using Naux.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : Singleton<SpawnHandler>
{
    [SerializeField] private MusicNote prefabMusicNote;

    private Queue<MusicNote> queueMusicNote = new Queue<MusicNote>();


    public MusicNote SpawnMusicNote(RectTransform parent)
    {
        if (queueMusicNote.Count > 0)
            return queueMusicNote.Dequeue();

        return Instantiate<MusicNote>(prefabMusicNote, parent);
    }
}
