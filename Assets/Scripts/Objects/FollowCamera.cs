using System;
using UnityEngine;

namespace Objects
{
    public class FollowCamera : MonoBehaviour
    {
        public Vector3 Offset = new Vector3();
        public float Top = 90f;
        public GameObject BasePosition;
        [HideInInspector]
        public Transform Target;

        private void Update()
        {
            if (Target != null)
            {
                var offset = Offset;
                offset.y += Top;
                transform.position = Target.position + offset;
                transform.LookAt(Target);
            }
        }
        public void SetDefaultPosition() {
            Target = BasePosition.transform;
           
        }
	}
}