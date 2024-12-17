<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Backup
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Backup))
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.BackupButton = New System.Windows.Forms.Button()
        Me.RestoreButton = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LbFechaHora = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel3.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 375)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(606, 26)
        Me.Panel4.TabIndex = 23
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Panel3.Controls.Add(Me.Button3)
        Me.Panel3.Controls.Add(Me.Button1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(606, 26)
        Me.Panel3.TabIndex = 24
        '
        'Button3
        '
        Me.Button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button3.BackgroundImage = CType(resources.GetObject("Button3.BackgroundImage"), System.Drawing.Image)
        Me.Button3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button3.FlatAppearance.BorderSize = 0
        Me.Button3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button3.Location = New System.Drawing.Point(535, 3)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(20, 20)
        Me.Button3.TabIndex = 2
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.BackgroundImage = CType(resources.GetObject("Button1.BackgroundImage"), System.Drawing.Image)
        Me.Button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Button1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(574, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(20, 20)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'BackupButton
        '
        Me.BackupButton.BackgroundImage = CType(resources.GetObject("BackupButton.BackgroundImage"), System.Drawing.Image)
        Me.BackupButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.BackupButton.Location = New System.Drawing.Point(157, 153)
        Me.BackupButton.Name = "BackupButton"
        Me.BackupButton.Size = New System.Drawing.Size(116, 110)
        Me.BackupButton.TabIndex = 25
        Me.BackupButton.UseVisualStyleBackColor = True
        '
        'RestoreButton
        '
        Me.RestoreButton.BackgroundImage = CType(resources.GetObject("RestoreButton.BackgroundImage"), System.Drawing.Image)
        Me.RestoreButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.RestoreButton.Location = New System.Drawing.Point(315, 153)
        Me.RestoreButton.Name = "RestoreButton"
        Me.RestoreButton.Size = New System.Drawing.Size(116, 110)
        Me.RestoreButton.TabIndex = 26
        Me.RestoreButton.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(41, Byte), Integer), CType(CType(77, Byte), Integer), CType(CType(97, Byte), Integer))
        Me.Panel1.Controls.Add(Me.LbFechaHora)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 26)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(606, 69)
        Me.Panel1.TabIndex = 27
        '
        'LbFechaHora
        '
        Me.LbFechaHora.AutoSize = True
        Me.LbFechaHora.Font = New System.Drawing.Font("Segoe UI", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbFechaHora.ForeColor = System.Drawing.SystemColors.AppWorkspace
        Me.LbFechaHora.Location = New System.Drawing.Point(12, 35)
        Me.LbFechaHora.Name = "LbFechaHora"
        Me.LbFechaHora.Size = New System.Drawing.Size(585, 21)
        Me.LbFechaHora.TabIndex = 8
        Me.LbFechaHora.Text = "Haga una copia de seguridad de la base de datos o restáurela cuando sea necesario" &
    ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlLightLight
        Me.Label1.Location = New System.Drawing.Point(244, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 25)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Backup"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(172, 266)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(101, 25)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Descargar"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(7, Byte), Integer), CType(CType(46, Byte), Integer), CType(CType(51, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(320, 266)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(97, 25)
        Me.Label3.TabIndex = 28
        Me.Label3.Text = "Restaurar"
        '
        'Backup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.ClientSize = New System.Drawing.Size(606, 401)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.RestoreButton)
        Me.Controls.Add(Me.BackupButton)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel4)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Backup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Backup"
        Me.Panel3.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel4 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Button3 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents BackupButton As Button
    Friend WithEvents RestoreButton As Button
    Friend WithEvents Panel1 As Panel
    Friend WithEvents LbFechaHora As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
