using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.Events;

public class TimeUtility : MonoSingleton<TimeUtility>
{
    public class Coroutine
    {
        public bool IsRunning { get; private set; } = false;

        public void Cancel()
        {
            IsRunning = false;
        }

        public void Start()
        {
            IsRunning = true;
        }

        public void End()
        {
            IsRunning = false;
        }
    }
    
    public Coroutine RunAfter(Action action, float time)
    {
        var coroutine = new Coroutine();
        DoStartCoroutine(RunAfterDelay(action, time, coroutine));
        return coroutine;
    }

    private IEnumerator RunAfterDelay(Action action, float time, Coroutine coroutine)
    {
        coroutine.Start();
        yield return new WaitForSeconds(time);
        if (coroutine.IsRunning)
            action.Invoke();
        coroutine.End();
    }

    public Coroutine RunAfterFrame(Action action)
    {
        return RunAfterFrames(action, 1);
    }

    public Coroutine RunAfterFrames(Action action, int frames)
    {
        var coroutine = new Coroutine();
        DoStartCoroutine(runAfterFrames(action, frames, coroutine));
        return coroutine;
    }

    private IEnumerator runAfterFrames(Action action, int frames, Coroutine coroutine)
    {
        coroutine.Start();
        for (var i = 0; i < frames; ++i)
        {
            if (!coroutine.IsRunning)
            {
                break;
            }
            yield return false;
        }
        if (coroutine.IsRunning)
            action.Invoke();
        coroutine.End();
    }

    public Coroutine RunOnce(Action action, Func<bool> check)
    {
        var coroutine = new Coroutine();
        DoStartCoroutine(runOnce(action, check, coroutine));
        return coroutine;
    }

    private IEnumerator runOnce(Action action, Func<bool> check, Coroutine coroutine)
    {
        coroutine.Start();
        while (!check.Invoke())
        {
            if (!coroutine.IsRunning)
            {
                break;
            }
            yield return false;
        }
        if (coroutine.IsRunning)
            action.Invoke();
        coroutine.End();
    }

    private void DoStartCoroutine(IEnumerator coroutine)
    {
#if UNITY_EDITOR
        if (!Application.isPlaying && !EditorApplication.isPlayingOrWillChangePlaymode)
        {
            StartEditorCoroutine(coroutine);
            return;
        }
#endif
        StartCoroutine(coroutine);
    }

    [HideInInspector]
    public UnityEvent OnUpdate = new UnityEvent();

    private void Update()
    {
        OnUpdate.Invoke();
    }

    public static void ListenToUpdate(UnityAction action) => instance.OnUpdate.AddListener(action);
    public static void StopListeningToUpdate(UnityAction action) => instance.OnUpdate.RemoveListener(action);
    
#if UNITY_EDITOR
    // This is my callable function
    public static IEnumerator StartEditorCoroutine(IEnumerator newCoroutine)
    {
        CoroutineInProgress.Add(newCoroutine);
        return newCoroutine;
    }
    /// <summary>
    ///  Coroutine to execute. Manage by the EasyLocalization_script
    /// </summary>
    private static List<IEnumerator> CoroutineInProgress = new List<IEnumerator>();
    
    [InitializeOnLoadMethod]
    private static void InitialiseEditor()
    {
        EditorApplication.update += ExecuteCoroutine;
    }
 
    static int currentExecute = 0;
    private static void ExecuteCoroutine()
    {
        if (CoroutineInProgress.Count <= 0)
        {
            return;
        }
 
        currentExecute = (currentExecute + 1) % CoroutineInProgress.Count;
         
        bool finish = !CoroutineInProgress[currentExecute].MoveNext();
 
        if (finish)
        {
            CoroutineInProgress.RemoveAt(currentExecute);
        }
    }
#endif
}

public static class TimeUtils
{
    public static TimeUtility.Coroutine RunAfter(this Action action, float time)
    {
        return TimeUtility.instance.RunAfter(action, time);
    }
    
    public static TimeUtility.Coroutine RunAfter(this UnityEvent action, float time)
    {
        return TimeUtility.instance.RunAfter(action.Invoke, time);
    }
    
    public static TimeUtility.Coroutine RunAfterFrames(this Action action, int frames)
    {
        return TimeUtility.instance.RunAfterFrames(action, frames);
    }
    
    public static TimeUtility.Coroutine RunAfterFrames(this UnityEvent action, int frames)
    {
        return TimeUtility.instance.RunAfterFrames(action.Invoke, frames);
    }
    
    public static TimeUtility.Coroutine RunAfterFrame(this Action action)
    {
        return TimeUtility.instance.RunAfterFrame(action);
    }
    
    public static TimeUtility.Coroutine RunAfterFrame(this UnityEvent action)
    {
        return TimeUtility.instance.RunAfterFrame(action.Invoke);
    }

    public static TimeUtility.Coroutine RunOnce(this Action action, Func<bool> check)
    {
        return TimeUtility.instance.RunOnce(action, check);
    }
    
    public static TimeUtility.Coroutine RunOnce(this UnityEvent action, Func<bool> check)
    {
        return TimeUtility.instance.RunOnce(action.Invoke, check);
    }
}
