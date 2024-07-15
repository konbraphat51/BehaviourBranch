using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BehaviourBranch.UI
{
    public class CommandInput : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent<string> onCommand;

        [SerializeField]
        private KeyCode keyStartingCommand = KeyCode.Return;

        [SerializeField]
        private InputField inputField;

        private bool flagCancelingInput = false;

        private int stopperId;

        private void Update()
        {
            if (!flagCancelingInput && Input.GetKeyDown(keyStartingCommand))
            {
                StartCommanding();
            }
            else if (flagCancelingInput)
            {
                flagCancelingInput = false;
            }
        }

        private void StartCommanding()
        {
            //stop time
            stopperId = TimeManager.Instance.AddStopper();

            //forcus on inputfield
            inputField.interactable = true;
            inputField.ActivateInputField();
        }

        public void Command()
        {
            //revert time
            TimeManager.Instance.RemoveStopper(stopperId);

            //get command
            string command = inputField.text;

            Debug.Log("Recept command: " + command);

            //invoke event
            onCommand.Invoke(command);

            //clear inputfield
            inputField.text = "";

            //unforcus on inputfield
            inputField.DeactivateInputField();
            inputField.interactable = false;

            flagCancelingInput = true;
        }

        public void RegisterCommandEvent(UnityAction<string> action)
        {
            onCommand.AddListener(action);
        }

        public void RemoveCommandEvent(UnityAction<string> action)
        {
            onCommand.RemoveListener(action);
        }
    }
}
