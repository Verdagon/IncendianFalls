using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IInertiaItem {
  IInertiaItem AsIInertiaItem();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IInertiaItem that);
  bool NullableIs(IInertiaItem that);
  ITerrainTileComponent AsITerrainTileComponent();
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  IItem AsIItem();
  int AffectInertia(int inertia);
  IItem ClonifyAndReturnNewReal(Root newRoot);
  Void Destruct();
}
}
