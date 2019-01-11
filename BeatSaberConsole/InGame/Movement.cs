using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using VRUIControls;

namespace BeatSaberConsole
{
    class Movement : MonoBehaviour
    {
        protected Transform _moverCube;
        protected VRController _grabbingController = null;
        protected Vector3 _grabPos;
        protected Quaternion _grabRot;
        protected Vector3 _realPos;
        protected Quaternion _realRot;
        protected VRPointer _vrPointer;
        protected bool _wasMoving = false;
        protected static Movement _this;

        protected const float MinScrollDistance = 0.25f;
        protected const float MaxLaserDistance = 50;

        protected GameObject hitGO;

        public void Init(Transform moverCube)
        {
            _this = this;
            _vrPointer = GetComponent<VRPointer>();
            _moverCube = moverCube;
        }

        // This code was straight copied from brian's enhanced twitch chat mod. OMEGALUL
        // This code was straight copied from xyonico's camera+ mod, so all credit goes to him :)
        public void Update()
        {
            if (this != _this)
            {
                Destroy(this);
                return;
            }
            
            if (_vrPointer.vrController != null)
            {
                if (_vrPointer.vrController.triggerValue > 0.9f)
                {
                    if (_grabbingController != null) return;
                    if (Physics.Raycast(_vrPointer.vrController.transform.position, _vrPointer.vrController.forward, out var hit, MaxLaserDistance))
                    {
                        if (!hit.transform.name.Contains("Moveable")) return;
                        hitGO = hit.transform.parent.gameObject;
                        _grabbingController = _vrPointer.vrController;
                        if (hitGO.transform.name.IndexOf("Console") != -1)
                            _grabPos = _vrPointer.vrController.transform.InverseTransformPoint(Config.consolePos);
                        else if (hitGO.transform.name.IndexOf("Output") != -1)
                            _grabPos = _vrPointer.vrController.transform.InverseTransformPoint(Config.outputPos);
                        _grabRot = Quaternion.Inverse(_vrPointer.vrController.transform.rotation) * hitGO.transform.rotation;
                    }
                }
            }
            if (_grabbingController == null || !(_grabbingController.triggerValue <= 0.9f)) return;
            _grabbingController = null;
        }

        void LateUpdate()
        {
            if (_grabbingController != null)
            {
                _wasMoving = true;
                var diff = _grabbingController.verticalAxisValue * Time.deltaTime;
                var horizDiff = _grabbingController.horizontalAxisValue * 5;
                if (_grabPos.magnitude > MinScrollDistance)
                    _grabPos -= Vector3.forward * diff;
                else
                    _grabPos -= Vector3.forward * Mathf.Clamp(diff, float.MinValue, 0);

                _realPos = _grabbingController.transform.TransformPoint(_grabPos);
                _realRot = _grabbingController.transform.rotation * _grabRot;

                if (hitGO.transform.name.IndexOf("Console") != -1)
                {
                    Config.consolePos = Vector3.Lerp(Config.consolePos, _realPos, 10 * Time.deltaTime);
                    Config.consoleRot = Quaternion.Slerp(Quaternion.Euler(Config.consoleRot), _realRot, 5 * Time.deltaTime).eulerAngles;
                }else if (hitGO.transform.name.IndexOf("Output") != -1)
                {
                    Config.outputPos = Vector3.Lerp(Config.outputPos, _realPos, 10 * Time.deltaTime);
                    Config.outputRot = Quaternion.Slerp(Quaternion.Euler(Config.outputRot), _realRot, 5 * Time.deltaTime).eulerAngles;
                }
            }
            else if (_wasMoving)
            {
                _wasMoving = false;
            }
        }
    };
}