using HarmonyLib;
using System.Reflection;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using static Config;
using UnityEngine.Tilemaps;
using Beebyte.Obfuscator;
using ai.behaviours;

namespace Unit
{
    [ObfuscateLiterals]
    public abstract class BaseLibrary
    {
        public string id;
        internal bool created = false;

        public virtual void init()
        {
            Debug.Log($"Unit : Library {this.id} Created");
        }

        public virtual void init_post()
        {
            this.created = true;
        }
    }
}