﻿using HarmonyLib;

public class Main
{
    public static Harmony HarmonyInstance;

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
        __instance.transform.Find("Sidebar").gameObject.SetActive(false);
        __instance.transform.Find("SidebarDropShadow").gameObject.SetActive(false);
        __instance.MainPanelRectTransform.offsetMax = new UnityEngine.Vector2(1920, 1080);

        UnityEngine.RectTransform vignette_rt = __instance.transform.Find("MainView/Vignette").GetComponent<UnityEngine.RectTransform>();
        vignette_rt.offsetMax = new UnityEngine.Vector2(960, 540);

        __instance.UnityGameCamera.aspect = 16.0f / 9.0f;

        UnityEngine.RectTransform[] all_rt = UnityEngine.Object.FindObjectsOfType<UnityEngine.RectTransform>();
        foreach (UnityEngine.RectTransform _rt in all_rt) {
            string name = Utils.GetGameObjectPath(_rt.gameObject);
            UnityEngine.Debug.Log($"{name} anchorMax {_rt.anchorMax} anchorMin {_rt.anchorMin} offsetMax {_rt.offsetMax} offsetMin {_rt.anchorMax}");
        }

        GameImpl.Instance.OnScreenResized();
    }
}