using HarmonyLib;
using ReflectionUtility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Config;
using NCMS;
using NCMS.Utils;
using Newtonsoft.Json;
using Beebyte.Obfuscator;
using ai.behaviours;

namespace Unit
{
    [ObfuscateLiterals]
    public abstract class Library<T> : BaseLibrary where T : Asset
    {
        public AssetLibrary<T> lib;
        public List<T> list = new List<T>();
        public Dictionary<string, T> dict = new Dictionary<string, T>();
        public List<T> tempList;
        public T t;

        public virtual T add(T pAsset, List<T> pList = null, bool pReset = false)
        {
            this.t = pAsset;
            this.lib.add(pAsset);
            this.list.Add(pAsset);

            if (pList != null) this.tempList = pList;

            if (pReset) this.tempList = null;

            if (this.tempList != null) this.tempList.Add(pAsset);

            return pAsset;
        }

        public virtual T get(string pID)
        {
            T t = default(T);
            this.dict.TryGetValue(pID, out t);
            if (t == null)
            {
                this.lib.dict.TryGetValue(pID, out t);
                if (t == null)
                {
                    Debug.Log("asset " + pID + " not found in lib " + this.id);
                    return default(T);
                }
            }
            return t;
        }
    }
}