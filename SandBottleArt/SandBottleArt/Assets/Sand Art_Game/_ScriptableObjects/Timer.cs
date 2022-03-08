using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class Timer : MonoBehaviour
    {
        static Timer instance;


        static void CreateTimer()
        {
            GameObject go = new GameObject("Timer");
            go.AddComponent<Timer>();
            instance = go.GetComponent<Timer>();
        }


        public static void Delay(float delay,System.Action callback)
        {

            if (instance == null)
            {
                CreateTimer();
            }


            instance.StartCoroutine(instance.DelayAction(delay,false, callback));
        }

        public static void DelayUnscaled(float delay, System.Action callback)
        {

            if (instance == null)
            {
                CreateTimer();
            }

            instance.StartCoroutine(instance.DelayAction(delay,true, callback));
        }

        IEnumerator DelayAction(float delay,bool unscaled, System.Action callback)
        {
            if (unscaled)
            {
                yield return new WaitForSecondsRealtime(delay);
            }
            else
            {
                yield return new WaitForSeconds(delay);
            }
            callback.Invoke();
        }

        public static void Delay(float delay, System.Action<object[]> callback,params object[] objs)
        {

            if (instance == null)
            {
                CreateTimer();
            }


            instance.StartCoroutine(instance.DelayAction(delay, callback,objs));
        }

        IEnumerator DelayAction(float delay, System.Action<object[]> callback,object[] objs)
        {
            yield return new WaitForSeconds(delay);
            callback.Invoke(objs);
        }

        public static void Periodic(float delay,int totalTimes,System.Action action)
        {
            if (instance == null)
            {
                CreateTimer();
            }

            instance.StartCoroutine(instance.PeriodicAction(delay, totalTimes, action));
        }

        IEnumerator PeriodicAction(float delay, int totalTimes, System.Action action)
        {
            if (totalTimes == -1)
            {
                yield return new WaitForSeconds(delay);
                action.Invoke();
                StartCoroutine(PeriodicAction(delay, totalTimes, action));
            }
            else
            {
                for (int i = 0; i < totalTimes; i++)
                {
                    yield return new WaitForSeconds(delay);
                    action.Invoke();
                }
            }
        }

        public static void Sequenced(float delay,params System.Action[] actions)
        {
            if (instance == null)
            {
                CreateTimer();
            }

            instance.StartCoroutine(instance.SequencedAction(delay, actions));
        }


        IEnumerator SequencedAction(float delay, System.Action[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                yield return new WaitForSeconds(delay);
                actions[i].Invoke();
            }
        }

        public static void Sequenced(float[] delay, params System.Action[] actions)
        {
            if (instance == null)
            {
                CreateTimer();
            }

            instance.StartCoroutine(instance.SequencedAction(delay, actions));
        }


        IEnumerator SequencedAction(float[] delay, System.Action[] actions)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                yield return new WaitForSeconds(delay[i]);
                actions[i].Invoke();
            }
        }
    }
