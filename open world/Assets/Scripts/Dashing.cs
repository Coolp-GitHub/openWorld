using System.Collections;
using UnityEngine;

namespace Assets.Scripts
{
    public class Dashing : MonoBehaviour
    {
        [SerializeField] private CharacterController _controller;
        [Header("Dashing")] 
        [SerializeField] private float dashSpeed;
        [SerializeField] private float dashTime;
    
        private float _firstClickTime;
        private float _timeSinceLastClick = 0f;

        private bool _canDash = true;
    
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.W) && _canDash)
            {
           
                _timeSinceLastClick = Time.time - _firstClickTime;
                if (_timeSinceLastClick <= .3f)
                {
                    StartCoroutine(ForwardStrafe());
                    _canDash = false;
                    StartCoroutine(DashWaitTimer());
                }

                _firstClickTime = Time.time;

            
            }
        }
        IEnumerator ForwardStrafe()
        {
            float startTime = Time.time;
    
            while (Time.time < startTime + dashTime) {
    
                _controller.Move(transform.GetComponent<ThirdPersonMovement>().moveDir * dashSpeed * Time.deltaTime);
                yield return null; 
            }
    
        }

        IEnumerator DashWaitTimer()
        {
            yield return new WaitForSeconds(0.5f);
            _canDash = true;
        }
    }
}
