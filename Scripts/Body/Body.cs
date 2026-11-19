using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GettingRidOfIt
{
    public class Body : InteractItem
    {
        public static Body Instance {  get; private set; }

        [Header("% of the body remaining")]
        [SerializeField]
        [Range(0f, 100f)]
        private float bodyRemaining = 100f;

        [Space(5)]
        [Header("Stinkiness")]
        [SerializeField]
        [Range(0f, 100f)]
        private float stinkiness = 0f;

        [SerializeField] private GameObject[] bodySprites;

        [Required]
        [FoldoutGroup("Body parts")]
        [SerializeField]
        private BodyParts head, rightArm, rightLeg, leftArm, leftLeg;

        [Space(10)]
        [SerializeField] private AudioSource dragAudioSource;


        [Space(10)]
        [Required]
        [Title("Detachable parts, when all of the limbs are removed")]
        [SerializeField] private GameObject[] detachableParts;


        private Player player;

        #region SomeDumb getters and setters

        #region bodyParts
        public BodyParts Head { get => head; }
        public BodyParts RightArm { get => rightArm; }
        public BodyParts LeftArm { get => leftArm; }
        public BodyParts RightLeg { get => rightLeg; }
        public BodyParts LeftLeg { get => leftLeg; }
        #endregion

        /// <summary>
        /// The percentage of the body remaining..
        /// Can not be set under 0 or over 100
        /// </summary>
        public float BodyRemaining
        {
            get
            {
                if (bodyRemaining < 0f) bodyRemaining = 0f;
                else if (bodyRemaining > 100f) bodyRemaining = 100f;

                return bodyRemaining;
            }

            set
            {
                if(value < 0f) value = 0f;
                else if(value > 100f) value = 100f;

                bodyRemaining = value;
            }
        }



        public float Stinkiness
        {
            get
            {
                if(stinkiness < 0f) stinkiness = 0f;
                if (stinkiness > 100f) stinkiness = 100f;

                return stinkiness;
            }
            set
            {
                if(value < 0f) value = 0f;
                if (value > 100f) value = 100f;

                stinkiness = value;
            }
        }

        #endregion
        public override InteractItem GetItem() => this;
        public override Body GetBody() => this;

        private void Awake()
        {
            //instance
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region enable disable
        private void OnEnable()
        {
            GameEvents.OnHourPassed += HourPassed;
        }
        private void OnDisable()
        {
            GameEvents.OnHourPassed -= HourPassed;
        }

        #endregion

        private void Start()
        {
            //set the slider
            UIManager.Instance.SetBodyRemainingSlider(bodyRemaining);
        }




        /// <summary>
        /// When the hour has passed
        /// </summary>
        private void HourPassed()
        {
            GameManager.Instance.SuspicionChange(itemSo.decompositionPerHour);
        }


        /// <summary>
        /// The default interact action for this object
        /// </summary>
        /// <param name="onOrOff"></param>
        public override void DefaultInteract(bool onOrOff)
        {
            interacted = onOrOff;
            

            player = Player.Instance;

            if (onOrOff)
            {
                Debug.Log("set interactions");

                foreach (var sprite in bodySprites)
                {
                    sprite.layer = 9;
                }

                player.Inventory.AddItem(this);

                dragAudioSource.Play();
            }

            else if (!onOrOff)
            {
                foreach (var sprite in bodySprites)
                {
                    sprite.layer = 10;
                }

                player.Inventory.RemoveItem(this);

                dragAudioSource.Stop();
            }

            GameEvents.PlayerDrag(onOrOff);

        }

        


        //Check if the body can be cut in half
        public bool CanDetachRemainingParts()
        {
            //return true if all of the bodyparts are unattached

            return !head.isAttached && !rightArm.isAttached && !leftArm.isAttached && !rightLeg.isAttached && !leftLeg.isAttached;
        }


        /// <summary>
        /// Detach the remaining parts from the body (Cut the torso in half)
        /// </summary>
        public void DetachRemainingParts()
        {
            //additional check here
            if(!CanDetachRemainingParts())
            {
                Debug.Log("Can not detach remaining parts");
                return;
            }

            foreach(var part in detachableParts)
            {
                //instantiates the remaining parts, in this case it cuts the torso in half
                Instantiate(part, transform.position, transform.rotation);
            }

            //DESTROY THE BODY
            Destroy(gameObject);
        }


        public void RemoveBodyPart(BodyParts bodyPart)
        {
            if (bodyPart == null) { Debug.LogError("body part is null"); return; }

            bodyPart.UnAttach();

            //if all of the body parts are unattached, detach the remaining parts
            if (CanDetachRemainingParts())
            {
                DetachRemainingParts();
            }
        }



        public BodyParts[] GetBodyParts()
        {
            return new BodyParts[] { head, rightArm, leftArm, rightLeg, leftLeg };
        }


        private void Update()
        {
            //if player is interacting with this object at this moments
            if (interacted)
            {
                if (player == null) player = Player.Instance;

                if (player == null) { Debug.LogError("wtf? player is still null"); return; }
                if (player.HandTransform == null) { Debug.LogError("wtf? Player has no hand.."); return; }

                //tool laitetaan pelaajan käteen...
                transform.SetPositionAndRotation(player.HandTransform.position, player.HandTransform.rotation);

            }
        }
    }




    [System.Serializable]
    public class BodyParts
    {
        [Required]
        [HideLabel]
        public GameObject bodyPart;
        public bool isAttached = true;

        [Space(10)]
        public GameObject replacementBodyPart;

        public void UnAttach()
        {
            if (!isAttached)
            {
                Debug.Log("body part is already removed");
                return;
            }
            isAttached = false;
            bodyPart.SetActive(false);

            if(replacementBodyPart != null )
            {
                replacementBodyPart = Object.Instantiate(replacementBodyPart, bodyPart.transform.position, Quaternion.identity);
            }
        }
    }
}
