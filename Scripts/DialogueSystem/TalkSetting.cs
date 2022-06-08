using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Dialogue
{
    [System.Serializable]
    public class TalkSetting {
        public string name;

        [TextArea(3, 10)]
        public string[] sentence;
    }
}
