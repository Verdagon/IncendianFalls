using System;
using System.Collections.Generic;
using IncendianFalls;
using Atharia;
using Atharia.Model;
using UnityEngine;

namespace Domino {
  public class SoundPlayer : MonoBehaviour {
    public AudioSource source;

    private Dictionary<string, AudioClip> clips;

    private AudioClip GetClip(string clipId) {
      if (clips == null) {
        clips = new Dictionary<string, AudioClip>();
      }
      if (clips.ContainsKey(clipId)) {
        return clips[clipId];
      }

      var clip = Resources.Load<AudioClip>(clipId);
      if (clip == null) {
        throw new Exception("Couldn't find " + clipId);
      }

      clips.Add(clipId, clip);
      return clip;
    }

    public void Play(string clipId) {
      source.PlayOneShot(GetClip(clipId));
    }
  }
}
