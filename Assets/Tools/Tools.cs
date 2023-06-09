using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace UnityEngine
{
    [Serializable]
    public class StringEvent : UnityEvent<string> { }
    [Serializable]
    public class FloatEvent : UnityEvent<float> { }
    [Serializable]
    public class IntEvent : UnityEvent<string> { }
    [Serializable]
    public class BoolEvent : UnityEvent<bool> { }
    [Serializable]
    public class GameObjectEvent : UnityEvent<GameObject> { }
    [Serializable]
    public class Vector2Event : UnityEvent<Vector2> { }

    public class Tools
    {
        public static bool muteSound = false;

        public static bool CheckSurfaces(Vector3[] pos, LayerMask whatIsGround, Transform parent = null, float checksRadius = 0.1f)
        {
            bool result = false;

            foreach (var i in pos)
            {
                result = result || CheckSurface(i + parent.position, whatIsGround, checksRadius);
            }

            return result;
        }

        public static bool CheckSurfaces(Transform[] pos, LayerMask whatIsGround, float checksRadius = 0.1f)
        {
            bool result = false;

            foreach (var i in pos)
            {
                result = result || CheckSurface(i.position, whatIsGround, checksRadius);
            }

            return result;
        }

        public static bool CheckSurface(Vector3 pos, LayerMask whatIsGround, float checksRadius = 0.1f)
        {
            return Physics2D.OverlapCircle(pos, checksRadius, whatIsGround);
        }

        public static AudioSource CreateSound(AudioClip sound, float volume, Vector3 position, Transform parent = null, float spatrialBlend = 1f)
        {
            if (sound == null) return null;

            GameObject gameObject = new GameObject();
            gameObject.name = "Sound: " + sound.name;

            gameObject.transform.position = position;
            gameObject.transform.parent = parent;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = sound;
            audioSource.volume = volume;
            audioSource.spatialBlend = spatrialBlend;
            audioSource.mute = muteSound;
            audioSource.Play();

            DestroyAfterTime destroy = gameObject.AddComponent<DestroyAfterTime>();
            destroy.Play(sound.length + 0.1f);

            return audioSource;
        }

        public static AudioSource CreateSound(AudioClip sound, float volume)
        {
            return CreateSound(sound, volume, Camera.main.transform.position, null, 0f);
        }

        public static bool GetKeys(KeyCode[] buttons)
        {
            bool buttonPressed = false;

            foreach (var i in buttons)
            {
                buttonPressed = buttonPressed || Input.GetKey(i);
            }

            return buttonPressed;
        }

        public static bool GetKeysDown(KeyCode[] buttons)
        {
            bool buttonPressed = false;

            foreach (var i in buttons)
            {
                buttonPressed = buttonPressed || Input.GetKeyDown(i);
            }

            return buttonPressed;
        }

        public static bool GetKeysUp(KeyCode[] buttons)
        {
            bool buttonPressed = false;

            foreach (var i in buttons)
            {
                buttonPressed = buttonPressed || Input.GetKeyUp(i);
            }

            return buttonPressed;
        }

        public static Vector2 ScreenToWorldPoint(Vector3 pos)
        {
            return Camera.main.ScreenToWorldPoint(pos);
        }

        static string loadLevelTeg = "";
        const string LOADLEVELTIGGER = "end";

        public static void ReloadScene()
        {
            LoadSceneAnimation();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void LoadScene(string name)
        {
            LoadSceneAnimation();
            SceneManager.LoadScene(name);
        }

        public static void LoadNextScene()
        {
            LoadSceneAnimation();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        private static void LoadSceneAnimation()
        {
            if (loadLevelTeg == "")
                return;
            GameObject.FindGameObjectWithTag(loadLevelTeg).GetComponent<Animator>().SetTrigger(LOADLEVELTIGGER);
        }

        public static float Sign(float num, float zone = 0)
        {
            if (num > zone)
                return 1;
            if (num < -zone)
                return -1;
            return 0;
        }

        public static Vector2 RawVector(Vector2 vec, float zone = 0)
        {
            var result = Vector2.zero;
            result.x = Sign(vec.x, zone);
            result.y = Sign(vec.y, zone);

            return result;
        }

        public static Vector2 Rotate(Vector2 v, float delta)
        {
            delta *= Mathf.Deg2Rad;

            return new Vector2(
                v.x * Mathf.Cos(delta) - v.y * Mathf.Sin(delta),
                v.x * Mathf.Sin(delta) + v.y * Mathf.Cos(delta)
            );
        }

        public static RaycastHit2D CheckCollision(Vector2 pos, LayerMask mask)
        {
            Ray ray = Camera.main.ScreenPointToRay(Camera.main.WorldToScreenPoint(pos));
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, mask);

            return hit;
        }

        public static float ToNormal(float angle)
        {
            float minDelta = Mathf.Infinity;
            float result = 0;

            for (int i = 0; i < 4; i++)
            {
                float normal = i * 90;
                float deltaAngle = Mathf.Abs(Mathf.DeltaAngle(angle, normal));
                if (deltaAngle < minDelta)
                {
                    minDelta = deltaAngle;
                    result = normal;
                }
            }

            return result;
        }
    }
}
