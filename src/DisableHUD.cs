﻿using HarmonyLib;

public class Main
{
    public static Harmony HarmonyInstance;
    public static HudBehaviour theHudBehaviour;
    public static float originalSidebarWidth;

    public static void Load()
    {
        // UnityEngine.Application.SetStackTraceLogType(UnityEngine.LogType.Log, UnityEngine.StackTraceLogType.None);

        HarmonyInstance = new Harmony("DisableHUD");
        HarmonyInstance.PatchAll();
        UnityEngine.Debug.Log("[DisableHUD] loaded!");
    }

    public static void Unload()
    {
        UnityEngine.Debug.Log("[DisableHUD] unloading!");
        if (theHudBehaviour != null) {
            theHudBehaviour.SidebarRectTransform.SetInsetAndSizeFromParentEdge(
                UnityEngine.RectTransform.Edge.Right,
                0,
                originalSidebarWidth
            );
            theHudBehaviour.transform.Find("SidebarDropShadow").gameObject.SetActive(true);
            theHudBehaviour.SidebarRectTransform.Find("NamesPanel").gameObject.SetActive(true);
        }
        if (HarmonyInstance != null)
        {
            HarmonyInstance.UnpatchAll();
        }
    }
}

static class Utils
{
    public static string GetGameObjectPath(UnityEngine.GameObject obj)
    {
        string path = "/" + obj.name;
        while (obj.transform.parent != null)
        {
            obj = obj.transform.parent.gameObject;
            path = "/" + obj.name + path;
        }
        return path;
    }
}

[HarmonyPatch(typeof(HudBehaviour), "OnActivate")]
static class SidebarRemoverPostfix
{
    static void Postfix(ref HudBehaviour __instance)
    {
        Main.theHudBehaviour = __instance;
        Main.originalSidebarWidth = __instance.SidebarRectTransform.rect.width;

        __instance.SidebarRectTransform.SetInsetAndSizeFromParentEdge(
            UnityEngine.RectTransform.Edge.Right,
            0,
            0
        );
        __instance.transform.Find("SidebarDropShadow").gameObject.SetActive(false);
        __instance.SidebarRectTransform.Find("NamesPanel").gameObject.SetActive(false);

        // UnityEngine.RectTransform[] all_rt = UnityEngine.Object.FindObjectsOfType<UnityEngine.RectTransform>();
        // foreach (UnityEngine.RectTransform _rt in all_rt) {
        //     string name = Utils.GetGameObjectPath(_rt.gameObject);
        //     UnityEngine.Debug.Log($"{name} anchorMax {_rt.anchorMax} anchorMin {_rt.anchorMin} offsetMax {_rt.offsetMax} offsetMin {_rt.anchorMax}");
        // }

        GameImpl.Instance.OnScreenResized();
    }
}