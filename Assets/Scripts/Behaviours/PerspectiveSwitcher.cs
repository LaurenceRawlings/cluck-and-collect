using System.Collections;
using UnityEngine;

namespace CluckAndCollect.Behaviours
{
    [RequireComponent(typeof(Camera))]
    public class PerspectiveSwitcher : MonoBehaviour
    {
        public bool IsOrthographic { get; private set; }

        [SerializeField] private float fov = 60f;
        [SerializeField] private float near = .3f;
        [SerializeField] private float far = 1000f;
        [SerializeField] private float orthographicSize = 10f;
        [SerializeField] private float ease;

        private Matrix4x4 _orthographic;
        private Matrix4x4 _perspective;
        private float _aspect;
        private Camera _camera;

        private void Awake()
        {
            _aspect = (float) Screen.width / Screen.height;
            _orthographic = Matrix4x4.Ortho(-orthographicSize * _aspect, orthographicSize * _aspect, -orthographicSize,
                orthographicSize, near, far);
            _perspective = Matrix4x4.Perspective(fov, _aspect, near, far);
            _camera = GetComponent<Camera>();
            _camera.projectionMatrix = _perspective;
            IsOrthographic = false;
        }

        public void Switch(float duration, bool reverse)
        {
            IsOrthographic = !IsOrthographic;
            BlendToMatrix(IsOrthographic ? _orthographic : _perspective, duration, ease, reverse);
        }

        private static Matrix4x4 MatrixLerp(Matrix4x4 from, Matrix4x4 to, float time)
        {
            var ret = new Matrix4x4();
            for (var i = 0; i < 16; i++)
                ret[i] = Mathf.Lerp(from[i], to[i], time);
            return ret;
        }

        private IEnumerator LerpFromTo(Matrix4x4 src, Matrix4x4 dest, float duration, float ease, bool reverse)
        {
            var startTime = Time.time;
            while (Time.time - startTime < duration)
            {
                float step;
                if (reverse) step = 1 - Mathf.Pow(1 - (Time.time - startTime) / duration, ease);
                else step = Mathf.Pow((Time.time - startTime) / duration, ease);
                _camera.projectionMatrix = MatrixLerp(src, dest, step);
                yield return 1;
            }

            _camera.projectionMatrix = dest;
        }

        private Coroutine BlendToMatrix(Matrix4x4 targetMatrix, float duration, float ease, bool reverse)
        {
            StopAllCoroutines();
            return StartCoroutine(LerpFromTo(_camera.projectionMatrix, targetMatrix, duration, ease, reverse));
        }
    }
}