using UnityEngine;
using Zenject;

namespace ECM.Examples
{
    public sealed class FollowCameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _targetTransform;

        [SerializeField]
        private float _distanceToTarget = 15.0f;

        [SerializeField]
        private float _followSpeed = 3.0f;

        [SerializeField]
        private float _rotationSpeed = 2.0f;

        [SerializeField]
        private Vector3 _offset;

        private Vector2 _rotationInput;
        private float _currentHorizontalAngle;
        private float _currentVerticalAngle;

        public Transform targetTransform
        {
            get { return _targetTransform; }
            set { _targetTransform = value; }
        }

        public float distanceToTarget
        {
            get { return _distanceToTarget; }
            set { _distanceToTarget = Mathf.Max(0.0f, value); }
        }

        public float followSpeed
        {
            get { return _followSpeed; }
            set { _followSpeed = Mathf.Max(0.0f, value); }
        }

        public float rotationSpeed
        {
            get { return _rotationSpeed; }
            set { _rotationSpeed = Mathf.Max(0.0f, value); }
        }

        private Vector3 cameraRelativePosition
        {
            get { return targetTransform.position - transform.forward * distanceToTarget + _offset; }
        }

        public void OnValidate()
        {
            distanceToTarget = _distanceToTarget;
            followSpeed = _followSpeed;
            rotationSpeed = _rotationSpeed;
        }

        public void Awake()
        {
            // Инициализируем углы вращения на основе текущего направления камеры
            Vector3 currentDirection = transform.forward;
            _currentHorizontalAngle = Mathf.Atan2(currentDirection.x, currentDirection.z) * Mathf.Rad2Deg;
            _currentVerticalAngle = Mathf.Asin(currentDirection.y) * Mathf.Rad2Deg;

            transform.position = cameraRelativePosition;
        }

        public void Update()
        {
            HandleRotationInput();
        }

        public void LateUpdate()
        {
            UpdateCameraRotation();
            transform.position = Vector3.Lerp(transform.position, cameraRelativePosition, followSpeed * Time.deltaTime);
        }

        private void HandleRotationInput()
        {
            // Обработка ввода мыши
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                _rotationInput.x = Input.GetAxis("Mouse X");
                _rotationInput.y = Input.GetAxis("Mouse Y");
            }
            // Обработка тачей (для мобильных устройств)
            else if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Moved)
                {
                    _rotationInput.x = touch.deltaPosition.x * 0.01f;
                    _rotationInput.y = touch.deltaPosition.y * 0.01f;
                }
            }
            else
            {
                _rotationInput = Vector2.zero;
            }
        }

        private void UpdateCameraRotation()
        {
            if (_rotationInput != Vector2.zero)
            {
                // Обновляем углы вращения без ограничений
                _currentHorizontalAngle += _rotationInput.x * rotationSpeed;
                _currentVerticalAngle -= _rotationInput.y * rotationSpeed;

                // Создаем кватернион вращения
                Quaternion targetRotation = Quaternion.Euler(_currentVerticalAngle, _currentHorizontalAngle, 0f);
                transform.rotation = targetRotation;
            }
        }

        [Inject]
        private void Construct(UserPlayer player)
        {
            _targetTransform = player.transform;

            // При установке таргета смотрим в том же направлении
            if (player.transform != null)
            {
                transform.rotation = player.transform.rotation;
                Vector3 currentDirection = transform.forward;
                _currentHorizontalAngle = Mathf.Atan2(currentDirection.x, currentDirection.z) * Mathf.Rad2Deg;
                _currentVerticalAngle = Mathf.Asin(currentDirection.y) * Mathf.Rad2Deg;
            }
        }
    }
}