﻿using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RimThreaded
{

    public class Room_Patch
	{

		internal static void RunDestructivePatches()
		{
			Type original = typeof(Room);
			Type patched = typeof(Room_Patch);
#if RW13
			RimThreadedHarmony.Prefix(original, patched, nameof(AddDistrict));
			RimThreadedHarmony.Prefix(original, patched, nameof(RemoveDistrict));
#endif
			RimThreadedHarmony.Prefix(original, patched, nameof(Notify_RoofChanged));
			RimThreadedHarmony.Prefix(original, patched, nameof(UpdateRoomStatsAndRole));
		}
		public static bool UpdateRoomStatsAndRole(Room __instance)
		{
			lock (__instance)
			{
				__instance.statsAndRoleDirty = false;
#if RW12
				if (!__instance.TouchesMapEdge && __instance.RegionType == RegionType.Normal && __instance.regions.Count <= 36)
#endif
#if RW13
				if (__instance.ProperRoom && __instance.RegionCount <= 36)
#endif
				{
                    DefMap<RoomStatDef, float> stats = __instance.stats;
					if (stats == null)
						stats = new DefMap<RoomStatDef, float>();
					foreach (RoomStatDef def in DefDatabase<RoomStatDef>.AllDefs.OrderByDescending(x => x.updatePriority))
						stats[def] = def.Worker.GetScore(__instance);
					__instance.role = DefDatabase<RoomRoleDef>.AllDefs.MaxBy(x => x.Worker.GetScore(__instance));
					__instance.stats = stats;
				}
				else
				{
					__instance.stats = null;
					__instance.role = RoomRoleDefOf.None;
				}
			}
			return false;
		}

#if RW13
		public static bool AddDistrict(Room __instance, District district)
		{
			bool newRoom = false; //ADDED
			lock (__instance) //ADDED
			{
				if (__instance.districts.Contains(district))
				{
					Log.Error(string.Concat("Tried to add the same district twice to Room. district=", district, ", room=", __instance));
				}
				else
				{
					__instance.districts.Add(district);
					if (__instance.districts.Count == 1)
					{
						newRoom = true;
					}
				}
			}
			if(newRoom)
            {
				lock (__instance.Map.regionGrid)
				{
					__instance.Map.regionGrid.allRooms.Add(__instance);
				}
			}
			return false;
		}
		public static bool RemoveDistrict(Room __instance, District district)
		{
			lock (__instance) //ADDED
			{
				if (!__instance.districts.Contains(district))
				{
					Log.Error(string.Concat("Tried to remove district from Room but this district is not here. district=", district, ", room=", __instance));
					return false;
				}

				Map map = __instance.Map;

				List<District> newDistrictList = new List<District>(__instance.districts);
				newDistrictList.Remove(district);
				__instance.districts = newDistrictList;

				if (newDistrictList.Count == 0)
				{

					lock (map.regionGrid) //ADDED
					{
						List<Room> newAllRooms = new List<Room>(map.regionGrid.allRooms);
						newAllRooms.Remove(__instance);
						map.regionGrid.allRooms = newAllRooms;
					}
				}

				__instance.statsAndRoleDirty = true;
			}
			return false;
		}		
#endif
		public static bool Notify_RoofChanged(Room __instance)
		{
			lock (__instance)
			{
				__instance.cachedOpenRoofCount = -1;
#if RW12

				__instance.cachedOpenRoofState = null;
				__instance.Group.Notify_RoofChanged();
#endif
#if RW13
				__instance.tempTracker.RoofChanged();
#endif
			}
			return false;
		}

	}
}
