namespace AutoDevSync
{
    partial class DependencyViewer
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
            this.treeDependencies = new System.Windows.Forms.TreeView();
            this.btnChooseProject = new System.Windows.Forms.Button();
            this.ofdProjectSelector = new System.Windows.Forms.OpenFileDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // treeDependencies
            // 
            this.treeDependencies.Location = new System.Drawing.Point(12, 12);
            this.treeDependencies.Name = "treeDependencies";
            this.treeDependencies.Size = new System.Drawing.Size(598, 487);
            this.treeDependencies.TabIndex = 0;
            // 
            // btnChooseProject
            // 
            this.btnChooseProject.Location = new System.Drawing.Point(519, 505);
            this.btnChooseProject.Name = "btnChooseProject";
            this.btnChooseProject.Size = new System.Drawing.Size(91, 23);
            this.btnChooseProject.TabIndex = 1;
            this.btnChooseProject.Text = "Choose project";
            this.btnChooseProject.UseVisualStyleBackColor = true;
            this.btnChooseProject.Click += new System.EventHandler(this.btnChooseProject_Click);
            // 
            // ofdProjectSelector
            // 
            this.ofdProjectSelector.FileName = "openFileDialog1";
            this.ofdProjectSelector.Filter = "VS projects|*.vcxproj;*.vcproj";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 508);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(398, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 510);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Project name";
            // 
            // DependencyViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 594);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnChooseProject);
            this.Controls.Add(this.treeDependencies);
            this.Name = "DependencyViewer";
            this.Text = "DependencyViewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView treeDependencies;
        private System.Windows.Forms.Button btnChooseProject;
        private System.Windows.Forms.OpenFileDialog ofdProjectSelector;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}