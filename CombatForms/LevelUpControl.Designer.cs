namespace CombatForms
{
    partial class LevelUpControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Health = new System.Windows.Forms.Button();
            this.Speed = new System.Windows.Forms.Button();
            this.Damage = new System.Windows.Forms.Button();
            this.Armor = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Health
            // 
            this.Health.Location = new System.Drawing.Point(117, 307);
            this.Health.Name = "Health";
            this.Health.Size = new System.Drawing.Size(160, 70);
            this.Health.TabIndex = 9;
            this.Health.Text = "Health";
            this.Health.UseVisualStyleBackColor = true;
            this.Health.Click += new System.EventHandler(this.Health_Click);
            // 
            // Speed
            // 
            this.Speed.Location = new System.Drawing.Point(117, 231);
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(160, 70);
            this.Speed.TabIndex = 8;
            this.Speed.Text = "Speed";
            this.Speed.UseVisualStyleBackColor = true;
            this.Speed.Click += new System.EventHandler(this.Speed_Click);
            // 
            // Damage
            // 
            this.Damage.Location = new System.Drawing.Point(117, 79);
            this.Damage.Name = "Damage";
            this.Damage.Size = new System.Drawing.Size(160, 70);
            this.Damage.TabIndex = 7;
            this.Damage.Text = "Damage";
            this.Damage.UseVisualStyleBackColor = true;
            this.Damage.Click += new System.EventHandler(this.Damage_Click);
            // 
            // Armor
            // 
            this.Armor.Location = new System.Drawing.Point(117, 155);
            this.Armor.Name = "Armor";
            this.Armor.Size = new System.Drawing.Size(160, 70);
            this.Armor.TabIndex = 6;
            this.Armor.Text = "Armor";
            this.Armor.UseVisualStyleBackColor = true;
            this.Armor.Click += new System.EventHandler(this.Armor_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(99, 53);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(191, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "Pick a stat to boost";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // LevelUpControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Health);
            this.Controls.Add(this.Speed);
            this.Controls.Add(this.Damage);
            this.Controls.Add(this.Armor);
            this.Controls.Add(this.textBox1);
            this.Name = "LevelUpControl";
            this.Size = new System.Drawing.Size(388, 430);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Health;
        private System.Windows.Forms.Button Speed;
        private System.Windows.Forms.Button Damage;
        private System.Windows.Forms.Button Armor;
        private System.Windows.Forms.TextBox textBox1;
    }
}
