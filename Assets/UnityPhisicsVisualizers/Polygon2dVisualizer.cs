﻿// Copyright (c) 2018 Archy Piragkov. All Rights Reserved.  Licensed under the MIT license
using UnityEngine;

namespace artics.UnityPhisicsVisualizers
{
    /// <summary>
    /// Draws gizmos of <seealso cref="PolygonCollider2D"/> which attached for current GameObject.
    /// Multiple polygon Paths not supported
    /// </summary>

    [RequireComponent(typeof(PolygonCollider2D))]

    public class Polygon2dVisualizer : BaseVisualizer
    {
        protected PolygonCollider2D Collider;
        protected Vector2[] MultipliedPoints;
        protected int PointsLenght;

        public override void Init()
        {
            Collider = GetComponent<PolygonCollider2D>();
            PointsLenght = Collider.points.Length;
            MultipliedPoints = new Vector2[PointsLenght];

            base.Init();
        }

        protected override void MultiplyMatrix()
        {
            if (MultipliedPoints.Length != PointsLenght)
            {
                PointsLenght = Collider.points.Length;
                MultipliedPoints = new Vector2[PointsLenght];
            }

            for (int i = 0; i < PointsLenght; i++)
                MultipliedPoints[i] = transform.localToWorldMatrix.MultiplyPoint(Collider.points[i] + Collider.offset);
        }

        protected override void Draw()
        {
            Gizmos.color = Color;

            for (int i = 0; i < PointsLenght - 1; i++)
                Gizmos.DrawLine(MultipliedPoints[i], MultipliedPoints[i + 1]);

            Gizmos.DrawLine(MultipliedPoints[0], MultipliedPoints[PointsLenght-1]);
        }

    }
}