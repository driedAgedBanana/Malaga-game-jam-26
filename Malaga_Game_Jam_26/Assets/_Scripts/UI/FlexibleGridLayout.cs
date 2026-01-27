using System;
using UnityEngine;
using UnityEngine.UI;

namespace Bas.Pennings.UnityTools
{
    public class FlexibleGridLayout : LayoutGroup
    {
        [SerializeField] private FitType _fitType;
        [SerializeField] private int _rows;
        [SerializeField] private int _columns;
        [SerializeField] private Vector2 _spacing;
        private Vector2 _cellSize;
        private bool fitX;
        private bool fitY;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            if (_fitType == FitType.Width || _fitType == FitType.Height || _fitType == FitType.Uniform)
            {
                fitX = true;
                fitY = true;

                float sqrRt = Mathf.Sqrt(transform.childCount);
                _rows = Mathf.CeilToInt(sqrRt);
                _columns = Mathf.CeilToInt(sqrRt);
            }

            if (_fitType == FitType.Width || _fitType == FitType.FixedColumns)
            {
                _rows = Mathf.CeilToInt(transform.childCount / (float)_columns);
            }
            if (_fitType == FitType.Height || _fitType == FitType.FixedRows)
            {
                _columns = Mathf.CeilToInt(transform.childCount / (float)_rows);
            }

            float parentWidth = rectTransform.rect.width;
            float parentHeight = rectTransform.rect.height;

            float cellWidth = parentWidth / _columns - (_spacing.x / _columns * 2) - (padding.left / _columns) - (padding.right / _columns);
            float cellHeight = parentHeight / _rows - (_spacing.y / _rows * 2) - (padding.top / _rows) - (padding.bottom / _rows);

            _cellSize.x = fitX ? cellWidth : _cellSize.x;
            _cellSize.y = fitY ? cellHeight : _cellSize.y;

            int rowCount = 0;
            int columnCount = 0;

            for (int i = 0; i < rectChildren.Count; i++)
            {
                rowCount = i / _columns;
                columnCount = i % _columns;

                var item = rectChildren[i];

                var xPos = _cellSize.x * columnCount + (_spacing.x * columnCount) + padding.left;
                var yPos = _cellSize.y * rowCount + (_spacing.y * rowCount) + padding.top;

                SetChildAlongAxis(item, 0, xPos, _cellSize.x);
                SetChildAlongAxis(item, 1, yPos, _cellSize.y);
            }
        }

        public override void SetLayoutHorizontal() { }
        public override void CalculateLayoutInputVertical() { }
        public override void SetLayoutVertical() { }

        public enum FitType
        {
            Uniform,
            Width,
            Height,
            FixedRows,
            FixedColumns,
        }
    }
}
