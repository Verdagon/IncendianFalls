﻿using System;
using System.Collections.Generic;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using IncendianFalls;

namespace Domino {
  public class CameraAnimator : MonoBehaviour {
    bool initialized = false;

    public GameObject cameraObject;

    public void Init(GameObject cameraObject, IMatrix4x4Animation initialAnimation) {
      this.cameraObject = cameraObject;
      this.cameraAnimation_ = initialAnimation;
      initialized = true;
    }

    IMatrix4x4Animation cameraAnimation_ = new IdentityMatrix4x4Animation();
    public IMatrix4x4Animation cameraAnimation {
      get { return cameraAnimation_; }
      set {
        cameraAnimation_ = value;
        Update();
      }
    }

    void Start() {
      Asserts.Assert(cameraObject != null);
      Asserts.Assert(initialized);
    }

    void Update() {
      cameraAnimation_ = cameraAnimation_.Simplify(Time.time);

      Matrix4x4 matrix = cameraAnimation_.Get(Time.time);
      cameraObject.transform.FromMatrix(matrix);

      if (cameraAnimation_ is ConstantMatrix4x4Animation || cameraAnimation_ is IdentityMatrix4x4Animation) {
        Destroy(this);
      }
    }
  }
}