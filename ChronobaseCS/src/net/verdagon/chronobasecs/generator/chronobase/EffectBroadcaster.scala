package net.verdagon.chronobasecs.generator.chronobase

import net.verdagon.chronobasecs.compiled.{MutableS, SuperstructureS}
import net.verdagon.chronobasecs.generator.chronobase.interface.InterfaceGenerator
import net.verdagon.chronobasecs.generator.chronobase.list.ListGenerator
import net.verdagon.chronobasecs.generator.chronobase.map.MapGenerator
import net.verdagon.chronobasecs.generator.chronobase.set.SetGenerator
import net.verdagon.chronobasecs.generator.chronobase.struct.StructGenerator

object EffectBroadcaster {

  def generateEffectBroadcaster(opt: ChronobaseOptions, ss: SuperstructureS): String = {
    val rootStructName =
      ss.structs.find(_.isRoot) match {
        case None => throw new RuntimeException("No root struct!")
        case Some(struct) => struct.name
      }
    val instanceTypeNames =
      (ss.structs.filter(_.mutability == MutableS).map(_.name) ++
        ss.lists.filter(_.mutability == MutableS).map(_.name) ++
        ss.sets.filter(_.mutability == MutableS).map(_.name) ++
        ss.maps.filter(_.mutability == MutableS).map(_.name))

    s"""
       |public class EffectBroadcaster : IEffectVisitor {
       |
       |  List<IEffectObserver> globalEffectObservers;
       |
         |""".stripMargin +
      ss.structs.filter(_.mutability == MutableS).map(struct => {
        StructGenerator.generateEffectBroadcasterMembers(opt, struct)
      }).mkString("") +
      ss.lists.filter(_.mutability == MutableS).map(list => {
        ListGenerator.generateEffectBroadcasterMembers(opt, list)
      }).mkString("") +
      ss.sets.filter(_.mutability == MutableS).map(set => {
        SetGenerator.generateEffectBroadcasterMembers(opt, set)
      }).mkString("") +
      ss.maps.filter(_.mutability == MutableS).map(map => {
        MapGenerator.generateEffectBroadcasterMembers(opt, map)
      }).mkString("") +
      s"""
         |  public EffectBroadcaster() {
         |    globalEffectObservers = new List<IEffectObserver>();
         |  }
         |
         |  public void AddGlobalObserver(IEffectObserver obs) {
         |    this.globalEffectObservers.Add(obs);
         |  }
         |
         |  public void RemoveGlobalObserver(IEffectObserver obs) {
         |    this.globalEffectObservers.Remove(obs);
         |  }
         |
       |""".stripMargin +
      ss.structs.filter(_.mutability == MutableS).map(struct => {
        StructGenerator.generateEffectBroadcasterMethods(struct)
      }).mkString("") +
      ss.lists.filter(_.mutability == MutableS).map(list => {
        ListGenerator.generateEffectBroadcasterMethods(list)
      }).mkString("") +
      ss.sets.filter(_.mutability == MutableS).map(set => {
        SetGenerator.generateEffectBroadcasterMethods(set)
      }).mkString("") +
      ss.maps.filter(_.mutability == MutableS).map(map => {
        MapGenerator.generateEffectBroadcasterMethods(map)
      }).mkString("") +
      s"""
         |  public void Broadcast(IEffect effect) {
         |    foreach (var obs in globalEffectObservers) {
         |      obs(effect);
         |    }
         |    effect.visitIEffect(this);
         |  }
         |}
         """.stripMargin
  }
}
