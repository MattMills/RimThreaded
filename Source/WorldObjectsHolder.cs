﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;
using RimWorld.Planet;
using System.Collections.Concurrent;

namespace RimThreaded
{

    public class WorldObjectsHolder_Patch
	{
        public static List<WorldObject> tmpWorldObjects =
            AccessTools.StaticFieldRefAccess<List<WorldObject>>(typeof(WorldObjectsHolder), "tmpWorldObjects");

        public static AccessTools.FieldRef<WorldObjectsHolder, List<WorldObject>> worldObjects =
            AccessTools.FieldRefAccess<WorldObjectsHolder, List<WorldObject>>("worldObjects");
        public static bool WorldObjectsHolderTick(WorldObjectsHolder __instance)
        {
            //tmpWorldObjects.Clear();
            //tmpWorldObjects.AddRange(worldObjects(__instance));
            Ticklist_Patch.tmpWorldObjects = new ConcurrentQueue<WorldObject>(worldObjects(__instance));
            Ticklist_Patch.CreateMonitorThread();
            Ticklist_Patch.MainThreadWaitLoop();
            //for (int index = 0; index < tmpWorldObjects.Count; ++index)
                //tmpWorldObjects[index].Tick();
            return false;
        }

    }
}