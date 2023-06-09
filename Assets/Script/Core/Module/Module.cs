using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Module������
    /// </summary>
    public class ModuleManager : BaseManager<ModuleManager>
    {
        public EventModule eventModule { get; set; }
        public MonoModule monoModule { get; set; }
        public PoolModule poolModule { get; set; }
        public ResModule resModule { get; set; }
        public ScenesModule scenesModule { get; set; }
        public UIModule uiModule { get; set; }
        public AudioModule audioModule { get; set; }
    }
}
