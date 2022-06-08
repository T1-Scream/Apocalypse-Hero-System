using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Dialogue
{
    [System.Serializable]
    public class ChoicesSetting {
        [Min(1)]
        public int sentence;
        public bool Multiple;
        [Header("Left")]
        public string label_left;
        public UnityEvent lEvents;
        [Header("Right")]
        public string label_right;
        public UnityEvent rEvents;
    }
}
