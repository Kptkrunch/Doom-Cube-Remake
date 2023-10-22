using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TitleScreen : MonoBehaviour
    {
        public GameObject newGameButton;
        public GameObject continueButton;
        public GameObject optionsButton;
        public GameObject quitButton;

        private int _currentSelection;

        private void Start()
        {
            // Set the initial selection to the new game button
            _currentSelection = 0;
            newGameButton.GetComponent<Button>().Select();
        }

        private void Update()
        {
            // Check for input from the controller
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            switch (horizontal)
            {
                // Move the selection based on input
                case > 0.5f:
                {
                    _currentSelection++;
                    if (_currentSelection > 3)
                    {
                        _currentSelection = 0;
                    }

                    break;
                }
                case < -0.5f:
                {
                    _currentSelection--;
                    if (_currentSelection < 0)
                    {
                        _currentSelection = 3;
                    }

                    break;
                }
            }

            // Update the selected button
            switch (_currentSelection)
            {
                case 0:
                    newGameButton.GetComponent<Button>().Select();
                    break;
                case 1:
                    continueButton.GetComponent<Button>().Select();
                    break;
                case 2:
                    optionsButton.GetComponent<Button>().Select();
                    break;
                case 3:
                    quitButton.GetComponent<Button>().Select();
                    break;
            }

            // Check for button press
            if (Input.GetButtonDown($"AButton"))
                switch (_currentSelection)
                {
                    case 0:
                        // Start a new game
                        break;
                    case 1:
                        // Continue the last game
                        break;
                    case 2:
                        // Open the options menu
                        break;
                    case 3:
                        // Quit to desktop
                        Application.Quit();
                        break;
                }
        }
    }
}

