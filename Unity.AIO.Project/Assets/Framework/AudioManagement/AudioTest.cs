using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

namespace Framework.Samples.AudioManagement
{
    public class AudioTest : MonoBehaviour
    {
        [SerializeField] private AudioCue _vineBoom;

        // Update is called once per frame
        void Update()
        {
#if ENABLE_INPUT_SYSTEM
            if (Keyboard.current[Key.Space].wasPressedThisFrame)
            {
                _vineBoom.Play(Random.insideUnitSphere * 2f);
            }
#else
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _vineBoom.Play(Random.insideUnitSphere * 2f);
            }
#endif
        }
    }
}