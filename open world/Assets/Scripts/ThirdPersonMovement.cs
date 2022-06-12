using System.Collections;
using UnityEngine;
using UnityEngine.iOS;

namespace Assets.Scripts
{
    public class ThirdPersonMovement : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [SerializeField] private float speed = 6f;
        
        [Header("Camera")]
        [SerializeField] private float turnSmoothing = 0.1f;
        [SerializeField] private Transform cam;
        [SerializeField] float turnSmoothVelocity = .1f;
        
        [Header("Gravity")]
        [SerializeField] private float gravity = -9.81f;
        [SerializeField] bool _isGrounded;
        [SerializeField] private float groundCheckradius;
        [SerializeField] private LayerMask groundCheckLayer;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float jumpForce;

        private Vector3 _velocity;
        public Vector3 moveDir;
        void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }


        void Update()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckradius, groundCheckLayer);
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            if (direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                    turnSmoothing);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                _controller.Move(speed * moveDir * Time.deltaTime);
            }

            _velocity.y += gravity * Time.deltaTime;

            if (_isGrounded && _velocity.y < 0f)
            {
                _velocity.y = -2f;
            }

            if (_isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                _velocity.y += jumpForce;
            }

            _controller.Move(_velocity * Time.deltaTime);

           

        }
    }
}
