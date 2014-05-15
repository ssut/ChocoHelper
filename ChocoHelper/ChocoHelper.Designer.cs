namespace ChocoHelper
{
    partial class ChocoHelper
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다.
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnFindUUID = new System.Windows.Forms.Button();
            this.textUUID = new System.Windows.Forms.TextBox();
            this.lblPCName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnFindUUID
            // 
            this.btnFindUUID.Location = new System.Drawing.Point(12, 12);
            this.btnFindUUID.Name = "btnFindUUID";
            this.btnFindUUID.Size = new System.Drawing.Size(75, 23);
            this.btnFindUUID.TabIndex = 0;
            this.btnFindUUID.Text = "Find UUID";
            this.btnFindUUID.UseVisualStyleBackColor = true;
            this.btnFindUUID.Click += new System.EventHandler(this.btnFindUUID_Click);
            // 
            // textUUID
            // 
            this.textUUID.Location = new System.Drawing.Point(93, 13);
            this.textUUID.Name = "textUUID";
            this.textUUID.ReadOnly = true;
            this.textUUID.Size = new System.Drawing.Size(125, 21);
            this.textUUID.TabIndex = 1;
            // 
            // lblPCName
            // 
            this.lblPCName.AutoSize = true;
            this.lblPCName.Location = new System.Drawing.Point(10, 46);
            this.lblPCName.Name = "lblPCName";
            this.lblPCName.Size = new System.Drawing.Size(100, 12);
            this.lblPCName.TabIndex = 2;
            this.lblPCName.Text = "Computer name:";
            // 
            // ChocoHelper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(233, 67);
            this.Controls.Add(this.lblPCName);
            this.Controls.Add(this.textUUID);
            this.Controls.Add(this.btnFindUUID);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "ChocoHelper";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChocoHelper";
            this.Load += new System.EventHandler(this.ChocoHelper_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFindUUID;
        private System.Windows.Forms.TextBox textUUID;
        private System.Windows.Forms.Label lblPCName;
    }
}

