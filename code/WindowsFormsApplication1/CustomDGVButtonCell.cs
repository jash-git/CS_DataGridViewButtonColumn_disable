﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsApplication1
{
    //C# DataGridViewButtonColumn 支持disable
    //https://www.twblogs.net/a/5c3203a8bd9eee35b3a4f5e7
    public class CustomDGVButtonCell : DataGridViewButtonCell
    {
        public bool Enabled { get; set; }

        // By default, enable the button cell.
        public CustomDGVButtonCell()
        {
            Enabled = true;
        }
        // Override the Clone method so that the Enabled property is copied.
        public override object Clone()
        {
            CustomDGVButtonCell cell = (CustomDGVButtonCell)base.Clone();
            cell.Enabled = Enabled;
            return cell;
        }
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue,
           DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
        }
        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            // The button cell is disabled, so paint the border, 
            // background, and disabled button for the cell.
            if (!this.Enabled)
            {
                // Draw the cell background, if specified.
                if ((paintParts & DataGridViewPaintParts.Background) == DataGridViewPaintParts.Background)
                {
                    SolidBrush cellBackground = new SolidBrush(cellStyle.BackColor);
                    graphics.FillRectangle(cellBackground, cellBounds);
                    cellBackground.Dispose();
                }

                // Draw the cell borders, if specified.
                if ((paintParts & DataGridViewPaintParts.Border) == DataGridViewPaintParts.Border)
                {
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);
                }

                // Calculate the area in which to draw the button.
                Rectangle buttonArea = cellBounds;
                Rectangle buttonAdjustment = BorderWidths(advancedBorderStyle);
                buttonArea.X += buttonAdjustment.X;
                buttonArea.Y += buttonAdjustment.Y;
                buttonArea.Height -= buttonAdjustment.Height;
                buttonArea.Width -= buttonAdjustment.Width;

                // Draw the disabled button.               
                ButtonRenderer.DrawButton(graphics, buttonArea, PushButtonState.Disabled);

                // Draw the disabled button text.
                if (FormattedValue is string)
                {
                    TextRenderer.DrawText(graphics, (string)FormattedValue, DataGridView.Font, buttonArea,
                        SystemColors.GrayText);
                }
            }
            else
            {
                // The button cell is enabled, so let the base class
                // handle the painting.
                base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText,
                    cellStyle, advancedBorderStyle, paintParts);
            }
        }
    }
    public class CustomDGVButtonColumn : DataGridViewButtonColumn
    {
        public CustomDGVButtonColumn()
        {
            CustomDGVButtonCell obj = new CustomDGVButtonCell();
            this.CellTemplate = obj;
            CellTemplate = new CustomDGVButtonCell();
        }
    }
}
