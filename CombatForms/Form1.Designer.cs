namespace CombatForms
{
    partial class Form1
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.PlayerHealth = new System.Windows.Forms.ProgressBar();
            this.PlayerStamina = new System.Windows.Forms.ProgressBar();
            this.EnemyHealth = new System.Windows.Forms.ProgressBar();
            this.EnemyStamina = new System.Windows.Forms.ProgressBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(113, 549);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 60);
            this.button1.TabIndex = 0;
            this.button1.Text = "Attack";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Attack_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(736, 549);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(150, 60);
            this.button2.TabIndex = 1;
            this.button2.Text = "Defend";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Defend_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1308, 549);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 60);
            this.button3.TabIndex = 2;
            this.button3.Text = "End Turn";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.EndTurn_Click);
            // 
            // PlayerHealth
            // 
            this.PlayerHealth.Location = new System.Drawing.Point(113, 357);
            this.PlayerHealth.Name = "PlayerHealth";
            this.PlayerHealth.Size = new System.Drawing.Size(100, 23);
            this.PlayerHealth.TabIndex = 3;
            this.PlayerHealth.Click += new System.EventHandler(this.PlayerHealth_Click);
            // 
            // PlayerStamina
            // 
            this.PlayerStamina.Location = new System.Drawing.Point(113, 418);
            this.PlayerStamina.Name = "PlayerStamina";
            this.PlayerStamina.Size = new System.Drawing.Size(100, 23);
            this.PlayerStamina.TabIndex = 4;
            // 
            // EnemyHealth
            // 
            this.EnemyHealth.Location = new System.Drawing.Point(1358, 357);
            this.EnemyHealth.Name = "EnemyHealth";
            this.EnemyHealth.Size = new System.Drawing.Size(100, 23);
            this.EnemyHealth.TabIndex = 5;
            this.EnemyHealth.Click += new System.EventHandler(this.EnemyHealth_Click);
            // 
            // EnemyStamina
            // 
            this.EnemyStamina.Location = new System.Drawing.Point(1358, 418);
            this.EnemyStamina.Name = "EnemyStamina";
            this.EnemyStamina.Size = new System.Drawing.Size(100, 23);
            this.EnemyStamina.TabIndex = 6;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(113, 331);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "Health";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(113, 392);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 8;
            this.textBox2.Text = "Stamina";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(1358, 331);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 9;
            this.textBox3.Text = "Health";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(1358, 392);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 10;
            this.textBox4.Text = "Stamina";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1607, 817);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.EnemyStamina);
            this.Controls.Add(this.EnemyHealth);
            this.Controls.Add(this.PlayerStamina);
            this.Controls.Add(this.PlayerHealth);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ProgressBar PlayerHealth;
        private System.Windows.Forms.ProgressBar PlayerStamina;
        private System.Windows.Forms.ProgressBar EnemyHealth;
        private System.Windows.Forms.ProgressBar EnemyStamina;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
    }
}

