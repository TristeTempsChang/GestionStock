namespace GestionStock
{
    partial class AddProduct
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddProduct));
            this.panel3 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.HomeLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelGeneral = new System.Windows.Forms.Panel();
            this.general = new System.Windows.Forms.Button();
            this.panelAttribute = new System.Windows.Forms.Panel();
            this.attribute = new System.Windows.Forms.Button();
            this.panelAddProduct = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Button();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelGeneral.SuspendLayout();
            this.panelAttribute.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(139)))), ((int)(((byte)(29)))));
            this.panel3.Controls.Add(this.btnExit);
            this.panel3.Controls.Add(this.button4);
            this.panel3.Controls.Add(this.HomeLabel);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(800, 100);
            this.panel3.TabIndex = 7;
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.YellowGreen;
            this.button4.FlatAppearance.BorderSize = 0;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button4.Font = new System.Drawing.Font("Montserrat Medium", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button4.ForeColor = System.Drawing.SystemColors.Control;
            this.button4.Location = new System.Drawing.Point(663, 32);
            this.button4.Margin = new System.Windows.Forms.Padding(5, 12, 8, 3);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(110, 39);
            this.button4.TabIndex = 6;
            this.button4.Text = "Enregistrer";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // HomeLabel
            // 
            this.HomeLabel.AutoSize = true;
            this.HomeLabel.Font = new System.Drawing.Font("Montserrat", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HomeLabel.ForeColor = System.Drawing.SystemColors.Control;
            this.HomeLabel.Location = new System.Drawing.Point(12, 32);
            this.HomeLabel.Name = "HomeLabel";
            this.HomeLabel.Size = new System.Drawing.Size(363, 33);
            this.HomeLabel.TabIndex = 0;
            this.HomeLabel.Text = "Ajouter un nouveau produit";
            this.HomeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.13636F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.86364F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 536F));
            this.tableLayoutPanel1.Controls.Add(this.panelGeneral, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelAttribute, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 100);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 51);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // panelGeneral
            // 
            this.panelGeneral.BackColor = System.Drawing.Color.White;
            this.panelGeneral.Controls.Add(this.general);
            this.panelGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGeneral.Location = new System.Drawing.Point(5, 5);
            this.panelGeneral.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.panelGeneral.Name = "panelGeneral";
            this.panelGeneral.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panelGeneral.Size = new System.Drawing.Size(124, 46);
            this.panelGeneral.TabIndex = 2;
            // 
            // general
            // 
            this.general.BackColor = System.Drawing.Color.White;
            this.general.Dock = System.Windows.Forms.DockStyle.Fill;
            this.general.FlatAppearance.BorderSize = 0;
            this.general.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.general.Font = new System.Drawing.Font("Montserrat SemiBold", 14F, System.Drawing.FontStyle.Bold);
            this.general.Image = ((System.Drawing.Image)(resources.GetObject("general.Image")));
            this.general.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.general.Location = new System.Drawing.Point(0, 0);
            this.general.Name = "general";
            this.general.Size = new System.Drawing.Size(124, 40);
            this.general.TabIndex = 1;
            this.general.Text = "Général";
            this.general.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.general.UseVisualStyleBackColor = false;
            this.general.Click += new System.EventHandler(this.Btn_Click);
            // 
            // panelAttribute
            // 
            this.panelAttribute.BackColor = System.Drawing.Color.White;
            this.panelAttribute.Controls.Add(this.attribute);
            this.panelAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAttribute.Location = new System.Drawing.Point(139, 5);
            this.panelAttribute.Margin = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.panelAttribute.Name = "panelAttribute";
            this.panelAttribute.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.panelAttribute.Size = new System.Drawing.Size(119, 46);
            this.panelAttribute.TabIndex = 3;
            // 
            // attribute
            // 
            this.attribute.BackColor = System.Drawing.Color.White;
            this.attribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.attribute.FlatAppearance.BorderSize = 0;
            this.attribute.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.attribute.Font = new System.Drawing.Font("Montserrat SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.attribute.Image = global::GestionStock.Properties.Resources.attribute_1_removebg_preview__1_;
            this.attribute.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.attribute.Location = new System.Drawing.Point(0, 0);
            this.attribute.Name = "attribute";
            this.attribute.Size = new System.Drawing.Size(119, 40);
            this.attribute.TabIndex = 2;
            this.attribute.Text = "Attribut";
            this.attribute.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.attribute.UseVisualStyleBackColor = false;
            this.attribute.Click += new System.EventHandler(this.Btn_Click);
            // 
            // panelAddProduct
            // 
            this.panelAddProduct.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelAddProduct.Location = new System.Drawing.Point(0, 151);
            this.panelAddProduct.Name = "panelAddProduct";
            this.panelAddProduct.Size = new System.Drawing.Size(800, 299);
            this.panelAddProduct.TabIndex = 9;
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.FlatAppearance.BorderSize = 0;
            this.btnExit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(139)))), ((int)(((byte)(29)))));
            this.btnExit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(92)))), ((int)(((byte)(139)))), ((int)(((byte)(29)))));
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Montserrat", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.Location = new System.Drawing.Point(765, 0);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(35, 29);
            this.btnExit.TabIndex = 7;
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // AddProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panelAddProduct);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddProduct";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelGeneral.ResumeLayout(false);
            this.panelAttribute.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label HomeLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panelAddProduct;
        private System.Windows.Forms.Panel panelGeneral;
        private System.Windows.Forms.Button general;
        private System.Windows.Forms.Panel panelAttribute;
        private System.Windows.Forms.Button attribute;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button btnExit;
    }
}