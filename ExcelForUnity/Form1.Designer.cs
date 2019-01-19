namespace ExcelForUnity
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ButtonLoad = new System.Windows.Forms.Button();
            this.ButtonConvertToJson = new System.Windows.Forms.Button();
            this.ButtonConvertToC = new System.Windows.Forms.Button();
            this.CheckBoxIsSerializable = new System.Windows.Forms.CheckBox();
            this.LabelFileName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelJSonConverted = new System.Windows.Forms.Label();
            this.labelCSharpConverted = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ButtonLoad
            // 
            this.ButtonLoad.Location = new System.Drawing.Point(12, 12);
            this.ButtonLoad.Name = "ButtonLoad";
            this.ButtonLoad.Size = new System.Drawing.Size(134, 38);
            this.ButtonLoad.TabIndex = 0;
            this.ButtonLoad.Text = "Load Excel File";
            this.ButtonLoad.UseVisualStyleBackColor = true;
            this.ButtonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // ButtonConvertToJson
            // 
            this.ButtonConvertToJson.Location = new System.Drawing.Point(12, 56);
            this.ButtonConvertToJson.Name = "ButtonConvertToJson";
            this.ButtonConvertToJson.Size = new System.Drawing.Size(134, 38);
            this.ButtonConvertToJson.TabIndex = 1;
            this.ButtonConvertToJson.Text = "Convert To JSon";
            this.ButtonConvertToJson.UseVisualStyleBackColor = true;
            this.ButtonConvertToJson.Click += new System.EventHandler(this.ButtonConvertToJson_Click);
            // 
            // ButtonConvertToC
            // 
            this.ButtonConvertToC.Location = new System.Drawing.Point(12, 100);
            this.ButtonConvertToC.Name = "ButtonConvertToC";
            this.ButtonConvertToC.Size = new System.Drawing.Size(134, 37);
            this.ButtonConvertToC.TabIndex = 2;
            this.ButtonConvertToC.Text = "Convert To C#";
            this.ButtonConvertToC.UseVisualStyleBackColor = true;
            this.ButtonConvertToC.Click += new System.EventHandler(this.ButtonConvertToCSharp_Click);
            // 
            // CheckBoxIsSerializable
            // 
            this.CheckBoxIsSerializable.AutoSize = true;
            this.CheckBoxIsSerializable.Location = new System.Drawing.Point(29, 143);
            this.CheckBoxIsSerializable.Name = "CheckBoxIsSerializable";
            this.CheckBoxIsSerializable.Size = new System.Drawing.Size(99, 17);
            this.CheckBoxIsSerializable.TabIndex = 3;
            this.CheckBoxIsSerializable.Text = "Create Serialize";
            this.CheckBoxIsSerializable.UseVisualStyleBackColor = true;
            // 
            // LabelFileName
            // 
            this.LabelFileName.AutoSize = true;
            this.LabelFileName.Location = new System.Drawing.Point(152, 25);
            this.LabelFileName.Name = "LabelFileName";
            this.LabelFileName.Size = new System.Drawing.Size(90, 13);
            this.LabelFileName.TabIndex = 4;
            this.LabelFileName.Text = "File not selected..";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(12, 174);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(230, 54);
            this.label1.TabIndex = 5;
            this.label1.Text = "Excel cell format type samples :\r\nstring:  (Cell Type is general) test,Test,100, " +
    "etc.\r\nfloat:    (Cell Type is general) 200f,3f,0.4f, etc.\r\nint:       (Cell Type" +
    " is number) 10,300,-200, etc.\r\n";
            // 
            // labelJSonConverted
            // 
            this.labelJSonConverted.AutoSize = true;
            this.labelJSonConverted.Location = new System.Drawing.Point(152, 69);
            this.labelJSonConverted.Name = "labelJSonConverted";
            this.labelJSonConverted.Size = new System.Drawing.Size(75, 13);
            this.labelJSonConverted.TabIndex = 6;
            this.labelJSonConverted.Text = "Not converted";
            // 
            // labelCSharpConverted
            // 
            this.labelCSharpConverted.AutoSize = true;
            this.labelCSharpConverted.Location = new System.Drawing.Point(152, 112);
            this.labelCSharpConverted.Name = "labelCSharpConverted";
            this.labelCSharpConverted.Size = new System.Drawing.Size(75, 13);
            this.labelCSharpConverted.TabIndex = 7;
            this.labelCSharpConverted.Text = "Not converted";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(257, 240);
            this.Controls.Add(this.labelCSharpConverted);
            this.Controls.Add(this.labelJSonConverted);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.LabelFileName);
            this.Controls.Add(this.CheckBoxIsSerializable);
            this.Controls.Add(this.ButtonConvertToC);
            this.Controls.Add(this.ButtonConvertToJson);
            this.Controls.Add(this.ButtonLoad);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "Excel Converter For Unity";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ButtonLoad;
        private System.Windows.Forms.Button ButtonConvertToJson;
        private System.Windows.Forms.Button ButtonConvertToC;
        private System.Windows.Forms.CheckBox CheckBoxIsSerializable;
        private System.Windows.Forms.Label LabelFileName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelJSonConverted;
        private System.Windows.Forms.Label labelCSharpConverted;
    }
}

