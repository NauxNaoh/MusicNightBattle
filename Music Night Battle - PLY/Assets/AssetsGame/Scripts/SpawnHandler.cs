using Naux.Patterns;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : Singleton<SpawnHandler>
{
    [SerializeField] private MusicNote prefabMusicNote;

    private Queue<MusicNote> queueMusicNote = new Queue<MusicNote>();


    public MusicNote SpawnMusicNote(RectTransform parent)
    {
        var _note = queueMusicNote.Count > 0
            ? queueMusicNote.Dequeue()
            : Instantiate<MusicNote>(prefabMusicNote, parent);

        _note.gameObject.SetActive(true);
        return _note;
    }

    public void PoolingMusicNote(MusicNote note)
    {
        queueMusicNote.Enqueue(note);
        note.gameObject.SetActive(false);
    }
}
