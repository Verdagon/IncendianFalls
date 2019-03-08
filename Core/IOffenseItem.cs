using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IOffenseItem {
  IOffenseItem AsIOffenseItem();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IOffenseItem that);
  bool NullableIs(IOffenseItem that);
  ITerrainTileComponent AsITerrainTileComponent();
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  IItem AsIItem();
  int AffectOutgoingDamage(int outgoingDamage);
  IItem ClonifyAndReturnNewReal(Root newRoot);
  Void Destruct();
}
}
