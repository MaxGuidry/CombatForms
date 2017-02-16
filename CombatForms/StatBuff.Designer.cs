namespace CombatForms
{
    partial class StatBuff
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.Armor = new System.Windows.Forms.Button();
            this.Damage = new System.Windows.Forms.Button();
            this.Speed = new System.Windows.Forms.Button();
            this.Health = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(37, 12);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(191, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "Pick a stat to boost";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Armor
            // 
            this.Armor.Location = new System.Drawing.Point(55, 114);
            this.Armor.Name = "Armor";
            this.Armor.Size = new System.Drawing.Size(160, 70);
            this.Armor.TabIndex = 1;
            this.Armor.Text = "Armor";
            this.Armor.UseVisualStyleBackColor = true;
            this.Armor.Click += new System.EventHandler(this.Armor_Click);
            // 
            // Damage
            // 
            this.Damage.Location = new System.Drawing.Point(55, 38);
            this.Damage.Name = "Damage";
            this.Damage.Size = new System.Drawing.Size(160, 70);
            this.Damage.TabIndex = 2;
            this.Damage.Text = "Damage";
            this.Damage.UseVisualStyleBackColor = true;
            this.Damage.Click += new System.EventHandler(this.Damage_Click);
            // 
            // Speed
            // 
            this.Speed.Location = new System.Drawing.Point(55, 190);
            this.Speed.Name = "Speed";
            this.Speed.Size = new System.Drawing.Size(160, 70);
            this.Speed.TabIndex = 3;
            this.Speed.Text = "Speed";
            this.Speed.UseVisualStyleBackColor = true;
            this.Speed.Click += new System.EventHandler(this.Speed_Click);
            // 
            // Health
            // 
            this.Health.Location = new System.Drawing.Point(55, 266);
            this.Health.Name = "Health";
            this.Health.Size = new System.Drawing.Size(160, 70);
            this.Health.TabIndex = 4;
            this.Health.Text = "Health";
            this.Health.UseVisualStyleBackColor = true;
            this.Health.Click += new System.EventHandler(this.Health_Click);
            // 
            // StatBuff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(263, 377);
            this.Controls.Add(this.Health);
            this.Controls.Add(this.Speed);
            this.Controls.Add(this.Damage);
            this.Controls.Add(this.Armor);
            this.Controls.Add(this.textBox1);
            this.Name = "StatBuff";
            this.Text = "StatBuff";
            this.Load += new System.EventHandler(this.StatBuff_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button Armor;
        private System.Windows.Forms.Button Damage;
        private System.Windows.Forms.Button Speed;
        private System.Windows.Forms.Button Health;
    }
}