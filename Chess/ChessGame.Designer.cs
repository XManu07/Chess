﻿namespace Chess
{
    partial class FChessGame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FChessGame));
            this.btnExitGame = new System.Windows.Forms.Button();
            this.chessBoard = new System.Windows.Forms.TableLayoutPanel();
            this.SuspendLayout();
            // 
            // btnExitGame
            // 
            resources.ApplyResources(this.btnExitGame, "btnExitGame");
            this.btnExitGame.Name = "btnExitGame";
            this.btnExitGame.UseVisualStyleBackColor = true;
            this.btnExitGame.Click += new System.EventHandler(this.btnExitGame_Click);
            // 
            // chessBoard
            // 
            this.chessBoard.BackColor = System.Drawing.SystemColors.ScrollBar;
            resources.ApplyResources(this.chessBoard, "chessBoard");
            this.chessBoard.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.chessBoard.Name = "chessBoard";
            // 
            // FChessGame
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chessBoard);
            this.Controls.Add(this.btnExitGame);
            this.Name = "FChessGame";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FChessGame_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnExitGame;
        private System.Windows.Forms.TableLayoutPanel chessBoard;
    }
}