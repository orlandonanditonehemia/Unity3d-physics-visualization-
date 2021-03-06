﻿// Copyright (c) 2018 Archy Piragkov. All Rights Reserved.  Licensed under the MIT license
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Artics.Physics.UnityPhisicsVisualizers.Base
{
    /// <summary>
    /// Base visualizator class
    /// You can enable and disable visualization by <see cref = "IsVisible" /> parameter
    /// If collider don't change his Offset, Size, and Direction - you can disable <see cref="DynamicBounds"/> to increase performance
    /// </summary>
    [ExecuteInEditMode]
    public class BaseVisualizer : MonoBehaviour
    {
        /// <summary>
        /// Enables or disables rendering of collider
        /// </summary>
        public bool IsVisible = true;
        public bool DrawEdgesId;


        /// <summary>
        /// Updates bounds of collider every time <see cref="OnDrawGizmos"/> calls. Useful when you changing Offset, Size or Direction of the collider. If you don't just disable to increase performance.
        /// </summary>
        public bool DynamicBounds = true;

        public Color Color = Color.white;

        protected Vector2[] MultipliedPoints;
        protected int PointsLenght;

        private void Awake()
        {
            if (CheckDuplicatedComponenets())
                return;

            Init();
        }

        protected bool CheckDuplicatedComponenets()
        {
            var visualizer = GetComponent<BaseVisualizer>();

            if (visualizer != this && visualizer != null && visualizer.GetType() == this.GetType())
            {
                DestroyImmediate(this);
                return true;
            }

            return false;
        }

        [ContextMenu("Init")]
        public virtual void Init()
        {
            UpdateBounds();
        }

        void OnDrawGizmos()
        {
            if (!IsVisible)
                return;

            OnGizmos();
        }

        public virtual void OnGizmos()
        {
            if (DynamicBounds)
                UpdateBounds();

            MultiplyMatrix();
            Draw();
        }

        /// <summary>
        /// Update bounds of collider manually.  Use it if you changed Offset, Size or Direction of the collider.
        /// </summary>
        public virtual void UpdateBounds()
        {

        }

        protected virtual void MultiplyMatrix()
        {

        }

        protected virtual void Draw()
        {
            Gizmos.color = Color;

            for (int i = 0; i < PointsLenght - 1; i++)
                Gizmos.DrawLine(MultipliedPoints[i], MultipliedPoints[i + 1]);

            DrawEdgesIdHandles();
        }

        protected virtual void DrawEdgesIdHandles()
        {
#if UNITY_EDITOR
            if (DrawEdgesId)
                for (int i = 0; i < PointsLenght; i++)
                    Handles.Label(MultipliedPoints[i], i.ToString());
#endif
        }


        public virtual IDrawData CreateDrawData()
        {
            return null;
        }

    }
}
