using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using AGAC.General;

namespace AGAC.SnakesLadders
{
    [DisallowMultipleComponent]
    public class Dice : MonoBehaviour
    {
        #region UNITY METHODS
        private void Start()
        {
            SetVariables();
            diceRigidBody.maxAngularVelocity = 15;
            CreateSides();
        }
        #endregion

        #region VARIABLES
        [SerializeField]
        private Transform diceObject;
        [SerializeField]
        private LayerMask diceSidesMask;
        [Space]
        [Header("Rolling")]
        [SerializeField]
        private RangedInt RollForce = new RangedInt(10, 35);
        [SerializeField]
        private RangedInt RotationForce = new RangedInt(1, 15);
        [Space]
        [Header("Sides")]
        [SerializeField]
        [Range(4,20)]
        private uint TotalSides = 6;
        [SerializeField]
        private Transform SidesContainer;

        private DiceSide[] Sides;
        private Quaternion diceRotation;
        private Rigidbody diceRigidBody;
        #endregion

        #region PUBLIC METHODS
        public void Roll() 
        {
            Roll(RollForce.Value, RotationForce.Value);
        }
        public void Roll(float rollForce, float rotationForce)
        {
            if (isMoving()) return;
            PrepareRoll();
            ApplyForces(rollForce, rotationForce);
        }

        public int GetValue() 
        {
            DiceSide side = GetDiceSide();
            if (isMoving() || side == null) 
                return 0;
            return side.Value;
        }
        #endregion

        #region PRIVATE METHODS
        #region get value
        private bool isMoving() 
        {
            if (diceRigidBody.angularVelocity.magnitude >= 0.1f) return true;
            if (diceRigidBody.velocity.magnitude >= 0.1f) return true;
            return false;
        }
        private DiceSide GetDiceSide()
        {
            Vector3 origin = diceObject.position + Vector3.up * 3;
            Ray ray = new Ray(origin, Vector3.down);
            RaycastHit info;
            int layer = 1 << GeneralMethods.GetLayer(diceSidesMask);
            if (!Physics.Raycast(ray, out info, 105, layer)) return null;
            return info.collider.GetComponent<DiceSide>();
        }
        #endregion

        #region rolling
        private void ApplyForces(float rollForce, float rotationForce) 
        {
            diceRigidBody.velocity = rollForce * GetRollDirection();
            diceRigidBody.angularVelocity = rotationForce * GetRotationDirection();
        }
        private void PrepareRoll() 
        {
            diceObject.localPosition = Vector3.zero;
            diceObject.localRotation = diceRotation;
            diceRigidBody.useGravity = true;
        }
        private Vector3 GetRollDirection() { return transform.forward; }
        private Vector3 GetRotationDirection() { return transform.right; }
        #endregion

        #region initialize
        private void SetVariables()
        {
            diceRotation = diceObject.localRotation;
            if (diceObject.GetComponent<Rigidbody>() == null)
                diceObject.gameObject.AddComponent<Rigidbody>().useGravity = false;
            diceRigidBody = diceObject.GetComponent<Rigidbody>();
        }
        private void CreateSides()
        {
            if (Sides != null && Sides.Length == TotalSides) return;
            Sides = new DiceSide[TotalSides];
            DiceSide[] sides = GeneralMethods.GetComponentsInChildren<DiceSide>(SidesContainer);
            SetSides(sides);
        }
        private void SetSides(DiceSide[] sides)
        {
            uint sidesToSet = TotalSides;
            if (sides.Length < TotalSides)
                sidesToSet = (uint)sides.Length;

            for (int side = 0; side < sidesToSet; ++side)
                Sides[side] = sides[side];
        }
        #endregion
        #endregion
    }
}