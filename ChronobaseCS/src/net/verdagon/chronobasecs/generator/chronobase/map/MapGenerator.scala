package net.verdagon.chronobasecs.generator.chronobase.map

import net.verdagon.chronobasecs.compiled.{MapS, MutableS, StructS}
import net.verdagon.chronobasecs.generator.chronobase.Generator.toCS
import net.verdagon.chronobasecs.generator.chronobase.ChronobaseOptions

object MapGenerator {
  def generateHandles(opt: ChronobaseOptions, map: MapS): Map[String, String] = {
    map.mutability match {
      case MutableS => {
        MutMapHandle.generateHandle(opt, map) ++
        MutMapIncarnation.generateIncarnation(opt, map) ++
        MutMapEffects.generateEffects(opt, map)
      }
    }
  }

  def generateRootMethods(opt: ChronobaseOptions, map: MapS): String = {
    map.mutability match {
      case MutableS => {
        MutMapRootMethods.generateRootMapMethods(opt, map)
      }
    }
  }

  def generateEffectBroadcasterMembers(opt: ChronobaseOptions, map: MapS): String = {
    val structCSType = toCS(map.tyype)
    s"""
       |  readonly SortedDictionary<int, List<I${structCSType}EffectObserver>> observersFor${structCSType} =
       |      new SortedDictionary<int, List<I${structCSType}EffectObserver>>();
       |""".stripMargin
  }

  def generateGlobalVisitorInterfaceMethods(map: MapS) = {
    MutMapEffects.generateGlobalVisitorInterfaceMethods(map)
  }

  def generateEffectBroadcasterMethods(map: MapS) = {
    MutMapEffects.generateEffectBroadcasterMethods(map)
  }

  def generateEffectApplierMethods(map: MapS): String = {
    MutMapEffects.generateEffectApplierMethods(map)
  }
}
