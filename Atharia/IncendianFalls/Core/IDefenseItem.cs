using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public interface IDefenseItem {
  IDefenseItem AsIDefenseItem();
  Root root { get; }
  int id { get; }
  void Delete();
  bool Exists();
  void FindReachableObjects(SortedSet<int> foundIds);
  bool Is(IDefenseItem that);
  bool NullableIs(IDefenseItem that);
  IDestructible AsIDestructible();
  IUnitComponent AsIUnitComponent();
  IItem AsIItem();
  int AffectIncomingDamage(int incomingDamage);
  IItem ClonifyAndReturnNewReal(Root newRoot);
  Void Destruct();
}
}
