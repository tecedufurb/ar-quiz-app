/*==============================================================================
Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;

namespace Vuforia {
    /// <summary>
    /// A copy of the DefaultTrackableEventHandler class with some little modifications.
    /// </summary>
    public class MyDefaultTrackableEventHandler : MonoBehaviour,
                                                ITrackableEventHandler {

        #region PRIVATE_MEMBER_VARIABLES

        private TrackableBehaviour mTrackableBehaviour;

        #endregion // PRIVATE_MEMBER_VARIABLES

        [SerializeField] private GameScreenHandle GameScreenBehavior;
        [SerializeField] private GameObject CanvasArrow;
        [SerializeField] private GameObject Timer;
        [SerializeField] private GameObject Character;

        private bool mFirstTime = true;

        #region UNTIY_MONOBEHAVIOUR_METHODS

        void Start() {
            mTrackableBehaviour = GetComponent<TrackableBehaviour>();
            if (mTrackableBehaviour) {
                mTrackableBehaviour.RegisterTrackableEventHandler(this);
            }
        }

        #endregion // UNTIY_MONOBEHAVIOUR_METHODS



        #region PUBLIC_METHODS

        /// <summary>
        /// Implementation of the ITrackableEventHandler function called when the
        /// tracking state changes.
        /// </summary>
        public void OnTrackableStateChanged(
                                        TrackableBehaviour.Status previousStatus,
                                        TrackableBehaviour.Status newStatus) {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
                OnTrackingFound();
            } else {
                OnTrackingLost();
            }
        }

        #endregion // PUBLIC_METHODS



        #region PRIVATE_METHODS


        private void OnTrackingFound() {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents) {
                component.enabled = true;
            }

            // Enable colliders:
            foreach (Collider component in colliderComponents) {
                component.enabled = true;
            }
            
            if (mFirstTime) {
                GameScreenBehavior.DisableTargetPanel();
                DifficultyHandler();
                Character.GetComponent<Rigidbody>().isKinematic = false;

                //Quiz quiz = FindObjectOfType<Quiz>();
                //quiz.InstantiateQuestion();

                mFirstTime = false;
            }

            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
        }

        private void OnTrackingLost() {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);
            Collider[] colliderComponents = GetComponentsInChildren<Collider>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents) {
                component.enabled = false;
            }

            // Disable colliders:
            foreach (Collider component in colliderComponents) {
                if ((component.tag != "Ground") || (component.tag != "Player"))
                    component.enabled = false;
            }
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
        }

        #endregion // PRIVATE_METHODS

        /// <summary>
        /// Active the Arrow or Timer object, depending on the difficulty level chosen previously.
        /// </summary>
        private void DifficultyHandler() {
            switch (QuestionSingleTon.Instance.Difficulty) {
                case Difficulty.EASY:
                    CanvasArrow.SetActive(true);
                    break;
                case Difficulty.HARD:
                    Timer.SetActive(true);
                    break;
            }
        }
    }
}
