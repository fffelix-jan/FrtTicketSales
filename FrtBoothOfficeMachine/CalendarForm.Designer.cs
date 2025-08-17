namespace FrtBoothOfficeMachine
{
    partial class CalendarForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.MainClassicMonthCalendar = new ClassicMonthCalendar();
            this.SuspendLayout();
            // 
            // MainClassicMonthCalendar
            // 
            this.MainClassicMonthCalendar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.MainClassicMonthCalendar.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainClassicMonthCalendar.Location = new System.Drawing.Point(18, 18);
            this.MainClassicMonthCalendar.MaxSelectionCount = 1;
            this.MainClassicMonthCalendar.Name = "MainClassicMonthCalendar";
            this.MainClassicMonthCalendar.ShowWeekNumbers = true;
            this.MainClassicMonthCalendar.TabIndex = 0;
            this.MainClassicMonthCalendar.TitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(160)))), ((int)(((byte)(193)))));
            // 
            // CalendarForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(199)))), ((int)(((byte)(199)))), ((int)(((byte)(199)))));
            this.ClientSize = new System.Drawing.Size(623, 468);
            this.Controls.Add(this.MainClassicMonthCalendar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalendarForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "CalendarForm";
            this.ResumeLayout(false);

        }

        #endregion

        private ClassicMonthCalendar MainClassicMonthCalendar;
    }
}